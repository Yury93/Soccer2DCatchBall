using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaper : MonoBehaviour
{
    public Transform handsPos;
    public GameObject currentCheese;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rat>())
        {
            Debug.Log("Колизия с крысой");
            if (currentCheese!=null)
            {
                if (currentCheese.GetComponent<Cheese>().isBad)
                {
                    Debug.Log("Отдали плохой сыр");
                }
                else Debug.Log("Отдали хороший сыр");
                Destroy(currentCheese);
            }
           
        }
        if (currentCheese != null) return;
        if (collision.gameObject.GetComponent<Cheese>())
        {
            collision.gameObject.GetComponent<Cheese>().Pickup();
            pickup(collision.gameObject);
        }
    }
    public void pickup(GameObject go)
    {
        go.GetComponent<Cheese>().Pickup();
        Destroy(go.GetComponent<Rigidbody2D>());
        currentCheese = go;
        go.transform.position = handsPos.position;
        go.transform.parent = handsPos;
    }
   
}

