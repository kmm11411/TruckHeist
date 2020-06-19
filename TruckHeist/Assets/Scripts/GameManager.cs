using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CarController> m_CarControllers;
    public int m_CarControllerIndex = 0;

    void Start()
    {
        for (int index = 0; index < m_CarControllers.Count; index++)
        {
            if (index == m_CarControllerIndex)
            {
                m_CarControllers[index].ActivateCarController();
            }
            else
            {
                m_CarControllers[index].DeactivateCarController();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
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
                }
            }
        }
    }
}
