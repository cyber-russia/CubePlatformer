using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum CubeColor { red, green, yellow };

public class GameCore : MonoBehaviour {

	public string NextLevel = "";

	public static GameCore Instance;


	public void CheckLevelComplete()
	{
		if (NextLevel == "")
			return;


		foreach (Cube cube in PlayerController.Instance.cubes)
			if (!cube.Finished)
				return;

		//if all cubes are on finish
		SceneManager.LoadScene(NextLevel);

	}


	private void Awake()
	{
		Instance = this;
	}

}
