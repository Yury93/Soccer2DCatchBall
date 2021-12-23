using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] List<SpriteRenderer> heroSr;

    public bool godMode;
    public bool enableShift;

    public float shiftSpeed;
    public float shiftDelay;
   
    float leftDoubleTapTImer, rightDoubleTimer;

    bool shiftProcess;
    bool left, right;
    MovementPlatformer mover;
    private void Awake()
    {
        mover = GetComponent<MovementPlatformer>();
    }
    void Start()
    {

    }

    void Update()
    {
        if (left || right)
        {
            anim.SetFloat("Speed", 1);
        }
        else anim.SetFloat("Speed", 0);
        leftDoubleTapTImer -= Time.deltaTime;
        rightDoubleTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A)) onButtonLeftDown();
        else if (Input.GetKeyDown(KeyCode.D)) onButtonRightDown();

        if (Input.GetKeyUp(KeyCode.A)) onButtonLeftUp();
        else if (Input.GetKeyUp(KeyCode.D)) onButtonRightUp();


        if (left) mover.Move(-1);
        else if (right) mover.Move(1);

        if (Input.GetKeyDown(KeyCode.Space)) mover.Jump();

    }



    public void onButtonLeftDown()
    {

        if (leftDoubleTapTImer > 0 && enableShift) StartCoroutine(dodgeCor(-1));
        leftDoubleTapTImer = 0.3f;
        left = true;
    }
    public void onButtonRightDown()
    {
        anim.SetFloat("Speed", 1);
        if (rightDoubleTimer > 0 && enableShift) { StartCoroutine(dodgeCor(1)); }
        rightDoubleTimer = 0.3f;
        right = true;

    }
    public void onButtonLeftUp()
    {
        left = false;
    }
    public void onButtonRightUp()
    {
        right = false;
    }
    public void Damage()
    {
        if (godMode) return;
        StartCoroutine(godMdCor());
      
    }
    IEnumerator godMdCor()
    {
        heroSr[0].color = new Color(1, 0, 0, 1f);

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 5; i++)
        {

            yield return new WaitForSeconds(0.1f);
            foreach (var item in heroSr)
            {
                item.color = new Color(1, 1, 1, 0.2f);
            }
            yield return new WaitForSeconds(0.1f);
            foreach (var item in heroSr)
            {
                item.color = new Color(1, 1, 1, 1f);
            }
        }

    }
    IEnumerator dodgeCor(float dir)
    {
        if (shiftProcess) yield break;
        shiftProcess = true;
        float t = 0.2f;
        while (t > 0)
        {
            t -= Time.deltaTime;
            yield return null;
            mover.Move(dir * shiftSpeed);

        }
        yield return new WaitForSeconds(shiftDelay);
        shiftProcess = false;
    }
}
