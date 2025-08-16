using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TextMeshProUGUI bestScoreValue;

    // Start is called before the first frame update
    void Start()
    {
        var loadedData = DataPersistanceManager.Instance;

        if (!string.IsNullOrEmpty(loadedData.playerName))
        {
            bestScoreValue.text = $"{loadedData.playerName}: {loadedData.highScore}";
        }
        else
        {
            bestScoreValue.text = "0";
        }
    }

    public void NewGame()
    {
        DataPersistanceManager.Instance.playerName = usernameInputField.text;

        SceneManager.LoadScene(2);
    }
}
