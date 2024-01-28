using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private TMP_Text inventoryText;

    private Vector2 moveVector;

    private EPlayerStatus playerStatus = EPlayerStatus.Idle;
    private Vector2 lastDirection = Vector2.down;

    private Inventory inventory;

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

            if (lastDirection == Vector2.up) { anim.Play("idleUp"); }
            else if (lastDirection == Vector2.right) { anim.Play("idleRight"); }
            else if (lastDirection == Vector2.down) { anim.Play("idleDown"); }
            else if (lastDirection == Vector2.left) { anim.Play("idleLeft"); }
            else { anim.Play("idleDown"); }
        }
        else
        {
            direction = direction.normalized;
            
            if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
            {
                if (direction.y > 0)
                {
                    if (lastDirection != Vector2.up || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveUp");
                        lastDirection = Vector2.up;
                    }
                }
                else
                {
                    if (lastDirection != Vector2.down || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveDown");
                        lastDirection = Vector2.down;
                    }
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    if (lastDirection != Vector2.right || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveRight");
                        lastDirection = Vector2.right;
                    }
                }
                else
                {
                    if (lastDirection != Vector2.left || playerStatus != EPlayerStatus.Moves)
                    {
                        anim.Play("moveLeft");
                        lastDirection = Vector2.left;
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
            Vector2 objectDir = (collider.transform.position - transform.position).normalized;

            if (Vector2.Dot(lastDirection, objectDir) > 0.5f)
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
