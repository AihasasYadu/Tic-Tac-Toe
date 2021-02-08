using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI input;
    [SerializeField] private PlayerDataScriptableObjectScript playerData;
    [SerializeField] private Button enterButton;
    private PlayerList saveData;
    void Start()
    {
        ClearData();
        saveData = JSonManager.Instance.GetSaveData;
        Debug.Log(saveData.player[0].name);
        enterButton.onClick.AddListener(CheckPlayerName);
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
