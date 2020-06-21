using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SphereController : MonoBehaviour
{

    public float m_acceleration = 0f;
    public Vector3 m_force;
    public float ACCELERATION = 200f;
    Rigidbody m_rigidbody = null;
    SphereCollider m_sphereCollider = null;
    public GameObject m_vehicle;
    public Transform m_Steering;
    public bool m_reverse = false;
    public bool m_ActivePlayer = false;
    public bool m_breaking = false;
    private float m_lastAcceleration;
    private float m_lastDistance;

    TruckAILogic m_truckAILogic;
    CarAILogic m_car1AILogic;
    CarAILogic m_car2AILogic;

    // public PathCreator m_pathCreator;
    // public EndOfPathInstruction m_end;
    // float m_distTravelled;



    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>(); 
        m_car1AILogic = GameObject.FindGameObjectWithTag("Car1").GetComponent<CarAILogic>();
        m_car2AILogic = GameObject.FindGameObjectWithTag("Car2").GetComponent<CarAILogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ActivePlayer)
        {
            m_acceleration = Input.GetAxis("Vertical") * ACCELERATION;

            m_breaking = Input.GetKey(KeyCode.Space);   
        }
        //add AI control
        else
        {
            if (this.tag == "Car1Sphere") {
                //Add Player AI Logic
                float dist = m_car1AILogic.m_distanceToTruck;
                m_acceleration = m_car1AILogic.m_lastAcceleration;
                Debug.Log("Distance: " + dist);
                if (dist > 15f) {
                    m_acceleration -= 15f;
                } else if (dist < -40f) {
                    m_acceleration += 5f;
                } else {
                    if(Mathf.Abs(dist) < Mathf.Abs(m_car1AILogic.m_lastDist)) {
                        m_acceleration -= 5f;
                    } else if (Mathf.Abs(dist) > Mathf.Abs(m_car1AILogic.m_lastDist)) {
                        m_acceleration += 10f;
                    } else {
                        m_acceleration = m_car1AILogic.m_lastAcceleration;
                    }
                }

                m_car1AILogic.m_lastAcceleration = m_acceleration;
                m_car1AILogic.m_lastDist = dist;

                m_breaking = false;
            } else if (this.tag == "Car2Sphere") {
                //Add Player AI Logic
                //m_acceleration = m_car2AILogic.m_lastAcceleration;
                float dist = m_car2AILogic.m_distanceToTruck;
                m_acceleration = m_car2AILogic.m_lastAcceleration;
                if (dist > 15f) {
                    m_acceleration -= 15f;
                } else if (dist < -40f) {
                    m_acceleration += 5f;
                } else {
                    if(Mathf.Abs(dist) < Mathf.Abs(m_car2AILogic.m_lastDist)) {
                        m_acceleration -= 10f;
                    } else if (Mathf.Abs(dist) > Mathf.Abs(m_car2AILogic.m_lastDist)) {
                        m_acceleration += 10f;
                    } else {
                        m_acceleration = m_car2AILogic.m_lastAcceleration;
                    }
                }

                m_car2AILogic.m_lastAcceleration = m_acceleration;
                m_car2AILogic.m_lastDist = dist;

                m_breaking = false;
            } else if(this.tag == "TruckSphere") {
                m_acceleration = 1.0f * ACCELERATION;
                m_breaking = false;
                //Debug.Log("Truck Acceleration: " + m_acceleration);
            }
        }

        //If offroad reduce acceleration
        if((this.tag == "TruckSphere" && (m_truckAILogic.m_truckLeftWheelOffroad || m_truckAILogic.m_truckRightWheelOffroad)) || (this.tag == "Car1Sphere" && (m_car1AILogic.m_carLeftWheelOffroad || m_car1AILogic.m_carRightWheelOffroad)) || (this.tag == "Car2Sphere" && (m_car2AILogic.m_carLeftWheelOffroad || m_car2AILogic.m_carRightWheelOffroad))) {
                m_acceleration = m_acceleration / 2.0f;
        }
         
        if (m_acceleration > .1f)
        {
            m_reverse = false;
        }
        else if ((m_rigidbody.velocity.magnitude < 1f || m_reverse == true) && m_acceleration < -.1f)
        {
            m_reverse = true;
            m_acceleration *= .6f;
        }
        else
        {
            //m_reverse = false;
            m_acceleration = 0;
        }

        if (m_breaking)
        {
            m_acceleration *= .15f;
        }
    }

    void FixedUpdate()
    {

        if (Mathf.Abs(m_acceleration) > .1)
        {
            m_force = m_acceleration * m_Steering.forward;
        }
        else
        {
            m_force = new Vector3(0, 0, 0);
        }
        m_rigidbody.AddForce(m_force);

    }
}
