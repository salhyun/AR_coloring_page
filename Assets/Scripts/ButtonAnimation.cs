using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {

	public float speed = 1.0f;
	public AnimationCurve scaleCurve;
	private float delta;
	private float value;

	private Vector3 originScale;

	public float rotSpeed = 1.0f;
	public AnimationCurve rotCurve;
	private float rotDelta;

	// Use this for initialization
	void Start () {

		originScale = this.transform.localScale;
		
	}
	
	// Update is called once per frame
	void Update () {

		delta += speed * Time.deltaTime;

		value = scaleCurve.Evaluate (delta);
		this.transform.localScale = new Vector2 (originScale.x * value, originScale.y * value);

		rotDelta += rotSpeed * Time.deltaTime;
		value = rotCurve.Evaluate (rotDelta);
		this.transform.RotateAround (this.transform.position, new Vector3 (0.0f, 0.0f, 1.0f), 45.0f);

		if (delta >= 1.0f)
			delta = 0.0f;
	}
}
