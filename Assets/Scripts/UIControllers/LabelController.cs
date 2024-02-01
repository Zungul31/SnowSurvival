using UnityEngine;

public class LabelController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private RectTransform labelRect;
    [SerializeField] private float distance;

    private Camera currentCamera;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    private void Update()
    {
        var newPos = currentCamera.WorldToScreenPoint(target.position);
        newPos.y += distance;
        newPos.z = labelRect.position.z;
        labelRect.position = newPos;
    }
}
