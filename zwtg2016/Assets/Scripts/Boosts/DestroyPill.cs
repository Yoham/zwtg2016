using UnityEngine;
using System.Collections;

public class DestroyPill : MonoBehaviour {

	public float destroyTime = 10.0f;
		
	void Start () {
		Destroy (gameObject, destroyTime);
	}
}
