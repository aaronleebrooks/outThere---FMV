using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public void OnNewGameButtonClicked()
    {
        GameManager.Instance.StartGame();
    }

    public void OnQuitButtonClicked()
    {
        GameManager.Instance.QuitGame();
    }
}
