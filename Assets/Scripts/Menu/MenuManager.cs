using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menus")] public GameObject mainMenu;
    public GameObject levelsMenu;
    public GameObject creditsMenu;
    public GameObject controlsMenu;

    [Header("Others Variables")] public GameObject backGroundLevelsImage;
    private Vector3 makeSmall = Vector3.zero;
    private Vector3 makeNormalSize = new Vector3(0.3f, 0.3f, 0.3f);

    public void TurnOnLvls()
    {
        mainMenu.transform.localScale = makeSmall;
        levelsMenu.transform.localScale = makeNormalSize;
        backGroundLevelsImage.SetActive(true);
    }

    public void CloseLvls()
    {
        mainMenu.transform.localScale = makeNormalSize;
        levelsMenu.transform.localScale = makeSmall;
        backGroundLevelsImage.SetActive(false);
    }

    public void TurnOnCredits()
    {
        mainMenu.transform.localScale = makeSmall;
        creditsMenu.transform.localScale = makeNormalSize;
    }

    public void CloseCredits()
    {
        mainMenu.transform.localScale = makeNormalSize;
        creditsMenu.transform.localScale = makeSmall;
    }

    public void TurnOnControls()
    {
        mainMenu.transform.localScale = makeSmall;
        controlsMenu.transform.localScale = makeNormalSize;
    }

    public void CloseControls()
    {
        mainMenu.transform.localScale = makeNormalSize;
        controlsMenu.transform.localScale = makeSmall;
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManagement.instance.LoadScene(sceneToLoad);
    }

    public void QuitGame()
    {
        SceneManagement.instance.QuitGame();
    }

    public void UpdateLanguage()
    {
        LanguageManager.instance.ChangeLanguage();
    }

}
