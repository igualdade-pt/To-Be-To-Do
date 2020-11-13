using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("--- Game Properties ---")]
    [Space]

    [SerializeField]
    private int indexGameSelected = -1;

    // Memory
    [Header("--- Memory Game ---")]
    [Space]

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private GameObject cardPool;

    [SerializeField]
    private int numberOfCards;

    [SerializeField]
    private float multiplierPos = 1.8f;

    private int numberOfCardsRevealed = 0;

    private List<int> cardIndexes;

    private List<Cards_Script> cardsRevealed = new List<Cards_Script>(2);

    // Puzzle
    [Header("--- Puzzle Game ---")]
    [Space]

    [SerializeField]
    private GameObject puzzlePiece;

    [SerializeField]
    private GameObject puzzlePiecePool;

    [SerializeField]
    private int numberOfPuzzlePieces;

    [SerializeField]
    private float scaleMultiplierUnder9 = 0.4f;

    [SerializeField]
    private float scaleMultiplierOver9 = 0.3f;

    private List<int> puzzlePiecesIndexes;

    // Tap&Drag
    [Header("--- Tap&Drag Game ---")]
    [Space]

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject itemPool;

    [SerializeField]
    private int numberOfItems;

    private List<int> itemsIndexes;

    private int rows;
    private int cols;

    private int randomIndex = 0;

    private void Start()
    {
        switch (indexGameSelected)
        {
            case 0:
                MemoryStart();
                break;

            case 1:
                PuzzleStart();
                break;
            case 2:
                TapDragStart();
                break;

            default:
                break;
        }
    }


    // ******************************************************
    // MEMORY GAME
    private void MemoryStart()
    {
        cardIndexes = new List<int>(numberOfCards);
        for (int i = 0; i < numberOfCards / 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                cardIndexes.Add(i);
            }
        }

        rows = Mathf.CeilToInt(numberOfCards / 3f);
        cols = 3;

        int numbersOfCardsCreated = 0;
        float correctX = 0;
        float increaseY = 0;
        if (numberOfCards > 9 && numberOfCards <= 12)
        {
            increaseY = -0.5f;
        }


        for (int y = 0; y < rows; y++)
        {
            if (y == rows - 1)
            {
                cols = numberOfCards - numbersOfCardsCreated;
                if (cols % 2 == 0)
                {
                    correctX = 0.5f;
                }
            }

            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, cardIndexes.Count);
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierPos, (rows / 2 - y + increaseY) * multiplierPos, 0);
                var temp = Instantiate(card, position, Quaternion.Euler(-5, 0, 0), cardPool.transform); // FIX ROTATION
                numbersOfCardsCreated++;
                temp.GetComponent<Cards_Script>().CardIndex = cardIndexes[randomIndex];
                temp.GetComponent<Cards_Script>().SetGameplayManager(this);
                cardIndexes.Remove(cardIndexes[randomIndex]);
            }
        }
    }

    public void AddCardRevealed(Cards_Script card)
    {
        cardsRevealed.Add(card);

        if (cardsRevealed.Count == cardsRevealed.Capacity)
        {
            numberOfCardsRevealed = 0;
            if (CheckMatch())
            {
                for (int i = 0; i < cardsRevealed.Count; i++)
                {
                    cardsRevealed[i].SetRemainVisible(true);
                    cardsRevealed[i].SetCanFlip(false);
                }
            }
            else
            {
                for (int j = 0; j < cardsRevealed.Count; j++)
                {
                    cardsRevealed[j].SetCanFlip(true);
                    cardsRevealed[j].CardClicked();
                }
            }

            // Clear List
            cardsRevealed.Clear();
        }
    }

    private bool CheckMatch()
    {
        bool success = false;
        if (cardsRevealed[0].CardIndex == cardsRevealed[1].CardIndex)
        {
            //Check if the indexes are the same
            success = true;
        }
        return success;
    }

    public bool TwoCardsRevealed()
    {
        bool success = false;
        if (numberOfCardsRevealed >= 2)
        {
            success = true;
        }
        return success;
    }

    public void IncreaseNumberOfCardsRevealed()
    {
        numberOfCardsRevealed++;
    }
    // ******************************************************

    //Puzzle Game
    private void PuzzleStart()
    {
        puzzlePiecesIndexes = new List<int>(numberOfPuzzlePieces);
        for (int i = 0; i < numberOfPuzzlePieces; i++)
        {
            puzzlePiecesIndexes.Add(i);
        }

        if (numberOfPuzzlePieces <= 9)
        {
            rows = Mathf.CeilToInt(numberOfPuzzlePieces / 3f);
            cols = 3;
        }
        else
        {
            rows = Mathf.CeilToInt(numberOfPuzzlePieces / 4f);
            cols = 4;
        }

        int numbersOfPiecesCreated = 0;
        float correctX = 0;
        float increaseY = 0;
        float multiplierX = 1f;
        float multiplierY = 1f;
        float positionY = 0;
        if (numberOfPuzzlePieces <= 9)
        {
            increaseY = 0.5f;
            multiplierX = 1.6f;
            multiplierY = 1.3f;
            positionY = -2.8f;
            if (numberOfPuzzlePieces <= 6)
            {
                positionY = -2f;
                multiplierY = 1.5f;
            }
        }
        else
        {
            correctX = 0.5f;
            multiplierX = 1.25f;
            multiplierY = 1.2f;
            positionY = -2.6f;
        }

        for (int y = 0; y < rows; y++)
        {
            if (y == rows - 1)
            {
                cols = numberOfPuzzlePieces - numbersOfPiecesCreated;
                if (cols % 3 == 0 && numberOfPuzzlePieces > 9)
                {
                    correctX = 0f;
                }
                else if (cols % 2 == 0 && numberOfPuzzlePieces <= 9)
                {
                    correctX = 0.5f;
                }
            }

            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, puzzlePiecesIndexes.Count);
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierX, (y - rows / 2 + increaseY + positionY) * multiplierY, 0);
                var temp = Instantiate(puzzlePiece, position, Quaternion.identity, puzzlePiecePool.transform);
                numbersOfPiecesCreated++;
                temp.GetComponent<Puzzle_Script>().Index = puzzlePiecesIndexes[randomIndex];
                temp.GetComponent<Puzzle_Script>().SetSprite();
                if (numberOfPuzzlePieces <= 9)
                {
                    temp.GetComponent<Transform>().localScale = temp.GetComponent<Transform>().localScale * scaleMultiplierUnder9;
                }
                else
                {
                    temp.GetComponent<Transform>().localScale = temp.GetComponent<Transform>().localScale * scaleMultiplierOver9;
                }
                puzzlePiecesIndexes.Remove(puzzlePiecesIndexes[randomIndex]);
            }
        }
    }
    // ******************************************************

    //Tap&Drag Game
    private void TapDragStart()
    {
        itemsIndexes = new List<int>(numberOfItems);
        for (int i = 0; i < numberOfItems; i++)
        {
            itemsIndexes.Add(i);
        }


        rows = Mathf.CeilToInt(numberOfItems / 3f);
        cols = 3;


        int numbersOfPiecesCreated = 0;
        float correctX = 0;
        float increaseY = 0.5f;
        float multiplierX = 1.6f;
        float multiplierY = 1.3f;
        float positionY = -2.8f;


        if (numberOfItems <= 6)
        {
            positionY = -2f;
            multiplierY = 1.5f;
        }


        for (int y = 0; y < rows; y++)
        {
            if (y == rows - 1)
            {
                cols = numberOfItems - numbersOfPiecesCreated;
                if (cols % 2 == 0)
                {
                    correctX = 0.5f;
                }
            }

            for (int x = 0; x < cols; x++)
            {
                randomIndex = Random.Range(0, itemsIndexes.Count);
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierX, (y - rows / 2 + increaseY + positionY) * multiplierY, 0);
                var temp = Instantiate(item, position, Quaternion.identity, itemPool.transform);
                numbersOfPiecesCreated++;
                temp.transform.GetChild(0).GetComponent<TapDrag_Script>().Index = itemsIndexes[randomIndex];
                temp.transform.GetChild(0).GetComponent<TapDrag_Script>().SetSprite();
                itemsIndexes.Remove(itemsIndexes[randomIndex]);
            }
        }
    }



}
