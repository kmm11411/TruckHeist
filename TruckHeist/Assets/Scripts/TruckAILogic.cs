using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TruckAILogic : MonoBehaviour
{

    float m_aggroRadius = 30f;
    public bool m_chasing = false;

    GameObject[] m_players;

    int m_index;

    SteeringController m_steeringController;

    // Start is called before the first frame update
    void Start()
    {
        m_players = GameObject.FindGameObjectsWithTag("Car");
        m_steeringController = GetComponentInChildren<SteeringController>();
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 0, 0, 0.7f);
        Gizmos.DrawSphere(transform.position, m_aggroRadius);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Truck is chasing: " + m_chasing);
        Debug.Log("Player index: " + m_index);
        Debug.Log("Players Length: " + m_players.Length);

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
}
