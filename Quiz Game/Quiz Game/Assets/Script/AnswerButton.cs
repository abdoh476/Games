using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AnswerButton : MonoBehaviour {
    public Text answerText;
    private AnswerData answerData;
    GameController gamecontroller;
    // Use this for initialization
    void Start () {
        gamecontroller = FindObjectOfType<GameController>();
	}
    public void Setup(AnswerData data)
    {
        answerData = data;
        answerText.text = answerData.answerText;
    }
    public void HandleClick()
    {
        gamecontroller.AnswerButtonClicked(answerData.isCorrect);
    }
}
