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
        Debug.Log("Shoting!!");

        Vector3 hitPoint;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Transform hitTransform = findClosestHitObject(ray, out hitPoint);

        if (hitTransform != null)
        {

            Debug.Log("We hit: " + hitTransform.name);

            Health h = hitTransform.GetComponent<Health>();

            while (h == null && hitTransform.parent)
            {
                hitTransform = hitTransform.parent;
                h = hitTransform.GetComponent<Health>();
            }

            if (h != null)
            {
                h.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBuffered, damage);
            }


            if (m_FX_Manager != null)
            {
                m_FX_Manager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitPoint);
            }
        }
        else
        {
            if (m_FX_Manager != null)
            {
                hitPoint = Camera.main.transform.position + (Camera.main.transform.forward * 100f);
                m_FX_Manager.GetComponent<PhotonView>().RPC("SniperBulletFX", PhotonTargets.All, Camera.main.transform.position, hitPoint);
            }
        }


        cooldown = fireRate;
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
