using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector2 distanceValue;

    private Vector2 oldPosition;
    
    private void FixedUpdate()
    {
        if ((Vector2)targetTransform.position != oldPosition)
        {
            Vector2 ditToTarget = targetTransform.position - transform.position;
            var newPos = transform.position;
            if (Mathf.Abs(ditToTarget.x) > distanceValue.x)
            {
                newPos.x = targetTransform.position.x;
            }
            if (Mathf.Abs(ditToTarget.y) > distanceValue.y)
            {
                newPos.y = targetTransform.position.y;
            }

            var distance = Vector2.Distance(transform.position, newPos);
            transform.position = Vector3.MoveTowards(transform.position, newPos, distance * 0.02f);
            
            if (Mathf.Abs(ditToTarget.x) <= distanceValue.x && Mathf.Abs(ditToTarget.y) <= distanceValue.y)
            {
                oldPosition = targetTransform.position;
            }
        }
    }
}
