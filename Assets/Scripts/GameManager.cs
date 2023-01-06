using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager Singleton;
	private List<AsyncOperation> _ScenesLoading = new();

	public void Awake() {
		Singleton = this;
		SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
	}

	public void StartGame() {
		_ScenesLoading.Add(SceneManager.UnloadSceneAsync("Menu"));
		_ScenesLoading.Add(SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive));
		StartCoroutine(_StartGameLoadProgress());
	}

	private IEnumerator _StartGameLoadProgress() {
		for (int i = 0; i < _ScenesLoading.Count; i++) {
			while (!_ScenesLoading[i].isDone) {
				yield return null;
			}
		}
		//idk do level load logic? load player data from prev level?
		// GameObject player = GameObject.FindGameObjectWithTag("Player");
	}
}
