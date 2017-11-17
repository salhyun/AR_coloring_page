
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class UIManager : MonoBehaviour{

	public int mClickCount = 0;
	public GameObject arCamera;
	public Camera cam;
	public GameObject button;
	public GameObject copyTargetModel;

	public GameObject mBackgroundPlane;
	public Texture mTexAss;

	public bool camPause=true;

	public float mDistFromCamera = 100.0f;

	public Texture texCameraTarget=null;

	private Texture defaultTexture=null;

	public GameObject canvas;
	public GameObject panelSetting;
	public GameObject btnSetting;
	public GameObject btnCloseSetting;
	public GameObject btnHelpAR;
	public GameObject btnHelpHolo;
	public GameObject btnHelpExp;

	public GameObject panelHelp;
	public GameObject btnCloseHelp;

	private SlidingAnimation mSlidingPanel;

	public float SlidingDist=800.0f;

	private float m_fTouchMoveSpeed = 0.25f;
	private bool m_bEnableCopyTargetModel=false;
	private Touch currentTouch;

	private int mCurrentLanguage = MyResources.LANGUAGE_KOR;
	public GameObject LanguageToggleGroup;

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

	public bool enableCopyTargetModel{
		get { return m_bEnableCopyTargetModel; }
		set { m_bEnableCopyTargetModel = enableCopyTargetModel; }
	}


	void Awake()
	{
		//Screen.SetResolution (1080, 1920, true);
		Debug.Log ("SCREEN width = " + Screen.width + ", height = " + Screen.height + ", DPI = " + Screen.dpi);
	}

	void Start()
	{
		MyResources.Instance.initialize ();
		
		mTexAss = Resources.Load ("images/150463757969262") as Texture;

		//enableButton (false);

		mSlidingPanel = new SlidingAnimation ();

		RectTransform rt = canvas.GetComponent<RectTransform> ();
		Debug.Log ("CANVAS width = " + rt.rect.width + ", height = " + rt.rect.height);

		LanguageToggleGroup.GetComponent<BetterToggleGroup> ().OnChange += onChangedlanguageToggleGroup;
	}

	void Update()
	{
		if (mSlidingPanel.getActive ()) {
			mSlidingPanel.process ();

			Vector3 pos = mSlidingPanel.mSlidingObject.transform.position;
			pos.x = mSlidingPanel.currentValue;
			mSlidingPanel.mSlidingObject.transform.position = pos;

//			if (mSlidingPanel.mButtonObject) {
//				UnityEngine.UI.Image img = mSlidingPanel.mButtonObject.GetComponent<UnityEngine.UI.Image> ();
//				Color color = img.color;
//				color.a = 1.0f - mSlidingPanel.curveValue;
//				img.color = color;
//			}
		}

		if (m_bEnableCopyTargetModel) {

			if (Input.touchCount > 0) {
				currentTouch = Input.GetTouch (0);
				if (currentTouch.phase == TouchPhase.Moved) {

					copyTargetModel.transform.Rotate (Vector3.up, -currentTouch.deltaPosition.x*m_fTouchMoveSpeed);
					
					//Debug.Log ("Touch delta = " + currentTouch.deltaPosition.ToString ());
				}
			}
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				Application.Quit ();
			}
		}
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

		//copyARCameraTexture();
		//copyCameraTexture ();

//		pauseARCamera (camPause);
//		if (camPause)
//			camPause = false;
//		else
//			camPause = true;

//		Vector3 pos = duck.transform.position;
//		pos.x += 30.0f;
//		Quaternion rot = duck.transform.rotation;
//
//		GameObject copyDuck = Instantiate (duck, pos, rot) as GameObject;

		Debug.Log ("onClickButton count = " + mClickCount);
		mClickCount++;
	}

	public void copyCameraTexture(GameObject targetModel)
	{
		if (texCameraTarget) {

			if (copyTargetModel) {

				copyTargetModel.SetActive (true);

				Camera cam = arCamera.GetComponentInChildren<Camera> ();
				Vector3 pos = cam.transform.localToWorldMatrix.MultiplyPoint (new Vector3 (0.0f, -40.0f, 0.0f));

				//copyTargetModel.transform.position = arCamera.transform.position + arCamera.transform.forward.normalized * mDistFromCamera;
				copyTargetModel.transform.position = pos + arCamera.transform.forward.normalized * mDistFromCamera;
				copyTargetModel.transform.rotation = arCamera.transform.rotation * Quaternion.AngleAxis (180.0f, new Vector3 (0.0f, 1.0f, 0.0f));

				Renderer []meshes = copyTargetModel.GetComponentsInChildren<Renderer> ();
				foreach (Renderer mesh in meshes) {

					if(mesh.GetComponent<GetTexture>())
						mesh.material.SetTexture ("_MainTex", texCameraTarget);
				}
				//copyTargetModel.GetComponentInChildren<Renderer> ().material.SetTexture ("_MainTex", texCameraTarget);

				m_bEnableCopyTargetModel = true;

				VoiceClips voices = copyTargetModel.GetComponent<VoiceClips> ();
				if (voices) {
					AudioSource audio = GetComponent<AudioSource> ();
					audio.clip = voices.mAudioClips [Random.Range(0, 2)];
					audio.Play ();
				}

			}

			Debug.Log ("SetTexture (\"_MainTex\", texCameraTarget)");
			texCameraTarget = null;
		}
	}

	public void onClickSetting()
	{
		mSlidingPanel.reset (true, panelSetting.transform.position.x, (float)Screen.width / 2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelSetting;
		mSlidingPanel.mButtonObject = btnSetting;
	}
	public void onClickCloseSetting()
	{
		mSlidingPanel.reset (true, panelSetting.transform.position.x, panelSetting.transform.position.x-Screen.width*2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelSetting;
		mSlidingPanel.mButtonObject = btnSetting;

		btnSetting.GetComponent<ButtonAnimation> ().restoreButton ();
	}
	public void onClickHelpAR()
	{
		panelHelp.GetComponent<UnityEngine.UI.Image> ().sprite = MyResources.Instance.getHelpPannel(0, mCurrentLanguage);

		mSlidingPanel.reset (true, panelHelp.transform.position.x, (float)Screen.width / 2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelHelp;
		mSlidingPanel.mButtonObject = null;
	}
	public void onClickCloseHelpAR()
	{
		mSlidingPanel.reset (true, panelHelp.transform.position.x, panelHelp.transform.position.x-(float)Screen.width*2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelHelp;
		mSlidingPanel.mButtonObject = null;
	}
	public void onClickHelpHolo()
	{
		panelHelp.GetComponent<UnityEngine.UI.Image> ().sprite = MyResources.Instance.getHelpPannel(1, mCurrentLanguage);

		mSlidingPanel.reset (true, panelHelp.transform.position.x, (float)Screen.width / 2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelHelp;
		mSlidingPanel.mButtonObject = null;
	}

	public void onClickHelpExp()
	{
		panelHelp.GetComponent<UnityEngine.UI.Image> ().sprite = MyResources.Instance.getHelpPannel(2, mCurrentLanguage);

		mSlidingPanel.reset (true, panelHelp.transform.position.x, (float)Screen.width / 2.0f, 0.5f);
		mSlidingPanel.mSlidingObject = panelHelp;
		mSlidingPanel.mButtonObject = null;
	}

	public void onChangedlanguageToggleGroup(UnityEngine.UI.Toggle newActive)
	{
		if (newActive.name == "kor")
			mCurrentLanguage = MyResources.LANGUAGE_KOR;
		else if (newActive.name == "eng")
			mCurrentLanguage = MyResources.LANGUAGE_ENG;
		else
			mCurrentLanguage = MyResources.LANGUAGE_CHA;

		//Debug.Log(string.Format("changed Language {0}", mCurrentLanguage));
		//Debug.Log(string.Format("LANGUAGE toggle changed {0} selected", newActive.name));
	}

	public void saveJPG(Texture2D texImage, string fileName)
	{
		byte []buffer = texImage.EncodeToJPG ();

		Debug.Log("saveJPG filename = " + Application.dataPath + "/" + fileName);

		System.IO.File.WriteAllBytes (Application.dataPath + "/" + fileName, buffer);

//		System.IO.FileStream fileStream = new System.IO.FileStream (Application.dataPath + "/" + fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
//		fileStream.Write (buffer, 0, 1);
//		fileStream.Close ();
	}


//	void OnGUI()
//	{
//		int w = Screen.width, h = Screen.height;
//
//		int posX = 0, posY = h * 15 / 100;
//		int fontHeight = 3;
//
//		GUIStyle style = new GUIStyle ();
//		Rect rect = new Rect (posX, posY, w, h * fontHeight / 100);
//		style.alignment = TextAnchor.UpperLeft;
//		style.fontSize = h * fontHeight / 100;
//		style.normal.textColor = new Color (1.0f, 1.0f, 0.0f, 1.0f);
//
//		string text = string.Format ("screen(" + w + ", " + h + "), panel x = " + panelSetting.transform.position.x);
//		GUI.Label (rect, text, style);
//	}

}
