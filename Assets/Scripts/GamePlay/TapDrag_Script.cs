using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapDrag_Script : MonoBehaviour
{
    private int index = 0;

    [Header("Note: Correct Ones First")]
    [Space]
    [SerializeField]
    private Sprite[] itemsCake;

    [SerializeField]
    private Sprite[] itemsDog;

    [SerializeField]
    private Sprite[] itemsDrums;

    public void SetSprite(int indexScenario)
    {
        Debug.Log(indexScenario);
        switch (indexScenario)
        {
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = itemsCake[index];
                break;
            case 7:
                gameObject.GetComponent<SpriteRenderer>().sprite = itemsDog[index];
                break;
            case 12:
                gameObject.GetComponent<SpriteRenderer>().sprite = itemsDrums[index];
                break;
            default:
                gameObject.GetComponent<SpriteRenderer>().sprite = itemsCake[index];
                break;
        }
    }

    public int Index
    {
        get { return index; }
        set { index = value; }
    }
}
