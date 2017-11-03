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

			ImageTargetBehaviour imageTarget = GetComponent<ImageTargetBehaviour> ();
			Debug.LogFormat ("imageTarget name = ", imageTarget);
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
			//targetModel.transform.position = this.transform.position;

			bTrackableFound = true;

			UIManager.Instance.enableButton (true);

			//30프레임 기준으로 InvokeTime 계산
			float invokeTime = 10.0f / FramePerSec.Instance.FPS;
			Debug.Log ("invokeTime = " + invokeTime);
			Invoke ("copyARCameraTexture", invokeTime);
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

			UIManager.Instance.copyCameraTexture (targetModel);
		}

		public void copyARCameraTexture()
		{
			if (targetModel) {
				GetTexture getTex = targetModel.GetComponentInChildren<GetTexture> ();

				RenderTexture rt = getTex.getRenderTexture ();
				Debug.Log("Render Texture width = " + rt.width + ", height = " + rt.height + ", format = " + rt.format.ToString());

				var oldRT = RenderTexture.active;

				var tex = new Texture2D (rt.width, rt.height, TextureFormat.ARGB32, false);
				RenderTexture.active = rt;
				tex.ReadPixels (new Rect (0, 0, rt.width, rt.height), 0, 0);
				tex.Apply ();
				UIManager.Instance.texCameraTarget = tex;

				RenderTexture.active = oldRT;

				if (UIManager.Instance.copyTargetModel) {
					Destroy (UIManager.Instance.copyTargetModel);
					UIManager.Instance.copyTargetModel = null;
				}

				//clone TargetModel
				if (UIManager.Instance.copyTargetModel == null) {
					Vector3 pos = targetModel.transform.position;
					pos.x += 35.0f;
					UIManager.Instance.copyTargetModel = Instantiate (targetModel, pos, Quaternion.identity) as GameObject;
					UIManager.Instance.copyTargetModel.transform.localScale = Vector3.Scale (targetModel.transform.parent.localScale, targetModel.transform.localScale);//부모의 Scale도 계산해준다.
				}

				Debug.Log ("GET " + mTrackableBehaviour.TrackableName + " Camera Texture");
			}
		}
		#endregion // PRIVATE_METHODS
	}

}
