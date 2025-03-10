using UnityEngine;
using System;
using System.Collections;

public class StarPath : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private int routeToGo;

    private float tParam = 0f;

    private Vector2 currentPos;

    [SerializeField] private float moveSpeed = 0.5f;

    public bool coroutineAllowed;
    public bool isKillZone;
    [Space]
    [Header("Objects")]
    [SerializeField] StarTrail starTrail;
    [SerializeField] GameObject gravityWell;
    [SerializeField] GameObject finishZone;
    [SerializeField] Collider2D cirCollider;
    [SerializeField] ParticleSystem starParicles;

    
    void Start()
    {
        routeToGo = 0;
    }

    void FixedUpdate()
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
        if (starTrail != null)
            starTrail.isSpawningStars = false;
        if (cirCollider != null)
            cirCollider.enabled = true;
        if (gravityWell != null)
            gravityWell.SetActive(true);
        if (starParicles != null)
            starParicles.Stop();
        
        if (isKillZone)
        {
            routeToGo += 1;
            if (routeToGo <= routes.Length - 1)
            {
                coroutineAllowed = true;
            }
            else
            {
                Debug.Log("End of routes");
            }
        }
    }

    public void GrowStar()
    {
        transform.localScale = new Vector3(3, 3, 3);
        finishZone.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.name);
        if (tParam <= 0)
        {
            if (collision.transform.tag == "Player") //GRRRRR
            {
                Debug.Log("hhhhhhhhhhhhhhhhhhhhhhhhhhhhh");
                if (cirCollider != null)
                    cirCollider.enabled = false;
                
                routeToGo += 1;
                if (routeToGo <= routes.Length - 1)
                {
                    gravityWell.SetActive(false);
                    starParicles.Play();
                    coroutineAllowed = true;
                    starTrail.isSpawningStars = true;

                    if (routeToGo == 2)
                    {
                        Invoke("GrowStar", 2);
                    }
                }
                else
                {
                    Debug.Log("End of routes");
                }
            }
            
        }
    }
}
