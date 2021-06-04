using PaintCraft.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_GM : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private AudioManager audioManager;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private GameObject gameEndedPanel;

    [SerializeField]
    private GameObject colourPanel;

    [SerializeField]
    private GameObject screenShotPanel;

    [SerializeField]
    private RectTransform rectPaint;

    [SerializeField]
    private LineConfig lineConfig;

    [SerializeField]
    private GameObject returnButton;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>().GetComponent<GameplayManager>();
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        loadingPanel.SetActive(false);
        gameEndedPanel.SetActive(false);
        colourPanel.SetActive(false);
        screenShotPanel.SetActive(false);
        returnButton.SetActive(false);

        StartCoroutine(x());
    }

    private IEnumerator x()
    {
        yield return new WaitForEndOfFrame();

        rectPaint.offsetMin = new Vector2(0, -130);
    }

    public void UpdateLanguage(int indexLanguage)
    {
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
                Debug.Log("UiManager_GM Menu, Unavailable language, English Selected: " + indexLanguage);

                break;
        }
    }

    public void _RestartButtonClicked()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        var x = SceneManager.GetActiveScene();
        gameplayManager.LoadSelectedScene(x.buildIndex);
    }

    public void SetGameEndedPanel(bool value)
    {
        gameEndedPanel.SetActive(value);
    }

    public void SetReturnButton(bool value)
    {
        returnButton.SetActive(value);
    }

    public void _Return()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
        loadingPanel.SetActive(true);
        gameplayManager.LoadSelectedScene(3);
    }


    public void SetColourPanel(bool value)
    {
        colourPanel.SetActive(value);
        lineConfig.scale = 0.6f;
    }

    public void _BrushSizeClicked(float value)
    {
        lineConfig.scale = value;
    }

    public void _ScreenShotButton()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        audioManager.PlayClip(2, 0.6f);
        // ****
        StartCoroutine(CaptureScreen());
    }
    public IEnumerator CaptureScreen()
    {
        // Wait till the last possible moment before screen rendering to hide the UI
        yield return null;
        screenShotPanel.SetActive(true);
        colourPanel.SetActive(false);

        // Wait for screen rendering to complete
        yield return new WaitForEndOfFrame();

        // Take screenshot
        string day = System.DateTime.Now.ToString("dd-MM-yy");
        string hour = System.DateTime.Now.ToString("HH-mm-ss");
        ScreenCapture.CaptureScreenshot("screenshot_" + day + "_" + hour + ".png");

        // Show UI after we're done
        colourPanel.SetActive(true);
        screenShotPanel.SetActive(false);
    }

    public void _PlayButtonSound()
    {
        // Play Sound
        audioManager.PlayClip(0, 0.6f);
        // ****
    }
}
