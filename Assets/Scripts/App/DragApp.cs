using UnityEngine;
using UnityEngine.EventSystems;

public class DragApp : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform parentPanel;
    private Vector2 offset;

    private void Start()
    {
        // Get the parent RectTransform explicitly
        parentPanel = transform.parent.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Calculate the offset between the cursor and the drag handle
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentPanel,
            eventData.position,
            eventData.pressEventCamera,
            out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)parentPanel.parent,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint);

        // Adjust the position by the offset
        Vector2 newPosition = localPoint - offset;
        parentPanel.anchoredPosition = newPosition;
    }
}
