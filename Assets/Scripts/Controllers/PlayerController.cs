using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private JoystickController joystick;
    
    [SerializeField] private Animator anim;

    private Vector2 moveVector;

    private EPlayerStatus playerStatus = EPlayerStatus.Idle;
    private Vector2 lastDirection = Vector2.down;

    private BasicInteractiveObj currentInteractiveObj;
    
    [SerializeField] private Inventory inventory = new Inventory();

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
            currentInteractiveObj?.Disconnected();
            currentInteractiveObj = null;
        }
    }
    
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "GameController"
            && playerStatus == EPlayerStatus.Idle
            && CheckDirectionToObject(collider.transform.position))
        {
            playerStatus = EPlayerStatus.Mines;
            currentInteractiveObj = collider.gameObject.GetComponent<BasicInteractiveObj>();
            if (currentInteractiveObj.type == ETypeInteractiveObj.Spawner)
            {
                currentInteractiveObj.Connected();
            }
            else
            {
                currentInteractiveObj.Connected(inventory);
            }
            return;
        }

        if (collider.tag == "ItemObject" && CheckDirectionToObject(collider.transform.position))
        {
            ItemObject item = collider.gameObject.GetComponent<ItemObject>();
            if (inventory.TryGivItem(item.GetItemType()))
            {
                item.HideItem();
            }
            return;
        }
    }
    
    private bool CheckDirectionToObject(Vector3 objectPosition)
    {
        Vector2 objectDir = (objectPosition - transform.position).normalized;
        return Vector2.Dot(lastDirection, objectDir) > 0.5f;
    }
}
