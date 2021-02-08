using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonController : MonoBehaviour
{
    [SerializeField] private RectTransform currentPanel;
    [SerializeField] private int x;
    [SerializeField] private int y;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(MoveToInitial);
    }

    private void MoveToInitial()
    {
        iTween.MoveTo(currentPanel.gameObject, iTween.Hash("x", x,
            "y", y,
            "islocal", true,
            "easetype", iTween.EaseType.easeInOutSine));
    }
}
