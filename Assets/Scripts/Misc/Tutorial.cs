using UnityEngine;
using System;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform star;

    [Space]
    [SerializeField] private FollowPlayer camScript;
    [SerializeField] private float speed;
    [SerializeField] public bool tutorialPlaying = true;

    [SerializeField] public StarPath killZone;

    AudioManager audioManager;

    bool zoomCam;
    static float t = 0.0f;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        StartCoroutine(TutorialZoom());
    }

    void Update()
    {
        if (tutorialPlaying)
        {
            if (zoomCam)
            {
                t += speed * Time.unscaledDeltaTime;
                camScript.GetComponent<Camera>().orthographicSize = Mathf.Lerp(camScript.GetComponent<Camera>().orthographicSize, 1f, t);

                if (t > 1.0f)
                {
                    t = 0.0f;
                }
            }
            else
            {
                t += speed * Time.unscaledDeltaTime;
                camScript.GetComponent<Camera>().orthographicSize = Mathf.Lerp(camScript.GetComponent<Camera>().orthographicSize, 3f, t);

                if (t > 1.0f)
                {
                    t = 0.0f;
                }

                
            }
        }
    }

    public IEnumerator TutorialZoom()
    {
        tutorialPlaying = true;
        yield return new WaitForSeconds(0.8f);
        //zoom to star
        t = 0.0f;
        zoomCam = true;
        Time.timeScale = 0.07f;
        audioManager.Play("Shine");

        yield return new WaitForSeconds(0.12f);
        //back to player
        t = 0.0f;
        zoomCam = false;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.1f);
        tutorialPlaying = false;
        killZone.coroutineAllowed = true;
    }
}
