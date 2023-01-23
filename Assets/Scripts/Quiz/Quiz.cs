using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{ 
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] private List<QuestionSO> _questions = new List<QuestionSO>();
    QuestionSO _questionSo;
    
    [Header("Answers")]
    [SerializeField] GameObject[] questionAnswersButton;
    int correctAnswerIndex;
    private bool hasAnswered = true;
    
    [Header("Button Color")]
    [SerializeField] Sprite correctAnswer;
    [SerializeField] Sprite defaultAnswerSprite;
    Button _button;
    TextMeshProUGUI buttonText;
    
    [Header("Timer")]
    [SerializeField] Image TimerImage;
    Image buttonImage;
    Timer _timer;

    [Header("Score")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    Score _scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    
    public bool isComplete;
    
    
    void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<Score>();
        progressBar.maxValue = _questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        TimerImage.fillAmount = _timer.fillFraction;
        if (_timer.loadNextQuestion)
        {
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            
            hasAnswered = false;
            GetNextQuestion();
            _timer.loadNextQuestion = false;
        }
        else if (!hasAnswered && !_timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = _questionSo.GetQuestion();

        for (int i = 0; i < questionAnswersButton.Length; i++)
        {
             buttonText = questionAnswersButton[i].GetComponentInChildren<TextMeshProUGUI>();
             buttonText.text = _questionSo.GetAnswer(i);
        }
    }

    void GetNextQuestion()
    {
        if (_questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestions();
            DisplayQuestion();
            progressBar.value++;
            _scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestions()
    {
        int index = Random.Range(0, _questions.Count);
        _questionSo = _questions[index];
        if (_questions.Contains(_questionSo))
        {
            _questions.Remove(_questionSo);
        }
    }
    public void AnswerSelected(int index)
    {
        hasAnswered = true;
        DisplayAnswer(index);
        SetButtonState(false);
        _timer.CancelTimer();
        scoreText.text = "Score: " + _scoreKeeper.CalculateScore();
        
    }

    void DisplayAnswer(int index)
    {
        if (index == _questionSo.GetCorrectAnswer())
        {
            questionText.text = "Doğru";
            buttonImage = questionAnswersButton[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswer;
            _scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = _questionSo.GetCorrectAnswer();
            string correctText = _questionSo.GetAnswer(correctAnswerIndex);
            questionText.text = "Üzgünüm, Doğru cevap ;\n " + correctText;
            buttonImage = questionAnswersButton[_questionSo.GetCorrectAnswer()].GetComponent<Image>();
            buttonImage.sprite = correctAnswer;
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < questionAnswersButton.Length; i++)
        {
            _button = questionAnswersButton[i].GetComponent<Button>();
            _button.interactable = state;
        }
    }

    void SetDefaultButtonSprite()
    {
        for (int i = 0; i < questionAnswersButton.Length; i++)
        {
            buttonImage = questionAnswersButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
