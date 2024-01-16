using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void StartGame()
    {
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
