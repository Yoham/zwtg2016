using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{

    public float fireRate = 0.5f;
    float cooldown = 0;
    public float damage = 25f;
    FX_Manager m_FX_Manager;

    void Start()
    {
        m_FX_Manager = GameObject.FindObjectOfType<FX_Manager>();

    }
    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }
        if (GetComponent<NetworkPlayer>().photonView.isMine)
        {
            Debug.Log("Shoting!!");
            int playerId = GetComponent<NetworkPlayer>().photonView.viewID;
            Vector3 spawn = GetComponent<NetworkPlayer>().transform.Find("FirstPersonCharacter/spawn").transform.position;
            Quaternion dir = GetComponent<NetworkPlayer>().transform.Find("FirstPersonCharacter/spawn").transform.rotation;
            float force = GetComponent<NetworkPlayer>().GetComponent<WeaponsSelector>().getWeaponForce();
            m_FX_Manager.GetComponent<PhotonView>().RPC("ShootBullet", PhotonTargets.All, spawn, playerId, force, dir);
        }
            
          
    }

    Transform findClosestHitObject(Ray ray, out Vector3 hitPoint)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Transform closestHit = null;
        hitPoint = Vector3.zero;
        float distance = 0;

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform != this.transform && (closestHit == null || hit.distance < distance))
            {
                closestHit = hit.transform;
                distance = hit.distance;
                hitPoint = hit.point;
            }
        }
        return closestHit;
    }
}
