using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public bool enableRat = false;
    public Vector2 randomLifeTime;
    public GameObject rat;
        
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Spawn()
    {
        enabled = true;
        StartCoroutine(lifetime());
    }
    IEnumerator lifetime()
    {
        rat.SetActive(true);
        yield return new WaitForSeconds(Random.Range(randomLifeTime.x, randomLifeTime.y));
        rat.SetActive(false);
        enableRat = false;
    }
}
