using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private Button[] buttonSetup = new Button[9];
    private Button[,] movesMap = new Button[3, 3];
    private Turn currTurn;
    private void Start()
    {
        CopyGridSetup();
        currTurn = Turn.X;
        EventManager.Instance.ChangeTurn(currTurn);
        EventManager.MoveMade += ChangeTurn;
    }

    private void CopyGridSetup()
    {
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                movesMap[i, j] = buttonSetup[k];
                k++;
            }
        }
    }

    private void ChangeTurn()
    {
        if (!CheckForWin())
        {
            if(currTurn.Equals(Turn.X))
            {
                currTurn = Turn.O;
            }
            else if(currTurn.Equals(Turn.O))
            {
                currTurn = Turn.X;
            }
            EventManager.Instance.ChangeTurn(currTurn);
        }
        Debug.Log("Current : " + currTurn.ToString());
    }

    private bool CheckForWin()
    {
        int moveCounter = 0, i, j;

        //Horizontal Check
        for(i = 0; i < 3; i++)
        {
            for(j = 0; j < 3; j++)
            {
                if(movesMap[i, j].GetComponent<MovesButtonController>().GetMove.Equals(currTurn))
                {
                    moveCounter++;
                }
            }
            if(CheckWin(moveCounter))
            {
                return true;
            }
            moveCounter = 0;
        }

        //Vertical Check
        for (i = 0; i < 3; i++)
        {
            for (j = 0; j < 3; j++)
            {
                if (movesMap[j, i].GetComponent<MovesButtonController>().GetMove.Equals(currTurn))
                {
                    moveCounter++;
                }
            }
            if (CheckWin(moveCounter))
            {
                return true;
            }
            moveCounter = 0;
        }
        

        //Left Diagonal
        for (i = 0; i < 3; i++)
        {
            if (movesMap[i, i].GetComponent<MovesButtonController>().GetMove.Equals(currTurn))
            {
                moveCounter++;
            }
        }
        if (CheckWin(moveCounter))
        {
            return true;
        }
        moveCounter = 0;

        //Right Diagonal
        for (i = 0, j = 2; i < 3; i++, j--)
        {
            if (movesMap[i, j].GetComponent<MovesButtonController>().GetMove.Equals(currTurn))
            {
                moveCounter++;
            }
        }
        if (CheckWin(moveCounter))
        {
            return true;
        }

        return false;
    }

    private bool CheckWin(int moveCounter)
    {
        if (moveCounter == 3)
        {
            Debug.Log("Player " + currTurn.ToString() + " Wins");
            DisableButtonsInteractable();
            return true;
        }
        return false;
    }

    private void DisableButtonsInteractable()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                movesMap[i, j].GetComponent<Button>().interactable = false;
            }
        }
    }
}
