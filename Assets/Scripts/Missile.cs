using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{

    public float missileSpeed;
    public GameObject explosionPrefab;

    private Transform cachedTransform;

    // Use this for initialization
    void Start() {
        cachedTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update() {
        float amountToMove = missileSpeed * Time.smoothDeltaTime;
        cachedTransform.Translate(Vector3.up * amountToMove);

        if (cachedTransform.position.y > 10.0f) {
            gameObject.active = false;            
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();

            Instantiate(explosionPrefab, enemy.transform.position, Quaternion.identity);
            enemy.Respawn();
            gameObject.active = false;
            Player.score += 100;

            if (Player.score >= 3000) {
                Application.LoadLevel(2);
            }
        }
    }
}
