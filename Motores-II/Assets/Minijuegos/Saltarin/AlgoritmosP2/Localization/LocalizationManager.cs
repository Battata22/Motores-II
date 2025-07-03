
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public TMP_FontAsset WingdingsFont;
    public Lang currentLang;
    public DataLocalization[] data;
    public event Action EventChangeLang;

    Dictionary<Lang, Dictionary<string, string>> _translate = new Dictionary<Lang, Dictionary<string, string>>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _translate = LanguageU.LoadTranslate(data);

            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeLang(Lang.SPA);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeLang(Lang.ENG);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeLang(Lang.CAT);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeLang(Lang.WIN);
    }

    public void ChangeLang(Lang lang)
    {
        if (lang == currentLang) return;

        currentLang = lang;

        if (EventChangeLang != null)
            EventChangeLang();
    }

    public string GetTranslate(string ID)
    {
        if (!_translate.ContainsKey(currentLang))
            return "No lang";

        if (!_translate[currentLang].ContainsKey(ID))
            return "No ID";

        return _translate[currentLang][ID];
    }
}

public enum Lang
{
    SPA, ENG, CAT, WIN
}