using System.Collections;
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

    void Awake()
    {
        // m_rigidbody = GetComponent<Rigidbody>();
        m_sphereTransform = m_sphere.transform;
        m_sphereRB = m_sphere.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (m_ActivePlayer)
        {
            m_steer = Input.GetAxis("Horizontal") * m_steeringPower;
            RayCastTerrain();
        }
        //Add AI steering Control
        else
        { 
            m_steer = 0; 
        }

        if (m_sphereRB.velocity.magnitude > 0)
        {
            wheelSpin += m_sphereRB.velocity.magnitude*2f;
        }
        if (wheelSpin > 360)
        {
            wheelSpin = wheelSpin % 360;
        }
        // Debug.Log(wheelSpin);

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

    void RayCastTerrain() {
        LayerMask layerMask = LayerMask.GetMask("Ground");
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, layerMask)) {
            string hitTag = hit.collider.gameObject.tag;
            //Debug.Log("RayCast to Ground:" + hitTag);
            if(hitTag == "Offroad") {
                Debug.Log("Here");
                Debug.Log(this.tag + "went offroad");
            }
        }
    }
    
}
