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

		public GameObject duck;
		public GameObject capsule;
		public Camera ARCamera;

		#region UNTIY_MONOBEHAVIOUR_METHODS

		void Start()
		{
			mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			if (mTrackableBehaviour)
			{
				mTrackableBehaviour.RegisterTrackableEventHandler(this);
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

			Debug.Log("duck Trackable " + mTrackableBehaviour.TrackableName + " found");

			Debug.Log ("ImageTargetPos : " + this.transform.position.ToString());
			duck.transform.position = this.transform.position;
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

//			Vector3 pos = ARCamera.transform.position + ARCamera.transform.forward.normalized * 50.0f;
//			duck.transform.position = capsule.transform.position;
//
//			GameObject copyDuck = Instantiate(duck, pos, capsule.transform.rotation) as GameObject;
//			Debug.Log ("copyDuck = " + copyDuck.name);
		}

		#endregion // PRIVATE_METHODS
	}
}
