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
    private SphereController m_sphereController;
    private Rigidbody m_sphereRB;
    public float m_steer;
    public Transform m_SteeringTransform;
    public Transform m_wheelTransform;
    public Transform m_wheelTransformReverse;
    public float m_steeringPower = 1f;
    public float m_velocityMagnitude = 0;
    // public float m_steeringParked = 2f;
    //    public float m_steeringDrift = 2f;
    //  public float m_steeringDrift = 2f;
    //  public float m_steeringDrift = 2f;
    private float steeringAdjustment = 1f;
    private bool m_drivingReverse;
    public float m_adjustmentYOffset = .25f;
    public bool m_ActivePlayer = false;
    public float MAXSTEERINGPOWER = 2f;

    void Awake()
    {
        // m_rigidbody = GetComponent<Rigidbody>();
        m_sphereTransform = m_sphere.transform;
        m_sphereController = m_sphere.GetComponent<SphereController>();
        m_sphereRB = m_sphere.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_drivingReverse = m_sphereController.m_reverse;

        if (m_ActivePlayer)
        {
            m_steer = Input.GetAxis("Horizontal") * m_steeringPower;
        }
        //Add AI steering Control
        else
        {
            m_steer = 0;
        }

        transform.position = new Vector3(m_sphereTransform.position.x, m_sphereTransform.position.y- m_adjustmentYOffset, m_sphereTransform.position.z);

        m_velocityMagnitude = m_sphereRB.velocity.magnitude;

        if (m_drivingReverse)
        {
            if (m_velocityMagnitude < 5f)
            {
                steeringAdjustment = m_velocityMagnitude / 5f;
                transform.rotation = math.slerp(transform.rotation, m_wheelTransformReverse.rotation, m_steeringPower * steeringAdjustment * .1f);
            }
            else
            {
                transform.rotation = math.slerp(transform.rotation, m_wheelTransformReverse.rotation, m_steeringPower * .1f);
            }
        }
        else
        {
            if (m_velocityMagnitude < 5f)
            {
                steeringAdjustment = m_velocityMagnitude / 5f;
                transform.rotation = math.slerp(transform.rotation, m_wheelTransform.rotation, m_steeringPower * steeringAdjustment * .1f);
            }
            else
            {
                transform.rotation = math.slerp(transform.rotation, m_wheelTransform.rotation, m_steeringPower * .1f);
            }
        }
    }
    
}
