using UnityEngine;


namespace CatchTheSphere
{
    [CreateAssetMenu(fileName ="Properties sphere")]
    public class SphereProperties : ScriptableObject
    {
        [SerializeField] private Color color;
        public Color colorProp => color;

        [SerializeField] private int score;
        public int scoreProp => score;

        [SerializeField] private float gravityScale;
        public float gravityProp => gravityScale;
        [SerializeField] private Sprite sprite;
        public Sprite Sprite => sprite;
    }
}