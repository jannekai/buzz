using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Texture backgroundTexture;
    private string instructions = "Instructions: \nLeft and Right Arrows to move.\nPress space to fire.";

	void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);
        GUI.Label(new Rect(10, 10, 200, 200), instructions);

        if (Input.anyKeyDown) {
            Application.LoadLevel(1);
        }
	}
}
