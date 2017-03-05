using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float hitPoints = 100f;
    public float currentHitPoints;

    public Texture2D emptyTex;
    public Texture2D fullTex;

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
        if (GetComponent<PhotonView>().isMine && gameObject.tag == "Player")
        {
            if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 40), "Kill Meh"))
            {
                // Debug.Log("BUTTON");
                Die();
            }
        }

        // Draw health bar.
        Vector2 size = new Vector2(Screen.width / 30, Screen.height / 10);
        Vector2 pos = new Vector2(Screen.width / 40, Screen.height * 39 / 40 - size.y);

        for (int i = 0; i < 10; ++i)
        {
            GUI.Box(new Rect(pos.x + size.x * i, pos.y, size.x, size.y), emptyTex);
            if (((int)currentHitPoints) / 10 > i)
            {
                GUI.Box(new Rect(pos.x + size.x * i, pos.y, size.x, size.y), fullTex);
            }
        }
    }

    void Die()
    {
       // Debug.Log("DIE");
        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else {
           // Debug.Log("Die Else");
            if (GetComponent<PhotonView>().isMine)
            {
                //Debug.Log("MINE");
                if (gameObject.tag == "Player")
                {
                    //Debug.Log("Player");
                    NetworkManager nm = GameObject.FindObjectOfType<NetworkManager>();

                    nm.standbyCamera.SetActive(true);
                    nm.respawnTimer = 3f;
                }

                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
