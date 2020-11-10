using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    [SerializeField]
    private int indexGameSelected;

    private GameObject pieceSelected;

    private Vector3 initialPos;

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

            RaycastHit2D hit = Physics2D.Raycast(ray, ray);
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

            RaycastHit2D hit = Physics2D.Raycast(ray, ray);
            Debug.DrawLine(ray, ray, Color.red);

            if (hit.collider != null)
            {
                pieceSelected = null;
                if (hit.collider.tag == "PuzzlePiece")
                {
                    pieceSelected = hit.collider.gameObject;
                    pieceSelected.GetComponent<Collider2D>().enabled = false;
                    pieceSelected.GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    initialPos = pieceSelected.GetComponent<Transform>().position;
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
            pieceSelected.GetComponent<Collider2D>().enabled = true;
            pieceSelected.GetComponent<Transform>().position = initialPos;
            pieceSelected.GetComponent<Transform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
            pieceSelected.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

    }


}
