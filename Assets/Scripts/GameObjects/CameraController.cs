using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player; // Mario's Transform
    public Transform startLimit; // GameObject that indicates end of map
    public Transform endLimit; // GameObject that indicates end of map
    public Vector3 startPosition = new Vector3(0.0f, 0.0f, 0.0f);
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;

    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); // the z-component is the distance of the resulting plane from the camera 
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - transform.position.x);
        offset = transform.position.x - player.position.x;
        startX = startLimit.transform.position.x + viewportHalfWidth;
        endX = endLimit.transform.position.x - viewportHalfWidth;
    }

    void Update()
    {
        float desiredX = player.position.x + offset;
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
            transform.position = new Vector3(desiredX, transform.position.y, transform.position.z);
        else
            Debug.Log("startX: " + startX + " endX: " + endX + " desiredX: " + desiredX);
    }

    public void GameRestart()
    {
        transform.position = startPosition;
    }
}
