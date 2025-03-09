using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject exitPanel;

    public void OnPlay()
    {

    }

    public void OnCredits()
    {

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
