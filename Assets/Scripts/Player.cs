using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private enum State
    {
        Playing,
        Explosion,
        Invisible
    }
    
	private static int lives = Constants.PLAYER_LIVES;
    public static int score = 0;
    public static int missed = 0;

    public int maximumMissileCount;
    public float playerSpeed;
    public GameObject missilePrefab;
    public GameObject explosionPrefab;

    private State state = State.Playing;
    private List<GameObject> missiles;

    private const float shipInvisibleTime = 1.5f;
    private const float shipMoveOnToScreenSpeed = 5.0f;
    private const float shipBlinkRate = 0.1f;

    private int numberOfTimesToBlink = 10;
    private int blinkCount;

    private float posX;
    private float posY;
    private float posZ;
    
    void Awake() {
        missiles = new List<GameObject>();
        for (int i = 0; i < maximumMissileCount; i++) {
            GameObject missile = (GameObject)Instantiate(missilePrefab);
            missile.active = false;
            missiles.Add(missile);
        }
    }

    void Start() {
        ResetShipPosition();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        if (state != State.Explosion) {
            posX += playerSpeed * Input.GetAxisRaw("Horizontal") * Time.smoothDeltaTime;
			posX = Math.Max(Math.Min(posX, 7.5f), -7.5f);
			
			posY += playerSpeed * Input.GetAxisRaw("Vertical") * Time.smoothDeltaTime;
			posY = Math.Max(Math.Min(posY, 8.6f), 0.2f);

            transform.position = new Vector3(posX, posY, posZ);
            transform.rotation = Quaternion.Euler(0, Input.GetAxis("Horizontal") * -35.0f, 0);

            if (Input.GetKeyDown("space")) {
                int index = getNextMissile();
                if (index != -1) {
                    missiles[index].transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y / 2.0f, transform.position.z);
                    missiles[index].active = true;
                }
            }
        }
    }

    private int getNextMissile() {
        for (int i = 0; i < missiles.Count; i++) {
            if (missiles[i].active == false) {
                return i;
            }
        }
        return -1;
    }

    void OnTriggerEnter(Collider other) {
        if (state == State.Playing && other.tag == "Enemy") {
            Player.lives--;
            Enemy enemy = other.gameObject.GetComponent<Enemy>();            
            enemy.Respawn();

            StartCoroutine(DestroyShip());
        }
    }

    void OnGUI() {
        GUI.Label(new Rect(10, 10, 200, 20), "Score : " + Player.score.ToString());
        GUI.Label(new Rect(10, 30, 200, 20), "Lives : " + Player.lives.ToString());
        GUI.Label(new Rect(10, 50, 200, 20), "Shots missed : " + Player.missed.ToString());
    }

    IEnumerator DestroyShip() {

        state = State.Explosion;
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.enabled = false;
        transform.position = new Vector3(0.0f, -2.0f, transform.position.z);
        ResetShipPosition();

        yield return new WaitForSeconds(shipInvisibleTime);

        if (Player.lives <= 0) {
            Application.LoadLevel(3);
        } else {
            meshRenderer.enabled = true;

            while (transform.position.y < posY) {
                float amountToMove = shipMoveOnToScreenSpeed * Time.deltaTime;
                transform.position = new Vector3(0.0f, transform.position.y + amountToMove, transform.position.z);
                yield return 0;
            }

            state = State.Invisible;

            blinkCount = 0;
            while (blinkCount < numberOfTimesToBlink) {
                meshRenderer.enabled = !meshRenderer.enabled;
                if (meshRenderer.enabled == true) {
                    blinkCount++;
                }
                yield return new WaitForSeconds(shipBlinkRate);
            }

            state = State.Playing;
        }
    }

    void ResetShipPosition() {
        posX = 0.0f;
        posY = 0.2f;
        posZ = 0.0f;
    }

    public static void Reset() {
        Player.lives = Constants.PLAYER_LIVES;
        Player.missed = 0;
        Player.score = 0;
    }
}
