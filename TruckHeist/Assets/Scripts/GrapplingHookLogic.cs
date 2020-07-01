using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookLogic : MonoBehaviour
{
    public bool m_hitTruck = false;

    private Rigidbody m_RigidBody;

    TruckAILogic m_truckAILogic;
    CarAILogic m_carAILogic;

    GameObject m_hitZoneLeftGrapplingPoint;
    GameObject m_hitZoneRightGrapplingPoint;
    GameObject m_hitZoneFrontGrapplingPoint;



    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>();
        m_carAILogic = GetComponentInParent<CarAILogic>();

        m_hitZoneLeftGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneLeftGrapplingPoint");
        m_hitZoneRightGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneRightGrapplingPoint");
        m_hitZoneFrontGrapplingPoint = GameObject.FindGameObjectWithTag("HitZoneFrontGrapplingPoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position -= m_RigidBody.velocity.normalized * 2f;

        Ray ray = new Ray(transform.position, m_RigidBody.velocity.normalized * 3f);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 3f))
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            if(other.tag == "HitZoneLeft" && !m_truckAILogic.m_hitOnLeft) {
                m_carAILogic.m_hitTruckLeft = true;
                m_truckAILogic.m_hitOnLeft = true;
                transform.position = m_hitZoneLeftGrapplingPoint.transform.position;
            } else if (other.tag == "HitZoneRight" && !m_truckAILogic.m_hitOnRight) {
                m_carAILogic.m_hitTruckRight = true;
                m_truckAILogic.m_hitOnRight = true;
                transform.position = m_hitZoneRightGrapplingPoint.transform.position;
            } else if (other.tag == "HitZoneFront" && !m_truckAILogic.m_hitOnFront && m_truckAILogic.m_hitOnLeft && m_truckAILogic.m_hitOnLeft) {
                m_carAILogic.m_hitTruckFront = true;
                m_truckAILogic.m_hitOnFront = true;
                transform.position = m_hitZoneFrontGrapplingPoint.transform.position;
            } else {
                transform.position = rayHit.point;
            }
        }
        else
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
        }

        transform.parent = other.transform;
    }
}
