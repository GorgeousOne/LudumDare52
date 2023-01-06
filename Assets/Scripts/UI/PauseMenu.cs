using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

	[SerializeField] private GameObject background;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private GameObject settingsMenu;
	
	private PlayerControls _controls;
	
	private void OnEnable() {
		_controls = new PlayerControls();
		_controls.Player.Menu.performed += _ => ToggleMenu();
		_controls.Player.Back.performed += _ => GoBackInMenu();
		_controls.Enable();
	}
	
	private void OnDisable() {
		_controls.Disable();
	}
	
	private void ToggleMenu() {
		if (!IsMenuOpen()) {
			OpenMenu();
		} else {
			CloseMenu();
		}
	}
	
	public void OpenMenu() {
		Time.timeScale = 0.0f;
		background.SetActive(true);
		settingsMenu.SetActive(false);
		DisplayMenu(pauseMenu);
	}
	
	public void CloseMenu() {
		background.SetActive(false);
		pauseMenu.SetActive(false);
		settingsMenu.SetActive(false);
		Time.timeScale = 1.0f;
	}

	private void GoBackInMenu() {
		if (settingsMenu.activeSelf) {
			settingsMenu.SetActive(false);
			DisplayMenu(pauseMenu);
		}
	}

	private bool IsMenuOpen() {
		return background.activeSelf;
	}
	
	public void OpenOptions() {
		pauseMenu.SetActive(false);
		DisplayMenu(settingsMenu);
	}

	public void CloseOptions() {
		settingsMenu.SetActive(false);
		DisplayMenu(pauseMenu);
	}
	
	public void QuitToMainMenu() {
		CloseMenu();
		GameManager.Singleton.BackToMenu();
	}
	
	private static void DisplayMenu(GameObject menu) {
		menu.SetActive(true);
		EventSystem.current.SetSelectedGameObject(menu.GetComponentInChildren<Button>().gameObject);
	}
}
