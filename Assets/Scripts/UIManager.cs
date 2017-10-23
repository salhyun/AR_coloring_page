using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIManager : MonoBehaviour{

	public int mClickCount = 0;
	public GameObject duck;
	public GameObject capsule;
	public GameObject arCamera;
	public Camera cam;

	public GameObject mBackgroundPlane;

	public bool camPause=true;

	public float mDistFromCamera = 100.0f;

	private Texture defaultTexture=null;

	public void onClickButton()
	{
		//Debug.Log("duck pos(" + duck.transform.position.x + ", " + duck.transform.position.y + ", " + duck.transform.position.z + ")");

		//duck.transform.Translate (30.0f, 0.0f, 0.0f);
		//duck.transform.position.Set (duck.transform.position.x+40.0f, duck.transform.position.y, duck.transform.position.z);
		//capsule.transform.position.Set (duck.transform.position.x+40.0f, duck.transform.position.y, duck.transform.position.z);
		//capsule.transform.position.x = capsule.transform.position.x + 50.0f;

		/*
		Vector3 capsulePos = capsule.transform.position;
		Debug.Log("capsule pos(" + capsulePos.x + ", " + capsulePos.y + ", " + capsulePos.z + ")");

		capsule.transform.Translate (1.0f, 0.0f, 0.0f);

		GameObject copyDuck = Instantiate(duck, capsule.transform.position, capsule.transform.rotation) as GameObject;
		Debug.Log ("copyDuck = " + copyDuck.name);

		copyDuck.transform.localScale = duck.transform.localScale * 180.0f;

		Vector3 lookat = arCamera.transform.forward;
		Debug.Log("CameraLookat (" + lookat.x + ", " + lookat.y + ", " + lookat.z + ")");
		*/

//		BackgroundPlaneBehaviour bgBehaviour = mBackgroundPlane.GetComponent<BackgroundPlaneBehaviour> ();
//		bool isActive = bgBehaviour.isActiveAndEnabled;
//		Debug.Log("backgroundEnable = " + isActive);
//		if (isActive)
//			bgBehaviour.enabled = false;
//		else
//			bgBehaviour.enabled = true;

		Debug.Log ("width = " + cam.pixelWidth + ", height = " + cam.pixelHeight + ", depth = " + cam.depth);

		Debug.Log ("ARCamera Pos : " + arCamera.transform.position.ToString ());
		Debug.Log ("ARCamera lookat : " + arCamera.transform.forward.ToString ());

//		bool active = CameraDevice.Instance.IsActive ();
//		if (active) {
//			CameraDevice.Instance.Stop ();
//		}
//		else
//			CameraDevice.Instance.Start ();

		VuforiaRenderer.Instance.Pause (camPause);
		if (camPause) {
			camPause = false;
			//VuforiaRenderer.Instance.VideoBackgroundTexture = Resources.Load ("images/150463757969262") as Texture;

			defaultTexture = VuforiaRenderer.Instance.VideoBackgroundTexture;

			Texture ass = Resources.Load ("images/150463757969262") as Texture;
			VuforiaRenderer.Instance.SetVideoBackgroundTexturePtr (ass, ass.GetNativeTexturePtr ());
		} else {
			camPause = true;

			VuforiaRenderer.Instance.SetVideoBackgroundTexturePtr (defaultTexture, defaultTexture.GetNativeTexturePtr ());
		}

		capsule.transform.position = arCamera.transform.position + arCamera.transform.forward.normalized * mDistFromCamera;
		capsule.transform.rotation = arCamera.transform.rotation;

		Debug.Log ("onClickButton count = " + mClickCount);
		mClickCount++;
	}
}
