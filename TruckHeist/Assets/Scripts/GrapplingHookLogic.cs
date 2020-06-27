using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookLogic : MonoBehaviour
{
    public bool m_hitTruck = false;

    private Rigidbody m_RigidBody;

    TruckAILogic m_truckAILogic;
    CarAILogic m_carAILogic;
    PathFollowerLogic m_pathFollowerLogic;



    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>();
        m_carAILogic = GetComponentInParent<CarAILogic>();
        m_pathFollowerLogic = GameObject.FindGameObjectWithTag("TruckFollowObject").GetComponent<PathFollowerLogic>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update() {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.tag);
        if(other.tag == "HitZoneLeft") {
            m_carAILogic.m_hitTruckLeft = true;
            m_truckAILogic.m_hitOnLeft = true;
        } else if (other.tag == "HitZoneRight") {
            m_carAILogic.m_hitTruckRight = true;
            m_truckAILogic.m_hitOnRight = true;
        } else if (other.tag == "HitZoneFront") {
            m_carAILogic.m_hitTruckFront = true;
            m_truckAILogic.m_hitOnFront = true;
        } 
        else {
            m_hitTruck = false;
        }

        transform.position -= m_RigidBody.velocity.normalized * 2f;

        Ray ray = new Ray(transform.position, m_RigidBody.velocity.normalized * 3f);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 3f))
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            transform.position = rayHit.point;
        }
        else
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
        }

        transform.parent = other.transform;
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit: " + other.tag);
    }
}
