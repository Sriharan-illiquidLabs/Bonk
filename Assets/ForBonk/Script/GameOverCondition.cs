using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverCondition : MonoBehaviour
{
    public static GameOverCondition Instance;

    [SerializeField] private GameObject gameoverScreen;
    [SerializeField] private GameObject timeObject;
    [SerializeField] private TMP_Text timeText;

    int timeRemaining = 60;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Hide Mouse Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameoverScreen.SetActive(false);
        timeObject.SetActive(true);

        timeText.text = timeRemaining.ToString();

        InvokeRepeating(nameof(UpdateTimer), 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimer()
    {
        timeRemaining -= 1;
        timeText.text = timeRemaining.ToString();

        if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        // Show Mouse Cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        timeObject.SetActive(false);

        gameoverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
