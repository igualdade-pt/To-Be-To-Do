using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_MM : MonoBehaviour
{
    private MainMenuManager mainMenuManager;

    private AudioManager audioManager;

    [SerializeField]
    private GameObject adventureCanvas;

    [SerializeField]
    private GameObject superPowerCanvas;

    [SerializeField]
    private GameObject companyCanvas;

    [SerializeField]
    private GameObject clothesCanvas;

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
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        adventureCanvas.SetActive(true);
        superPowerCanvas.SetActive(false);
        companyCanvas.SetActive(false);
        clothesCanvas.SetActive(false);

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

    public void _SettingsButtonClicked(int indexScene)
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        mainMenuManager.LoadScene(indexScene);
    }

    public void _RestartButtonClicked()
    {
        if (!adventureCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(true);
            superPowerCanvas.SetActive(false);
            companyCanvas.SetActive(false);
            clothesCanvas.SetActive(false);
        }
    }

    public void _ReturnButtonClicked()
    {        
        if (!adventureCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            if (companyCanvas.activeSelf)
            {
                adventureCanvas.SetActive(false);
                superPowerCanvas.SetActive(true);
                companyCanvas.SetActive(false);
                clothesCanvas.SetActive(false);
            }
            else if (clothesCanvas.activeSelf)
            {
                adventureCanvas.SetActive(false);
                superPowerCanvas.SetActive(false);
                companyCanvas.SetActive(true);
                clothesCanvas.SetActive(false);
            }            
        }
    }



    public void UpdateLanguage(int indexLanguage)
    {

    }


    public void _AdventureButtonSelected(int buttonIndex)
    {
        if (adventureCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(true);
            companyCanvas.SetActive(false);
            clothesCanvas.SetActive(false);

            Debug.Log("Button: " + buttonIndex + ", Adventure: " + adventureButtonIndexes[buttonIndex]);
            mainMenuManager.UpdateAdventureSelected(adventureButtonIndexes[buttonIndex]);
        }
    }

    public void _SuperPowerButtonSelected(int superPowerIndex)
    {
        if (superPowerCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companyCanvas.SetActive(true);
            clothesCanvas.SetActive(false);

            Debug.Log("SuperPower:" + superPowerIndex);
            mainMenuManager.UpdateSuperPowerSelected(superPowerIndex);
        }
    }

    public void _CompanyButtonSelected(int CompanyIndex)
    {
        if (companyCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companyCanvas.SetActive(false);
            clothesCanvas.SetActive(true);

            mainMenuManager.UpdateCompanySelected(CompanyIndex);
            Debug.Log("Company:" + CompanyIndex);
        }
    }

    public void _ClothesButtonSelected(int ClothesIndex)
    {
        if (clothesCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companyCanvas.SetActive(false);
            clothesCanvas.SetActive(false);

            //mainMenuManager.UpdateCompanySelected(CompanyIndex);
            Debug.Log("Clothes:" + ClothesIndex);

            mainMenuManager.LoadAsyncGamePlay();
        }
    }

}
