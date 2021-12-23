using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeDelay=1;
    public bool isUnscale = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnscale)
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay < 0) Destroy(gameObject);
        }
        else
        {
            timeDelay -= Time.unscaledDeltaTime;
        }
      
        if (timeDelay < 0) Destroy(gameObject);
    }
}
