
using UnityEngine;

namespace CatchTheSphere
{
    public class Respawner : SingletonBase<Respawner>
    {
        [SerializeField] private GameObject prefabCube;
        public GameObject prefabCub => prefabCube;
        
        public void Respawn()
        {
            prefabCube = Instantiate(prefabCube, new Vector3(0,-4,0), Quaternion.identity);
            
        }
    }
}