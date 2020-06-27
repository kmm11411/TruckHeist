using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public SteeringController m_SteeringController;
    public SphereController m_SphereController;
    public WheelSteeringLogic m_WheelSteeringLogicRight;
    public WheelSteeringLogic m_WheelSteeringLogicLeft;
    public WheelSteeringLogic m_WheelSteeringReverse;
    public GrapplingGunLogic m_GrapplingGunLogic;
    public GameObject m_GrapplingGun;
    public CinemachineVirtualCamera m_GrapplingGun_CMVirtualCamera;
    public CinemachineVirtualCamera m_CMVirtualCamera;
    public CarAILogic m_CarAILogic;
    public GrapplingHookLogic m_GrapplingHookLogic;
    public GameObject m_truck;
    public GameObject m_grappleLeftFollowSphere;
    public GameObject m_grappleRightFollowSphere;

    // Start is called before the first frame update
    void Start()
    {
        InitializeCars();
        m_GrapplingHookLogic = GetComponentInChildren<GrapplingHookLogic>();
        m_grappleLeftFollowSphere = GameObject.FindGameObjectWithTag("FollowSpaceLeft");
        m_grappleRightFollowSphere = GameObject.FindGameObjectWithTag("FollowSpaceRight");
    }

    // Update is called once per frame
    void Update()
    {
       if(m_CarAILogic.m_hitTruckFront || m_CarAILogic.m_hitTruckLeft || m_CarAILogic.m_hitTruckRight) {
           transform.parent = m_truck.transform;
           if(m_CarAILogic.m_hitTruckLeft) {
               MoveToGrapplePoint(m_grappleLeftFollowSphere.transform.position);
           } else if (m_CarAILogic.m_hitTruckRight) {
                MoveToGrapplePoint(m_grappleRightFollowSphere.transform.position);
           }
       }
    }


    public void ActivateCarController()
    {
        m_SteeringController.m_ActivePlayer = true;
        m_SphereController.m_ActivePlayer = true;
        m_WheelSteeringLogicRight.m_ActivePlayer = true;
        m_WheelSteeringLogicLeft.m_ActivePlayer = true;
        m_WheelSteeringReverse.m_ActivePlayer = true;
        m_CMVirtualCamera.enabled = true;
    }

    public void ActivateGrapplingGun()
    {
        m_CMVirtualCamera.enabled = false;
        m_GrapplingGun_CMVirtualCamera.enabled = true;
        m_GrapplingGun.SetActive(true);
    }
    public void DeactivateGrapplingGun()
    {
        m_GrapplingGun_CMVirtualCamera.enabled = false;
        m_CMVirtualCamera.enabled = true;
        m_GrapplingGun.SetActive(false);
    }

    public void DeactivateCarController()
    {
        m_CarAILogic.m_lastAcceleration = m_SphereController.m_acceleration;
        m_SteeringController.m_ActivePlayer = false;
        m_SphereController.m_ActivePlayer = false;
        m_WheelSteeringLogicRight.m_ActivePlayer = false;
        m_WheelSteeringLogicLeft.m_ActivePlayer = false;
        m_WheelSteeringReverse.m_ActivePlayer = false;
        m_CMVirtualCamera.enabled = false;
        
    }

    private void InitializeCars() {
        if(this.tag != "Truck") {
            m_SphereController.m_acceleration = 57000f;
        }
    }

    private void MoveToGrapplePoint(Vector3 dest) {
        Vector3 start = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
    }
}
