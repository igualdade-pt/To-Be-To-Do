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
    private Sprite[] piecesMoonB;

    [SerializeField]
    private Sprite[] piecesMoonE;

    [SerializeField]
    private Sprite[] piecesHouseB;

    [SerializeField]
    private Sprite[] piecesHouseE;

    [SerializeField]
    private Sprite[] piecesGymnasticB;

    [SerializeField]
    private Sprite[] piecesGymnasticE;

    private void Start()
    {
        empty = true;
    }

    public void SetSprite(int indexScenario, int indexDificulty)
    {
        switch (indexScenario)
        {
            case 13:
                if (indexDificulty == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoonB[index];
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoonE[index];
                }

                break;
            case 3:
                if (indexDificulty == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesHouseB[index];
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesHouseE[index];
                }
                break;
            case 8:
                if (indexDificulty == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesGymnasticB[index];
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesGymnasticE[index];
                }
                break;
            default:
                if (indexDificulty == 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoonB[index];
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = piecesMoonE[index];
                }
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
