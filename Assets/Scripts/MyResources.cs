using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResources : MonoBehaviour {

	public static int LANGUAGE_KOR = 0;
	public static int LANGUAGE_ENG = 1;
	public static int LANGUAGE_CHA = 2;

	private static int MAX_HELPPANNEL = 3*3;

	private Sprite[] spHelpPannels;

	private static MyResources instance;
	public static MyResources Instance
	{
		get {
			if (instance == null) {
				instance = FindObjectOfType<MyResources> ();
				if (instance == null) {
					GameObject manager = new GameObject ("MyResources");
					instance = manager.AddComponent<MyResources> ();
				}
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
	}

	public void initialize()
	{
		spHelpPannels = new Sprite[MAX_HELPPANNEL];
		spHelpPannels [0] = Resources.Load<Sprite> ("images/manual01_kor");
		spHelpPannels [1] = Resources.Load<Sprite> ("images/manual02_kor");
		spHelpPannels [2] = Resources.Load<Sprite> ("images/manual03_kor");

		spHelpPannels [3] = Resources.Load<Sprite> ("images/manual01_eng");
		spHelpPannels [4] = Resources.Load<Sprite> ("images/manual02_eng");
		spHelpPannels [5] = Resources.Load<Sprite> ("images/manual03_eng");

		spHelpPannels [6] = Resources.Load<Sprite> ("images/manual01_cha");
		spHelpPannels [7] = Resources.Load<Sprite> ("images/manual02_cha");
		spHelpPannels [8] = Resources.Load<Sprite> ("images/manual03_cha");
	}

	public Sprite getHelpPannel(int num, int language)
	{
		return spHelpPannels [num+(language*3)];
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
