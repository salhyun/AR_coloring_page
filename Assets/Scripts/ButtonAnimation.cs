using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {

	public float ScaleSpeed = 1.0f;
	public float rotSpeed = 480.0f;
	public AnimationCurve scaleCurve;
	private float delta;
	private float value;

	private Vector3 originScale;
	private Vector3 centerPosition;

	private bool bAnimation = false;

	// Use this for initialization
	void Start () {

		originScale = this.transform.localScale;
		bAnimation = false;

		centerPosition = this.transform.position;

//		Debug.Log ("Button Name = " + this.name);
//		Debug.Log ("transform.position = " + this.transform.position.ToString ());
//		Debug.Log ("centerPosition = " + centerPosition.ToString());
//		Debug.Log ("<color=red>rect width = " + rt.rect.width + ", height = " + rt.rect.height + ", pivot = " + rt.pivot.ToString() + "</color>");
//
//		string msg = string.Format ("<color=red>rect left={0:F}, top={0:F}</color>", rt.rect.x, rt.rect.y);
//		Debug.Log (msg);
	}

	Vector3 getCenterPosition()
	{
		RectTransform rt = GetComponent<RectTransform> ();

		Vector3 pos = this.transform.position;
		pos.x += (rt.rect.width * (0.5f-rt.pivot.x));
		pos.y += (rt.rect.height * (0.5f-rt.pivot.y));

		return pos;
	}
	
	// Update is called once per frame
	void Update () {

		if (bAnimation)
			process ();

	}

	void process()
	{
		delta += ScaleSpeed * Time.deltaTime;

		value = scaleCurve.Evaluate (delta);
		this.transform.localScale = new Vector2 (originScale.x * value, originScale.y * value);

		this.transform.RotateAround (centerPosition, new Vector3 (0.0f, 0.0f, -1.0f), rotSpeed*Time.deltaTime);

		if (delta >= 1.0f) {
			bAnimation = false;
			restoreButton ();
		}
	}

	void restoreButton()
	{
		this.transform.localScale = originScale;
		this.transform.rotation = new Quaternion ();
		bAnimation = false;
		delta = 0.0f;
	}

	public void onClickButton()
	{
		bAnimation = true;
	}
}
