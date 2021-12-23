using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    public int currentLifeCount;
    [SerializeField] int startLifeCount;
    public delegate void OnDie();
    public OnDie onDie;
    [SerializeField] Transform lifesCOntent;
    [SerializeField] GameObject lifesPrefab;

    private void Start()
    {
        currentLifeCount = startLifeCount;
        for (int i = 0; i < startLifeCount; i++)
        {
            Instantiate(lifesPrefab, lifesCOntent);
        }
    }
    public void RemoveLife(int count =1)
    {
        currentLifeCount -= count;
        if (currentLifeCount<=0)
        {
            onDie.Invoke();
        }
        foreach (Transform item in lifesCOntent)
        {
            Destroy(item.gameObject);
            count--;
            if (count == 0) return;
            
        }
    }
    public void AddLife()
    {
        Instantiate(lifesPrefab, lifesCOntent);
        currentLifeCount++;
    }
}
