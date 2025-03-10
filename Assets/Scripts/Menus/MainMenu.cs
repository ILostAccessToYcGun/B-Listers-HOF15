using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject exitPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] Camera mainCam;
    [SerializeField] private float speed;

    bool zoomCam;
    static float t = 0.0f;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.StopAllSounds();
    }

    void Update()
    {
        if (zoomCam)
        {
            t += speed * Time.deltaTime;
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, 0.15f, t);

            if (t > 1.0f)
            {
                t = 0.0f;
            }
        }
        else
        {
            t += speed * Time.deltaTime;
            mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, 5f, t);

            if (t > 1.0f)
            {
                t = 0.0f;
            }
        }
    }

    public void OnPlay()
    {
        Debug.Log("Play");
        //Change Scene
        SceneManager.LoadScene(1);
    }

    public void OnCredits()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
        t = 0f;
        zoomCam = true;
    }

    public void OnCreditsBack()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
        t = 0f;
        zoomCam = false;
    }

    public void OnExit()
    {
        mainPanel.SetActive(false);
        exitPanel.SetActive(true);
        t = 0f;
        zoomCam = true;
    }

    public void OnExitYes()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void OnExitNo()
    {
        mainPanel.SetActive(true);
        exitPanel.SetActive(false);
        t = 0f;
        zoomCam = false;
    }
}
