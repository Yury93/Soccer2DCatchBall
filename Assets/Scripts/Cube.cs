using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatchTheSphere
{
    public class Cube : Destructible
    {
        [SerializeField] private Animator animator;
        
        /// <summary>
        /// Сфера наносит кубику урон
        /// </summary>
        /// <param name="collision"></param>

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject)
            { 
                var des = collision.gameObject.transform.GetComponent<Destructible>();
                var prop = collision.gameObject.transform.GetComponent<Sphere>().SphereProperties;

                animator.SetBool("AddBall", true);

                if (prop)
                {
                    PlayerMetrics.Instance.AddScore(des.Use(prop));
                }

                StartCoroutine(corAnim());

                if (des != null)
                {
                    if (des.currentHp > 0)
                    {
                        
                        des.ApplyDamage(1);
                    }
                }

                IEnumerator corAnim()
                {
                    yield return new WaitForSeconds(1f);
                    animator.SetBool("AddBall", false);
                }
            }
        }
    }
}