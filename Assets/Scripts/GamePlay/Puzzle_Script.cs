using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Script : MonoBehaviour
{
    [SerializeField]
    private bool isPiece;

    [SerializeField]
    private int index;

    private bool empty = true;

    private void Start()
    {
        empty = true;
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
