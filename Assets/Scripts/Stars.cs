using UnityEngine;
using System.Collections;

public class Stars : MonoBehaviour {

    private float speed = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float amountToMove = speed * Time.deltaTime;
        transform.Translate(Vector3.down * amountToMove, Space.World);

        if (transform.position.y < -6) {
            transform.position = new Vector3(transform.position.x, 14, transform.position.z);
        }
	}
}
