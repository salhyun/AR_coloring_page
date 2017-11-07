using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingAnimation {

	private bool mActive = false;
	private float mElapsedTime=0.0f;
	private float mTimeScale = 1.0f;
	private float mCurveScale = 1.0f;
	private float mCurveValue = 0.0f;

	private float mStartValue = 0.0f;
	private float mEndValue = 0.0f;

	private float mCurrentValue = 0.0f;

	public GameObject mSlidingObject;
	public GameObject mButtonObject;

	public float curveValue {
		get { return mCurveValue; }
	}

	public float currentValue {
		get { return mCurrentValue; }
	}

	public void reset(bool active, float startValue, float endValue, float timeScale=1.0f)
	{
		mActive = active;
		mElapsedTime = 0.0f;
		mTimeScale = timeScale;

		mStartValue = startValue;
		mEndValue = endValue;

		mCurveScale = mEndValue - mStartValue;
		mCurveValue = 0.0f;

		Debug.Log ("startValue = " + mStartValue + ", endValue = " + mEndValue + ", curveScale = " + mCurveScale);

		mCurrentValue = 0.0f;
	}

	public void process () {

		if (mActive) {

			mElapsedTime += Time.deltaTime;

			if (mElapsedTime >= mTimeScale) {
				mCurveValue = SinDampingBCurve (1.0f, mCurveScale);
				mActive = false;
			} else {
				mCurveValue = SinDampingBCurve (mElapsedTime/mTimeScale, mCurveScale);
			}

			mCurrentValue = mStartValue + mCurveValue;

			//Debug.Log ("SlidingAnimation Time = " + mElapsedTime + ", curveValue = " + mCurveValue + "\n" + "currentValue = " + mCurrentValue);
		}
	}

	private float SinDampingBCurve(float t, float scale)
	{
		return (t - 0.5f / Mathf.PI * Mathf.Sin (2.0f * Mathf.PI * t)) * scale;
	}

	public bool getActive()
	{
		return mActive;
	}
	public void setActive(bool active)
	{
		mActive = active;
	}

}
