using System;
using UnityEngine;

public class EventManager : MonoSingletonGeneric<EventManager>
{
    public delegate void Executable();
    public static event Executable MoveMade;
    public static Action<Turn> ChangeTurnEvent;

    public void MoveEvent()
    {
        MoveMade?.Invoke();
    }

    public void ChangeTurn(Turn t)
    {
        ChangeTurnEvent?.Invoke(t);
    }
}
