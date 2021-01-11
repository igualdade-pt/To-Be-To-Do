using System.Collections;
using System.Collections.Generic;
using UnityEngine;






/// <summary>
///  DELETE
/// </summary>







public class Drag_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject brush;

    [SerializeField]
    private Gradient colorOfLine;

    private LineRenderer currentLineRenderer;

    private Vector2 lastPos;

    private void Update()
    {
        Drawing();

    }

    void Drawing()
    {
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
                if (hit.collider.tag == "Maze" || hit.collider.tag == "MazeBeginning")
                {
                    PointToMousePos();
                }
                else if (hit.collider.tag == "MazeEnd")
                {
                    // WIN
                }          
            }
            else
            {
                //LOST
                currentLineRenderer.gameObject.SetActive(false);
                currentLineRenderer = null;
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
            currentLineRenderer = null;
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

}
