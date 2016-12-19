using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float lifeTime = 1;

	void Start () {
        Destroy(gameObject, lifeTime);
	}

}
