using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;



public class GameManager : MonoBehaviour
{
    

    //public DroneController _DroneController;
    Rigidbody rb;

    public Button _FlyButton;
    public Button _LandButton;
    public  GameObject _Drone;
    
 

    public GameObject _Controls;
    
    public GameObject plane;
    public GameObject rotor1;
    public GameObject rotor2;
    public GameObject rotor3;
    public bool startedrotor;
    
    
    //AR
    public ARRaycastManager _RaycastManager;
    public ARPlaneManager _PlaneManager;
    List<ARRaycastHit> _HitRelult = new List<ARRaycastHit>();

    
    public HeliController heliControlls;

   


    // Start is called before the first frame update
    void Start()
    {
        

        _Controls.SetActive(false);
        _FlyButton.onClick.AddListener(EventOnClickFlyButton);
        _LandButton.onClick.AddListener(EventOnClickLandButton);
    }
   
        void FixedUpdate()
    {
       if (startedrotor)
    {
        StartCoroutine( Rotate(rotor1 ,Vector3.up, -180, 0.001f) );
        StartCoroutine( Rotate(rotor2 ,Vector3.forward, 90, 0.001f) );
        StartCoroutine( Rotate(rotor3 ,Vector3.forward, 85, 0.001f) );
    }
     

        
        if(!_Controls.active)
        {
            UpdateAR();
        }
        if (_Controls.active)
        {

            heliControlls.Move();    
        }


    }
    
    
       IEnumerator Rotate(GameObject rotor ,Vector3 axis, float angle, float duration = 1.0f )
   {
     Quaternion from = this.transform.rotation;
     Quaternion to = this.transform.rotation;
     to *= Quaternion.Euler( axis * angle );
    
     float elapsed = 0.0f;
     while( elapsed < duration )
     {
       rotor.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration );
       elapsed += Time.deltaTime;
       yield return null;
     }
     rotor.transform.rotation = to;
     
     
   }

     IEnumerator waiter(int x)
{
 
    //Wait for x seconds
    yield return new WaitForSecondsRealtime(x);

    
    _FlyButton.gameObject.SetActive(false);
    _Controls.SetActive(true);

   
}   
        

    void UpdateAR()
    {
        Vector2 positionScreenSpace = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        _RaycastManager.Raycast(positionScreenSpace, _HitRelult, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinBounds);
        if(_HitRelult.Count > 0)
        {
            if(_PlaneManager.GetPlane(_HitRelult[0].trackableId).alignment == UnityEngine.XR.ARSubsystems.PlaneAlignment.HorizontalUp)
            {
                Pose pose = _HitRelult[0].pose;
                
                _Drone.transform.position = pose.position;
                _Drone.SetActive(true);
                plane.transform.position = pose.position;
                plane.SetActive(true);
            }
        }
    }

    
    void EventOnClickFlyButton()
    {
            StartCoroutine( Rotate(rotor1 ,Vector3.up, -180, 1.5f) );
            StartCoroutine( Rotate(rotor2 ,Vector3.forward, 180, 1.5f) );
            StartCoroutine( Rotate(rotor3 ,Vector3.forward, 270, 1.5f) );
            startedrotor = true;
            heliControlls.Works();

            
            StartCoroutine(waiter(2));
            
            
            
           
        
    }
    void EventOnClickLandButton()
        {
            
                
                _LandButton.gameObject.SetActive(false);
                _FlyButton.gameObject.SetActive(true);
                _Controls.SetActive(false);
            
        }
        
       
    

    // Update is called once per frame

}

