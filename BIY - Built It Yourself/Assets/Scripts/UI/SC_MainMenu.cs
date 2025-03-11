using UnityEngine.SceneManagement;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreateWorld;
    public GameObject ListWorld;
    public GameObject OptionsMenu;
    public GameObject CreditsMenu;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton();
    }

    public void ListOfWorldButton()
    {
        ListWorldButton();
    }

    public void PlayNowButton()
    {
        SceneManager.LoadScene("BIY- WORLD"); // Load the game (World) 

    }

    public void SetInfo(string name, string? WorlSize)
    {

    }

    public void OptionsButton()
    {
        // Show Options Menu
        MainMenu.SetActive(false);
        CreateWorld.SetActive(false);
        ListWorld.SetActive(false);
        OptionsMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void CreditsButton()
    {
        // Load the Credits Scene Menu
        //MainMenu.SetActive(false);
        //CreateWorld.SetActive(false);
        //ListWorld.SetActive(false);
        //OptionsMenu.SetActive(false);
        //CreditsMenu.SetActive(true);

        SceneManager.LoadScene("Credits");
    }

    public void CreateWorldButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreateWorld.SetActive(true);
        ListWorld.SetActive(false);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void ListWorldButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreateWorld.SetActive(false);
        ListWorld.SetActive(true);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void MainMenuButton()
    {
        // Show Main Menu
        MainMenu.SetActive(true);
        CreateWorld.SetActive(false);
        ListWorld.SetActive(false);
        OptionsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        // Quit Game
        Application.Quit();
    }
}
