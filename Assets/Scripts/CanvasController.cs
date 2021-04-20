using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private CanvasTypes canvasType;
    public CanvasTypes GetCanvasType { get { return canvasType; } }
}
