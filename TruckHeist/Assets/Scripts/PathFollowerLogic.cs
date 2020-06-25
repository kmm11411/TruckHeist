using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollowerLogic : MonoBehaviour
{
    public float m_speed;
    public PathCreator m_pathCreator;
    public EndOfPathInstruction m_end;
    float m_distTravelled;
    float m_followSpace = 30f;

    float m_distance;
    Vector3 m_heading;
    float m_direction;

    GameObject m_truck;


    // Start is called before the first frame update
    void Start()
    {
        m_truck = GameObject.FindGameObjectWithTag("Truck");
    }

    // void OnDrawGizmos() {
    //     Gizmos.color = new Color(1, 0, 0, 0.5f);
    //     Gizmos.DrawSphere(transform.position, m_followSpace);
    // }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "TruckFollowObject") {
            m_speed = 90f;
            m_distance = Vector3.Distance(m_truck.transform.position, transform.position);
            m_heading = transform.position - m_truck.transform.position;
            m_direction = Vector3.Dot(m_heading, transform.forward);
        }

        if(m_distance < m_followSpace) {
            m_distTravelled += m_speed * Time.deltaTime;
            transform.position = m_pathCreator.path.GetPointAtDistance(m_distTravelled);
            transform.rotation = m_pathCreator.path.GetRotationAtDistance(m_distTravelled);
        }
    }
}
