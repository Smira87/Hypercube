﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliController : MonoBehaviour
{
    Rigidbody rb;
    public Animator anim;
    public Joystick joystick;
    public Joystick joystick2;
    public float MoveSpd = 2.0f;
    Vector3 m_EulerAngleVelocity;
    Vector3 m_EulerAngleVelocity2;
    

    public  GameObject _Drone;
    public  GameObject _Model;

    //public float torque = 0.02f;
     

    
    // Start is called before the first frame update
    void Start()
    {
        rb = _Drone.GetComponent<Rigidbody>();


    }
    public void Works() {
        
        anim.SetBool("IsWorking", true);
        
        //rb.AddForce(rb.transform.right/100f);
    }
    public void Stops() {
        
        anim.SetBool("IsWorking", true);
        
    }
    // Update is called once per frame
  
 
    public void Move() {
    
        Vector3 moveDirection = transform.right * joystick2.Vertical + transform.forward * (-joystick2.Horizontal) + transform.up * joystick.Vertical;

        rb.AddForce(moveDirection * MoveSpd * Time.deltaTime, ForceMode.VelocityChange);        

        m_EulerAngleVelocity = new Vector3(0, joystick.Horizontal * 100f, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.deltaTime);
        
        
        rb.MoveRotation(rb.rotation * deltaRotation);
        
        
    }

    
    
        public void Tilting()
    {
        var localVel = transform.InverseTransformDirection(rb.velocity);

        float angleZ = -15 * localVel.x * 60.0f * Time.deltaTime;
        float angleX = -15 * -localVel.z * 60.0f * Time.deltaTime;

        Vector3 rotation = _Model.transform.localRotation.eulerAngles;

        _Model.transform.localRotation = Quaternion.Euler(angleX, rotation.y, angleZ);

    }

  
    
    

}
