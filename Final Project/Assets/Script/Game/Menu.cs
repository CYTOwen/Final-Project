using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup info;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && info.alpha == 1)
        {
            info.alpha = 0;
            info.interactable = false;
            info.blocksRaycasts = false;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Info()
    {
        info.alpha = 1f;
        info.blocksRaycasts = true;
        info.interactable = true;
    }
}
