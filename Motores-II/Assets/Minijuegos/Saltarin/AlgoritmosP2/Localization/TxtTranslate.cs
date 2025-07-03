
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TxtTranslate : MonoBehaviour
{
    public string ID;

    TextMeshProUGUI _txt;

    TMP_FontAsset _defaultFont;

    private void Start()
    {
        _txt = GetComponent<TextMeshProUGUI>();

        _defaultFont = _txt.font;

        LocalizationManager.instance.EventChangeLang += Translate;
        Translate();
    }

    void Translate()
    {
        if (LocalizationManager.instance.currentLang == Lang.WIN)
        {
            _txt.font = LocalizationManager.instance.WingdingsFont;
        }
        else
        {
            _txt.font = _defaultFont;
        }

        _txt.text = LocalizationManager.instance.GetTranslate(ID);
    }
}