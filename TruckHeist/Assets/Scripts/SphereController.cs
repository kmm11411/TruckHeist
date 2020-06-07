using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{

    float m_acceleration = 0f;
    public Vector3 m_force;
    public float ACCELERATION = 200f;
    Rigidbody m_rigidbody = null;
    SphereCollider m_sphereCollider = null;
    public GameObject m_vehicle;
    public Transform m_Steering;
    

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_acceleration = Input.GetAxis("Vertical") * ACCELERATION;
    }
    
    void FixedUpdate() {
        
        if (Mathf.Abs(m_acceleration) > .1)
        {
            m_force = m_acceleration * m_Steering.forward;
        }
        else
        {
            m_force = new Vector3(0, 0, 0);
        }
        m_rigidbody.AddForce(m_force);

        //m_Steering.rotation = Quaternion.Euler(new Vector3(0,0,0));
        //m_vehicle.transform.position = transform.position;
    }
}
