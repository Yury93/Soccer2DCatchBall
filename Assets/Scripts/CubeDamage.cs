using UnityEngine;

namespace CatchTheSphere
{
    /// <summary>
    /// Урон нашему кубику от сфер
    /// </summary>
    public class CubeDamage : MonoBehaviour
    {
        [SerializeField] private Cube cube;
        private void Start()
        {
            PlayerMetrics.Instance.textHealth.text = "Health: " + cube.startHp.ToString();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.GetComponent<Sphere>())
            {
                cube.ApplyDamage(1);
                PlayerMetrics.Instance.HP = cube.currentHp;
                PlayerMetrics.Instance.textHealth.text = "Health: " + cube.currentHp.ToString();
                Destroy(collision.gameObject);
            }
        }
    }
}