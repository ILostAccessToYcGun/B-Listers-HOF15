using UnityEngine;
using System;
using System.Collections;

public class StarPath : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    private int routeToGo;

    private float tParam = 0f;

    private Vector2 currentPos;

    [SerializeField] private float moveSpeed = 0.5f;

    private bool coroutineAllowed = true;

    void Start()
    {
        routeToGo = 0;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(Schmove(routeToGo));
        }
    }

    private IEnumerator Schmove(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNumber].GetChild(0).position;
        Vector2 p1 = routes[routeNumber].GetChild(1).position;
        Vector2 p2 = routes[routeNumber].GetChild(2).position;
        Vector2 p3 = routes[routeNumber].GetChild(3).position;

        while(tParam < 1f)
        {
            tParam += Time.deltaTime * moveSpeed;

            currentPos = Mathf.Pow(1 - tParam, 3) * p0 
                + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 
                + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) 
                * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = currentPos;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo < routes.Length - 1)
        {
            coroutineAllowed = true;
        }
    }
}
