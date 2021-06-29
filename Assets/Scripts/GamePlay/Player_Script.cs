using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    [Header("Test Properties")]
    [Space]
    [SerializeField]
    private bool gameplayTest = false;

    [SerializeField]
    private bool mobile = false;

    [Header("Player Properties")]
    [Space]
    [SerializeField]
    private int indexAdventureSelected = -1;

    private GameObject pieceSelected;

    private Vector3 initialPos;

    private Vector3 initialScale;

    private GameInstanceScript gameInstance;

    private GameplayManager gameplayManager;

    private int indexLanguage = 0;

    private int indexDifficulty = 0;

    private int indexSuperPower = 0;

    // **** Match Color ****

    private Color colorSelected;

    // **** Maze ****
    [Header("Maze Properties, Only")]
    [Space]
    [SerializeField]
    private GameObject brush;

    [SerializeField]
    private Gradient colorOfLine;

    private LineRenderer currentLineRenderer;

    private Vector2 lastPos;
    private bool gameStarted;

    private void Start()
    {
        if (!gameplayTest)
        {
            gameInstance = FindObjectOfType<GameInstanceScript>().GetComponent<GameInstanceScript>();

            // Attribute Language      
            indexLanguage = gameInstance.LanguageIndex;
            Debug.Log("Player Language Selected: " + indexLanguage);
            switch (indexLanguage)
            {
                case 0:

                    break;

                case 1:

                    break;

                case 2:

                    break;

                case 3:

                    break;

                case 4:

                    break;

                default:

                    break;
            }

            // Attribute Difficulty      
            indexDifficulty = gameInstance.DifficultyLevelIndex;
            Debug.Log("Difficulty Selected " + indexDifficulty);

            // Attribute SuperPower      
            indexSuperPower = gameInstance.SuperPowerIndex;
            Debug.Log("SuperPower Selected " + indexSuperPower);

            // Attribute Adventure      
            indexAdventureSelected = gameInstance.AdventureIndex;
            Debug.Log("Adventute Selected " + indexAdventureSelected);

        }

        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();

        switch (indexAdventureSelected)
        {
            case 0:
                colorSelected = new Color(1, 1, 1, 1);
                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            default:
                Debug.Log("Adventute Selected Error, index Adventure" + indexAdventureSelected);
                break;
        }

    }


    private void Update()
    {
        switch (indexAdventureSelected)
        {
            case 0:
            case 5:
            case 10:
                //MatchColorGameUpdate();
                break;

            case 1:
            case 6:
            case 11:
                MazeGameUpdate();
                break;

            case 2:
            case 7:
            case 12:
                TagDragGameUpdate();
                break;

            case 3:
            case 8:
            case 13:
                PuzzleGameUpdate();
                break;

            case 4:
            case 9:
            case 14:
                MemoryGameUpdate();
                break;

            default:
                Debug.Log("Adventute Selected Error, index Adventure" + indexAdventureSelected);
                break;
        }
    }


    // ************ Match Color
    private void MatchColorGameUpdate()
    {
        if (gameStarted)
        {
            switch (mobile)
            {
                case true:
                    // MOBILE
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(touch.position);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);

                            if (hit.collider != null)
                            {
                                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                                Debug.Log(hit.collider);
                            }
                        }
                    }

                    break;

                case false:
                    // PC
                    if (Input.GetMouseButtonDown(0))
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                            Debug.Log(hit.collider);
                        }
                    }

                    break;
            }
        }
    }

    public void _ColorClicked(Button button)
    {
        colorSelected = button.image.color;
        Debug.Log(colorSelected);
    }


    // ************* Maze 
    private void MazeGameUpdate()
    {
        if (gameStarted)
        {
            switch (mobile)
            {
                case true:
                    // MOBILE
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                if (hit.collider.tag == "MazeBeginning")
                                {
                                    CreateBrush();
                                }
                            }

                        }
                        else if (touch.phase == TouchPhase.Moved && currentLineRenderer != null)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                if (hit.collider.tag == "MazeBeginning")
                                {
                                    PointToMousePos();
                                }
                                else if (hit.collider.tag == "MazeEnd")
                                {
                                    gameplayManager.CheckMazeWinCondition();
                                    // WIN
                                }
                                else if (hit.collider.tag == "Maze")
                                {
                                    gameplayManager.CheckMazeLossCondition();
                                    // Lost
                                    currentLineRenderer.gameObject.SetActive(false);
                                    currentLineRenderer = null;

                                }
                              }
                            else
                            {
                                PointToMousePos();
                            }
                        }
                        else if (touch.phase == TouchPhase.Ended && currentLineRenderer != null)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                if (hit.collider.tag != "MazeEnd")
                                {
                                    currentLineRenderer.gameObject.SetActive(false);
                                }
                            }
                            else
                            {
                                gameplayManager.CheckMazeLossCondition();
                            }
                            currentLineRenderer = null;
                        }
                    }

                    break;

                case false:
                    // PC
                    if (Input.GetMouseButtonDown(0))
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "MazeBeginning")
                            {
                                CreateBrush();
                            }
                        }
                    }
                    else if (Input.GetMouseButton(0) && currentLineRenderer != null)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "MazeBeginning")
                            {
                                PointToMousePos();
                            }
                            else if (hit.collider.tag == "MazeEnd")
                            {
                                gameplayManager.CheckMazeWinCondition();
                                // WIN
                            }
                            else if (hit.collider.tag == "Maze")
                            {
                                gameplayManager.CheckMazeLossCondition();
                                // Lost
                                currentLineRenderer.gameObject.SetActive(false);
                                currentLineRenderer = null;
                            }
                        }
                        else
                        {
                            PointToMousePos();
                        }
                    }
                    else if (Input.GetMouseButtonUp(0) && currentLineRenderer != null)
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            if (hit.collider.tag != "MazeEnd")
                            {
                                currentLineRenderer.gameObject.SetActive(false);
                            }
                        }
                        else
                        {
                            gameplayManager.CheckMazeLossCondition();
                        }
                        currentLineRenderer = null;
                    }

                    break;
            }
        }

    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.colorGradient = colorOfLine;

        //2 points to start a line renderer 
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos)
        {
            AddAPoint(mousePos);
            lastPos = mousePos;
        }
    }


    // *********** Tap and Drag
    private void TagDragGameUpdate()
    {
        if (gameStarted)
        {
            switch (mobile)
            {
                case true:
                    // MOBILE
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                pieceSelected = null;
                                if (hit.collider.tag == "TapDragPiece")
                                {
                                    pieceSelected = hit.collider.gameObject;
                                    pieceSelected.GetComponent<Collider2D>().enabled = false;
                                    initialPos = pieceSelected.GetComponent<Transform>().position;
                                    initialScale = pieceSelected.GetComponent<Transform>().localScale;
                                    pieceSelected.GetComponent<Transform>().localScale = pieceSelected.GetComponent<Transform>().localScale * 1.5f;
                                    pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                }
                            }
                        }
                        else if (touch.phase == TouchPhase.Moved && pieceSelected != null)
                        {
                            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            pieceSelected.GetComponent<Transform>().position = mousePos;
                        }
                        else if (touch.phase == TouchPhase.Ended && pieceSelected != null)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(ray, ray);

                            if (hit.collider != null)
                            {
                                if (hit.collider.tag == "TapDragImage")
                                {
                                    if (gameplayManager.CheckItem(pieceSelected.GetComponent<TapDrag_Script>()))
                                    {
                                        pieceSelected.SetActive(false);
                                    }
                                }
                            }
                            pieceSelected.GetComponent<Transform>().position = initialPos;
                            pieceSelected.GetComponent<Transform>().localScale = initialScale;
                            pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            pieceSelected.GetComponent<Collider2D>().enabled = true;
                            pieceSelected = null;
                        }
                    }

                    break;

                case false:
                    // PC
                    if (Input.GetMouseButtonDown(0)) // MOUSE BUTTON BEGAN
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            pieceSelected = null;
                            if (hit.collider.tag == "TapDragPiece")
                            {
                                pieceSelected = hit.collider.gameObject;
                                pieceSelected.GetComponent<Collider2D>().enabled = false;
                                initialPos = pieceSelected.GetComponent<Transform>().position;
                                initialScale = pieceSelected.GetComponent<Transform>().localScale;
                                pieceSelected.GetComponent<Transform>().localScale = pieceSelected.GetComponent<Transform>().localScale * 1.5f;
                                pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            }
                        }
                    }

                    else if (Input.GetMouseButton(0) && pieceSelected != null) // MOUSE BUTTON MOVED
                    {
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pieceSelected.GetComponent<Transform>().position = mousePos;
                    }

                    else if (Input.GetMouseButtonUp(0) && pieceSelected != null) // MOUSE BUTTON ENDED
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(ray, ray);

                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "TapDragImage")
                            {
                                if (gameplayManager.CheckItem(pieceSelected.GetComponent<TapDrag_Script>()))
                                {
                                    pieceSelected.SetActive(false);
                                }
                            }
                        }
                        pieceSelected.GetComponent<Transform>().position = initialPos;
                        pieceSelected.GetComponent<Transform>().localScale = initialScale;
                        pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 0;
                        pieceSelected.GetComponent<Collider2D>().enabled = true;
                        pieceSelected = null;
                    }

                    break;
            }
        }
    }


    // ******************** Puzzle
    private void PuzzleGameUpdate()
    {
        if (gameStarted)
        {
            switch (mobile)
            {
                case true:
                    // MOBILE
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                pieceSelected = null;
                                if (hit.collider.tag == "PuzzlePiece")
                                {
                                    pieceSelected = hit.collider.gameObject;
                                    pieceSelected.GetComponent<Collider2D>().enabled = false;
                                    initialPos = pieceSelected.GetComponent<Transform>().position;
                                    initialScale = pieceSelected.GetComponent<Transform>().localScale;
                                    pieceSelected.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                                    pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                }
                            }
                        }
                        else if (touch.phase == TouchPhase.Moved && pieceSelected != null)
                        {
                            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            pieceSelected.GetComponent<Transform>().position = mousePos;
                        }
                        else if (touch.phase == TouchPhase.Ended && pieceSelected != null)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            RaycastHit2D hit = Physics2D.Raycast(ray, ray);

                            if (hit.collider != null)
                            {
                                if (hit.collider.tag == "PuzzleBoard")
                                {
                                    Puzzle_Script puzzleBoard = hit.collider.GetComponent<Puzzle_Script>();
                                    if (puzzleBoard.Empty && gameplayManager.CheckPiece(pieceSelected.GetComponent<Puzzle_Script>(), puzzleBoard))
                                    {
                                        puzzleBoard.GetComponent<SpriteRenderer>().sprite = pieceSelected.GetComponent<SpriteRenderer>().sprite;
                                        puzzleBoard.Empty = false;
                                        pieceSelected.SetActive(false);
                                    }
                                }
                            }
                            pieceSelected.GetComponent<Transform>().position = initialPos;
                            pieceSelected.GetComponent<Transform>().localScale = initialScale;
                            pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            pieceSelected.GetComponent<Collider2D>().enabled = true;
                            pieceSelected = null;
                        }
                    }

                    break;

                case false:
                    // PC
                    if (Input.GetMouseButtonDown(0)) // MOUSE BUTTON BEGAN
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            pieceSelected = null;
                            if (hit.collider.tag == "PuzzlePiece")
                            {
                                pieceSelected = hit.collider.gameObject;
                                pieceSelected.GetComponent<Collider2D>().enabled = false;
                                initialPos = pieceSelected.GetComponent<Transform>().position;
                                initialScale = pieceSelected.GetComponent<Transform>().localScale;
                                pieceSelected.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                                pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 1;
                            }
                        }
                    }

                    else if (Input.GetMouseButton(0) && pieceSelected != null) // MOUSE BUTTON MOVED
                    {
                        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        pieceSelected.GetComponent<Transform>().position = mousePos;
                    }

                    else if (Input.GetMouseButtonUp(0) && pieceSelected != null) // MOUSE BUTTON ENDED
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        RaycastHit2D hit = Physics2D.Raycast(ray, ray);

                        if (hit.collider != null)
                        {
                            if (hit.collider.tag == "PuzzleBoard")
                            {
                                Puzzle_Script puzzleBoard = hit.collider.GetComponent<Puzzle_Script>();
                                if (puzzleBoard.Empty && gameplayManager.CheckPiece(pieceSelected.GetComponent<Puzzle_Script>(), puzzleBoard))
                                {
                                    puzzleBoard.GetComponent<SpriteRenderer>().sprite = pieceSelected.GetComponent<SpriteRenderer>().sprite;
                                    puzzleBoard.Empty = false;
                                    pieceSelected.SetActive(false);
                                }
                            }
                        }
                        pieceSelected.GetComponent<Transform>().position = initialPos;
                        pieceSelected.GetComponent<Transform>().localScale = initialScale;
                        pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 0;
                        pieceSelected.GetComponent<Collider2D>().enabled = true;
                        pieceSelected = null;
                    }
                    break;
            }
        }
    }


    // ******************** Memory Game
    private void MemoryGameUpdate()
    {
        if (gameStarted)
        {
            switch (mobile)
            {
                case true:
                    // MOBILE
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        if (touch.phase == TouchPhase.Began)
                        {
                            var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                            RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                            Debug.DrawLine(ray, ray, Color.red);

                            if (hit.collider != null)
                            {
                                hit.collider.gameObject.GetComponent<Cards_Script>().CardClicked();
                            }
                        }
                    }

                    break;

                case false:
                    // PC
                    if (Input.GetMouseButtonDown(0))
                    {
                        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                        RaycastHit2D hit = Physics2D.Linecast(ray, ray);
                        Debug.DrawLine(ray, ray, Color.red);

                        if (hit.collider != null)
                        {
                            hit.collider.gameObject.GetComponent<Cards_Script>().CardClicked();
                        }
                    }

                    break;
            }
        }
    }

    /// <summary>
    /// Game Started?
    /// </summary>
    public void GameStarted(bool value)
    {
        gameStarted = value;
    }
}
