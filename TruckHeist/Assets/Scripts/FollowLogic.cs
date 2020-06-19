using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLogic : MonoBehaviour
{
    TruckAILogic m_truckAILogic;
    SteeringController m_steeringControllerCar1;
    SteeringController m_steeringControllerCar2;

    // Start is called before the first frame update
    void Start()
    {
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>();
        m_steeringControllerCar1 = GameObject.FindGameObjectWithTag("Car1").GetComponent<SteeringController>();
        m_steeringControllerCar2 = GameObject.FindGameObjectWithTag("Car2").GetComponent<SteeringController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Car1" && m_steeringControllerCar1.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = true;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = true;
            }
        } else if(other.tag == "Car2" && m_steeringControllerCar2.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = true;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Car1" && m_steeringControllerCar1.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = false;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = false;
            }
        } else if(other.tag == "Car2" && m_steeringControllerCar2.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = false;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = false;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.tag == "Car1" && m_steeringControllerCar1.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = true;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = true;
            }
        } else if (other.tag == "Car1" && !m_steeringControllerCar1.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = false;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = false;
            }
        }
        
        if(other.tag == "Car2" && m_steeringControllerCar2.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = true;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = true;
            }
        } else if (other.tag == "Car2" && !m_steeringControllerCar2.m_ActivePlayer) {
            if(this.tag == "FollowSpaceLeft") {
                m_truckAILogic.m_carOnLeft = false;
            } else if (this.tag == "FollowSpaceRight") {
                m_truckAILogic.m_carOnRight = false;
            }
        }
    }
}
