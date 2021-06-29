using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Script : MonoBehaviour
{
    [SerializeField]
    private bool isPiece;

    [SerializeField]
    private int index = 0;

    private bool empty = true;

    [SerializeField]
    private Sprite [] piecesMoon;

    [SerializeField]
    private Sprite[] piecesHouse;

    [SerializeField]
    private Sprite[] piecesGymnastic;

    private void Start()
    {
        empty = true;
    }

    public void SetSprite(int indexScenario)
    {
        switch (indexScenario)
        {
            case 13:
                gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoon[index];
                break;
            case 3:
                gameObject.GetComponent<SpriteRenderer>().sprite = piecesHouse[index];
                break;
            case 8:
                gameObject.GetComponent<SpriteRenderer>().sprite = piecesGymnastic[index];
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoon[index];
                break;
        }
    }


    public bool IsPiece
    {
        get { return isPiece; }
        set { isPiece = value; }
    }

    public bool Empty
    {
        get { return empty; }
        set { empty = value; }
    }

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}
