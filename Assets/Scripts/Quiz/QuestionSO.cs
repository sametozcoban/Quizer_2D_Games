using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tester Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,10)] //Satır ve sütun sayısını kontrol etmemizi sağlar
    [SerializeField] string question = "Enter your quesiton";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
    {
        return question;
    }
    
    public string GetAnswer(int index)
    {
        return answers[index];
    }
    
    public int GetCorrectAnswer()
    {
        return correctAnswerIndex;
    }

    
}
