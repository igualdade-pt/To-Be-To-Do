using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [Header("Game Properties")]
    [Space]
    [SerializeField]
    private int indexGameSelected;

    private GameObject pieceSelected;

    private Vector3 initialPos;

    private Vector3 initialScale;

    private void Update()
    {
        switch (indexGameSelected)
        {
            case 0:
                MemoryGameUpdate();
                break;

            case 1:
                PuzzleGameUpdate();
                break;
            case 2:
                TagDragGameUpdate();
                break;
            default:
                break;
        }

    }


    private void MemoryGameUpdate()
    {
        // MOBILE
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                Debug.Log(hit.collider);
            }
        }*/

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
    }


    private void PuzzleGameUpdate()
    {
        // MOBILE
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                Debug.Log(hit.collider);
            }
        }*/

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
                    if (puzzleBoard.Empty && puzzleBoard.Index == pieceSelected.GetComponent<Puzzle_Script>().Index)
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

    private void TagDragGameUpdate()
    {
        // MOBILE
        /*if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            var ray = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<SpriteRenderer>().color = colorSelected;
                Debug.Log(hit.collider);
            }
        }*/

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
                    if (pieceSelected.GetComponent<TapDrag_Script>().Index < 4)
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


}
