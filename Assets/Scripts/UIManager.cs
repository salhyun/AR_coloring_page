﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIManager : MonoBehaviour{

	public int mClickCount = 0;
	public GameObject duck;
	public GameObject capsule;
	public GameObject arCamera;
	public Camera cam;
	public GameObject button;

	public GameObject mBackgroundPlane;
	public Texture mTexAss;

	public bool camPause=true;

	public float mDistFromCamera = 100.0f;

	public Texture texCameraTarget=null;

	private Texture defaultTexture=null;

	private static UIManager instance;
	public static UIManager Instance
	{
		get {
			if (instance == null) {
				instance = FindObjectOfType<UIManager> ();
				if (instance == null) {
					GameObject manager = new GameObject ("UIManager");
					instance = manager.AddComponent<UIManager> ();
				}
			}
			return instance;
		}
	}

	void Start()
	{
		mTexAss = Resources.Load ("images/150463757969262") as Texture;

		//enableButton (false);
	}

	public void enableButton(bool enable)
	{
		button.SetActive (enable);
		Debug.Log ("button Active=" + button.activeSelf);
	}

	public void pauseARCamera(bool enable)
	{
		VuforiaRenderer.Instance.Pause (enable);
		if (enable == true) {
			defaultTexture = VuforiaRenderer.Instance.VideoBackgroundTexture;

			VuforiaRenderer.Instance.SetVideoBackgroundTexturePtr (mTexAss, mTexAss.GetNativeTexturePtr ());
		} else {
			if(defaultTexture)
				VuforiaRenderer.Instance.SetVideoBackgroundTexturePtr (defaultTexture, defaultTexture.GetNativeTexturePtr ());
		}
	}

	public void centerPosition(GameObject obj)
	{
		obj.transform.position = arCamera.transform.position + arCamera.transform.forward.normalized * mDistFromCamera;
		obj.transform.rotation = arCamera.transform.rotation;
	}

	public void onClickButton()
	{

		Debug.Log ("width = " + cam.pixelWidth + ", height = " + cam.pixelHeight + ", depth = " + cam.depth);

		Debug.Log ("ARCamera Pos : " + arCamera.transform.position.ToString ());
		Debug.Log ("ARCamera lookat : " + arCamera.transform.forward.ToString ());

//		pauseARCamera (camPause);
//		if (camPause)
//			camPause = false;
//		else
//			camPause = true;

		capsule.transform.position = arCamera.transform.position + arCamera.transform.forward.normalized * mDistFromCamera;
		capsule.transform.rotation = arCamera.transform.rotation;

		if(texCameraTarget)
			capsule.GetComponent<Renderer>().material.SetTexture("_MainTex", texCameraTarget);

		Debug.Log ("onClickButton count = " + mClickCount);
		mClickCount++;
	}
}
