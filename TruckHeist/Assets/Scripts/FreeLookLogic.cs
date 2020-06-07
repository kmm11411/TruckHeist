using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookLogic : MonoBehaviour
{
    public float m_screenoffset = 1f;
    public float m_cameraSpeedX = 0f;
    public float m_maxCameraSpeedX = 1.5f;
    public float m_cameraSpeedY = 0f;
    public float m_maxCameraSpeedY = 3f;

    bool m_controllerDetected = false;

    public CinemachineFreeLook cm_freeLook;



    // Start is called before the first frame update
    void Start()
    {
        cm_freeLook = GetComponent<CinemachineFreeLook>();
        m_controllerDetected = DetectController();
    }

    // Update is called once per frame
    void Update()
    {
        m_controllerDetected = DetectController();
        UpdateCameraValue(m_controllerDetected);
        cm_freeLook.m_XAxis.m_InputAxisValue = m_cameraSpeedX;
        cm_freeLook.m_YAxis.m_InputAxisValue = m_cameraSpeedY;
    }

    void UpdateCameraValue(bool controllerDetected) {
        if(!controllerDetected) {
            if (Input.mousePosition.x >= Screen.width - m_screenoffset) {
                m_cameraSpeedX = m_maxCameraSpeedX;
            } else if (Input.mousePosition.x <= m_screenoffset) {
                m_cameraSpeedX = -m_maxCameraSpeedX;
            } else {
                m_cameraSpeedX = Input.GetAxis("Mouse X");
            }

            if (Input.mousePosition.y >= Screen.height - m_screenoffset) {
                m_cameraSpeedY = m_maxCameraSpeedY;
            } else if (Input.mousePosition.y <= m_screenoffset) {
                m_cameraSpeedY = -m_maxCameraSpeedY;
            } else {
                m_cameraSpeedY = Input.GetAxis("Mouse Y") * 0.5f;
            }
        } else {
            m_cameraSpeedX = Input.GetAxis("Mouse X");
            m_cameraSpeedY = Input.GetAxis("Mouse Y") * 0.5f;
        }
    }

    bool DetectController() {
        //Get Joystick Names
        string[] temp = Input.GetJoystickNames();
        //Check whether array contains anything
        if(temp.Length > 0) {
            //Iterate over every element
            for (int i = 0; i < temp.Length; i++) {
                //Check if the string is empty of not
                if (!string.IsNullOrEmpty(temp[i])) {
                    return true;
                }
            }
        }
        return false;
    }
}
