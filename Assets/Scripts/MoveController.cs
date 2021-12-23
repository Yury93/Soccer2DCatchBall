using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CatchTheSphere
{
    public class MoveController : MonoBehaviour
    {
        [SerializeField] private PointerClickHold clickLeft;
        [SerializeField] private PointerClickHold clickRight;
        [SerializeField] private Cube cube;
        private Rigidbody2D rb;
        [SerializeField] private float speedMove;

        private void Start()
        {
                rb = cube.GetComponent<Rigidbody2D>();
        }
        void Update()
        { 
            MovementCube();
        }
        private void MovementCube()
        {
            if(!rb)
            {
                //TODO: «аглушка, если мы захотим сделать респавн
                return;
            }
            if(clickLeft.IsHold)
            {
                rb.drag = 5;
                rb.AddForce(Vector2.left * speedMove * Time.deltaTime ,ForceMode2D.Impulse);
            }
            if (clickRight.IsHold)
            {
                rb.drag = 5;
                rb.AddForce(Vector2.right * speedMove * Time.deltaTime, ForceMode2D.Impulse);
            }
            if(!clickLeft.IsHold && !clickRight.IsHold)
            {
                rb.drag = 100;
            }
        }
    }
}