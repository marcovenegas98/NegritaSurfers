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
	}

	public void NewGame()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Debug.Log("Quit game!!!");
		SceneManager.LoadScene(0);
	}
}
