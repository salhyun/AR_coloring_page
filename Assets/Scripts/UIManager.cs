using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIManager : MonoBehaviour{

	public int mClickCount = 0;
	public GameObject duck;
	public GameObject capsule;
	public Camera arCamera;

	public void onClickButton()
	{
		//Debug.Log("duck pos(" + duck.transform.position.x + ", " + duck.transform.position.y + ", " + duck.transform.position.z + ")");

		//duck.transform.Translate (30.0f, 0.0f, 0.0f);
		//duck.transform.position.Set (duck.transform.position.x+40.0f, duck.transform.position.y, duck.transform.position.z);
		//capsule.transform.position.Set (duck.transform.position.x+40.0f, duck.transform.position.y, duck.transform.position.z);
		//capsule.transform.position.x = capsule.transform.position.x + 50.0f;

		Vector3 capsulePos = capsule.transform.position;
		Debug.Log("capsule pos(" + capsulePos.x + ", " + capsulePos.y + ", " + capsulePos.z + ")");

		capsule.transform.Translate (1.0f, 0.0f, 0.0f);

		GameObject copyDuck = Instantiate(duck, capsule.transform.position, capsule.transform.rotation) as GameObject;
		Debug.Log ("copyDuck = " + copyDuck.name);
		Debug.Log ("parent = " + copyDuck.transform.parent.name);

		Vector3 lookat = arCamera.transform.forward;
		Debug.Log("CameraLookat (" + lookat.x + ", " + lookat.y + ", " + lookat.z + ")");

		Debug.Log ("onClickButton count = " + mClickCount);
		mClickCount++;
	}
}
