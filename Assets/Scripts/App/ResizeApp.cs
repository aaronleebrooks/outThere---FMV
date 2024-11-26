using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeApp : MonoBehaviour, IDragHandler
{
    private RectTransform parentRectTransform;

    private void Start()
    {
        // Get the parent RectTransform
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.delta;
        // Calculate the new size for the parent RectTransform
        Vector2 newSize = new Vector2(parentRectTransform.sizeDelta.x + delta.x, parentRectTransform.sizeDelta.y + delta.y);
        // Resize the parent RectTransform
        AppManager.Instance.ResizeApp(parentRectTransform, newSize);
    }
}
