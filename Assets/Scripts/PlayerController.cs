using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory
{
    public int maxCapacity;
    public Dictionary<EResources, int> resources;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private JoystickController joystick;
    
    [SerializeField] private Animator anim;

    private Vector2 moveVector;

    private EPlayerStatus playerStatus = EPlayerStatus.Idle;
    private EDirection lastDirection = EDirection.Down;

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            moveVector.x = Input.GetAxis("Horizontal");
            moveVector.y = Input.GetAxis("Vertical");
            moveVector = moveVector.normalized;
        }
        else if (joystick.Direction != Vector2.zero)
        {
            moveVector = joystick.Direction;
        }
        else
        {
            moveVector = Vector2.zero;
        }
        SetAnimation(moveVector);
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * speed;
    }

    private void SetAnimation(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            if (playerStatus == EPlayerStatus.Moves) { playerStatus = EPlayerStatus.Idle; }

            switch (lastDirection)
            {
                case EDirection.Up: anim.Play("idleUp"); break;
                case EDirection.Right: anim.Play("idleRight"); break;
                case EDirection.Down: anim.Play("idleDown"); break;
                case EDirection.Left: anim.Play("idleLeft"); break;
                default: anim.Play("idleDown"); break;
            }
        }
        else
        {
            direction = direction.normalized;
            
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    if (lastDirection != EDirection.Up || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveUp");
                        lastDirection = EDirection.Up;
                    }
                }
                else
                {
                    if (lastDirection != EDirection.Down || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveDown");
                        lastDirection = EDirection.Down;
                    }
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    if (lastDirection != EDirection.Right || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveRight");
                        lastDirection = EDirection.Right;
                    }
                }
                else
                {
                    if (lastDirection != EDirection.Left || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveLeft");
                        lastDirection = EDirection.Left;
                    }
                }
            }

            playerStatus = EPlayerStatus.Moves;
        }
    }

    // private void OnTriggerEnter2D(Collider2D collider)
    // {
    //     Debug.Log("TriggerEnter: " + collider.name);
    // }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "GameController" && playerStatus == EPlayerStatus.Idle)
        {
            Vector2 playerDir;
            switch (lastDirection)
            {
                case EDirection.Up: playerDir = Vector2.up; break;
                case EDirection.Right: playerDir = Vector2.right; break;
                case EDirection.Down: playerDir = Vector2.down; break;
                case EDirection.Left: playerDir = Vector2.left; break;
                case EDirection.None: playerDir = Vector2.zero; break;
                default: playerDir = Vector2.zero; break;
            }

            Vector2 objectDir = (collider.transform.position - transform.position).normalized;

            if (Vector2.Dot(playerDir, objectDir) > 0.5f)
            {
                playerStatus = EPlayerStatus.Mines;
                
                Debug.Log("Object: " + collider.name);
            }
        }
    }

    // private void OnTriggerExit2D(Collider2D collider)
    // {
    //     Debug.Log("TriggerExit: " + collider.name);
    // }
}
