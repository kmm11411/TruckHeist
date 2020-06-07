using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;


public class WheelSteeringLogic : MonoBehaviour
{
    //Rigidbody m_rigidbody;
    private Transform m_sphereTransform;
    public GameObject m_sphere;
    public float m_steer;
    public Transform m_SteeringTransform;
    public float m_steeringPower = 1f;
    public bool m_ReverseRotation = false;

    private float MAXSTEERINGPOWER = 2f;

    void Awake()
    {
        // m_rigidbody = GetComponent<Rigidbody>();
        m_sphereTransform = m_sphere.transform;
    }

    void FixedUpdate()
    {
        m_steer = Input.GetAxis("Horizontal")* m_steeringPower;
        if (m_ReverseRotation)
        {
            m_steer *= -1;
        }

        if(m_steer!=0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
        }
        else
        {
            math.lerp(m_steer, m_SteeringTransform.rotation.y, .5f);
            transform.localRotation = Quaternion.Euler(new Vector3(0, -m_steer, 0));
        }
    }
    
}
