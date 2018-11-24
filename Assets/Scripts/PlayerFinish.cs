using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{

	public CubeColor cubeColor = CubeColor.red;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Cube cube = other.GetComponent<Cube>();

			if (cube.color == cubeColor)
			{
				cube.Finished = true;
				GameCore.Instance.CheckLevelComplete();
			}

		}
	}


	private void OnTriggerExit(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			Cube cube = other.GetComponent<Cube>();
			cube.Finished = false;
		}
	}


}
