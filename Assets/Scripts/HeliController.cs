using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour
{

    Rigidbody rb;
    public Animator anim;
    public Joystick joystick;
    public Joystick joystick2;
    public float MoveSpd = 0.2f;
    Vector3 m_EulerAngleVelocity;
    
    public bool IsWorking;
    public  GameObject _Drone;

    public bool isMovingForward;
    public bool isMovingBackward;
    public bool isMovingRight;
    public bool isMovingLeft;
    // Start is called before the first frame update
    void Start()
    {
        rb = _Drone.GetComponent<Rigidbody>();
    }
    public void Works() {
        this.IsWorking = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(IsWorking)
        {
            anim.SetBool("IsWorking", true);
        }
    }
    public void Move() {
        //Rigidbody rb = _Drone.GetComponent<Rigidbody>();
        var newpos = rb.position - rb.transform.forward * joystick2.Horizontal * MoveSpd + rb.transform.right * joystick2.Vertical * MoveSpd + rb.transform.up * joystick.Vertical * MoveSpd;
        rb.MovePosition (newpos);
        
        m_EulerAngleVelocity = new Vector3(0, joystick.Horizontal * 100f, 0);
        
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        
        
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        
    }
    public void tilting() {
            
    

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
}
