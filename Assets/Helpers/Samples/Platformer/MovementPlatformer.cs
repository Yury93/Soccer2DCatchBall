using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformer : MonoBehaviour
{
    [Header("Drag&Drop")]
    [SerializeField] Transform flipTarget;
    [SerializeField] Animator anim;
    [SerializeField] Helpers.Clip jumpSound;


    public float speed;
    public float wallDownSpeed;
    public float jumpForce;
    public float wallJumpFactor = 1;
    public bool enableDoubleJump;
    public bool enableWallHook;
    public bool isGrounded;
    public bool onWall;
    [SerializeField] float yCheckOffset = 1;


    bool rightWallHooked, leftWallHooked;
    Rigidbody2D rb;
    float doubleJumpCount = 1;
    Transform myTransform;
    bool blockLeftMoving, blockRightMoving;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }
    void Start()
    {

    }
    private void Update()
    {
        anim.SetBool("OnWall", onWall);
        anim.SetBool("IsGround", isGrounded);


        if (onWall && !isGrounded)
        {
            rb.velocity += Vector2.up * 9 * Time.deltaTime;
            transform.Translate(-Vector3.up * Time.deltaTime * wallDownSpeed);

        }
    }

    public void Jump()
    {


        if (isGrounded)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpForce);
        }
        else if (!isGrounded && !onWall && enableDoubleJump && doubleJumpCount > 0)
        {
            anim.SetTrigger("Jump");
            doubleJumpCount--;
            rb.AddForce(Vector2.up * jumpForce);

        }
        else if (onWall)
        {
            anim.SetTrigger("Jump");
            doubleJumpCount = 1;
            if (rightWallHooked) { rb.AddForce(new Vector2(-jumpForce, wallJumpFactor * jumpForce)); FlipToLeft(); }
            else if (leftWallHooked)
            {
                rb.AddForce(new Vector2(jumpForce, wallJumpFactor * jumpForce));
                FlipToRight();
            }
        }
    }
    public void Move(float xDir)
    {
        if (xDir > 0 && !blockRightMoving && !rightWallHooked)//если вправо
        {

            rb.velocity = new Vector2(0, rb.velocity.y);
            transform.Translate(new Vector3(xDir * speed, 0, 0) * Time.deltaTime);
            FlipToRight();
        }
        else
        if (xDir < 0 && !blockLeftMoving && !leftWallHooked)//если вправо
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            FlipToLeft();
            transform.Translate(new Vector3(xDir * speed, 0, 0) * Time.deltaTime);
        }

    }




    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform"))
        {
            if (collision.contacts[0].point.y < myTransform.transform.position.y - yCheckOffset)
            {
                isGrounded = true; doubleJumpCount = 1; onWall = false;
            }
            else { isGrounded = false; }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (collision.transform.position.x < myTransform.position.x)//если стена слева
            {
                blockLeftMoving = true;
            }
            if (collision.transform.position.x > myTransform.position.x)//если стена слева
            {
                blockRightMoving = true;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            blockLeftMoving = false;
            blockRightMoving = false;
        }
    }



    void hookRightWall()
    {
        onWall = true;
        rb.velocity = Vector2.zero;
        rightWallHooked = true;
    }
    void hookLeftWall()
    {
        onWall = true;
        rb.velocity = Vector2.zero;
        leftWallHooked = true;
    }
    void dropWall()
    {
        onWall = false;
        leftWallHooked = false;
        rightWallHooked = false;
    }



    public void OnHookTriggerEnter(bool isLEft)
    {
        if (!isGrounded && enableWallHook)
        {
            if (isLEft) hookLeftWall(); else hookRightWall();
        }
    }
    public void OnHookTriggerExit()
    {
        dropWall();
    }



    public void FlipToLeft()
    {
        flipTarget.transform.localScale =
            new Vector3(-1, flipTarget.localScale.y, flipTarget.localScale.z);
    }
    public void FlipToRight()
    {
        flipTarget.transform.localScale =
           new Vector3(1, flipTarget.localScale.y, flipTarget.localScale.z);
    }
}
