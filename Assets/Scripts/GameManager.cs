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
    
   
    
    
    
    //AR
    public ARRaycastManager _RaycastManager;
    public ARPlaneManager _PlaneManager;
    List<ARRaycastHit> _HitRelult = new List<ARRaycastHit>();

    
    public HeliController heliControlls;

   


    // Start is called before the first frame update
    void Start()
    {
        
        rb = _Drone.GetComponent<Rigidbody>();
        
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
        if (_Controls.active){
            heliControlls.Move();
            
          }
    }
    
    
   
        
         void LateUpdate() {
         
        heliControlls.tilting();
        
        
         
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
            }
        }
    }

    
    void EventOnClickFlyButton()
    {
        
           
            
            _FlyButton.gameObject.SetActive(false);
            _Controls.SetActive(true);
           
        
    }
    void EventOnClickLandButton()
        {
            
                
                _LandButton.gameObject.SetActive(false);
            
        }
        
       
    

    // Update is called once per frame

}

