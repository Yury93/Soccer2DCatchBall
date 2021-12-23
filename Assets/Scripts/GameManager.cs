using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LifeSystem ls;
    void Start()
    {
        ls.onDie += die;
    }
    void die()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
