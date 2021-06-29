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
    private GameObject companionCanvas;

    [SerializeField]
    private GameObject clothesCanvas;

    [SerializeField]
    private GameObject loadingCanvas;

    [SerializeField]
    private GameObject clothesSelectedCanvas;

    [SerializeField]
    private Image clothesSelectedImage;

    [SerializeField]
    private Sprite[] clothesSelectedSprite;

    [SerializeField]
    private Image characterFImage;
    [SerializeField]
    private Image characterMImage;

    [SerializeField]
    private Image [] companionImage;

    [SerializeField]
    private Sprite[] characterFSprite;

    [SerializeField]
    private Sprite[] characterMSprite;

    [SerializeField]
    private Sprite[] companionSprite;

    [SerializeField]
    private Button[] adventureButtons;

    [SerializeField]
    private Sprite[] adventureSprite;

    [Header("Texts")]
    [Space]
    [SerializeField]
    private Text textAdventure;

    [SerializeField]
    private Text textSuperPower;

    [SerializeField]
    private Text textCompanion;

    [SerializeField]
    private Text textClothes;

    [Header("Sound")]
    [SerializeField]
    private int indexSoundWrong;

    [SerializeField]
    private int indexSoundRight;


    private int randomIndex;

    private List<int> adventureIndexes;

    private List<int> adventureButtonIndexes = new List<int>(4);

    private int adventureIndexSelected = -1;

    private int companionIndexSelected = -1;

    private int clothesIndexSelected = -1;

    private void Start()
    {
        mainMenuManager = FindObjectOfType<MainMenuManager>().GetComponent<MainMenuManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();

        adventureCanvas.SetActive(true);
        superPowerCanvas.SetActive(false);
        companionCanvas.SetActive(false);
        clothesCanvas.SetActive(false);
        loadingCanvas.SetActive(false);
        clothesSelectedCanvas.SetActive(false);
        companionImage[0].gameObject.SetActive(false);
        companionImage[1].gameObject.SetActive(false);

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
            companionCanvas.SetActive(false);
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
            if (companionCanvas.activeSelf)
            {
                adventureCanvas.SetActive(false);
                superPowerCanvas.SetActive(true);
                companionCanvas.SetActive(false);
                clothesCanvas.SetActive(false);
            }
            else if (clothesCanvas.activeSelf)
            {
                adventureCanvas.SetActive(false);
                superPowerCanvas.SetActive(false);
                companionCanvas.SetActive(true);
                clothesCanvas.SetActive(false);
            }
            else if (superPowerCanvas.activeSelf)
            {
                adventureCanvas.SetActive(true);
                superPowerCanvas.SetActive(false);
                companionCanvas.SetActive(false);
                clothesCanvas.SetActive(false);
            }
        }
    }



    public void UpdateLanguage(int indexLanguage)
    {
        // Change Title
        switch (indexLanguage)
        {
            case 0:
                // English
                textAdventure.text = "Choose Your Adventure!";
                textSuperPower.text = "Choose Your Superpower!";
                textCompanion.text = "Choose Your Companion!";
                textClothes.text = "Choose Your Clothes!";
                break;

            case 1:
                // Italian
                textAdventure.text = "Scegli La Tua Avventura!";
                textSuperPower.text = "Scegli Il Tuo Superpotere!";
                textCompanion.text = "Scegli Il Tuo Compagno!";
                textClothes.text = "Scegli I Tuoi Vestiti!";
                break;

            case 2:
                // Portuguese
                textAdventure.text = "Escolhe A Tua Aventura!";
                textSuperPower.text = "Escolhe O Teu Superpoder!";
                textCompanion.text = "Escolhe A Tua Companhia!";
                textClothes.text = "Escolhe As Tuas Roupas!!";
                break;

            case 3:
                // Spanish
                textAdventure.text = "Elige Tu Aventura!";
                textSuperPower.text = "Elige Tu Superpoder!";
                textCompanion.text = "Elige Tu Acompañante!";
                textClothes.text = "Elige Tu Ropa!";
                break;

            case 4:
                // Swedish
                textAdventure.text = "Välj Ditt Äventyr!";
                textSuperPower.text = "Välj Din Supermakt!";
                textCompanion.text = "Välj Din Följeslagare!";
                textClothes.text = "Välj Din Kläder!!";
                break;

            default:
                // English
                textAdventure.text = "Choose Your Adventure!";
                textSuperPower.text = "Choose Your Superpower!";
                textCompanion.text = "Choose Your Companion!";
                textClothes.text = "Choose Your Clothes!";
                break;
        }
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
            companionCanvas.SetActive(false);
            clothesCanvas.SetActive(false);

            Debug.Log("Button: " + buttonIndex + ", Adventure: " + adventureButtonIndexes[buttonIndex]);
            adventureIndexSelected = adventureButtonIndexes[buttonIndex];
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
            companionCanvas.SetActive(true);
            clothesCanvas.SetActive(false);

            Debug.Log("SuperPower:" + superPowerIndex);
            mainMenuManager.UpdateSuperPowerSelected(superPowerIndex);
        }
    }

    public void _CompanionButtonSelected(int CompanionIndex)
    {
        if (companionCanvas.activeSelf)
        {
            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companionCanvas.SetActive(false);
            clothesCanvas.SetActive(true);

            companionIndexSelected = CompanionIndex;
            mainMenuManager.UpdateCompanionSelected(CompanionIndex);
            Debug.Log("Companion:" + CompanionIndex);
        }
    }

    public void _ClothesButtonSelected(int clothesIndex)
    {
        if (clothesCanvas.activeSelf)
        {
            if (clothesIndex == adventureIndexSelected)
            {
                // Play Sound
                audioManager.PlayClip(indexSoundRight, 0.6f);
                // ****      
                clothesSelectedImage.sprite = clothesSelectedSprite[0];
            }
            else
            {
                // Play Sound
                audioManager.PlayClip(indexSoundWrong, 0.6f);
                // ****      
                clothesSelectedImage.sprite = clothesSelectedSprite[1];
            }
            clothesSelectedCanvas.SetActive(true);

            // Play Sound
            audioManager.PlayClip(0, 0.6f);
            // ****          

            clothesIndexSelected = clothesIndex;
            mainMenuManager.UpdateClothesSelected(clothesIndex);
            Debug.Log("Clothes:" + clothesIndex);

            StartCoroutine(StartAdventure());
        }
    }

    private IEnumerator StartAdventure()
    {       
        yield return new WaitForSeconds(1f);

        if (clothesIndexSelected == adventureIndexSelected)
        {
            characterFImage.sprite = characterFSprite[clothesIndexSelected];
            characterMImage.sprite = characterMSprite[clothesIndexSelected];
            if (companionIndexSelected < 2)
            {
                companionImage[0].sprite = companionSprite[companionIndexSelected];
                companionImage[0].gameObject.SetActive(true);
            }
            else if (companionIndexSelected == 2)
            {
                companionImage[1].sprite = companionSprite[companionIndexSelected];
                companionImage[1].gameObject.SetActive(true);
            }
            else
            {
                characterFImage.transform.localPosition = new Vector3(-200, characterFImage.transform.localPosition.y, characterFImage.transform.localPosition.z);
                characterMImage.transform.localPosition = new Vector3(200, characterMImage.transform.localPosition.y, characterMImage.transform.localPosition.z);
            }
            

            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companionCanvas.SetActive(false);
            clothesCanvas.SetActive(false);
            clothesSelectedCanvas.SetActive(false);
            loadingCanvas.SetActive(true);

            mainMenuManager.LoadAsyncGamePlay();
        }
        else
        {
            adventureCanvas.SetActive(false);
            superPowerCanvas.SetActive(false);
            companionCanvas.SetActive(false);
            clothesCanvas.SetActive(true);
            clothesSelectedCanvas.SetActive(false);
        }

    }

}
