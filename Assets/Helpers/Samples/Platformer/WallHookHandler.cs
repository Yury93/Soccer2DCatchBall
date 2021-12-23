using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHookHandler : MonoBehaviour
{
  public  MovementPlatformer mov;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (collision.transform.position.x < transform.position.x)
            {
              
                mov.OnHookTriggerEnter(true);
            }
            else mov.OnHookTriggerEnter(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            mov.OnHookTriggerExit();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                Debug.Log("левая стена");
                mov.OnHookTriggerEnter(true);
            }
            else mov.OnHookTriggerEnter(false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            mov.OnHookTriggerExit();
        }
    }
}
