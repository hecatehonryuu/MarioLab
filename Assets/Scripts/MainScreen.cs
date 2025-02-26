using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScreen : MonoBehaviour
{
    public IntVariable gameScore;
    public GameObject highScoreText;
    public AudioSource resetSound;

    void Start()
    {
        highScoreText.GetComponent<TextMeshProUGUI>().text = "TOP-" + gameScore.previousHighestValue.ToString("D6");
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("LoadScreen", LoadSceneMode.Single);
    }

    public void ResetHighScore()
    {
        gameScore.previousHighestValue = 0;
        highScoreText.GetComponent<TextMeshProUGUI>().text = "TOP-" + gameScore.previousHighestValue.ToString("D6");
        resetSound.Play();
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
}