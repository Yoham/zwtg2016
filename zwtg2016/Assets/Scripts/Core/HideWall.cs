using UnityEngine;
using System.Collections;

public class HideWall : MonoBehaviour {


	void OnTriggerEnter(Collider other) {


		gameObject.GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerExit(Collider other) {

		gameObject.GetComponent<Renderer>().enabled = true;


	}
}
