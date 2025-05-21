using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OLordGiveAd : MonoBehaviour
{
    public void CallAd()
    {
        AdsManager.Instance.adButtonScript.ExecuteButton();
    }

    
}
