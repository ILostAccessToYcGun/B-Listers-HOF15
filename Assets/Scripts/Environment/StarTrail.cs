using UnityEngine;
using System.Collections;

public class StarTrail : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] public bool isSpawningStars;
    [SerializeField] GameObject trailStars;

    private bool coroutineAllowed = false;


    private IEnumerator SpawnStars()
    {
        
        while (true)
        {
            //if (coroutineAllowed)
            //    coroutineAllowed = false;
            yield return new WaitForSeconds(0.5f);

            if (isSpawningStars)
            {
                
                GameObject newStar = Instantiate(trailStars, this.transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0), Quaternion.identity);
                Spin spin = newStar.GetComponentInChildren<Spin>();
                spin.turnSpeed = Random.Range(30f, 90f);
                Debug.Log("e");
            }
            
        }
    }

    void Start()
    {
        StartCoroutine(SpawnStars());
    }

    private void Update()
    {
        
    }
}
