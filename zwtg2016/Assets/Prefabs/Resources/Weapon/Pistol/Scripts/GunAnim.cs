using UnityEngine;
using System.Collections;

public class GunAnim : Photon.MonoBehaviour {

    private Animator anim;


    public GameObject bulletPrefab;
    public GameObject spawn;
    public float force = 1;

    public float timeBetweenShoot = 1;

    private bool isShooting = false;



	// Use this for initialization
	void Start () {
            anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

    }

}
