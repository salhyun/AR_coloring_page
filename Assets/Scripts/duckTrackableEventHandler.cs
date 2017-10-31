using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vuforia
{
	public class duckTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
	{
		#region PRIVATE_MEMBER_VARIABLES

		private TrackableBehaviour mTrackableBehaviour;

		#endregion // PRIVATE_MEMBER_VARIABLES

		public GameObject targetModel;
		public GameObject capsule;
		public Camera ARCamera;

		public GameObject childModel=null;

		private bool bTrackableFound = false;

		#region UNTIY_MONOBEHAVIOUR_METHODS

		void Start()
		{
			Debug.Log ("duckTrackableEventHandler Start()");

			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
			}
			Debug.Log ("mTrackableBehaviour = " + mTrackableBehaviour);

			Transform[] children = GetComponentsInChildren<Transform> ();
			foreach (Transform child in children) {
				//Debug.Log ("child name = " + child.name);
				if (child.name.Contains ("Box008")) {
					childModel = child.gameObject;
					Debug.Log ("childModel name = " + childModel.name);
				}
			}
		}

		#endregion // UNTIY_MONOBEHAVIOUR_METHODS



		#region PUBLIC_METHODS

		/// <summary>
		/// Implementation of the ITrackableEventHandler function called when the
		/// tracking state changes.
		/// </summary>
		public void OnTrackableStateChanged(
			TrackableBehaviour.Status previousStatus,
			TrackableBehaviour.Status newStatus)
		{
			if (newStatus == TrackableBehaviour.Status.DETECTED ||
				newStatus == TrackableBehaviour.Status.TRACKED ||
				newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
			{
				OnTrackingFound();
			}
			else
			{
				OnTrackingLost();
			}
		}

		#endregion // PUBLIC_METHODS



		#region PRIVATE_METHODS


		private void OnTrackingFound()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Enable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = true;
			}

			// Enable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = true;
			}

			Debug.Log("targetModel Trackable " + mTrackableBehaviour.TrackableName + " found");

			Debug.Log ("ImageTargetPos : " + this.transform.position.ToString());
			targetModel.transform.position = this.transform.position;

			bTrackableFound = true;

			UIManager.Instance.enableButton (true);

			if(childModel)
				UIManager.Instance.texCameraTarget = childModel.GetComponent<Renderer> ().material.GetTexture ("_MainTex");
		}


		private void OnTrackingLost()
		{
			Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
			Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

			// Disable rendering:
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

			// Disable colliders:
			foreach (Collider component in colliderComponents)
			{
				component.enabled = false;
			}

			Debug.Log("duck Trackable " + mTrackableBehaviour.TrackableName + " lost");

			Debug.Log ("ARCamera lookat : " + ARCamera.transform.forward.ToString ());
			Debug.Log ("ARCamera pos : " + ARCamera.transform.position.ToString ());
			Debug.Log ("capsule pos : " + capsule.transform.position.ToString ());

//			if (bTrackableFound) {
//				UIManager.Instance.pauseARCamera (true);
//				bTrackableFound = false;
//			}


//			Vector3 pos = ARCamera.transform.position + ARCamera.transform.forward.normalized * 50.0f;
//			duck.transform.position = capsule.transform.position;
//
//			GameObject copyDuck = Instantiate(duck, pos, capsule.transform.rotation) as GameObject;
//			Debug.Log ("copyDuck = " + copyDuck.name);
		}

		#endregion // PRIVATE_METHODS
	}
}
