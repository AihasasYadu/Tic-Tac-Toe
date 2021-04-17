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

    protected override void Awake()
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
            if (canvasList[i].GetCanvasType != ct && canvasList[i].enabled)
            {
                StartCoroutine(DeactivateWithDelay(canvasList[i].gameObject));
            }
            else
            {
                canvasList[i].gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator DeactivateWithDelay(GameObject GO)
    {
        yield return new WaitForSeconds(1);
        GO.SetActive(false);
    }
}
