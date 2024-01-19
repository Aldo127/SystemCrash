using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public GameSettings gameSettings;
    public void StartGame()
    {
        if (Input.GetKey(KeyCode.LeftShift)) gameSettings.endless = true;
        else gameSettings.endless = false;
        SceneManager.LoadScene("SceneyScene");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Credits()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
