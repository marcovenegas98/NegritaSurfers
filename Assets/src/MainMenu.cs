using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void NewGame()
	{
		SceneManager.LoadScene(1);
	}	
	
	public void QuitGame()
	{
		Debug.Log("Quit game!!!");
		Application.Quit();
	}
}
