using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class CameraSpectate : MonoBehaviour
{

	public GameObject tree;

	Vector2 rotation = new Vector2(0, 0);
	public float speed = 3;
	public float flyspeed;
	void Update()
	{
		rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;

		float V = Input.GetAxis("Vertical");
		float H = Input.GetAxis("Horizontal");

		flyspeed = 20;

		if (Input.GetKey(KeyCode.LeftShift))
		{
			flyspeed = flyspeed * 2;

		}

		transform.position = transform.position + (V*transform.forward *flyspeed* Time.deltaTime);
		transform.position = transform.position + (H * transform.right * flyspeed * Time.deltaTime);




		RaycastHit hit;

		Physics.Raycast(transform.position, transform.forward, out hit, 1000);

		Debug.Log(hit.transform.position);

		//bool hasspawned = false;

			if(Input.GetMouseButtonDown(0))
		{
			Instantiate(tree, transform.position+transform.forward*hit.distance, tree.transform.rotation);
			//hasspawned = true;
		
		}
		
			//if(Input.GetMouseButtonDown


	}
}
