using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookLogic : MonoBehaviour
{
    public bool m_hitTruck = false;
    public bool m_fired = false;
    private Rigidbody m_RigidBody;
    private CapsuleCollider m_CapsuleCollider;


    TruckAILogic m_truckAILogic;
    CarAILogic m_carAILogic;
    // public GameObject m_grapplingHookSpawn;
    GameObject m_hitZoneLeftGrapplingPoint;
    GameObject m_hitZoneRightGrapplingPoint;
    GameObject m_hitZoneFrontGrapplingPoint;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_CapsuleCollider = GetComponent<CapsuleCollider>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>();
        m_carAILogic = GetComponentInParent<CarAILogic>();

        m_hitZoneLeftGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneLeftGrapplingPoint");
        m_hitZoneRightGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneRightGrapplingPoint");
        m_hitZoneFrontGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneFrontGrapplingPoint");
        
    }

    //private void Update()
    //{
    //    if (!m_fired)
    //    {
    //        transform.position = m_grapplingHookSpawn.transform.position;
    //        transform.rotation = m_grapplingHookSpawn.transform.rotation;
    //    }
    //}
    private void FixedUpdate()
    {
        //if (m_fired)
        //{
        //    Debug.Log("m_RigidBody.velocity.normalized" + m_RigidBody.velocity.normalized * 360);
        //    transform.rotation = Quaternion.Euler(m_RigidBody.velocity.normalized *360);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (m_hitTruck)
        //{
        //    Debug.Log(m_hitTruck);
        //    m_CapsuleCollider.enabled = false;
        //}
        Debug.Log(other.tag);
        transform.position -= m_RigidBody.velocity.normalized * 2f;
 
        if (other.tag == "HitZoneLeft" && !m_truckAILogic.m_hitOnLeft)
        {
            //m_carAILogic.m_hitTruckLeft = true;
            //m_truckAILogic.m_hitOnLeft = true;
            m_hitTruck = true;
            transform.position = m_hitZoneLeftGrapplingPoint.transform.position;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            m_CapsuleCollider.enabled = false;
            m_RigidBody.isKinematic = true;
            transform.parent = m_hitZoneLeftGrapplingPoint.transform;
        }
        else if(other.tag == "HitZoneRight" && !m_truckAILogic.m_hitOnRight)
        {
            //m_carAILogic.m_hitOnRight = true;
            //m_truckAILogic.m_hitOnRight = true;
            m_hitTruck = true;
            transform.position = m_hitZoneRightGrapplingPoint.transform.position;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            m_RigidBody.isKinematic = true;
            m_CapsuleCollider.enabled = false;
            transform.parent = m_hitZoneRightGrapplingPoint.transform;
        }
        else if (other.tag == "HitZoneFront" && !m_truckAILogic.m_hitOnFront)
        {
            //m_carAILogic.m_hitTruckFront = true;
            //m_truckAILogic.m_hitOnFront = true;
            m_hitTruck = true;
            transform.position = m_hitZoneFrontGrapplingPoint.transform.position;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            m_RigidBody.isKinematic = true;
            m_CapsuleCollider.enabled = false;
            transform.parent = m_hitZoneFrontGrapplingPoint.transform;
        }

        //Ray ray = new Ray(transform.position, m_RigidBody.velocity.normalized * 3f);
        //RaycastHit rayHit;



        //if (Physics.Raycast(ray, out rayHit, 3f))
        //{
        //    m_RigidBody.velocity = Vector3.zero;
        //    m_RigidBody.useGravity = false;
        //    if(other.tag == "HitZoneLeft" && !m_truckAILogic.m_hitOnLeft) {
        //        //m_carAILogic.m_hitTruckLeft = true;
        //        //m_truckAILogic.m_hitOnLeft = true;
        //        transform.position = m_hitZoneLeftGrapplingPoint.transform.position;
        //    } else if (other.tag == "HitZoneRight" && !m_truckAILogic.m_hitOnRight) {
        //        //m_carAILogic.m_hitTruckRight = true;
        //        //m_truckAILogic.m_hitOnRight = true;
        //        transform.position = m_hitZoneRightGrapplingPoint.transform.position;
        //    } else if (other.tag == "HitZoneFront" && !m_truckAILogic.m_hitOnFront && m_truckAILogic.m_hitOnLeft && m_truckAILogic.m_hitOnLeft) {
        //        //m_carAILogic.m_hitTruckFront = true;
        //        //m_truckAILogic.m_hitOnFront = true;
        //        transform.position = m_hitZoneFrontGrapplingPoint.transform.position;
        //    } else {
        //        transform.position = rayHit.point;
        //    }
        //}
        //else
        //{
        //    m_RigidBody.velocity = Vector3.zero;
        //    m_RigidBody.useGravity = false;
        //}

        //transform.parent = other.transform;
    }
}
