using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLogic : MonoBehaviour
{
    TruckAILogic m_truckAILogic;

    // Start is called before the first frame update
    void Start()
    {
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter: " + other.tag);
        if(other.tag == "Car") {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = true;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerExit: " + other.tag);
        if(other.tag == "Car") {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = false;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = false;
            }
        }
    }
}
