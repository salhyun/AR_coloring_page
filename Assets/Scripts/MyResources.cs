using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyResources : MonoBehaviour {

	private static int MAX_HELPPANNEL = 3;

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
		spHelpPannels [0] = Resources.Load<Sprite> ("images/qaz");
		spHelpPannels [1] = Resources.Load<Sprite> ("images/illgirl002");
		spHelpPannels [2] = Resources.Load<Sprite> ("images/illgirl003");
	}

	public Sprite getHelpPannel(int num)
	{
		return spHelpPannels [num];
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
