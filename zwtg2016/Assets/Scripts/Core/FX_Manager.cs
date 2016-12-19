using UnityEngine;
using System.Collections;

public class FX_Manager : MonoBehaviour
{

    public ParticleSystem hitWallEfect;

    public GameObject sniperBullerFXPrefab;
    [PunRPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("FX");
        /*
        GameObject sniperFX = (GameObject)Instantiate(sniperBullerFXPrefab, startPos, Quaternion.LookRotation(endPos - startPos));

        LineRenderer lr = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
        */
        GameObject pistolBullet = (GameObject)Instantiate(sniperBullerFXPrefab, startPos, Quaternion.LookRotation(endPos - startPos));
        pistolBullet.GetComponent<Rigidbody>().velocity = pistolBullet.transform.forward * 6;


    }

    [PunRPC]
    public void HitWall(float time, string tag, Vector3 sparkDir, Vector3 sparkSpaw)
    {
        StartCoroutine(HitOnTime(time, tag, sparkDir, sparkSpaw));
    }

    IEnumerator HitOnTime(float time, string tag, Vector3 sparkDir, Vector3 sparkSpawn)
    {
        yield return new WaitForSeconds(time);

        if (tag == "map")
        {
            ParticleSystem particle = (ParticleSystem) Instantiate(hitWallEfect, sparkSpawn, Quaternion.LookRotation(sparkDir));
            //Destroy(gameObject);
        }
    }
}
