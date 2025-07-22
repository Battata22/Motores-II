using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOrientator : MonoBehaviour
{
    [SerializeField] ScreenOrientation orientation;

    private void Awake()
    {
        //a
        Screen.orientation = orientation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortraitUpsideDown = false;

        QualitySettings.vSyncCount = 1;
    }
}
