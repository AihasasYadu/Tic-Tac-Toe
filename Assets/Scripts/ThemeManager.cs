using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeManager : MonoSingletonGeneric<ThemeManager>
{
    [SerializeField] private UIDictScriptable themeData;
    private int currentThemeIndex = 0;
    public Sprite GetBGImage { get { return themeData.Values[currentThemeIndex].bgImg; } }
    public Color GetTextColor { get { return themeData.Values[currentThemeIndex].movesColor; } }
    public int SetCurrentThemeIndex { set { currentThemeIndex = value; } }
}
