using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject stalaktit, heart, bonusTIme, big;
    public Vector2 randomX, randomSpawnTimeStalaktit, randomSpawnTimeHeart, randomSpawnTimebonus, randomBig;

    void Start()
    {
        if (stalaktit) StartCoroutine(spawn());
        if (heart) StartCoroutine(HeartSpawn());
        if (bonusTIme) StartCoroutine(timeSpawn());
        if (big) StartCoroutine(spawnBig());
    }
    IEnumerator timeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomSpawnTimebonus.x, randomSpawnTimebonus.y));
            Vector3 pos = new Vector3(transform.position.x + Random.RandomRange(randomX.x, randomX.y), transform.position.y, 0);
            Instantiate(bonusTIme, pos, Quaternion.identity);
        }
    }
    IEnumerator HeartSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomSpawnTimeHeart.x, randomSpawnTimeHeart.y));
            Vector3 pos = new Vector3(transform.position.x + Random.RandomRange(randomX.x, randomX.y), transform.position.y, 0);
            Instantiate(heart, pos, Quaternion.identity);
        }
    }
    IEnumerator spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomSpawnTimeStalaktit.x, randomSpawnTimeStalaktit.y));
            Vector3 pos = new Vector3(transform.position.x + Random.RandomRange(randomX.x, randomX.y), transform.position.y, 0);

            Instantiate(stalaktit, pos, Quaternion.identity);
        }
    }
    IEnumerator spawnBig()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomBig.x, randomBig.y));
            Vector3 pos = new Vector3(transform.position.x + Random.RandomRange(randomX.x, randomX.y), transform.position.y, 0);

            Instantiate(big, pos, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
