using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaSetter : MonoBehaviour
{
    public Canvas canvas;
    private RectTransform panelSafeArea;

    private Rect currentSafeArea = new Rect();
    private ScreenOrientation currentOrrientation = ScreenOrientation.AutoRotation;
    
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();

        currentOrrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if (panelSafeArea == null)
            return;

        Rect safeArea = Screen.safeArea;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;
        
        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;

        currentOrrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

    }
    
    void Update()
    {
        if ((currentOrrientation != Screen.orientation) || (currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
