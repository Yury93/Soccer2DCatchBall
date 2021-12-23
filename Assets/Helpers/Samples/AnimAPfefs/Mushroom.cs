using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Animator anim;
    public GameObject effectSpore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Instantiate(effectSpore, transform.position, Quaternion.identity);
            anim.SetTrigger("Play");
        }
    }
}
