using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramePerSec : MonoBehaviour {

	float deltaTime = 0.0f;
	int fontHeight = 5;
	float mFps;

	private static FramePerSec instance;
	public static FramePerSec Instance
	{
		get {
			if (instance == null) {
				instance = FindObjectOfType<FramePerSec> ();
				if (instance == null) {
					GameObject manager = new GameObject ("FramePerSec");
					instance = manager.AddComponent<FramePerSec> ();
				}
			}
			return instance;
		}
	}

	public float FPS {
		get { return mFps; }
	}

	public float DeltaTime {
		get { return deltaTime; }
	}

	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle ();
		Rect rect = new Rect (0, 0, w, h * fontHeight / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * fontHeight / 100;
		style.normal.textColor = new Color (1.0f, 1.0f, 0.0f, 1.0f);
		float msec = deltaTime * 1000.0f;
		mFps = 1.0f / deltaTime;
		string text = string.Format ("{0:0.0} ms ({1:0.} fps)", msec, mFps);
		GUI.Label (rect, text, style);
	}
}
