using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Squares : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text textField;
  
   
    [SerializeField] private GameObject Square;
    [SerializeField] private GameObject SquareUP;

    public List<GameObject> rowSquare = new List<GameObject>();
    public List<GameObject> rowSquareUP = new List<GameObject>();

    int letterCount = 0;

    public void OnCreateSquare(string word)
    {
        letterCount = GetComponent<GetWord>().theWord.Length;
        
        char[] wordArr = new char[letterCount];
        FillArray(wordArr, word);

        
        Transform textPos = textField.transform;
        int startpositionX = -125 * (letterCount/2);
        for (int i = 0; i < letterCount; i++)
        {
            GameObject square = Instantiate(Square, new Vector3(textPos.position.x + startpositionX, textPos.position.y, textPos.position.z), textPos.rotation);
            GameObject squareUP = Instantiate(SquareUP, new Vector3(textPos.position.x + startpositionX, textPos.position.y, textPos.position.z), textPos.rotation);
            square.GetComponentInChildren<TMP_Text>().text = wordArr[i].ToString();
            
            rowSquare.Add(square);
            rowSquareUP.Add(squareUP);
            startpositionX += 130;
        }
    }

    void FillArray(char[] wordArr, string theWord)
    {
        for(int i = 0; i < theWord.Length; i++)
        {
            wordArr[i] = theWord[i];
        }
    }

    void Update()
    {
        
    }

}
