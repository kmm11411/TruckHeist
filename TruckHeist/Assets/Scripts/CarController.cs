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

    // Start is called before the first frame update
    void Start()
    {
        InitializeCars();
        m_GrapplingHookLogic = GetComponentInChildren<GrapplingHookLogic>();
    }

    // Update is called once per frame
    void Update()
    {
       if(m_GrapplingHookLogic.m_hitTruck) {
           transform.parent = m_truck.transform;
           m_CarAILogic.m_stuckToTruck = true;
       }

       Debug.Log("Parent: " + transform.parent);
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
}
