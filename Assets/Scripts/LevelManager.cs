using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    ScoreKeeper scoreKeeper;
    LevelTracker levelTracker;

    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelTracker = FindObjectOfType<LevelTracker>();
    }

    public void LoadGame() {
      scoreKeeper.ResetScore();
      SceneManager.LoadScene("Level 1");
    }

    public void LoadMainMenu() {
      SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver() {
      StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void LoadLevelComplete() {
      StartCoroutine(WaitAndLoad("LevelComplete", sceneLoadDelay));
    }

    public void LoadFinalLevelComplete() {
      StartCoroutine(WaitAndLoad("FinalLevelComplete", sceneLoadDelay));
    }

    public void LoadLevel(int index) {
      scoreKeeper.ResetScore();
      levelTracker.UpdateLastLevel(index);
      StartCoroutine(WaitIndexLoad(index, sceneLoadDelay));
    }

    public void LoadNextLevel() {
      scoreKeeper.ResetScore();
      levelTracker.UpdateLastLevel(levelTracker.GetLastLevelIndex() + 1);
      StartCoroutine(WaitIndexLoad(levelTracker.GetLastLevelIndex(), sceneLoadDelay));
    }

    public void LoadLastLevelPlayed() {
      scoreKeeper.ResetScore();
      StartCoroutine(WaitIndexLoad(levelTracker.GetLastLevelIndex(), sceneLoadDelay));
    }

    public void LoadLevelSelect() {
      SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame() {
      Debug.Log("Quitting game...");
      Application.Quit(); //if using a WEB.GL build this will not work, and you might want to do extra things before exiting on mobile
    }

    IEnumerator WaitAndLoad(string sceneName, float delayTime) {
      yield return new WaitForSeconds(delayTime);
      SceneManager.LoadScene(sceneName);
    }

    IEnumerator WaitIndexLoad(int sceneIndex, float delayTime) {
      yield return new WaitForSeconds(delayTime);
      SceneManager.LoadScene(sceneIndex);
    }
}
