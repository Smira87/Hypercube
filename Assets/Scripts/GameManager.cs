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
    
        //float speedX = Input.GetAxis("Horizontal");
        //float speedZ = Input.GetAxis("Vertical");
        
        //UpdateControls(ref _MovingLeft);
       // UpdateControls(ref _MovingBack);

        
        if(!_Controls.active)
        {
            UpdateAR();
        }
        if (_Controls.active)
        {

            heliControlls.Move();    
            heliControlls.Tilting();
        }


    }
    
    

     IEnumerator waiter(int x)
{
 
    //Wait for x seconds
    yield return new WaitForSecondsRealtime(x);

    
    
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
            
            
            heliControlls.Works();
            _LandButton.gameObject.SetActive(true);
            _FlyButton.gameObject.SetActive(false);
            
            StartCoroutine(waiter(2));
            
            
            
           
        
    }
    void EventOnClickLandButton()
        {
            
                heliControlls.Stops();

                _LandButton.gameObject.SetActive(false);
                _FlyButton.gameObject.SetActive(true);
                _Controls.SetActive(false);
            
        }
        
       
    

    // Update is called once per frame

}

