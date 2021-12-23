using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour
{
    public float makeBadTimer;
    public bool isBad;
    public bool onGround;
    public bool onHands;
    public SpriteRenderer sr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (onGround) return;
        if (collision.gameObject.tag == "Platform" || collision.gameObject.GetComponent<Cheese>())
        {
            onGround = true;
            StartCoroutine(onGroundCor());
        }
    }
   
    IEnumerator onGroundCor()
    {
        yield return new WaitForSeconds(makeBadTimer);
        if (onHands==false)
        {
            makeBad();
        }
        yield return new WaitForSeconds(10);
        if (! onHands)
        {
            Destroy(gameObject);
        }
    }
    void makeBad()
    {
        isBad = true;
        sr.color = Color.green;
    }
    public void Pickup()
    {
        onHands = true;
    }
}
