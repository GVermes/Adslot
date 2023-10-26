using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    int score;
    TextMeshProUGUI voteText;

    void Start()
    {
        score = 0;
        voteText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        voteText.text = score.ToString();
    }

    public void Add()
    {
        score++;
    }

    public void Subtract()
    {
        score--;
    }

}
