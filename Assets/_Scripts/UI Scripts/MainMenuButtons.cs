using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject CreditsWindow;

    public void MovetoScene(int sceneID) //PlayGame Button
    {
        SceneManager.LoadScene(sceneID);
    }

    public void Credits()
    {
        //set active credits window
        if (CreditsWindow.activeSelf)
        {
            CreditsWindow.SetActive(false);
        }
        else
        {
            CreditsWindow.SetActive(true);
        }      
    }

    public void CloseCredits()
    {
        CreditsWindow.SetActive(false);
    }

    public void ExitGame()
    {
        //Close game

        Application.Quit();

        Debug.Log("Game is exiting");
    }

}
