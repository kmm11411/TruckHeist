using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private bool m_nosFull = false;
    private float NOSCOOLDOWN = 10f;
    public bool m_nosActive = false;

    TruckAILogic m_truckAILogic;

    [SerializeField]
    public CarAILogic m_carAILogic;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(this.tag != "TruckSphere" && (m_carAILogic.m_hitTruckFront || m_carAILogic.m_hitTruckLeft || m_carAILogic.m_hitTruckRight)) {
            m_acceleration = 0;
            return;
        }

        if (m_ActivePlayer)
        {
            m_acceleration = Input.GetAxis("Vertical") * ACCELERATION;

            m_breaking = Input.GetKey(KeyCode.Space);
            m_nosActive = Input.GetKey(KeyCode.V);

            m_carAILogic.m_lastAcceleration = m_acceleration;
        }
        //add AI control
        else
        {
            if(this.tag == "TruckSphere") {
                if(!m_truckAILogic.m_hitOnLeft && !m_truckAILogic.m_hitOnRight && !m_truckAILogic.m_hitOnFront) {
                   m_acceleration = 1.0f * ACCELERATION; 
                } else if (m_truckAILogic.m_hitOnLeft || m_truckAILogic.m_hitOnRight) {
                    m_acceleration = 0.8f * ACCELERATION;
                } else if (m_truckAILogic.m_hitOnLeft && m_truckAILogic.m_hitOnRight) {
                    m_acceleration = 0.5f * ACCELERATION;
                } 
                m_breaking = false;
            } else {
                float dist = m_carAILogic.m_directionToTruck;
                m_acceleration = m_carAILogic.m_lastAcceleration;
                if (dist > 30f) {
                    m_acceleration += 250f;
                } else if (dist < -20f) {
                    m_acceleration -= 250f;
                } else {
                    if(dist < m_carAILogic.m_lastDist) {
                        m_acceleration -= 300f;
                    } else if (dist > m_carAILogic.m_lastDist) {
                        m_acceleration += 300f;
                    } else {
                        m_acceleration = m_carAILogic.m_lastAcceleration;
                    }
                }

                m_carAILogic.m_lastAcceleration = m_acceleration;
                m_carAILogic.m_lastDist = dist;

                m_breaking = false;
            }
        }

        if(this.tag == "TruckSphere") {
            if(m_truckAILogic.m_truckLeftWheelOffroad || m_truckAILogic.m_truckRightWheelOffroad) {
                m_acceleration = m_acceleration / 2.0f;
            }
        } else if (m_carAILogic.m_carLeftWheelOffroad || m_carAILogic.m_carRightWheelOffroad) {
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

        if (m_nosActive)
        {
            m_acceleration *= 1.5f;
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
