using UnityEngine;
using System.Collections;

public class DestroyHealingAura : MonoBehaviour {

	public float destroyTime = 5.0f;

	void Start () {
		Destroy (gameObject, destroyTime);
	}
}
