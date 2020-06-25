using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CarController> m_CarControllers;
    public int m_CarControllerIndex = 0;

    CarAILogic m_car1AILogic;
    CarAILogic m_car2AILogic;
    CarAILogic m_car3AILogic;
    SphereController m_car1SphereController;
    SphereController m_car2SphereController;
    SphereController m_car3SphereController;

    void Start()
    {
        m_car1AILogic = GameObject.FindGameObjectWithTag("Car1").GetComponent<CarAILogic>();
        m_car2AILogic = GameObject.FindGameObjectWithTag("Car2").GetComponent<CarAILogic>();
        m_car3AILogic = GameObject.FindGameObjectWithTag("Car3").GetComponent<CarAILogic>();

        m_car1SphereController = GameObject.FindGameObjectWithTag("Car1Sphere").GetComponent<SphereController>();
        m_car2SphereController = GameObject.FindGameObjectWithTag("Car2Sphere").GetComponent<SphereController>();
        m_car3SphereController = GameObject.FindGameObjectWithTag("Car3Sphere").GetComponent<SphereController>();

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
                    if(index == 0) {
                        m_car1AILogic.m_lastAcceleration = m_car1SphereController.m_acceleration;
                    } else if (index == 1) {
                        m_car2AILogic.m_lastAcceleration = m_car2SphereController.m_acceleration;
                    } else if (index == 2) {
                        m_car3AILogic.m_lastAcceleration = m_car3SphereController.m_acceleration;
                    }

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
                    if(index == 0) {
                        m_car1AILogic.m_lastAcceleration = m_car1SphereController.m_acceleration;
                    } else if (index == 1) {
                        m_car2AILogic.m_lastAcceleration = m_car2SphereController.m_acceleration;
                    } else if (index == 2) {
                        m_car3AILogic.m_lastAcceleration = m_car3SphereController.m_acceleration;
                    }

                    m_CarControllers[index].DeactivateCarController();
                }
            }
        }
    }
}
