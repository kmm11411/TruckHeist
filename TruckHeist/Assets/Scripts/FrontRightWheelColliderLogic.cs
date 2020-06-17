using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontRightWheelColliderLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("FrontRightWheelCollider: " + other.tag);
        if(other.tag != "Road") {

        }
    }
}
