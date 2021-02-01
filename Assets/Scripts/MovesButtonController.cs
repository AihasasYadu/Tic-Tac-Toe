using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovesButtonController : MonoBehaviour
{
    private Turn playerMove;
    public Turn GetMove { get { return playerMove; } }
    private Turn currTurn;
    [SerializeField] private TextMeshProUGUI buttonText;
    private Button moveButton;

    private void Awake()
    {
        EventManager.ChangeTurnEvent += ChangeTurn;
    }

    private void Start()
    {
        playerMove = Turn.None;
        moveButton = gameObject.GetComponent<Button>();
        moveButton.onClick.AddListener(MakeMove);
    }

    private void MakeMove()
    {
        buttonText.SetText(currTurn.ToString());
        playerMove = currTurn;
        Debug.Log("Player : " + playerMove.ToString());
        moveButton.onClick.RemoveListener(MakeMove);
        EventManager.Instance.MoveEvent();
        EventManager.ChangeTurnEvent -= ChangeTurn;
    }

    private void ChangeTurn(Turn t)
    {
        currTurn = t;
    }
}
