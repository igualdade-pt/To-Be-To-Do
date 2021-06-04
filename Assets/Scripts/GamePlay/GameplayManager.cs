using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    [SerializeField]
    private int indexDifficultyTEST;


    [Header("--- Game Properties ---")]
    [Space]

    [SerializeField]
    private int indexAdventureSelected = -1;

    private GameInstanceScript gameInstance;

    private UIManager_GM uiManager_GM;

    private AudioManager audioManager;
    private MusicManagerScript musicManager;

    private int indexLanguage = 0;

    private int indexDifficulty = 0;

    private int indexSuperPower = 0;


    // Match the Colour
    [Header("--- Match the Colour Game --- (0)")]
    [Space]

    [SerializeField]
    private GameObject MatchColourAdventurePool;

    // Maze the Colour
    [Header("--- Maze Game --- (1)")]
    [Space]

    [SerializeField]
    private GameObject mazeAdventurePool;

    // Tap&Drag
    [Header("--- Tap&Drag Game --- (2)")]
    [Space]

    [SerializeField]
    private GameObject TapDragAdventurePool;

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

    // Puzzle
    [Header("--- Puzzle Game --- (3)")]
    [Space]

    [SerializeField]
    private GameObject PuzzleAdventurePool;

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

    // Memory
    [Header("--- Memory Game --- (4)")]
    [Space]

    [SerializeField]
    private GameObject MemoryAdventurePool;

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


    private void Start()
    {
        if (!gameplayTest)
        {
            gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

            uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();

            // Attribute Language      
            indexLanguage = gameInstance.LanguageIndex;
            switch (indexLanguage)
            {
                case 0:
                    Debug.Log("Gameplay Menu, System language English: " + indexLanguage);

                    break;
                case 1:
                    Debug.Log("Gameplay Menu, System language Italian: " + indexLanguage);

                    break;
                case 2:
                    Debug.Log("Gameplay Menu, System language Portuguese: " + indexLanguage);

                    break;
                case 3:
                    Debug.Log("Gameplay Menu, System language Spanish: " + indexLanguage);

                    break;
                case 4:
                    Debug.Log("Gameplay Menu, System language Swedish: " + indexLanguage);
                    break;

                default:
                    Debug.Log("Gameplay Menu, Unavailable language, English Selected: " + indexLanguage);

                    break;
            }

            uiManager_GM.UpdateLanguage(indexLanguage);


            // Attribute Difficulty      
            indexDifficulty = gameInstance.DifficultyLevelIndex;
            Debug.Log("Difficulty Selected " + indexDifficulty);

            // Attribute SuperPower      
            indexSuperPower = gameInstance.SuperPowerIndex;
            Debug.Log("SuperPower Selected " + indexSuperPower);

            // Attribute Adventure      
            indexAdventureSelected = gameInstance.AdventureIndex;
            Debug.Log("Adventute Selected " + indexAdventureSelected);

            //Set All Adventures SetActive False
            MatchColourAdventurePool.SetActive(false);
            mazeAdventurePool.SetActive(false);
            TapDragAdventurePool.SetActive(false);
            PuzzleAdventurePool.SetActive(false);
            MemoryAdventurePool.SetActive(false);
        }
        else
        {
            indexDifficulty = indexDifficultyTEST;
        }

        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        // Set Only the Adventure Selected
        switch (indexAdventureSelected)
        {
            case 0:
                MatchColourAdventurePool.SetActive(true);
                MatchColourStart();
                uiManager_GM.SetColourPanel(true);
                break;

            case 1:
                MazeStart();
                mazeAdventurePool.SetActive(true);
                break;

            case 2:
                TapDragStart();
                TapDragAdventurePool.SetActive(true);
                break;

            case 3:
                PuzzleStart();
                PuzzleAdventurePool.SetActive(true);
                break;

            case 4:
                MemoryStart();
                MemoryAdventurePool.SetActive(true);
                break;

            default:
                break;
        }

        musicManager = FindObjectOfType<MusicManagerScript>().GetComponent<MusicManagerScript>();

        musicManager.LowMusic();
    }



    //MATCH THE COLOUR GAME
    private void MatchColourStart()
    {

    }

    // ******************************************************

    //MAZE GAME
    private void MazeStart()
    {

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

    // ******************************************************

    public void GameEnded()
    {
        uiManager_GM.SetGameEndedPanel(true);
    }

    public void LoadSelectedScene(int indexSelected)
    {
        StartCoroutine(StartLoadAsyncScene(indexSelected));
    }

    private IEnumerator StartLoadAsyncScene(int indexLevel)
    {
        yield return new WaitForSeconds(3f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(indexLevel);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }

}
