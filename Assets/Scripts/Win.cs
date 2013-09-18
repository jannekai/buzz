using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {
	
    public Texture backgroundTexture;

    void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        if (Time.timeSinceLevelLoad > Constants.MIN_LEVEL_SHOW_TIME_IN_SECONDS && Input.anyKeyDown) {
            Player.Reset();
            Application.LoadLevel(1);
        }
    }
}
