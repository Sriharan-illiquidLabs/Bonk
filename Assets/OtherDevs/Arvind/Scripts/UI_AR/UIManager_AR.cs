using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_AR : MonoBehaviour
{
    [SerializeField]
    private bool isGameMenu = false;

    [Header("Pause Items")]
    [SerializeField]
    private Canvas pauseCanvas;    

    [SerializeField]
    private string AnimateInName;

    [SerializeField]
    private string AnimateOutName;

    private Animator pauseCanvasAnimator;
    private bool isPaused = false;
  

    public void SceneChanger(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(false);

            if (pauseCanvas.GetComponent<Animator>() != null)
            {
                pauseCanvasAnimator = pauseCanvas.GetComponent<Animator>();
            }
        }
        
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameMenu)
        {
            //Play
            if (isPaused == true)
            {
                Resume();
            }
            //Pause
            else
            {
                Pause();
            }
        }
    }
    //Resume
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        pauseCanvas?.gameObject.SetActive(true);
        pauseCanvasAnimator?.Play(AnimateInName);
        isPaused = true;
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseCanvasAnimator?.Play(AnimateOutName);

    }
    public void UnPause()
    {
        Time.timeScale = 1.0f;
        pauseCanvas?.gameObject.SetActive(false);
        isPaused = false;
    }
}
