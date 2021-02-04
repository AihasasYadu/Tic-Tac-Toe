using System;
using UnityEngine;

public class EventManager : MonoSingletonGeneric<EventManager>
{
    public delegate void Executable();
    public static event Executable MoveMade;
    public static Action<Turn> ChangeTurnTo;

    public void MoveEvent()
    {
        MoveMade();
    }

    public void ChangeTurnEvent(Turn t)
    {
        ChangeTurnTo(t);
    }
}
