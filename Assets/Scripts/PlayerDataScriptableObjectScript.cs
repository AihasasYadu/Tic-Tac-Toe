using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Current Player", menuName = "PlayerData")]
public class PlayerDataScriptableObjectScript : ScriptableObject
{
    public Player data;
}
