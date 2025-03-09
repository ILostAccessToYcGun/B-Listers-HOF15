using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject creditsPanel;

    public void OnPlay()
    {
        Debug.Log("Play");
        //Change Scene
    }

    public void OnCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);

    }

    public void OnCreditsBack()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void OnExit()
    {
        exitPanel.SetActive(true);
    }

    public void OnExitYes()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnExitNo()
    {
        exitPanel.SetActive(false);
    }
}
