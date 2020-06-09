using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookLogic : MonoBehaviour
{
    private bool m_IsStuck = false;

    private Rigidbody m_RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
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
            m_IsStuck = true;
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.useGravity = false;
        }

        transform.parent = other.transform;
    }

}
