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

    
 

    public GameObject _Controls;
    public Joystick joystick;
    public Joystick joystick2;

    Vector3 m_EulerAngleVelocity;
    Vector3 m_EulerAngleVelocity2;
   
    
    public bool isMovingForward;
     public bool isMovingBackward;
    public bool isMovingRight;
     public bool isMovingLeft;

     public Vector3 LastPOS;
     public Vector3 NextPOS;
    
    public float MoveSpd = 0.2f;
    //AR
    public ARRaycastManager _RaycastManager;
    public ARPlaneManager _PlaneManager;
    List<ARRaycastHit> _HitRelult = new List<ARRaycastHit>();

    public  GameObject _Drone;


   


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
            Move();
            
          }
    }
    
    
   
        void tilting() {
            
            if (joystick2.Horizontal > 0) {
                isMovingRight = true;
                isMovingLeft = false;
                }
            else if (joystick2.Horizontal < 0) {
                isMovingLeft = true;
                isMovingRight = false;
                }
            if (joystick2.Vertical < 0) {
                isMovingBackward = true;
               isMovingForward = false;
                }
            else if (joystick2.Vertical > 0) {
            
                isMovingForward = true;
                isMovingBackward = false;

                

                }
            if (joystick2.Vertical == 0) {
                isMovingForward = false;
                isMovingBackward = false;
                }    
            if ((joystick2.Horizontal) == 0) {
                isMovingLeft = false;
                isMovingRight = false;
            }

            if (isMovingForward) {
                
                 
                }
            if (isMovingBackward) {

                
                }    
        
    }
         void LateUpdate() {
         
        tilting();
        
        
         
    }
    void Move() {
        //Rigidbody rb = _Drone.GetComponent<Rigidbody>();
        var newpos = rb.position - rb.transform.forward * joystick2.Horizontal * MoveSpd + rb.transform.right * joystick2.Vertical * MoveSpd + rb.transform.up * joystick.Vertical * MoveSpd;
        rb.MovePosition (newpos);
        
        m_EulerAngleVelocity = new Vector3(0, joystick.Horizontal * 100f, 0);
        
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        
        
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        
   
 
    
    //Debug.Log(m_EulerAngleVelocity);
        
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

