using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLeftWheelColliderLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("FrontLeftWheelCollider: " + other.tag);
        if(other.tag != "Road") {

        }
    }
}
