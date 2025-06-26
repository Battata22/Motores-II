using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSafeArea : MonoBehaviour
{

    [SerializeField] Canvas canvas;
    [SerializeField] RectTransform safeAreaRect;

    [SerializeField] bool applySafeArea;

    void Start()
    {
        if(canvas != null) canvas = GetComponentInParent<Canvas>();
        if(safeAreaRect != null ) safeAreaRect = GetComponent<RectTransform>();

#if UNITY_ANDROID
        if (applySafeArea) AdjustRect();
#endif
    }

    [ContextMenu ("Adjust Safe Area")] 
    private void AdjustRect()
    {
        if (safeAreaRect == null) return;

        Rect lastSafeArea = Screen.safeArea;

        Vector2 minAnchor = lastSafeArea.position;
        Vector2 maxAnchor = minAnchor + lastSafeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        safeAreaRect.anchorMin = minAnchor;
        safeAreaRect.anchorMax = maxAnchor;
    }
}
