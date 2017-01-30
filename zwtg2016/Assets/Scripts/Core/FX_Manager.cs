using UnityEngine;
using System.Collections;

public class FX_Manager : MonoBehaviour
{

    public ParticleSystem hitWallEfect;
    
    [PunRPC]
    void ShootBullet(Vector3 startPos, int playerID, float force, Quaternion dir)
    {
        Debug.Log("FX");
        GameObject bullet = PhotonView.Find(playerID).gameObject.GetComponent<WeaponsSelector>().getBullet();

        GameObject newBullet = (GameObject)Instantiate(bullet, startPos, dir);
        newBullet.GetComponent<Rigidbody>().velocity = newBullet.transform.forward * force;

    }

    [PunRPC]
    public void HitWall(float time, string tag, Vector3 sparkDir, Vector3 sparkSpaw, float damage)
    {
        StartCoroutine(HitOnTime(time, tag, sparkDir, sparkSpaw, damage));
    }

    IEnumerator HitOnTime(float time, string tag, Vector3 sparkDir, Vector3 sparkSpawn, float damage)
    {
        yield return new WaitForSeconds(time);

        if (tag == "map")
        {
            ParticleSystem particle = (ParticleSystem)Instantiate(hitWallEfect, sparkSpawn, Quaternion.LookRotation(sparkDir));

        }
        /*
        if (tag == "psycho")
        {
            //hit player effect
        }
        */

        //Destroy(gameObject);
    }
}
