using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour
{


    Rigidbody rb;
    public Animator anim;
    public Joystick joystick;
    public Joystick joystick2;
    //public float MoveSpd = 0.1f;
    Vector3 m_EulerAngleVelocity;
    Vector3 m_EulerAngleVelocity2;
    
    public bool IsWorking;
    public  GameObject _Drone;
    public  GameObject _Model;

    public float torque = 0.02f;
     

    
    // Start is called before the first frame update
    void Start()
    {
        rb = _Drone.GetComponent<Rigidbody>();
       
        

        

    }
    public void Works() {
        this.IsWorking = true;
        
        //rb.AddForce(rb.transform.right/100f);
    }
    public void Stops() {
        this.IsWorking = false;
        
    }
    // Update is called once per frame
    void Update()
    {   
        
        if(IsWorking)
        {
            anim.SetBool("IsWorking", true);
            
        }
        else 
        {
            anim.SetBool("IsWorking", false);
            
        }
       
    }
 
    public void Move() {
        //Rigidbody rb = _Drone.GetComponent<Rigidbody>();
        //var newpos = rb.position - rb.transform.forward * joystick2.Horizontal * MoveSpd + rb.transform.right * joystick2.Vertical * MoveSpd + rb.transform.up * joystick.Vertical * MoveSpd;
        //rb.MovePosition (newpos);
        
        
        
        
        //m_EulerAngleVelocity = new Vector3(0, joystick.Horizontal * 100f, 0);
        
        
        

        if (joystick2.Vertical > 0){
        rb.AddForce(rb.transform.right/100f);
        }
        if (joystick2.Vertical < 0){
        rb.AddForce(-rb.transform.right/100f);
        }
        if (joystick2.Horizontal > 0){
        rb.AddForce(-rb.transform.forward/100f);
        }
        if (joystick2.Horizontal < 0){
        rb.AddForce(rb.transform.forward/100f);
        }
        if (joystick.Vertical < 0){
        rb.AddForce(-rb.transform.up/100f);
        }
        if (joystick.Vertical > 0){
        rb.AddForce(rb.transform.up/100f);
        }
        //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        
        
        //rb.MoveRotation(rb.rotation * deltaRotation);
        
        float turn = joystick.Horizontal;
        rb.AddTorque(Vector3.up * turn * torque);
        
    }

    
    
        public void Tilting()
    {
        float angleZ = -20 * joystick2.Vertical * 60.0f * Time.deltaTime;
        float angleX = -20 * joystick2.Horizontal * 60.0f * Time.deltaTime;

        Vector3 rotation = _Model.transform.localRotation.eulerAngles;
        
      


        _Model.transform.localRotation = Quaternion.Euler(angleX, rotation.y, angleZ);

    }

  
    
    

}
