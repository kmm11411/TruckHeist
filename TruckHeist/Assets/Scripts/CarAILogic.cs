using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAILogic : MonoBehaviour
{
    public bool m_carOffroad = false;
    public bool m_carLeftWheelOffroad = false;
    public bool m_carRightWheelOffroad = false;
    public float m_distanceToTruck;
    public float m_lastAcceleration = 1.0f;
    public float m_lastDist = 0;

    GameObject m_truck;

    [SerializeField]
    Transform m_carLeftWheel;
    [SerializeField]
    Transform m_carRightWheel;


    // Start is called before the first frame update
    void Start()
    {
        m_truck = GameObject.FindGameObjectWithTag("Truck");
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
    }

    float CheckDistancetoTruck() {
        var heading = transform.position - m_truck.transform.position;
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
}
