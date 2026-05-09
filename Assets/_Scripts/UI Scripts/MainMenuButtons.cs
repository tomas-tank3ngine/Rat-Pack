using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject CreditsWindow;

    public void PlayGame()
    {
        // Change scene
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




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
