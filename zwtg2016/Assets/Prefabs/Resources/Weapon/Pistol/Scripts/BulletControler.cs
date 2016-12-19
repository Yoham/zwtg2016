using UnityEngine;
using System.Collections;

public class BulletControler : MonoBehaviour {

    public ParticleSystem hitWallEfect;

    public float duration = 5;

    private float FIXEDUPDATE_TIME = 0.02f;
    private float VELOCITY;

    private Ray ray;

    void Start()
    {
        Destroy(gameObject, duration);
        VELOCITY = gameObject.GetComponent<Rigidbody>().velocity.magnitude;
        ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
    }

    void FixedUpdate()
    {
        //Raycast - detekcja kolizji do nastepnego fixedupdate
        //raycast na dystansie jak zostanie pokonany w kroku czasowym
        float distance = VELOCITY * FIXEDUPDATE_TIME;

        ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, distance))
        {
            Vector3 spark = Vector3.Reflect(ray.direction, hit.normal);
            float timeToHit = hit.distance / VELOCITY;
            GameObject.FindObjectOfType<FX_Manager>().GetComponent<PhotonView>().RPC("HitWall", PhotonTargets.All, timeToHit, hit.transform.tag, spark, hit.point);
            Destroy(gameObject);
        }

    }
}
