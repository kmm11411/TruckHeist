using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGunLogic : MonoBehaviour
{
    public GameObject m_grapplingHook;
    public GameObject m_grapplingHookSpawn;
    private Rigidbody m_grapplingHookRigidBody;
    public bool m_gunFired;
    public float m_gunForce;
    private LineRenderer m_lineRenderer;
    public GameObject m_GrapplingPlayer;

    private float m_LookVerticalAngle = 0;
    private float m_LookHorizontalAngle = 0;
    public CinemachineVirtualCamera m_GrapplingHookCam;
    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_gunFired = true;
            m_grapplingHook.SetActive(true);
            m_lineRenderer.enabled = true;
            m_grapplingHookRigidBody = m_grapplingHook.GetComponent<Rigidbody>();
            m_grapplingHookRigidBody.useGravity = true;
            m_grapplingHook.transform.parent = null;
            
            m_grapplingHook.transform.position = m_grapplingHookSpawn.transform.position;
            m_grapplingHook.transform.rotation = m_grapplingHookSpawn.transform.rotation;
            var forwardDirection = transform.forward;
            m_grapplingHookRigidBody.velocity = Vector3.zero;
            m_grapplingHookRigidBody.AddForce(forwardDirection * m_gunForce);
        }

        if (m_gunFired)
        {
            m_lineRenderer.SetPosition(0, m_grapplingHookSpawn.transform.position);
            m_lineRenderer.SetPosition(1, m_grapplingHook.transform.position);
        }

        m_LookVerticalAngle -= Input.GetAxis("Mouse Y");
        m_LookHorizontalAngle += Input.GetAxis("Mouse X");
        m_LookVerticalAngle = Mathf.Clamp(m_LookVerticalAngle, -89f, 89f);

        // apply the vertical angle as a local rotation to the camera transform along its right axis (makes it pivot up and down)
        m_GrapplingPlayer.transform.localRotation = Quaternion.Euler( new Vector3(m_LookVerticalAngle, m_LookHorizontalAngle, 0));

    }
}
