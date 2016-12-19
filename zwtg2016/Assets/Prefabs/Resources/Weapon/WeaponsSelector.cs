using UnityEngine;
using System.Collections;

public class WeaponsSelector : MonoBehaviour {

    public GameObject[] weapons;
    private GameObject currWeaponObject;
    private int currWeapon;

	// Use this for initialization
	void Start () {

        currWeaponObject = Instantiate(weapons[0], gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        currWeapon = 0;

        currWeaponObject.transform.parent = transform.Find("FirstPersonCharacter/WeaponCamera").transform;

        if (GetComponent<NetworkPlayer>().photonView.isMine)
        {
            Debug.Log("layerChange to 8");
            currWeaponObject.layer = 8;
            foreach(Transform element in currWeaponObject.transform)
            {
                element.gameObject.layer = 8;
                foreach (Transform part in element)
                {
                    part.gameObject.layer = 8;
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
