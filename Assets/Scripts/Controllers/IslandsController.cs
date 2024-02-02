using System.Collections.Generic;
using UnityEngine;

public class IslandsController : MonoBehaviour
{
    [SerializeField] private List<Island> islands;
    [SerializeField] private float minDistance;

    private void Awake()
    {
        foreach (var island in islands)
        {
            island.OnEnableIsland += UpdateAdjacentWalls;
        }
    }

    private void UpdateAdjacentWalls(Island currentIsland)
    {
        foreach (var island in islands)
        {
            if ((Vector2) island.transform.position == (Vector2)currentIsland.gameObject.transform.position
                || !island.gameObject.activeSelf
                || Vector2.Distance(island.transform.position,
                    currentIsland.gameObject.transform.position) > minDistance)
            {
                continue;
            }

            var dir = (Vector2) (currentIsland.gameObject.transform.position - island.transform.position).normalized;
            currentIsland.OffWall(dir * -1);
            island.OffWall(dir);
        }
    }
}
