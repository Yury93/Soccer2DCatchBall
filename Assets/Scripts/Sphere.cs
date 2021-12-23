using UnityEngine;

namespace CatchTheSphere
{
    public class Sphere : MonoBehaviour
    {
        [SerializeField] private SphereProperties sphereAsset;
        public SphereProperties SphereProperties => sphereAsset;
        public void Start()
        {
            gameObject.GetComponent<Sphere>().Use(sphereAsset);
            gameObject.GetComponent<Destructible>().Use(sphereAsset);
        }
        public void Use(SphereProperties sphereProperties)
        { 
            gameObject.GetComponentInChildren<SpriteRenderer>().material.color = sphereProperties.colorProp;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = sphereProperties.gravityProp;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sphereProperties.Sprite;
        }
        public void SphereEditor(SphereProperties sphereProperties)
        {
            sphereAsset = sphereProperties;
        }
    }
}