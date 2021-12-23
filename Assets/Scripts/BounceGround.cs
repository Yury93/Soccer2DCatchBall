using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGround : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.Translate(Random.Range(-10, 10), Random.Range(-10, 10), transform.position.z);
    }
}
