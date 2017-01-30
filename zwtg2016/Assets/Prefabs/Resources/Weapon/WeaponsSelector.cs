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

    void nextWeapon()
    {
        currWeapon = (currWeapon + 1) % weapons.Length;
        initiateWeapon(currWeapon);
    }

    void initiateWeapon(int i)
    {
        Transform weaponCamera = transform.Find("FirstPersonCharacter/WeaponCamera").transform;
        Destroy(currWeaponObject);
        currWeaponObject = Instantiate(weapons[i], weaponCamera.position, weaponCamera.rotation) as GameObject;

        currWeaponObject.transform.parent = weaponCamera;

        if (GetComponent<NetworkPlayer>().photonView.isMine)
        {
            Debug.Log("layerChange to 8");
            currWeaponObject.layer = 8;
            foreach (Transform element in currWeaponObject.transform)
            {
                element.gameObject.layer = 8;
                foreach (Transform part in element)
                {
                    part.gameObject.layer = 8;
                }
            }
        }
    }

    public GameObject getBullet()
    {
        return currWeaponObject.GetComponent<GunAnim>().bulletPrefab;
    }
    public float getWeaponForce()
    {
        return currWeaponObject.GetComponent<GunAnim>().force;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<NetworkPlayer>().photonView.isMine)
        {
            if(Input.GetAxis("Mouse ScrollWheel")!=0)
            {
                nextWeapon();
            }
        }

    }
}
