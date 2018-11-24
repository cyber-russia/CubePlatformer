using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{

	public CubeColor cubeColor = CubeColor.red;



	IEnumerator CAnimateSnap(Cube cube) 
	{
		const float anim_time = 1f;
		float time = anim_time;

		while (time > 0)
		{
			cube.transform.position = Vector3.Lerp(cube.transform.position, transform.position, (anim_time - time) / time);
			time -= Time.deltaTime;

			yield return 0;
		}

		//finally apply finish state
		cube.Finished = true;
		GameCore.Instance.CheckLevelComplete();
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Cube cube = other.GetComponent<Cube>();

			if (cube.color == cubeColor)
				StartCoroutine(CAnimateSnap(cube));

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
