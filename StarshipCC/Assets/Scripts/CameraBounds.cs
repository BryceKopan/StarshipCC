using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour {

    public Camera camera = null;
    EdgeCollider2D leftBound, rightBound, topBound, bottomBound;

	// Use this for initialization
	void Start () {
        leftBound = gameObject.AddComponent<EdgeCollider2D>();
        rightBound = gameObject.AddComponent<EdgeCollider2D>();
        topBound = gameObject.AddComponent<EdgeCollider2D>();
        bottomBound = gameObject.AddComponent<EdgeCollider2D>();

        if(camera == null) {
            camera = Camera.main;
        }

        CalculateSize();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CalculateSize() {

        //Actually half the width and half the height
        float cameraHeight = camera.orthographicSize;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;

        Vector2[] leftBoundPoints = { new Vector2(-cameraWidth, -cameraHeight), new Vector2(-cameraWidth, cameraHeight) };
        leftBound.points = leftBoundPoints;

        Vector2[] rightBoundPoints = { new Vector2(cameraWidth, -cameraHeight), new Vector2(cameraWidth, cameraHeight) };
        rightBound.points = rightBoundPoints;

        Vector2[] topBoundPoints = { new Vector2(-cameraWidth, cameraHeight), new Vector2(cameraWidth, cameraHeight) };
        topBound.points = topBoundPoints;

        Vector2[] bottomBoundPoints = { new Vector2(-cameraWidth, -cameraHeight), new Vector2(cameraWidth, -cameraHeight) };
        bottomBound.points = bottomBoundPoints;
    }
}
