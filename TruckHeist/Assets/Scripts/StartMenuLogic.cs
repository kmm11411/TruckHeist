using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuLogic : MonoBehaviour
{

    [SerializeField]
    Button m_startButton;
    [SerializeField]
    Button m_exitButton;

    [SerializeField]
    GameObject m_startCamera;
    [SerializeField]
    GameObject m_transitionCamera;
    [SerializeField]
    GameObject m_startMenu;

    public void OnStartClicked() {
        m_transitionCamera.SetActive(true);
        m_startMenu.SetActive(false);
        m_startCamera.SetActive(false);
    }

    public void OnExitSelected() {
        Application.Quit();
    }
}
