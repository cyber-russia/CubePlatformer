using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

	public CubeColor color = CubeColor.red;

	[HideInInspector]
	public bool Finished = false;

	
	bool in_air = true;
	bool in_double = false;


	const float jump_velocity = 8.1f;

	const float strafe_velocity = 4f;
	const float strafe_friction = 10f;

	Rigidbody rbody;
	GameObject coneMarker;

	Vector3 start_coord;


	IEnumerator CAnimateJump()
	{
		const float half_anim_time = 0.2f;
		float time = 0;

		Vector3 orig_scale = transform.localScale;
		Vector3 target_scale = new Vector3(orig_scale.x, orig_scale.y * 0.9f, orig_scale.z);

		//scale to target
		while (time < half_anim_time)
		{
			transform.localScale = Vector3.Lerp( orig_scale, target_scale, time/half_anim_time );
			time += Time.deltaTime;

			yield return null;
		}


		//scale back
		while (time >=0)
		{
			transform.localScale = Vector3.Lerp(orig_scale, target_scale, time / half_anim_time);
			time -= Time.deltaTime;

			yield return null;
		}

	}



	public void Jump() {
		if (in_air && in_double)
			return;


		//make first jump
		if (!in_air)
		{
			rbody.velocity = new Vector3(0, jump_velocity, rbody.velocity.z);

			in_air = true;
			StartCoroutine(CAnimateJump());
			return;
		}

		if (!in_double && (rbody.velocity.y > 0))
		{
			rbody.velocity = new Vector3(0, jump_velocity, rbody.velocity.z);

			in_double = true;
		}

	}


	public void Strafe(int direction)
	{
		//add torque
		rbody.velocity = new Vector3(rbody.velocity.x, 
		                             rbody.velocity.y, 
		                             direction*strafe_velocity);

	}


	public void MakeSurfaceFriction() {
		if (!in_air)
		{
			float z_velocity = rbody.velocity.z*(1 - Time.deltaTime * strafe_friction);

			rbody.velocity = new Vector3(0, rbody.velocity.y, z_velocity);
		}
	}


	void RestartLevel()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.position = start_coord;
	}


	public void SetControl(bool new_control)
	{
		coneMarker.SetActive(new_control);

		if (new_control)
			CameraControl.Instance.targetCube = this.gameObject;
	}


	void CheckStanding()
	{
		if (!in_air || rbody.velocity.y > 0)
			return;

		LayerMask pmask = LayerMask.GetMask("platform");

		float y = transform.position.y - 0.5f * transform.localScale.y - 0.03f;
		float z = transform.position.z;

		if (Physics.CheckSphere(new Vector3(0, y, z), .01f, pmask))
		{
			in_air = false;
			in_double = false;
		}
	}


	private void OnCollisionEnter(Collision collision)
	{
		CheckStanding();

		if (collision.gameObject.CompareTag("enemy"))
		{
			RestartLevel();
			return;
		}
			
	}


	private void Start()
	{
		rbody = GetComponent<Rigidbody>();
		
		//save coords
		start_coord = transform.position;
	}


	// Update is called once per frame
	private void FixedUpdate()
	{
		CheckStanding();
		MakeSurfaceFriction();
	}


	private void Awake()
	{
		coneMarker = transform.Find("Cone").gameObject;
	}


	private void OnGUI()
	{
		if (!coneMarker.activeSelf)
			return;

		GUI.Label(new Rect(20, 20, 100, 20), in_air.ToString());
		GUI.Label(new Rect(20, 50, 100, 20), in_double.ToString());

	}


}
