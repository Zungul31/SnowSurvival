using System;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField] private List<GameObject> walls;

    public Action<Island> OnEnableIsland;

    private void OnEnable()
    {
        OnEnableIsland?.Invoke(this);
    }

    public void OffWall(Vector2 direction)
    {
        if (direction == Vector2.up) { walls[0].SetActive(false); }
        if (direction == Vector2.right) { walls[1].SetActive(false); }
        if (direction == Vector2.down) { walls[2].SetActive(false); }
        if (direction == Vector2.left) { walls[3].SetActive(false); }
    }
}