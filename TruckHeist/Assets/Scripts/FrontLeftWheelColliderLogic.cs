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
        Debug.Log("OnTriggerEnter. FrontLeftWheelCollider: ");
        if(other.tag != "Road") {

        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("OnCollisionEnter. FrontLeftWheelCollider: " + other.collider.gameObject.layer);
    }
}
