using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
    Rect crosshairRectangle;
    Texture2D crosshairTexture;

	// Use this for initialization
	void Start () {
        crosshairTexture = Resources.Load ("Textures/crosshair") as Texture2D;
        crosshairRectangle = new Rect (Screen.width  / 2 - crosshairTexture.width  / 2,
                                       Screen.height / 2 - crosshairTexture.height / 2,
                                       crosshairTexture.width, crosshairTexture.height);
	}

    // Draws once per frame, like Update() does
    void OnGUI () {
        GUI.DrawTexture (crosshairRectangle, crosshairTexture);
    }
}
