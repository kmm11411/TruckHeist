using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAILogic : MonoBehaviour
{
    public bool m_carOffroad = false;
    public bool m_carLeftWheelOffroad = false;
    public bool m_carRightWheelOffroad = false;
    public float m_distanceToTruck;
    public float m_lastAcceleration = 50f;
    public float m_lastDist = 0;

    GameObject m_truck;

    [SerializeField]
    Transform m_carLeftWheel;
    [SerializeField]
    Transform m_carRightWheel;

    public float m_distanceToFollowObject;

    GameObject m_truckFollowSpaceLeft;
    GameObject m_truckFollowSpaceRight;
    GameObject m_car1FollowObject;
    GameObject m_car2FollowObject;
    
    Vector3 m_heading;
    float m_dist;

    public float m_directionToTruckFollowSpaceLeft;
    public float m_directionToTruckFollowSpaceRight;
    public float m_directionToTruck;


    // Start is called before the first frame update
    void Start()
    {
        m_truck = GameObject.FindGameObjectWithTag("Truck");
        //m_car1FollowObject = GameObject.FindGameObjectWithTag("Car1FollowObject");
        //m_car2FollowObject = GameObject.FindGameObjectWithTag("Car2FollowObject");
        m_truckFollowSpaceLeft = GameObject.FindGameObjectWithTag("FollowSpaceLeft");
        m_truckFollowSpaceRight = GameObject.FindGameObjectWithTag("FollowSpaceRight");
    }

    // Update is called once per frame
    void Update()
    {
        m_distanceToTruck = CheckDistancetoTruck();

        if(!m_carRightWheelOffroad) {
            m_carLeftWheelOffroad = CheckOffRoad(m_carLeftWheel.position);
        }

        if(!m_carLeftWheelOffroad) {
            m_carRightWheelOffroad = CheckOffRoad(m_carRightWheel.position);
        }

        //CheckDirectionFromFollowObject();
        m_directionToTruck = CheckDirectionToTruck();
        m_directionToTruckFollowSpaceLeft = CheckDirectionToTruckFollowSpaceLeft();
        m_directionToTruckFollowSpaceRight = CheckDirectionToTruckFollowSpaceRight();
    }

    float CheckDistancetoTruck() {
        var heading = m_truck.transform.position - transform.position;
        float dist = Vector3.Dot(heading, m_truck.transform.forward);
        return dist;
    }

    bool CheckOffRoad(Vector3 position) {
        LayerMask layerMask = LayerMask.GetMask("Ground");
        Ray ray = new Ray(position, Vector3.down);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, layerMask)) {
            string hitTag = hit.collider.gameObject.tag;

            if(hitTag == "Offroad") {
                return true;
            } 
        }
        return false;
    }

    // void CheckDirectionFromFollowObject() {
    //     if(this.tag == "Car1") {
    //         m_heading = m_car1FollowObject.transform.position - transform.position;
    //         m_dist = Vector3.Dot(m_heading, transform.forward);
    //     } else if (this.tag == "Car2") {
    //         m_heading = m_car2FollowObject.transform.position - transform.position;
    //         m_dist = Vector3.Dot(m_heading, transform.forward);
    //     }
    //     m_distanceToFollowObject = m_dist;
    // }

    float CheckDirectionToTruck() {
        var heading = m_truck.transform.position - transform.position;
        float dist = Vector3.Dot(heading, transform.forward);
        return dist;
    }

    float CheckDirectionToTruckFollowSpaceLeft() {
        var heading = m_truckFollowSpaceLeft.transform.position - transform.position;
        float dist = Vector3.Dot(heading, transform.forward);
        return dist;
    }

    float CheckDirectionToTruckFollowSpaceRight() {
        var heading = m_truckFollowSpaceRight.transform.position - transform.position;
        float dist = Vector3.Dot(heading, transform.forward);
        return dist;
    }
}
