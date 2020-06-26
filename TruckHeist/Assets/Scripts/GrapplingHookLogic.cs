using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookLogic : MonoBehaviour
{
    public bool m_IsStuck = false;
    public bool m_hitTruck = false;

    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_IsStuck = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    void Update() {
        // if(!m_IsStuck) {
        //     transform.position -= m_RigidBody.velocity.normalized * 2f;

        //     Ray ray = new Ray(transform.position, m_RigidBody.velocity.normalized * 3f);
        //     RaycastHit rayHit;

        //     if (Physics.Raycast(ray, out rayHit, 3f))
        //     {
        //         m_IsStuck = true;
        //         m_RigidBody.velocity = Vector3.zero;
        //         m_RigidBody.useGravity = false;
        //         transform.position = rayHit.point;
        //     }
        //     else
        //     {
        //         m_IsStuck = false;
        //         m_RigidBody.velocity = Vector3.zero;
        //         m_RigidBody.useGravity = false;
        //     }
        // }
        

        //transform.parent = other.transform;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "HitZone1") {
            m_hitTruck = true;
        } else {
            m_hitTruck = false;
        }

        transform.position -= m_RigidBody.velocity.normalized * 2f;

        Ray ray = new Ray(transform.position, m_RigidBody.velocity.normalized * 3f);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, 3f))
        {
            m_IsStuck = true;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
            transform.position = rayHit.point;
        }
        else
        {
            m_IsStuck = false;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
        }

        transform.parent = other.transform;
    }

}
