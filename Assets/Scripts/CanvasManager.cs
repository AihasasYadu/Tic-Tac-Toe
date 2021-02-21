using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoSingletonGeneric<CanvasManager>
{
    [SerializeField] private List<CanvasController> canvasList;

    public CanvasTypes ChangeActiveCanvas { set { 
                                           CanvasTypes temp = value;
                                           ChangeCanvasTo(temp);
                                          } }

    protected void Awake()
    {
        base.Awake();
        for (int i = 0; i < canvasList.Count; i++)
        {
            if(canvasList[i].GetCanvasType != CanvasTypes.Main)
            {
                canvasList[i].gameObject.SetActive(false);
            }
        }
    }

    private void ChangeCanvasTo(CanvasTypes ct)
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            if (canvasList[i].GetCanvasType != ct)
            {
                canvasList[i].gameObject.SetActive(false);
            }
            else
            {
                canvasList[i].gameObject.SetActive(true);
            }
        }
    }
}
