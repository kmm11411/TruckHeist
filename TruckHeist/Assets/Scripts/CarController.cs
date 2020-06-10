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
    public CinemachineVirtualCamera m_CMVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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

    public void DeactivateCarController()
    {
        m_SteeringController.m_ActivePlayer = false;
        m_SphereController.m_ActivePlayer = false;
        m_WheelSteeringLogicRight.m_ActivePlayer = false;
        m_WheelSteeringLogicLeft.m_ActivePlayer = false;
        m_WheelSteeringReverse.m_ActivePlayer = false;
        m_CMVirtualCamera.enabled = false;
    }


}
