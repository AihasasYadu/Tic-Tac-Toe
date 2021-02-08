using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Theme", menuName = "ThemeData")]
public class UIDictScriptable : ScriptableObject
{
    [SerializeField] private List<Themes> keys;
    [SerializeField] private List<ThemeData> values;

    public List<Themes> Keys { get => keys; set => keys = value; }
    public List<ThemeData> Values { get => values; set => values = value; }
}

[System.Serializable]
public class ThemeData
{
    public Sprite bgImg;
    public Color movesColor;
}
