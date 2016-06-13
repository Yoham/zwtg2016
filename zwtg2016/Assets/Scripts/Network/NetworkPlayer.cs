using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class NetworkPlayer : Photon.MonoBehaviour {


	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f;

	void Start () {
	
		if (photonView.isMine) {

			GetComponent<FirstPersonController> ().enabled = true;
            GetComponent<Shooting>().enabled = true;
            //GetComponent<HeadBob> ().enabled = true;

            foreach (Camera cam in GetComponentsInChildren<Camera>()) {
				cam.enabled = true;
			}


		} else {
			StartCoroutine ("UpdateData");
		}

	}

	IEnumerator UpdateData() {
		while (true) {
			transform.position = Vector3.Lerp (transform.position, position, Time.deltaTime * smoothing);
			transform.rotation = Quaternion.Lerp (transform.rotation, rotation, Time.deltaTime * smoothing);

			yield return null;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else {
			position = (Vector3)stream.ReceiveNext ();
			rotation = (Quaternion)stream.ReceiveNext ();
		}
	}
}
