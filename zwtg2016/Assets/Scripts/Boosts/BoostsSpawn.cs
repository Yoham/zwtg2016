using UnityEngine;
using System.Collections;

public class BoostsSpawn : MonoBehaviour {

	public Transform[] SpawnPoints;
	public float spawnTime = 3.0f;


	public GameObject[] Pills;
	//public GameObject Coins;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnPills", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnPills() {
		int spawnIndex = Random.Range (0, SpawnPoints.Length);
		int pillIndex = Random.Range (0, Pills.Length);
		Instantiate (Pills[pillIndex], SpawnPoints [spawnIndex].position, SpawnPoints [spawnIndex].rotation);
	}

}
