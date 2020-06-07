using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;


public class SteeringController : MonoBehaviour
{
    //Rigidbody m_rigidbody;
    private Transform m_sphereTransform;
    public GameObject m_sphere;
    public float m_steer;
    public Transform m_SteeringTransform;
    public Transform m_wheelTransform;
    public float m_steeringPower = 1f;

   // public float m_steeringParked = 2f;
//    public float m_steeringDrift = 2f;
  //  public float m_steeringDrift = 2f;
  //  public float m_steeringDrift = 2f;


    public float MAXSTEERINGPOWER = 2f;

    void Awake()
    {
        // m_rigidbody = GetComponent<Rigidbody>();
        m_sphereTransform = m_sphere.transform;
    }

    void FixedUpdate()
    {
        m_steer = Input.GetAxis("Horizontal")* m_steeringPower;
        transform.position = new Vector3(m_sphereTransform.position.x, m_sphereTransform.position.y-.25f, m_sphereTransform.position.z);
        // transform.rotation = new Vector3(m_sphereTransform.position.x, m_sphereTransform.position.y - .5f, m_sphereTransform.position.z);
       
        transform.rotation = math.slerp(transform.rotation, m_wheelTransform.rotation, m_steeringPower*.1f);
        
        if (m_steer!=0)
        {
            //m_SteeringTransform.Rotate(0,m_steer,0);
        }
       // m_SteeringTransform.Rotate(0,m_steer,0);
       // m_SteeringTransform.rotation = Quaternion
       // m_rigidbody.rotation = transform.rotation;
    }
    
}
