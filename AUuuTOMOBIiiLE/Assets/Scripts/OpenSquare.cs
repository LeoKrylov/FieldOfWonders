using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Linq;

public class OpenSquare : MonoBehaviour
{
    List<GameObject> rowSquare = new List<GameObject>();
    List<GameObject> rowSquareUP = new List<GameObject>();
    public List<string> winWords = new List<string>();
    public TMP_Text attempts;
    public TMP_Text score;
    public TMP_Text message;
    public SettingsOfGame gameData;
    public int attemptsLeft;
    
    char[] usedButtons = new char[28];
    int j = 0;
    public int openedSquares = 0;
    int myScore;
    public bool flagOfWin = false;

    private void Start()
    {
        attemptsLeft = gameData.AmountOfAttempt;
        attempts.text = attemptsLeft.ToString();
        myScore = 0;
        score.text = myScore.ToString();
    }
    
    void Update()
    {
        OpenSquares();
        
        if(attemptsLeft == 0|| openedSquares == rowSquare.Count)
        {
            for(int i = 0; i < usedButtons.Length; i++)
            {
                usedButtons[i] = ' ';
                j = 0;
            }
           
        }

    }
    private void OpenSquares()
    {

        var allKeys = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>();
        foreach (var key in allKeys)
        {
            if (Input.GetKeyDown(key) && !Input.GetMouseButtonDown(0))
            {
                char button = key.ToString().ToLower()[0];
                bool check = false;
                bool checkUsedButton = false;
                rowSquare = GetComponent<Squares>().rowSquare;
                rowSquareUP = GetComponent<Squares>().rowSquareUP;

                for (int k = 0; k < usedButtons.Length; k++)
                {
                    if (button == usedButtons[k])
                    {
                        checkUsedButton = true;

                    }
                }
               
                for (int i = 0; i < rowSquare.Count; i++)
                {

                    if (button == rowSquare[i].GetComponentInChildren<TMP_Text>().text[0])
                    {

                        rowSquareUP[i].SetActive(false);
                        if (checkUsedButton == false)
                            openedSquares++;
                        usedButtons[j] = button;
                        j++;
                        Debug.Log(openedSquares);
                        check = true;
                    }
                }

                if (check == false)
                {
                    if (checkUsedButton == false)
                    {
                        attemptsLeft--;
                        attempts.text = attemptsLeft.ToString();
                        usedButtons[j] = button;
                        j++;
                    }
                }
                ClearALL();
            }
            
        }
    }

    private void ClearALL()
    {
        if (attemptsLeft == 0)
        {
            winWords.Clear();
            GetComponent<GetWord>().necessaryWords = GetComponent<GetWord>().FindCertainWords(GetComponent<GetWord>().allWords, gameData.MinWordLength);
            attemptsLeft = gameData.AmountOfAttempt;
            attempts.text = attemptsLeft.ToString();
            myScore = 0;
            score.text = myScore.ToString();
            GetComponent<GetWord>().amountOfWinWords = 0;
            openedSquares = 0;
            message.text = "You have lost!";
            ClearField();
        }
        else if (openedSquares == rowSquare.Count)
        {
            winWords.Add(GetComponent<GetWord>().theWord);
            if (GetComponent<GetWord>().necessaryWords.Count == 0)
            {
                winWords.Clear();

                score.text = myScore.ToString();
                message.text = "You just won this game! Congratulations! Your score is " + myScore;
                flagOfWin = true;
                myScore = 0;
                score.text = myScore.ToString();
                attemptsLeft = gameData.AmountOfAttempt;
                attempts.text = attemptsLeft.ToString();
                GetComponent<GetWord>().amountOfWinWords = 0;
            }
            else
            {
                myScore += attemptsLeft;
                score.text = myScore.ToString();
                attemptsLeft = gameData.AmountOfAttempt;
                attempts.text = attemptsLeft.ToString();
                GetComponent<GetWord>().amountOfWinWords++;
                int amountOfWinWords = GetComponent<GetWord>().amountOfWinWords;
                openedSquares = 0;
                message.text = "You have won! " + amountOfWinWords + " word/s!";
            }
            ClearField();
        }
    }

    private void ClearField()
    {
        foreach (var obj in rowSquare)
        {
            Destroy(obj);
        }
        foreach (var obj in rowSquareUP)
        {
            Destroy(obj);
        }
        GetComponent<Squares>().rowSquare.Clear();
        GetComponent<Squares>().rowSquareUP.Clear();
    }

}
