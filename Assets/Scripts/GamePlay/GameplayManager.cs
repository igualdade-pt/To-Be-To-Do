using PaintCraft.Canvas;
using PaintCraft.Canvas.Configs;
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

    private Player_Script playerScript;

    private int indexLanguage = 0;

    private int indexDifficulty = 0;

    private int indexSuperPower = 0;


    // Match the Colour
    [Header("--- Match the Colour Game --- (0)")]
    [Space]

    [SerializeField]
    private GameObject matchColourAdventurePool;

    [SerializeField] 
    private ScriptableObject pagePaintBasic;

    [SerializeField]
    private ScriptableObject pageMountainBasic;

    [SerializeField]
    private ScriptableObject pageBallBasic;


    [SerializeField]
    private ScriptableObject pagePaintExpert;

    [SerializeField]
    private ScriptableObject pageMountainExpert;

    [SerializeField]
    private ScriptableObject pageBallExpert;

    private IPageConfig pageConfig;


    // ***************************************************************************************** //

    // Maze the Colour
    [Header("--- Maze Game --- (1)")]
    [Space]

    [SerializeField]
    private GameObject mazeAdventurePool;

    [SerializeField]
    private GameObject mazeAdventure1;

    [SerializeField]
    private GameObject mazeAdventure2;

    [SerializeField]
    private GameObject mazeAdventure3;

    [SerializeField]
    private GameObject[] mazeBasic;

    [SerializeField]
    private GameObject[] mazeExpert;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Element 0 , 1 - Basic (min , sec), Element 2 ,3 - Expert (min , sec)")]
    [SerializeField]
    private int[] timeMazeByDifficulty = new int[4] { 1, 30,
                                                    0, 59 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfMazeAttemptsByDifficulty = new int[2] { 4, 6 };

    private int numberOfMazeAttempts;

    // ***************************************************************************************** //

    // Tap&Drag
    [Header("--- Tap&Drag Game --- (2)")]
    [Space]

    [SerializeField]
    private GameObject tapDragAdventurePool;

    [SerializeField]
    private GameObject tapDragAdventure1;

    [SerializeField]
    private GameObject tapDragAdventure2;

    [SerializeField]
    private GameObject tapDragAdventure3;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private GameObject itemPool;

    [SerializeField]
    private int numberOfAllItems;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Difficulty: ")]
    [SerializeField]
    private int[] numberOfItemsByDifficulty = new int[2] { 6, 8 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfRightItemsByDifficulty = new int[2] { 4, 4 };

    [Header("Element 0 , 1 - Basic (min , sec), Element 2 ,3 - Expert (min , sec)")]
    [SerializeField]
    private int[] timeTapDragByDifficulty = new int[4] { 1, 30,
                                                    0, 59 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfTapDragAttemptsByDifficulty = new int[2] { 4, 3 };

    private int numberOfItems;

    private int numberOfRightItems;

    private int numberOfTapDragAttempts;

    private List<int> itemsIndexes;

    // ***************************************************************************************** //

    // Puzzle
    [Header("--- Puzzle Game --- (3)")]
    [Space]

    [SerializeField]
    private GameObject puzzleAdventurePool;

    [SerializeField]
    private GameObject puzzleAdventure1_Basic;

    [SerializeField]
    private GameObject puzzleAdventure1_Expert;

    [SerializeField]
    private GameObject puzzleAdventure2_Basic;

    [SerializeField]
    private GameObject puzzleAdventure2_Expert;

    [SerializeField]
    private GameObject puzzleAdventure3_Basic;

    [SerializeField]
    private GameObject puzzleAdventure3_Expert;

    [SerializeField]
    private GameObject puzzlePiece;

    [SerializeField]
    private GameObject puzzlePiecePool;

    [SerializeField]
    private float scaleMultiplierUnder9 = 0.4f;

    [SerializeField]
    private float scaleMultiplierOver9 = 0.3f;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Difficulty: ")]
    [SerializeField]
    private int[] numberOfPiecesByDifficulty = new int[2] { 6, 6 };

    [Header("Element 0 , 1 - Basic (min , sec), Element 2 ,3 - Expert (min , sec)")]
    [SerializeField]
    private int[] timePuzzleByDifficulty = new int[4] { 1, 30,
                                                    0, 59 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfPuzzleAttemptsByDifficulty = new int[2] { 4, 3 };

    private List<int> puzzlePiecesIndexes;

    private int numberOfPuzzlePieces;

    private int numberOfPuzzlePiecesDone;

    private int numberOfPuzzleAttempts;

    // ***************************************************************************************** //

    // Memory
    [Header("--- Memory Game --- (4)")]
    [Space]

    [SerializeField]
    private GameObject memoryAdventurePool;

    [SerializeField]
    private GameObject card;

    [SerializeField]
    private GameObject cardPool;

    [SerializeField]
    private float multiplierYForMemoryGame = 1.8f;

    [SerializeField]
    private float multiplierXForMemoryGame = 1.8f;

    [SerializeField]
    private int numberOfCardsFaces = 7;

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [Space]

    [Header("Difficulty: ")]
    [SerializeField]
    private int[] numberOfCardsByDifficulty = new int[2] { 10, 14 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private float[] timeToFlipCardsByDifficulty = new float[2] { 4, 2 };

    [Header("Element 0 - Basic, Element 1 - Expert")]
    [SerializeField]
    private int[] numberOfAttemptsByDifficulty = new int[2] { 4, 5 };

    private int numberOfCards;

    private int numberOfRightPairs;

    private int numberOfMemoryAttempts;

    private int numberOfCardsRevealed = 0;

    private List<int> cardIndexes;

    private List<Cards_Script> cardsRevealed = new List<Cards_Script>(2);

    // ***************************************************************************************** //

    private int rows;
    private int cols;

    private int randomIndex = -1;

    private int sec;
    private int min;


    [Header("Sounds")]
    [Space]
    [SerializeField]
    private int indexSoundWon = 3;

    [SerializeField]
    private int indexSoundRight = 0;

    [SerializeField]
    private int indexSoundLost = 4;

    [SerializeField]
    private int indexSoundWrong = 1;

    [SerializeField]
    private int indexSoundCountDown = 0;

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
            matchColourAdventurePool.SetActive(false);
            mazeAdventurePool.SetActive(false);
            tapDragAdventurePool.SetActive(false);
            puzzleAdventurePool.SetActive(false);
            memoryAdventurePool.SetActive(false);
        }
        else
        {
            indexDifficulty = indexDifficultyTEST;
        }

        uiManager_GM = FindObjectOfType<UIManager_GM>().GetComponent<UIManager_GM>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        musicManager = FindObjectOfType<MusicManagerScript>().GetComponent<MusicManagerScript>();

        musicManager.LowMusic();

        // Set Only the Adventure Selected
        switch (indexAdventureSelected)
        {
            case 0: case 5: case 10:
                MatchColourStart();
                matchColourAdventurePool.SetActive(true);

                uiManager_GM.SetColourPanel(true);
                break;

            case 1: case 6: case 11:
                switch (indexDifficulty)  // TIMER
                {
                    case 0:
                        min = timeMazeByDifficulty[0];
                        sec = timeMazeByDifficulty[1];
                        break;

                    case 1:
                        min = timeMazeByDifficulty[2];
                        sec = timeMazeByDifficulty[3];
                        break;

                    default:
                        min = timeMazeByDifficulty[0];
                        sec = timeMazeByDifficulty[1];
                        break;
                }

                uiManager_GM.SetTimerActive(true);
                uiManager_GM.UpdateTimer(min, sec);

                uiManager_GM.SetReturnButton(true);

                mazeAdventurePool.SetActive(true);

                MazeStart();

                playerScript = FindObjectOfType<Player_Script>().GetComponent<Player_Script>();
                playerScript.GameStarted(true);
                StartCoroutine(CountdownSec());
                break;

            case 2: case 7: case 12:
                numberOfItems = numberOfItemsByDifficulty[indexDifficulty];
                numberOfTapDragAttempts = numberOfTapDragAttemptsByDifficulty[indexDifficulty];
                numberOfRightItems = numberOfRightItemsByDifficulty[indexDifficulty];

                switch (indexDifficulty)  // TIMER
                {
                    case 0:
                        min = timeTapDragByDifficulty[0];
                        sec = timeTapDragByDifficulty[1];
                        break;

                    case 1:
                        min = timeTapDragByDifficulty[2];
                        sec = timeTapDragByDifficulty[3];
                        break;

                    default:
                        min = timeTapDragByDifficulty[0];
                        sec = timeTapDragByDifficulty[1];
                        break;
                }

                uiManager_GM.SetReturnButton(true);

                uiManager_GM.SetAttempts(true);
                uiManager_GM.UpdateAttempts(numberOfTapDragAttempts);

                uiManager_GM.SetTimerActive(true);
                uiManager_GM.UpdateTimer(min, sec);

                tapDragAdventure1.SetActive(false);
                tapDragAdventure2.SetActive(false);
                tapDragAdventure3.SetActive(false);
                switch (indexAdventureSelected)
                {
                    case 2:
                        tapDragAdventure1.SetActive(true);
                        break;

                    case 7:
                        tapDragAdventure2.SetActive(true);
                        break;

                    case 12:
                        tapDragAdventure3.SetActive(true);
                        break;
                    default:
                        tapDragAdventure1.SetActive(true);
                        break;
                }             
                

                tapDragAdventurePool.SetActive(true);

                TapDragStart();

                playerScript = FindObjectOfType<Player_Script>().GetComponent<Player_Script>();
                playerScript.GameStarted(true);
                StartCoroutine(CountdownSec());
                break;

            case 3: case 8: case 13:
                numberOfPuzzlePieces = numberOfPiecesByDifficulty[indexDifficulty];
                numberOfPuzzlePiecesDone = numberOfPuzzlePieces;
                numberOfPuzzleAttempts = numberOfPuzzleAttemptsByDifficulty[indexDifficulty];
                switch (indexDifficulty)  // TIMER
                {
                    case 0:
                        min = timePuzzleByDifficulty[0];
                        sec = timePuzzleByDifficulty[1];
                        break;

                    case 1:
                        min = timePuzzleByDifficulty[2];
                        sec = timePuzzleByDifficulty[3];
                        break;

                    default:
                        min = timePuzzleByDifficulty[0];
                        sec = timePuzzleByDifficulty[1];
                        break;
                }

                uiManager_GM.SetReturnButton(true);

                uiManager_GM.SetAttempts(true);
                uiManager_GM.UpdateAttempts(numberOfPuzzleAttempts);

                uiManager_GM.SetTimerActive(true);
                uiManager_GM.UpdateTimer(min, sec);

                puzzleAdventure1_Basic.SetActive(false);
                puzzleAdventure2_Basic.SetActive(false);
                puzzleAdventure3_Basic.SetActive(false);

                puzzleAdventure1_Expert.SetActive(false);
                puzzleAdventure2_Expert.SetActive(false);
                puzzleAdventure3_Expert.SetActive(false);
                if (indexDifficulty == 0)
                {
                    switch (indexAdventureSelected)
                    {
                        case 3:
                            puzzleAdventure1_Basic.SetActive(true);
                            break;

                        case 8:
                            puzzleAdventure2_Basic.SetActive(true);
                            break;

                        case 13:
                            puzzleAdventure3_Basic.SetActive(true);
                            break;
                        default:
                            tapDragAdventure1.SetActive(true);
                            break;
                    }
                }
                else
                {
                    switch (indexAdventureSelected)
                    {
                        case 3:
                            puzzleAdventure1_Expert.SetActive(true);
                            break;

                        case 8:
                            puzzleAdventure2_Expert.SetActive(true);
                            break;

                        case 13:
                            puzzleAdventure3_Expert.SetActive(true);
                            break;
                        default:
                            tapDragAdventure1.SetActive(true);
                            break;
                    }
                }

                puzzleAdventurePool.SetActive(true);

                PuzzleStart();

                playerScript = FindObjectOfType<Player_Script>().GetComponent<Player_Script>();
                playerScript.GameStarted(true);
                StartCoroutine(CountdownSec());
                break;

            case 4: case 9: case 14:
                numberOfCards = numberOfCardsByDifficulty[indexDifficulty]; 
                numberOfMemoryAttempts = numberOfAttemptsByDifficulty[indexDifficulty];

                uiManager_GM.SetReturnButton(true);

                uiManager_GM.SetAttempts(true);
                uiManager_GM.UpdateAttempts(numberOfMemoryAttempts);

                memoryAdventurePool.SetActive(true);

                numberOfRightPairs = 0;

                MemoryStart();

                playerScript = FindObjectOfType<Player_Script>().GetComponent<Player_Script>();
                StartCoroutine(WaitToFlipCards(timeToFlipCardsByDifficulty[indexDifficulty]));
                break;

            default:
                break;
        }


    }



    //MATCH THE COLOUR GAME
    private void MatchColourStart()
    {

        if (indexDifficulty == 0)
        {
            switch (indexAdventureSelected)
            {
                case 0:
                    pageConfig = pageBallBasic as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;

                case 5:
                    pageConfig = pagePaintBasic as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;

                case 10:
                    pageConfig = pageMountainBasic as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;
                default:
                    pageConfig = pageBallBasic as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;
            }
        }
        else
        {
            switch (indexAdventureSelected)
            {
                case 0:
                    pageConfig = pageBallExpert as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;

                case 5:
                    pageConfig = pagePaintExpert as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;

                case 10:
                    pageConfig = pageMountainExpert as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;
                default:
                    pageConfig = pageBallExpert as IPageConfig;
                    AppData.SelectedPageConfig = pageConfig;
                    break;
            }
        }
    }

    // ******************************************************

    //MAZE GAME
    private void MazeStart()
    {
        mazeAdventure1.SetActive(false);
        mazeAdventure2.SetActive(false);
        mazeAdventure3.SetActive(false);
        switch (indexAdventureSelected)
        {
            case 1:
                mazeAdventure1.SetActive(true);
                break;

            case 6:
                mazeAdventure2.SetActive(true);
                break;

            case 11:
                mazeAdventure3.SetActive(true);
                break;

            default:
                mazeAdventure1.SetActive(true);
                break;
        }

        for (int i = 0; i < mazeBasic.Length; i++)
        {
            mazeBasic[i].SetActive(false);
        }

        for (int i = 0; i < mazeExpert.Length; i++)
        {
            mazeExpert[i].SetActive(false);
        }

        switch (indexDifficulty)
        {
            case 0:
                switch (indexAdventureSelected)
                {
                    case 1:
                        mazeBasic[0].SetActive(true);
                        break;

                    case 6:
                        mazeBasic[1].SetActive(true);
                        break;

                    case 11:
                        mazeBasic[2].SetActive(true);
                        break;

                    default:
                        mazeBasic[0].SetActive(true);
                        break;
                }                
                break;

            case 1:
                switch (indexAdventureSelected)
                {
                    case 1:
                        mazeExpert[0].SetActive(true);
                        break;

                    case 6:
                        mazeExpert[1].SetActive(true);
                        break;

                    case 11:
                        mazeExpert[2].SetActive(true);
                        break;

                    default:
                        mazeExpert[0].SetActive(true);
                        break;
                }
                break;

            default:
                switch (indexDifficulty)
                {
                    case 0:
                        mazeBasic[0].SetActive(true);
                        break;

                    case 1:
                        mazeBasic[1].SetActive(true);
                        break;

                    case 2:
                        mazeBasic[1].SetActive(true);
                        break;

                    default:
                        mazeBasic[0].SetActive(true);
                        break;
                }
                break;
        }

        numberOfMazeAttempts = numberOfMazeAttemptsByDifficulty[indexDifficulty];

        uiManager_GM.UpdateAttempts(numberOfMazeAttempts);

        uiManager_GM.SetAttempts(true);
    }

    public void CheckMazeLossCondition()
    {
        // Play Sound
        audioManager.PlayClip(indexSoundWrong, 0.6f);
        // ****
        numberOfMazeAttempts--;
        uiManager_GM.UpdateAttempts(numberOfMazeAttempts);
        if (numberOfMazeAttempts <= 0)
        {
            // Play Sound
            audioManager.PlayClip(indexSoundLost, 0.6f);
            // ****
            StopAllCoroutines();
            uiManager_GM.UpdateAttempts(0);
            GameEnded();
        }
    }

    public void CheckMazeWinCondition()
    {
        // Play Sound
        audioManager.PlayClip(indexSoundRight, 0.6f);
        // ****
        if (numberOfMazeAttempts > 0)
        {
            // Play Sound
            audioManager.PlayClip(indexSoundWon, 0.8f);
            // ****
            StopAllCoroutines();
            GameEnded();
        }
    }

    // ******************************************************

    // MEMORY GAME
    private void MemoryStart()
    {
        var cardsFace = new List<int>(numberOfCardsFaces);
        for (int i = 0; i < numberOfCardsFaces; i++)
        {
            cardsFace.Add(i);
        }


        cardIndexes = new List<int>(numberOfCards);
        for (int i = 0; i < numberOfCards / 2; i++)
        {
            int x = cardsFace[Random.Range(0, cardsFace.Count)];
            for (int j = 0; j < 2; j++)
            {
                cardIndexes.Add(x);
            }
            cardsFace.Remove(x);
        }

        if (numberOfCards < 10)
        {
            rows = Mathf.CeilToInt(numberOfCards / 2f);
            cols = 2;
        }
        else
        {
            rows = Mathf.CeilToInt(numberOfCards / 3f);
            cols = 3;
        }


        int numbersOfCardsCreated = 0;
        float correctX = 0.5f;
        float increaseY = 0;
        if (numberOfCards > 9 && numberOfCards <= 12)
        {
            increaseY = -0.7f;
            correctX = 0f;
        }


        for (int y = 0; y < rows; y++)
        {
            if (y == rows - 1 && numberOfCards > 9)
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
                Vector3 position = new Vector3((cols / 2 - x - correctX) * multiplierXForMemoryGame, (rows / 2 - y + increaseY) * multiplierYForMemoryGame, 0);
                var temp = Instantiate(card, position, Quaternion.Euler(-5, 0, 0), cardPool.transform); // FIX ROTATION
                numbersOfCardsCreated++;
                temp.GetComponent<Cards_Script>().ScenarioIndex = indexAdventureSelected;
                temp.GetComponent<Cards_Script>().CardIndex = cardIndexes[randomIndex];
                temp.GetComponent<Cards_Script>().SetGameplayManager(this);
                temp.GetComponent<Cards_Script>().FlipCards(false);
                cardIndexes.Remove(cardIndexes[randomIndex]);
            }
        }
    }

    private IEnumerator WaitToFlipCards(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        var cards = GameObject.FindGameObjectsWithTag("Cards");

        foreach (GameObject card in cards)
        {
            card.GetComponent<Cards_Script>().FlipCards(true);
        }

        // Play Sound
        audioManager.PlayClip(4, 0.8f);
        // ****

        playerScript.GameStarted(true);
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
            /// Play Sound
            audioManager.PlayClip(indexSoundRight, 0.6f);
            // ****
            //Check if the indexes are the same
            success = true;
            numberOfRightPairs++;
            if (numberOfCards == numberOfRightPairs * 2)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundWon, 0.8f);
                // ****
                GameEnded();
            }
        }
        else
        {
            // Play Sound
            audioManager.PlayClip(indexSoundWrong, 0.6f);
            // ****
            numberOfMemoryAttempts--;
            uiManager_GM.UpdateAttempts(numberOfMemoryAttempts);
            if (numberOfMemoryAttempts <= 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundLost, 0.6f);
                // ****
                uiManager_GM.UpdateAttempts(0);
                GameEnded();
            }
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
                temp.GetComponent<Puzzle_Script>().SetSprite(indexAdventureSelected, indexDifficulty);
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
    public bool CheckPiece(Puzzle_Script piece, Puzzle_Script puzzleBoard)
    {
        bool success = false;
        if (piece.Index == puzzleBoard.Index)
        {
            /// Play Sound
            audioManager.PlayClip(indexSoundRight, 0.6f);
            // ****
            //Check if the indexes are the same
            success = true;
            numberOfPuzzlePiecesDone--;
            if (numberOfPuzzlePiecesDone <= 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundWon, 0.8f);
                // ****
                StopAllCoroutines();
                GameEnded();                
            }
        }
        else
        {
            // Play Sound
            audioManager.PlayClip(indexSoundWrong, 0.6f);
            // ****
            numberOfPuzzleAttempts--;
            uiManager_GM.UpdateAttempts(numberOfPuzzleAttempts);
            if (numberOfPuzzleAttempts <= 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundLost, 0.6f);
                // ****
                StopAllCoroutines();
                uiManager_GM.UpdateAttempts(0);
                GameEnded();
              }
        }

        return success;
    }


    // ******************************************************

    //Tap&Drag Game
    private void TapDragStart()
    {
        var allItems = new List<int>(numberOfAllItems - 4);
        for (int i = 4; i < numberOfAllItems; i++)
        {
            allItems.Add(i);
        }

        itemsIndexes = new List<int>(numberOfItems);
        for (int i = 0; i < numberOfItems; i++)
        {
            if (i <= 3)
            {
                itemsIndexes.Add(i);
            }
            else
            {
                int x = allItems[Random.Range(0, allItems.Count)];
                itemsIndexes.Add(x);
                allItems.Remove(x);
            }

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
                temp.transform.GetChild(0).GetComponent<TapDrag_Script>().SetSprite(indexAdventureSelected);
                itemsIndexes.Remove(itemsIndexes[randomIndex]);
            }
        }
    }


    public bool CheckItem(TapDrag_Script item)
    {
        bool success = false;
        if (item.Index < 4)
        {
            /// Play Sound
            audioManager.PlayClip(indexSoundRight, 0.6f);
            // ****
            //Check if the indexes are the same
            success = true;
            numberOfRightItems--;
            if (numberOfRightItems <= 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundWon, 0.8f);
                // ****
                StopAllCoroutines();
                GameEnded();
            }
        }
        else
        {
            // Play Sound
            audioManager.PlayClip(indexSoundWrong, 0.6f);
            // ****
            numberOfTapDragAttempts--;
            uiManager_GM.UpdateAttempts(numberOfTapDragAttempts);
            if (numberOfTapDragAttempts <= 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundLost, 0.6f);
                // ****
                StopAllCoroutines();
                uiManager_GM.UpdateAttempts(0);
                GameEnded();
            }
        }

        return success;
    }

    // ******************************************************

    // CountDown

    private IEnumerator CountdownSec()
    {
        int counter = sec;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            if (counter <= 3 && counter > 0)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundCountDown, 0.6f);
                // ****
            }
            uiManager_GM.UpdateTimer(min, counter);
        }
        CountdownMin();
    }

    private void CountdownMin()
    {
        if (min > 0)
        {
            min--;
            sec = 59;
            uiManager_GM.UpdateTimer(min, sec);
            StartCoroutine(CountdownSec());
        }
        else
        {
            StopAllCoroutines();
            // Play Sound
            audioManager.PlayClip(indexSoundLost, 0.6f);
            // ****
            GameEnded();
        }
    }


    // ******************************************************

    private void GameEnded()
    {
        playerScript.GameStarted(false);
        uiManager_GM.SetGameEndedPanel(true);
    }

    public void LoadSelectedScene(int indexSelected)
    {
        musicManager.UpMusic();

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
