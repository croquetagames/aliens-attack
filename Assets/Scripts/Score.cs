using UnityEngine;
using UnityEngine.UI;
using Variables;

public class Score : MonoBehaviour
{
    public IntVariable score;

    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void Start()
    {
        //reset score
        score.Value = 0;
    }

    public void AddScore()
    {
        score.Value++;
        _text.text = score.Value.ToString();
    }
}