using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text scoreText;
    public Text questionText;
    public Text timeReminingText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionPanel;
    public GameObject roundOverPanel;


    DataController datacontroller;
    roundData currentRoundData;
    QuestionData[] questionPool;
    List<GameObject> answerButtonGameObjects = new List<GameObject>();
    bool isRoundActive;
    float timeReamaning;
    int questionIndex;
    int playerScore;
	// Use this for initialization
	void Start () {
        datacontroller = FindObjectOfType<DataController>();
        currentRoundData = datacontroller.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        timeReamaning = currentRoundData.timeLimit;
        updateTimeRemining();
        playerScore = 0;
        questionIndex = 0;

        showQuestion();
        isRoundActive = true;

	}
	
    void showQuestion()
    {
        removeAnswerButtons();
        QuestionData questionData = questionPool[questionIndex];
        questionText.text = questionData.questionText;

        for (int i = 0; i < questionData.answers.Length; i++)
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObject.transform.SetParent(answerButtonParent);
            answerButtonGameObjects.Add(answerButtonGameObject);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]); 
        }
    }

    void removeAnswerButtons()
    {
        while (answerButtonGameObjects.Count>0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }

    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsForRightAnswer;
            scoreText.text = "Score: " + playerScore.ToString();
        }
        if (questionPool.Length>questionIndex+1)
        {
            questionIndex++;
            showQuestion();
        }
        else
        {
            EndRound();
        }
    }
    public void EndRound()
    {
        isRoundActive = false;
        questionPanel.SetActive(false);
        roundOverPanel.SetActive(true);
    }
    public void returnToMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
    void updateTimeRemining()
    {
        timeReminingText.text = "Time: " + Mathf.Round(timeReamaning).ToString();
    }
	// Update is called once per frame
	void Update () {
        if (isRoundActive)
        {
            timeReamaning -= Time.deltaTime;
            updateTimeRemining();
            if (timeReamaning<=0f)
            {
                EndRound();
            }
        }
	}
}
