using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenApi : MonoBehaviour
{
[SerializeField]
private Canvas _canvas;

    private RectTransform _panelSafeArea;

    private Rect _currentSafeArea = new Rect();

    private ScreenOrientation _currentOrientation = ScreenOrientation.AutoRotation;
    // Start is called before the first frame update
    void Start()
    {
        _panelSafeArea = GetComponent<RectTransform>();
        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;

        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        if(_panelSafeArea==null)return;
        
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= _canvas.pixelRect.width;
        anchorMin.y /= _canvas.pixelRect.height;

        anchorMax.x /= _canvas.pixelRect.width;
        anchorMax.y /= _canvas.pixelRect.height;
        _panelSafeArea.anchorMin = anchorMin;
        _panelSafeArea.anchorMax = anchorMax;
        _currentOrientation = Screen.orientation;
        _currentSafeArea = Screen.safeArea;

    }

    // Update is called once per frame
    void Update()
    {
        if ((_currentOrientation != Screen.orientation) || (_currentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
