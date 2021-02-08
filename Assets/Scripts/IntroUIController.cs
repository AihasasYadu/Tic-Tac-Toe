using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroUIController : MonoBehaviour
{
    [SerializeField] private RectTransform namePanel;
    [SerializeField] private RectTransform menuPanel;
    [SerializeField] private RectTransform highScorePanel;
    [SerializeField] private RectTransform themePanel;
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private PlayerDataScriptableObjectScript playerData;
    [SerializeField] private Button enterButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Button highScoreButton;
    [SerializeField] private Button changeThemeButton;
    [SerializeField] private Image bgIMG;
    [SerializeField] private List<TextMeshProUGUI> highScoreNames = new List<TextMeshProUGUI>(MAX_SIZE);
    [SerializeField] private List<TextMeshProUGUI> highScorePoints = new List<TextMeshProUGUI>(MAX_SIZE);
    [SerializeField] private List<Button> themeButtons;
    private const int MAX_SIZE = 3;
    private PlayerList saveData;
    private void Start()
    {
        bgIMG.sprite = ThemeManager.Instance.GetBGImage;
        ClearData();
        saveData = JSonManager.Instance.GetSaveData;
        Debug.Log(saveData.player[0].name);
        AddListenersToButtons();
    }

    private void AddListenersToButtons()
    {
        enterButton.onClick.AddListener(CheckPlayerName);
        startButton.onClick.AddListener(MoveNamePanel);
        highScoreButton.onClick.AddListener(DisplayHighScore);
        changeThemeButton.onClick.AddListener(MoveThemePanel);
        for(int i = 0; i < themeButtons.Count; i++)
        {
            int local = i;
            themeButtons[i].onClick.AddListener(() => ChangeTheme(local));
        }
    }

    private void ChangeTheme(int index)
    {
        ThemeManager.Instance.SetCurrentThemeIndex = index;
        bgIMG.sprite = ThemeManager.Instance.GetBGImage;
    }

    private void MoveThemePanel()
    {
        iTween.MoveTo(themePanel.gameObject, iTween.Hash("x", 0,
            "y", 0,
            "islocal", true,
            "easetype", iTween.EaseType.easeInOutSine));
    }

    private void MoveNamePanel()
    {
        iTween.MoveTo(namePanel.gameObject, iTween.Hash("x", 0, 
            "y", 0, 
            "islocal", true,
            "easetype", iTween.EaseType.easeInOutSine));
    }

    private void DisplayHighScore()
    {
        iTween.MoveTo(highScorePanel.gameObject, iTween.Hash("x", 0, 
            "y", 0,
            "islocal", true,
            "easetype", iTween.EaseType.easeInOutSine));

        for(int i = 0; i < MAX_SIZE; i++)
        {
            highScoreNames[i].SetText(saveData.player[i].name);
            highScorePoints[i].SetText(saveData.player[i].score.ToString());
        }
    }

    private void ClearData()
    {
        playerData.data.name = "";
        playerData.data.score = 0;
    }

    private void CheckPlayerName()
    {
        for (int i = 0; i < 3; i++)
        {
            if (saveData.player[i].name.Equals(input.text.ToString()))
            {
                Debug.Log("Name Already Exists");
                input.SetText("");
                return;
            }
        }
        playerData.data.name = input.text.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
