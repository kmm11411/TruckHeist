using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TruckAILogic : MonoBehaviour
{

    float m_aggroRadius = 30f;
    public bool m_chasing = false;
    public bool m_truckOffroad = false;
    public bool m_truckLeftWheelOffroad = false;
    public bool m_truckRightWheelOffroad = false;
    public bool m_carOnRight = false;
    public bool m_carOnLeft = false;
    public bool m_hitOnLeft = false;
    public bool m_hitOnRight = false;
    public bool m_hitOnFront = false;

    GameObject[] m_players;

    int m_index;

    SteeringController m_steeringController;
    [SerializeField]
    Transform m_truckLeftWheel;
    [SerializeField]
    Transform m_truckRightWheel;

    GameObject m_truckFollowObject;
    
    public float m_distanceFromFollowObject;
    public float m_directionFromFollowObject;


    // Start is called before the first frame update
    void Start()
    {
        m_players = GameObject.FindGameObjectsWithTag("Car");
        m_steeringController = GetComponentInChildren<SteeringController>();
        m_truckFollowObject = GameObject.FindGameObjectWithTag("TruckFollowObject");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Truck is chasing: " + m_chasing);
        //Debug.Log("Player index: " + m_index);
        //Debug.Log("Players Length: " + m_players.Length);
        CheckChasing();

        if(!m_truckRightWheelOffroad) {
            m_truckLeftWheelOffroad = CheckOffRoad(m_truckLeftWheel.position);
        }

        if(!m_truckLeftWheelOffroad) {
            m_truckRightWheelOffroad = CheckOffRoad(m_truckRightWheel.position);
        }

        m_distanceFromFollowObject = CheckDistanceFromFollowObject();
        m_distanceFromFollowObject = CheckDirectionFromFollowObject();
    }

    void CheckChasing() {
        if (m_chasing) {
            float distance = Vector3.Distance(m_players[m_index].transform.position, transform.position);

            if(distance < m_aggroRadius) {
                m_chasing = true;
                return;
            } else {
                m_chasing = false;
            }
        }

        for(int i = 0; i < m_players.Length; i++) {
            float distance = Vector3.Distance(m_players[i].transform.position, transform.position);

            if(distance < m_aggroRadius) {
                m_chasing = true;
                m_index = i;
            }
        }
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

    float CheckDirectionFromFollowObject() {
        var heading = m_truckFollowObject.transform.position - transform.position;
        float dist = Vector3.Dot(heading, transform.forward);
        return dist;
    }

    float CheckDistanceFromFollowObject() {
        float distance = Vector3.Distance(m_truckFollowObject.transform.position, transform.position);
        return distance;
    }

}
