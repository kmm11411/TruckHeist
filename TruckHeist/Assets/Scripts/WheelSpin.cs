using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSpin : MonoBehaviour
{
    float wheelSpin = 0;
    public Rigidbody m_SphereRB;
    public SphereController m_SphereController;
    // Start is called before the first frame update
    void Start()
    {
    }

    void FixedUpdate()
    {
        if(m_SphereController.m_reverse == false) { 
            wheelSpin += m_SphereRB.velocity.magnitude;
        }
        else
        {
            wheelSpin -= m_SphereRB.velocity.magnitude;
        }

        if (wheelSpin > 360)
        {
            wheelSpin = wheelSpin % 360;
        }else if (wheelSpin < 0)
        {
            wheelSpin += 360;
        }
        transform.localRotation = Quaternion.Euler(new Vector3(wheelSpin, transform.localRotation.y, transform.localRotation.z));
    }

    public void SpinWheel(float spinAmount)
    {

    }
}
