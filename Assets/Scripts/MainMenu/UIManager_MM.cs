using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    [SerializeField]
    private GameObject adventureCanvas;

    [SerializeField]
    private GameObject superPowerCanvas;

    [SerializeField]
    private GameObject companyCanvas;

    [SerializeField]
    private Button[] adventureButtons;

    [SerializeField]
    private Sprite[] adventureSprite;

    private int randomIndex;

    private List<int> adventureIndexes;

    private List<int> adventureButtonIndexes = new List<int>(4);


    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        adventureCanvas.SetActive(true);
        superPowerCanvas.SetActive(false);
        companyCanvas.SetActive(false);

        adventureIndexes = new List<int>(adventureSprite.Length);
        for (int i = 0; i < adventureSprite.Length; i++)
        {
            adventureIndexes.Add(i);
        }


        for (int i = 0; i < adventureButtons.Length; i++)
        {
            randomIndex = Random.Range(0, adventureIndexes.Count);
            adventureButtons[i].image.sprite = adventureSprite[adventureIndexes[randomIndex]];
            Debug.Log("i: " + i + ", Adventure?: " + adventureIndexes[randomIndex]);
            adventureButtonIndexes.Add(adventureIndexes[randomIndex]);
            adventureIndexes.Remove(adventureIndexes[randomIndex]);
        }
    }


    public void _InformationButtonClicked()
    {

    }

    public void _LanguageButtonClicked(int indexScene)
    {
        Debug.Log("Language Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _AgeButtonClicked(int indexScene)
    {
        Debug.Log("Age Clicked, Index Scene: " + indexScene);

        mainMenuManager.LoadScene(indexScene);
    }

    public void _BooksButtonClicked()
    {

    }

    public void _SoundButtonClicked()
    {

    }


    public void UpdateLanguage(int indexLanguage)
    {

    }

    /*public void UpdadeLevelButtons(int unlockedLevels)
    {
        // Set All Buttons Lock
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }

        // Set Only the Buttons Unlocked
        int unlock = unlockedLevels + 1;
        Mathf.Clamp(unlock, 0, levelButtons.Length);
        Debug.Log("Unlocked Levels:  " + unlock);
        for (int j = 0; j < unlock; j++)
        {
            levelButtons[j].SetActive(true);
            if (j >= unlock-1)
            {
                LeanTween.scale(levelButtons[j], levelButtons[j].transform.localScale * 1.2f, 0.5f).setLoopPingPong();
            }
        }
    }*/

    public void _AdventureButtonSelected(int buttonIndex)
    {
        adventureCanvas.SetActive(false);
        superPowerCanvas.SetActive(true);
        companyCanvas.SetActive(false);

        Debug.Log("Button: " + buttonIndex + ", Adventure: " + adventureButtonIndexes[buttonIndex]);
        mainMenuManager.UpdateAdventureSelected(adventureButtonIndexes[buttonIndex]);
    }

    public void _SuperPowerButtonSelected(int superPowerIndex)
    {
        adventureCanvas.SetActive(false);
        superPowerCanvas.SetActive(false);
        companyCanvas.SetActive(true);

        Debug.Log("SuperPower:" + superPowerIndex);
        mainMenuManager.UpdateSuperPowerSelected(superPowerIndex);
    }

    public void _CompanyButtonSelected(int CompanyIndex)
    {
        adventureCanvas.SetActive(false);
        superPowerCanvas.SetActive(false);
        companyCanvas.SetActive(false);

        mainMenuManager.UpdateCompanySelected(CompanyIndex);
        Debug.Log("Company:" + CompanyIndex);

        mainMenuManager.LoadAsyncGamePlay();
    }

}
