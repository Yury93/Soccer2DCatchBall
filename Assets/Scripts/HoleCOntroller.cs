using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleCOntroller : MonoBehaviour
{
    public List<Hole> holes;

    public Vector2 randomSpawnRat;

    void Start()
    {
        StartCoroutine(spawner());
    }
    IEnumerator spawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomSpawnRat.x, randomSpawnRat.y));

            while (true)
            {
                int r = Random.Range(0, holes.Count);
                if (!holes[r].enableRat)
                {
                    holes[r].Spawn();
                    break;
                }
                yield return null;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
