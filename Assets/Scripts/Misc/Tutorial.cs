using UnityEngine;
using System;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform star;
    [Space]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform starCamera;


    void Start()
    {
        StartCoroutine(TutorialZoom());
    }

    void Update()
    {
        
    }

    public IEnumerator TutorialZoom()
    {
        yield return new WaitForSeconds(0.3f);
        //playerCamera.GetComponent<Camera>().enabled = false;
        //starCamera.GetComponent<Camera>().enabled = true;
        Time.timeScale = 0.3f;

        yield return new WaitForSeconds(1f);
        //playerCamera.GetComponent<Camera>().enabled = true;
        //starCamera.GetComponent<Camera>().enabled = false;
        Time.timeScale = 1.0f;
    }
}
