using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    Score scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<Score>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "Başarı puanın " + 
                              scoreKeeper.CalculateScore() + "%";
    }

}
