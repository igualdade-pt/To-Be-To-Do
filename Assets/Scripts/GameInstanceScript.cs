using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstanceScript : MonoBehaviour
{
    /// <summary>
    /// Save Variables Between Scene
    /// </summary>

    private int indexAdventure = 0;

    private int indexSuperPower = 0;

    private int indexLanguage = 0;

    private int indexLevelDifficulty = 0;

    /// <summary>
    /// Index of the chosen language
    /// </summary>
    public int LanguageIndex
    {
        get { return indexLanguage; }
        set { indexLanguage = value; }
    }


    /// <summary>
    /// Index of the chosen difficulty ; 0 - Basic , 1 - Expert
    /// </summary>
    public int DifficultyLevelIndex
    {
        get { return indexLevelDifficulty; }
        set { indexLevelDifficulty = value; }
    }


    /// <summary>
    /// Index of the Adventure Selected
    /// </summary>
    public int AdventureIndex
    {
        get { return indexAdventure; }
        set { indexAdventure = value; }
    }

    /// <summary>
    /// Index of the SuperPower Selected; 0 - Courage , 1 - Speed , 2 - Intelligence
    /// </summary>
    public int SuperPowerIndex
    {
        get { return indexSuperPower; }
        set { indexSuperPower = value; }
    }

}
