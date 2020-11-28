using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSystem : MonoBehaviour
{
    public void MainScene()
    {
        // TODO: main scene
    }

    public void PlayScene()
    {
        StartCoroutine(nameof(Load), "PlayScene");
    }

    public void GameOverScene()
    {
        Time.timeScale = 0f;
        StartCoroutine(nameof(Load), "Game Over");
    }

    private IEnumerator Load(string name)
    {
        yield return new WaitForSecondsRealtime(.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }
}