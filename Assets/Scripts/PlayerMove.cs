using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PlayerMove : NetworkBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float defaultSpeed = 2;
    public float sprintSpeed = 4;
    public float crouchingSpeed = 1;

    private float speed = 2;

    float forwardVelocity;
    double velocityZ = 0;

    float rightVelocity;
    double velocityX = 0;

    double velocityY = 0;

    bool jumpTrigger = false;
    bool isGrounded;
    bool isCrouching;

    Rigidbody rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        if (hasAuthority)
        {
            speed = defaultSpeed;
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        if (hasAuthority)
        {
            Vector3 move = (transform.right.normalized * (float)velocityX * speed) + (transform.forward.normalized * (float)velocityZ * speed);
            move.y = rb.velocity.y;
            rb.velocity = move;

            if (jumpTrigger)
            {
                rb.AddForce(rb.velocity.x, 5, rb.velocity.z, ForceMode.Impulse);
                jumpTrigger = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority)
        {
            forwardVelocity = Input.GetAxisRaw("Vertical");
            velocityZ = GetSpeed(forwardVelocity, velocityZ);
            animator.SetFloat("VelocityZ", (float)velocityZ);

            rightVelocity = Input.GetAxisRaw("Horizontal");
            velocityX = GetSpeed(rightVelocity, velocityX);
            animator.SetFloat("VelocityX", (float)velocityX);

            if (!isCrouching && forwardVelocity > 0 && Input.GetKey(KeyCode.LeftShift))
            {
                if (speed < sprintSpeed)
                    speed += 2 * Time.deltaTime;
                else
                    speed = sprintSpeed;
            }
            else
            {
                if (speed > defaultSpeed)
                    speed -= 2 * Time.deltaTime;
                else
                    speed = defaultSpeed;
            }

            float runVelocity = (speed - defaultSpeed) / (sprintSpeed - defaultSpeed);
            animator.SetFloat("RunVelocity", runVelocity);

            isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
            if (!isCrouching && isGrounded && Input.GetKeyDown(KeyCode.Space))
                jumpTrigger = true;

            animator.SetBool("isGrounded", isGrounded);
            animator.SetBool("jumpTrigger", jumpTrigger);

            if (!isGrounded)
            {
                velocityY = rb.velocity.y;
            }
            else
            {
                if(velocityY != 0)
                    velocityY = 0;
            }

            animator.SetFloat("VelocityY", (float)velocityY);

            if(isGrounded && Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;

                if (isCrouching)
                {
                    SetCrouchCollider();
                    SetCrouchView();
                }
                else
                {
                    SetDefaultCollider();
                    SetDefaultView();
                }
            }

            if (isCrouching)
            {
                speed = crouchingSpeed;
            }

            animator.SetBool("isCrouching", isCrouching);
        }
    }

    double GetSpeed(float input, double speed)
    {
        if (input != 0)
        {
            if (speed > -1 && speed < 1)
            {
                speed += input * 3 * Time.deltaTime;
                speed = Math.Round(speed, 2);
            }
            else
            {
                speed = input;
            }
        }
        else
        {
            if (speed != 0)
            {
                int multiplier = speed > 0 ? 1 : -1;
                speed -= multiplier * 3 * Time.deltaTime;
                speed = Math.Round(speed, 2);
                if ((speed > 0 && speed < 0.1) || (speed < 0 && speed > -0.1))
                    speed = 0;
            }
        }

        return speed;
    }

    void SetCrouchCollider()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.center = new Vector3(0f, 0.55f, 0.2f);
        col.height = 1.1f;
    }

    void SetDefaultCollider()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        col.center = new Vector3(0f, 0.9f, 0.03f);
        col.height = 1.81f;
    }

    void SetCrouchView()
    {
        MouseLook look = GetComponent<MouseLook>();
        look.SetCrouchView();
    }

    void SetDefaultView()
    {
        MouseLook look = GetComponent<MouseLook>();
        look.SetDefaultView();
    }
}
