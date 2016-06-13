using UnityEngine;
using System.Collections;

public class FX_Manager : MonoBehaviour
{

    public GameObject sniperBullerFXPrefab;
    [PunRPC]
    void SniperBulletFX(Vector3 startPos, Vector3 endPos)
    {
        Debug.Log("FX");

        GameObject sniperFX = (GameObject)Instantiate(sniperBullerFXPrefab, startPos, Quaternion.LookRotation(endPos - startPos));

        LineRenderer lr = sniperFX.transform.Find("LineFX").GetComponent<LineRenderer>();
        lr.SetPosition(0, startPos);
        lr.SetPosition(1, endPos);
    }
}
