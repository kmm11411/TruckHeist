﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Unity.Mathematics;
using UnityEngine;


public class WheelSteeringLogic : MonoBehaviour
{
    //Rigidbody m_rigidbody;
    private Transform m_sphereTransform;
    private Rigidbody m_sphereRB;
    public GameObject m_sphere;
    public float m_steer;
    public Transform m_SteeringTransform;
    public float m_steeringPower = 1f;
    public bool m_ReverseRotation = false;
    float wheelSpin = 0f;
    public bool m_ActivePlayer = false;

    private float MAXSTEERINGPOWER = 2f;
    
    public bool m_wheelOffroad = false;
    
    TruckAILogic m_truckAILogic;

    [SerializeField]
    CarAILogic m_carAILogic;

    GameObject m_truckFollowObject;
    GameObject m_truckFollowSpaceLeft;
    GameObject m_truckFollowSpaceRight;
    GameObject m_trailer;

    void Awake()
    {
        m_sphereTransform = m_sphere.transform;
        m_sphereRB = m_sphere.GetComponent<Rigidbody>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>(); 
        m_truckFollowObject = GameObject.FindGameObjectWithTag("TruckFollowObject");
        m_truckFollowSpaceLeft = GameObject.FindGameObjectWithTag("FollowSpaceLeft");
        m_truckFollowSpaceRight = GameObject.FindGameObjectWithTag("FollowSpaceRight");
        m_trailer = GameObject.FindGameObjectWithTag("Trailer");
    }

    void FixedUpdate()
    {
        if(this.tag != "Truck" && m_carAILogic.m_stuckToTruck) {
            return;
        }
        
        if (m_ActivePlayer)
        {
            m_steer = Input.GetAxis("Horizontal") * m_steeringPower;

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
        } else { 
            if(this.tag == "TruckLeftWheel" || this.tag == "TruckRightWheel") {
                //Check if on road or offroad. If offroad, steer back on road
                if(m_truckAILogic.m_truckLeftWheelOffroad) {
                    m_steer = 0.5f * m_steeringPower;
                    transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                } else if (m_truckAILogic.m_truckRightWheelOffroad) {
                    m_steer = -0.5f * m_steeringPower;
                    transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                } else {
                    if(m_truckAILogic.m_carOnLeft) {
                        m_steer = -0.3f * m_steeringPower;
                        transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                    } else if (m_truckAILogic.m_carOnRight) {
                        m_steer = 0.3f * m_steeringPower;
                        transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                    } else {
                        if(m_truckAILogic.m_distanceFromFollowObject > 0) {
                            transform.LookAt(m_truckFollowObject.transform);
                        } else {
                            m_steer = 0;
                            math.lerp(m_steer, m_SteeringTransform.rotation.y, .5f);
                            transform.localRotation = Quaternion.Euler(new Vector3(0, -m_steer, 0));
                        }
                    }
                }
            }  else {
                if(m_carAILogic.m_carLeftWheelOffroad) {
                    m_steer = 0.5f * m_steeringPower;
                    transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                } else if (m_carAILogic.m_carRightWheelOffroad) {
                    m_steer = -0.5f * m_steeringPower;
                    transform.localRotation = Quaternion.Euler(new Vector3(0, m_steer, 0));
                } else {
                    if (m_carAILogic.m_directionToTruckFollowSpaceLeft < 5 && m_carAILogic.m_directionToTruckFollowSpaceRight < 5) {
                        m_steer = 0;
                        math.lerp(m_steer, m_SteeringTransform.rotation.y, .5f);
                        transform.localRotation = Quaternion.Euler(new Vector3(0, -m_steer, 0));
                    } else if(m_carAILogic.m_directionToTruckFollowSpaceLeft < m_carAILogic.m_directionToTruckFollowSpaceRight) {
                        if(m_carAILogic.m_directionToTruckFollowSpaceLeft < 6) {
                            m_steer = 0;
                            math.lerp(m_steer, m_SteeringTransform.rotation.y, .5f);
                            transform.localRotation = Quaternion.Euler(new Vector3(0, -m_steer, 0));
                        } else {
                            transform.LookAt(m_truckFollowSpaceLeft.transform);
                        }
                    } else if (m_carAILogic.m_directionToTruckFollowSpaceLeft > m_carAILogic.m_directionToTruckFollowSpaceRight) {
                        if(m_carAILogic.m_directionToTruckFollowSpaceRight < 6) {
                            m_steer = 0;
                            math.lerp(m_steer, m_SteeringTransform.rotation.y, .5f);
                            transform.localRotation = Quaternion.Euler(new Vector3(0, -m_steer, 0));
                        } else {
                            transform.LookAt(m_truckFollowSpaceRight.transform);
                        }                        
                    } else {
                        transform.LookAt(m_trailer.transform);
                    }
                }
            }
        }

        if (m_sphereRB.velocity.magnitude > 0)
        {
            wheelSpin += m_sphereRB.velocity.magnitude*2f;
        }
        if (wheelSpin > 360)
        {
            wheelSpin = wheelSpin % 360;
        }       
    }
}
