using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public string menuSceneName = "MainMenu";
    public string playSceneName = "Play";
    string currentSceneName;
    Canvas canvas;
    public static bool isGamePaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
        currentSceneName = menuSceneName;
    }

    private void Update()
    {
        if (currentSceneName != menuSceneName && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Unpause();
            } else
            {
                Pause();
            }
        }
    }

    public void MainMenu()
    {
        StartCoroutine(LoadSceneInBackground(menuSceneName));
    }

    public void Play()
    {
        StartCoroutine(LoadSceneInBackground(playSceneName));
    }

    public void Resume()
    {
        Unpause();
    }

    public void Pause()
    {
        canvas.enabled = true;
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void Pause(bool showPausemenu)
    {
        canvas.enabled = showPausemenu;
        isGamePaused = true;
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        canvas.enabled = false;
        isGamePaused = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneInBackground(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
            yield return null;
        currentSceneName = sceneName;
        Unpause();
    }



}
