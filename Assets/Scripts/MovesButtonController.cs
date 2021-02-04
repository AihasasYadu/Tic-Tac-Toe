using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovesButtonController : MonoBehaviour
{
    private Turn playerMove;
    public Turn GetMove { get { return playerMove; } }
    private Turn currentTurn;
    [SerializeField] private TextMeshProUGUI  buttonText;
    private Button moveButton;

    private void Awake()
    {
        EventManager.ChangeTurnTo += ChangeTurn;
    }

    private void Start()
    {
        playerMove = Turn.None;
        moveButton = gameObject.GetComponent<Button>();
        moveButton.onClick.AddListener(MakeMove);
    }

    private void MakeMove()
    {
        buttonText.SetText(currentTurn.ToString());
        playerMove = currentTurn;
        moveButton.onClick.RemoveListener(MakeMove);
        EventManager.Instance.MoveEvent();
    }

    private void ChangeTurn(Turn t)
    {
        currentTurn = t;
    }

    private void OnDestroy()
    {
        EventManager.ChangeTurnTo -= ChangeTurn;
    }
}
