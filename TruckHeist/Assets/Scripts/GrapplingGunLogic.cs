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

    CarAILogic m_carAILogic;
    TruckAILogic m_truckAILogic;

    CarController m_carController;

    // Start is called before the first frame update
    void Start()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_carAILogic = GetComponentInParent<CarAILogic>();
        m_truckAILogic = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckAILogic>(); 
        m_carController = GetComponentInParent<CarController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_carController.m_freezeGrapple) {
            m_lineRenderer.SetPosition(0, m_grapplingHookSpawn.transform.position);
            m_lineRenderer.SetPosition(1, m_grapplingHook.transform.position);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(m_carAILogic.m_hitTruckLeft) {
                m_carAILogic.m_hitTruckLeft = false;
                m_truckAILogic.m_hitOnLeft = false;
            } else if (m_carAILogic.m_hitTruckRight) {
                m_carAILogic.m_hitTruckRight = false;
                m_truckAILogic.m_hitOnRight = false;
            } else if (m_carAILogic.m_hitTruckFront) {
                m_carAILogic.m_hitTruckFront = false;
                m_truckAILogic.m_hitOnFront = false;
            }

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
        } //else if (Input.GetKeyDown(KeyCode.Space) && m_gunFired) {
        //     m_gunFired = false;
        //     m_grapplingHook.SetActive(false);
        //     m_lineRenderer.enabled = false;
        //     m_grapplingHookRigidBody = m_grapplingHook.GetComponent<Rigidbody>();
        //     m_grapplingHookRigidBody.useGravity = false;
        //     m_grapplingHook.transform.parent = transform.m_GrapplingPlayer;
            
        //     // m_grapplingHook.transform.position = m_grapplingHookSpawn.transform.position;
        //     // m_grapplingHook.transform.rotation = m_grapplingHookSpawn.transform.rotation;
        //     // var forwardDirection = transform.forward;
        //     // m_grapplingHookRigidBody.velocity = Vector3.zero;
        //     // m_grapplingHookRigidBody.AddForce(forwardDirection * m_gunForce);
        // }

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
