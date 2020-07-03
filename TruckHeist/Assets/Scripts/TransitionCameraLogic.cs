using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCameraLogic : MonoBehaviour
{

    public GameObject m_gameManager;

    GameManager m_gameManagerLogic;

    public float m_transitionTimer = 0f;
    float m_transitionTimeLimit = 2f;
    float m_triggerCarMotionTime = 1f;

    public GameObject m_transitionCamera;



    // Start is called before the first frame update
    void Start()
    {
        m_gameManagerLogic = m_gameManager.GetComponent<GameManager>();
        m_gameManagerLogic.m_triggerTruckMotion = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_transitionTimer <= m_transitionTimeLimit){
            m_transitionTimer += Time.fixedDeltaTime;
        }

        if(m_transitionTimer > m_triggerCarMotionTime) {
            m_gameManagerLogic.m_triggerCarMotion = true;
        }

        if(m_transitionTimer > m_transitionTimeLimit){
            m_gameManagerLogic.m_openingScene = false;
            m_gameManagerLogic.StartGame();
            m_transitionCamera.SetActive(false);
        }
    }
}
