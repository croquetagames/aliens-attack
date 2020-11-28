using UnityEngine;
using UnityEngine.UI;
using Variables;

public class ScoreBoard : MonoBehaviour
{
    public Text scoreText;
    public Text bestText;

    public IntVariable currentScore;

    private void Start()
    {
        scoreText.text = currentScore.Value.ToString();

        var bestScore = BestScore();
        bestText.text = bestScore.ToString();
        PlayerPrefs.SetInt("BestScore", bestScore);
    }

    private int BestScore()
    {
        var bestScore = PlayerPrefs.GetInt("BestScore");
        return currentScore.Value > bestScore ? currentScore.Value : bestScore;
    }
}