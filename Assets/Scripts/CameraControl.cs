using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	
	public GameObject targetCube;
    public static CameraControl Instance;


	void FollowCube()
	{
		if (targetCube == null)
			return;

		float y = Mathf.Lerp(transform.position.y, targetCube.transform.position.y+2, 0.03f);
		float z = Mathf.Lerp(transform.position.z, targetCube.transform.position.z, 0.07f);

		transform.position = new Vector3( transform.position.x, y, z);	
	}


	private void Awake()
	{
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		FollowCube();
	}
}
