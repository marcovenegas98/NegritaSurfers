using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject pauseMenu;
	private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
		ShowPauseMenu();
		isPaused = false;
	}

	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			isPaused = !isPaused;
			ShowPauseMenu();
		}
    }

	public void ShowPauseMenu()
	{
		pauseMenu.SetActive(isPaused);
		if (isPaused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

	public void NewGame()
	{
        PotatoController.TotalPotatoes = 0;
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{

        PotatoController.TotalPotatoes = 0;
        Debug.Log("Quit game!!!");
		SceneManager.LoadScene(0);
	}
}
