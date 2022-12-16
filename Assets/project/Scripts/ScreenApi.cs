using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenApi : MonoBehaviour
{
[SerializeField]
private Canvas _canvas;

    private RectTransform panelSafeArea;

    private Rect currentSafeArea = new Rect();

    private ScreenOrientation currentOrientation = ScreenOrientation.AutoRotation;
    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if(panelSafeArea==null)return;
        
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;
        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
        currentOrientation = Screen.orientation;
        currentSafeArea = Screen.safeArea;

    }

    // Update is called once per frame
    void Update()
    {
        if ((currentOrientation != Screen.orientation) || (currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
