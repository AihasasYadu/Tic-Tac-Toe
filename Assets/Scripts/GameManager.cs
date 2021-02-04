using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingletonGeneric<GameManager>
{
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button movesPrefab;
    [SerializeField] private GridLayoutGroup layoutPanel;
    [SerializeField] private TextMeshProUGUI currentTurnIndicator;
    [SerializeField] private PlayerDataScriptableObjectScript playerData;
    private Button[,] movesMap = new Button[3, 3];
    private Turn currTurn;
    private PlayerList saveData;
    private void Start()
    {
        GridSetup();
        currTurn = Turn.X;
        currentTurnIndicator.SetText(currTurn.ToString());
        saveData = JSonManager.Instance.GetSaveData;
        UpdateScoreText();
        EventManager.Instance.ChangeTurnEvent(currTurn);
        EventManager.MoveMade += ChangeTurn;
    }

    private void UpdateScoreText()
    {
        scoreText.SetText(playerData.data.score.ToString());
        highScoreText.SetText(saveData.player[0].score.ToString());
    }

    private void CheckPlayerData()
    {
        SortSaveData();
        for (int i = 0; i < 3; i++)
        {
            if (saveData.player[i].score < playerData.data.score)
            {
                ShiftSaveData(i);
                saveData.player[i] = playerData.data;
                JSonManager.Instance.SetSaveData = saveData;
                return;
            }
        }
    }

    private void SortSaveData()
    {
        int n = saveData.player.Length;
        for (int i = 0; i < 3 - 1; i++)
        {
            for (int j = 0; j < 3 - i - 1; j++)
            {
                if (saveData.player[j].score < saveData.player[j + 1].score)
                {
                    Player temp = saveData.player[j];
                    saveData.player[j] = saveData.player[j + 1];
                    saveData.player[j + 1] = temp;
                }
            }
        }
    }

    private void ShiftSaveData(int n)
    {
        for (int j = 2; j > n - 1; j--)
        {
            saveData.player[j] = saveData.player[j - 1];
        }
    }

    private void GridSetup()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Button instancePref = Instantiate(movesPrefab);
                instancePref.transform.parent = layoutPanel.transform;
                instancePref.transform.localScale = new Vector3(1, 1, 0);
                movesMap[i, j] = instancePref;
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
            EventManager.Instance.ChangeTurnEvent(currTurn);
        }
        Debug.Log("Current : " + currTurn.ToString());
        currentTurnIndicator.SetText(currTurn.ToString());
    }

    private bool CheckForWin()
    {
        int moveCounter = 0, i, j, nullMove = 0;

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
        moveCounter = 0;

        //Draw Check
        for (i = 0; i < 3; i++)
        {
            for (j = 0; j < 3; j++)
            {
                if(movesMap[i,j].GetComponent<MovesButtonController>().GetMove.Equals(Turn.None))
                {
                    nullMove++;
                }
            }
        }

        if(nullMove != 0)
        {
            return false;
        }
        else
        {
            Debug.Log("It's A Draw");
            CheckAndClearData();
            StartCoroutine(ReloadScene());
            return true;
        }
    }

    private bool CheckWin(int moveCounter)
    {
        if (moveCounter == 3)
        {
            if(currTurn.Equals(Turn.O))
            {
                CheckAndClearData();
                Debug.Log("You Lose");
                SceneManager.LoadScene(0);
            }
            else if(currTurn.Equals(Turn.X))
            {
                playerData.data.score++;
            }
            Debug.Log("Player " + currTurn.ToString() + " Wins");
            StartCoroutine(ReloadScene());
            return true;
        }
        return false;
    }

    private void CheckAndClearData()
    {
        CheckPlayerData();
        playerData.data.name = "";
        playerData.data.score = 0;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        EventManager.MoveMade -= ChangeTurn;
    }
}
