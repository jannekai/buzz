using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;

    private float currentSpeed;
    private float currentRotSpeed;
    private float currentScaleX;
    private float currentScaleY;
    private float currentScaleZ;

    private float minRotSpeed = 60;
    private float maxRotSpeed = 120;
    private float minScale = 0.8f;
    private float maxScale = 2.0f;

    // Use this for initialization
    void Start() {
        Respawn();
    }

    // Update is called once per frame
    void Update() {
        float rotationSpeed = currentRotSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(-1, 0, 0) * rotationSpeed);

        float amountToMove = currentSpeed * Time.smoothDeltaTime;
        transform.Translate(Vector3.down * amountToMove, Space.World);

        if (transform.position.y < -2) {
            Respawn();
            Player.missed++;
        }
    }

    public void Respawn() {
        minSpeed += 0.2f;
        maxSpeed += 0.2f;

        currentSpeed = Random.Range(minSpeed, maxSpeed);
        currentRotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
        currentScaleX = Random.Range(minScale, maxScale);
        currentScaleY = Random.Range(minScale, maxScale);
        currentScaleZ = Random.Range(minScale, maxScale);
        
        transform.position = new Vector3(Random.Range(-6.0f, 6.0f), 10.0f, 0.0f);
        transform.localScale = new Vector3(currentScaleX, currentScaleY, currentScaleZ);
    }
}
