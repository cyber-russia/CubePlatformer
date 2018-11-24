using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public static PlayerController Instance; 

	public Cube[] cubes;
	Cube ActiveCube;
	int ActiveCubeIndex = 0;


	void InitCubes()
	{
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
		cubes = new Cube[objects.Length];

		for (int i = 0; i < cubes.Length; i++)
		{
			cubes[i] = objects[i].GetComponent<Cube>();
			cubes[i].SetControl(i == 0);
		}


		ActiveCube = cubes[0];
	}


	void ChooseNext()
	{
		if (cubes.Length == 1)
			return;

		//deactivate old
		ActiveCube.SetControl(false);

		//activate new
		ActiveCubeIndex = ActiveCubeIndex+1 > cubes.Length-1 ? 0 : ActiveCubeIndex+1;

		ActiveCube = cubes[ActiveCubeIndex];
		ActiveCube.SetControl(true);
	}

	private void Start()
	{
		InitCubes();
	}


	private void Awake()
	{
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Tab))
			ChooseNext();

		if (Input.GetKeyDown(KeyCode.UpArrow))
			ActiveCube.Jump();

		if (Input.GetKey(KeyCode.RightArrow))
			ActiveCube.Strafe(1);
		else
		if (Input.GetKey(KeyCode.LeftArrow))
			ActiveCube.Strafe(-1);
//		else
//			ActiveCube.MakeSurfaceFriction();
	}
}
