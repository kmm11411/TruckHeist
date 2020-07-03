using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CarController> m_CarControllers;
    public int m_CarControllerIndex = 0;

    void Start()
    {
        // for (int index = 0; index < m_CarControllers.Count; index++)
        // {
        //     if (index == m_CarControllerIndex)
        //     {
        //         m_CarControllers[index].ActivateCarController();
        //     }
        //     else
        //     {
        //         m_CarControllers[index].DeactivateCarController();
        //     }
        // }

        for (int index = 0; index < m_CarControllers.Count; index++)
        {
            m_CarControllers[index].DeactivateCarController();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Grappling Gun Active Self: " + m_CarControllers[m_CarControllerIndex].m_GrapplingGun.activeSelf);
            Debug.Log("Freeze Grapple: " + m_CarControllers[m_CarControllerIndex].m_freezeGrapple);

            if (m_CarControllers[m_CarControllerIndex].m_GrapplingGun.activeSelf || (m_CarControllers[m_CarControllerIndex].m_GrapplingGun.activeSelf && m_CarControllers[m_CarControllerIndex].m_freezeGrapple))
            {
                m_CarControllers[m_CarControllerIndex].DeactivateGrapplingGun();
                m_CarControllers[m_CarControllerIndex].ActivateCarController();
            }
            else
            {
                m_CarControllers[m_CarControllerIndex].ActivateGrapplingGun();
                m_CarControllers[m_CarControllerIndex].DeactivateCarController();
            }
        }
            if (Input.GetKeyDown(KeyCode.R))
        {
            m_CarControllers[m_CarControllerIndex].DeactivateGrapplingGun();
            m_CarControllers[m_CarControllerIndex].ActivateCarController();

            m_CarControllerIndex++;
            m_CarControllerIndex %= m_CarControllers.Count;
            for (int index = 0; index< m_CarControllers.Count; index++)
            {
                if(index == m_CarControllerIndex)
                {
                    m_CarControllers[index].ActivateCarController();
                }
                else
                {
                    m_CarControllers[index].DeactivateCarController();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_CarControllers[m_CarControllerIndex].DeactivateGrapplingGun();
                m_CarControllers[m_CarControllerIndex].ActivateCarController();
            m_CarControllerIndex--;
            if (m_CarControllerIndex < 0)
            {
                m_CarControllerIndex += m_CarControllers.Count;
            }
            for (int index = 0; index < m_CarControllers.Count; index++)
            {
                if (index == m_CarControllerIndex)
                {
                    m_CarControllers[index].ActivateCarController();
                }
                else
                {
                    m_CarControllers[index].DeactivateCarController();

                    //m_CarControllers[index].DeactivateGrapplingGun();
                }
            }
        }
    }
}
