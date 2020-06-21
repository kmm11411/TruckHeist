using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathFollowerLogic : MonoBehaviour
{
    public float m_speed = 90f;
    public PathCreator m_pathCreator;
    public EndOfPathInstruction m_end;
    float m_distTravelled;
    float m_followSpace = 30f;

    GameObject m_truck;


    // Start is called before the first frame update
    void Start()
    {
        m_truck = GameObject.FindGameObjectWithTag("Truck");
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, m_followSpace);
    }

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector3.Distance(m_truck.transform.position, transform.position);

        var heading = transform.position - m_truck.transform.position;
        float dist = Vector3.Dot(heading, m_truck.transform.forward);
        //Debug.Log("Distance: " + dist);

        if(dist < 0 || Mathf.Abs(dist) < m_followSpace) {
            m_distTravelled += m_speed * Time.deltaTime;
            transform.position = m_pathCreator.path.GetPointAtDistance(m_distTravelled);
            transform.rotation = m_pathCreator.path.GetRotationAtDistance(m_distTravelled);
        } else {
            transform.position = Vector3.zero;
        }
    }
}
