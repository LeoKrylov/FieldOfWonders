using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GetWord : MonoBehaviour
{
    public List<string> allWords = new List<string>();
    public List<string> necessaryWords = new List<string>();

    List<GameObject> rowSquare = new List<GameObject>();

    public string theWord;
    public SettingsOfGame gameData;
    public TMP_Text attempts;
    
    int attemptsLeft;
    int openedSquares;
    public int amountOfWinWords = 0;
   

    // Start is called before the first frame update
    void Start()
    {
        string path = @"Assets\TextSource\alice30.txt";
        string[] textFile = File.ReadAllLines(path);
        attemptsLeft = GetComponent<OpenSquare>().attemptsLeft;

        FindWords(textFile, allWords, gameData.MinWordLength);
        
        necessaryWords = FindCertainWords(allWords, gameData.MinWordLength);
        Debug.Log(necessaryWords.Count);

        theWord = getRandomWord(necessaryWords);
        GetComponent<Squares>().OnCreateSquare(theWord);
    }

    // Update is called once per frame
    void Update()
    {
        rowSquare = GetComponent<Squares>().rowSquare;
        openedSquares = GetComponent<OpenSquare>().openedSquares;

        attemptsLeft = GetComponent<OpenSquare>().attemptsLeft;

        if (attemptsLeft == 0)
        {
            attemptsLeft = gameData.AmountOfAttempt;
            GetComponent<OpenSquare>().attemptsLeft = attemptsLeft;
            attempts.text = attemptsLeft.ToString();
            necessaryWords = FindCertainWords(allWords, gameData.MinWordLength);
            theWord = getRandomWord(necessaryWords);
            GetComponent<Squares>().OnCreateSquare(theWord);
            
        }
        if (openedSquares == rowSquare.Count)
        {
            if (GetComponent<OpenSquare>().flagOfWin)
            {
                GetComponent<OpenSquare>().flagOfWin = false;
                necessaryWords = FindCertainWords(allWords, gameData.MinWordLength);
            }
            theWord = getRandomWord(necessaryWords);
                GetComponent<Squares>().OnCreateSquare(theWord);
        }

    }

    private void FindWords(string[] textFile, List<string> words, int countOfLetters)
    {
        char apostroph = "'".ToCharArray()[0];
        for (int i = 0; i < textFile.Length; i++)
        {
            string[] line = textFile[i].Split(' ', '.', ',', ';', '!', '?', '\n', '(', ')', '"', apostroph, '`', ':', '-');
            if (line.Length != 0)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j].Length >= countOfLetters)
                        words.Add(line[j].ToLower());
                }
                    
            }
        }
    }

    public List<string> FindCertainWords(List<string> words,int countOfLetters)
    {
        List<string> certainWords = new List<string>();
        
        for(int i= 0; i < words.Count; i++)
        {
            if (words[i].Length >= countOfLetters)
            {
                    certainWords.Add(words[i]);
            }           
        }

        return certainWords;
    }

    private string getRandomWord(List<string> words)
    {
        
        string word = "";
        bool flagEqual = false;
        while(word == "")
        {
            if (words.Count > 0)
            {
                int number = Random.Range(0, words.Count);
                word = words[number];
                for (int k = 0; k < GetComponent<OpenSquare>().winWords.Count; k++)
                {
                    if (GetComponent<OpenSquare>().winWords[k] == word)
                    {
                        flagEqual = true;
                        words.RemoveAt(number);
                        word = "";
                        break;
                    }
                }
                if (flagEqual)
                {
                    flagEqual = false;
                }
                if (words.Count > 0)
                {
                    //words.RemoveAt(number);
                }
                else
                    break;
            }
            else
                break;
        }
        

        return word;
    }
}
