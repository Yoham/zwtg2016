using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float hitPoints = 100f;
    float currentHitPoints;
    // Use this for initialization
    void Start()
    {
        currentHitPoints = hitPoints;
    }

    [PunRPC]
    public void TakeDamage(float dmg)
    {
        currentHitPoints -= dmg;

        if (currentHitPoints <= 0)
        {
            Die();
        }
    }

     void OnGUI()
    {
        if(GetComponent<PhotonView>().isMine && gameObject.tag == "Player")
        {
            if(GUI.Button(new Rect(Screen.width-100, 0, 100, 40), "Kill Meh"))
            {
                Die();
            }
        }
    } 

    void Die()
    {
        Debug.Log("DIE");
        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else {
            if (GetComponent<PhotonView>().isMine)
            {
                if (gameObject.tag == "Player")
                {
                    NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

                    nm.standbyCamera.SetActive(true);
                    nm.respawnTimer = 3f;
                }

                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

}
