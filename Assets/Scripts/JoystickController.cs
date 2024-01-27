using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform stick;

    [SerializeField] private float distance;

    public Vector2 Direction { get; private set; } = Vector2.zero;

    private Vector2 startPos;
    
    private void Awake()
    {
        HideStick();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = eventData.position;
        background.position = startPos;
        stick.position = startPos;
        
        ShowStick();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if (Vector2.Distance(startPos,eventData.position) > distance)
        {
            var direction = (eventData.position - startPos).normalized;
            stick.position = startPos + direction * distance;
            Direction = direction;
        }
        else
        {
            var direction = (eventData.position - startPos) / distance;
            stick.position = eventData.position;
            Direction = direction;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        HideStick();
        Direction = Vector2.zero;
    }

    private void ShowStick()
    {
        background.gameObject.SetActive(true);
        stick.gameObject.SetActive(true);
    }

    private void HideStick()
    {
        background.gameObject.SetActive(false);
        stick.gameObject.SetActive(false);
    }
}
