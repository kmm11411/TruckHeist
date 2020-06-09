using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSping : MonoBehaviour
{
    float wheelSpin = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*
    void Update()
    {
        float wheelSpin = transform.localRotation.x;
        Debug.Log(wheelSpin);
        wheelSpin +=15f;
        Debug.Log(wheelSpin);

        transform.localRotation = Quaternion.Euler(new Vector3(wheelSpin, transform.localRotation.y, transform.localRotation.z));
    }
    */

    void FixedUpdate()
    {
        //float wheelSpin = transform.localRotation.x;
        Debug.Log(wheelSpin);
        wheelSpin += 15f;
        if (wheelSpin > 360)
        {
            wheelSpin = wheelSpin % 360;
        }
        Debug.Log(wheelSpin);

        transform.localRotation = Quaternion.Euler(new Vector3(wheelSpin, transform.localRotation.y, transform.localRotation.z));
    }
}
