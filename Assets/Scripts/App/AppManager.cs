using UnityEngine;
using System.Collections.Generic;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    private List<RectTransform> activeApps;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        activeApps = new List<RectTransform>();
    }

    public void RegisterApp(RectTransform app)
    {
        if (!activeApps.Contains(app))
        {
            activeApps.Add(app);
        }
    }

    public void MinimizeApp(RectTransform app)
    {
        app.gameObject.SetActive(false);
    }

    public void MaximizeApp(RectTransform app)
    {
        app.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
        app.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
        app.anchoredPosition = Vector2.zero;
        app.gameObject.SetActive(true);
    }

    public void DragApp(RectTransform app, Vector2 dragPosition)
    {
        app.anchoredPosition = dragPosition;
    }

    public void ResizeApp(RectTransform app, Vector2 newSize)
    {
        app.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);
        app.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);
    }

    public void PinAppToTop(RectTransform app)
    {
        app.SetAsLastSibling(); // Bring the app to the front
        // TODO: Implement logic to keep the app on top
        // DesktopManager.Instance.SetPinnedApp(app);
    }

    public void UnregisterApp(RectTransform app)
    {
        if (activeApps.Contains(app))
        {
            activeApps.Remove(app);
        }
    }

    // public void OnMinimizeButtonClicked()
    // {
    //     AppManager.Instance.MinimizeApp(appPanel);
    // }

    // public void OnMaximizeButtonClicked()
    // {
    //     AppManager.Instance.MaximizeApp(appPanel);
    // }

    // public void OnPinToTopButtonClicked()
    // {
    //     AppMaznager.Instance.PinAppToTop(appPanel);
    // }

}
