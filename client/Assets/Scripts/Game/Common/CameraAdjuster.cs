using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour {

    public Rect zoomArea;

    private Camera camera;
    private bool isMoving = false;
    private Vector3 mouseOrigin;
    private float zoomMin = 2.0f;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isMoving = true;
        }

        if(Input.GetMouseButtonUp(0))
            isMoving = false;

        if (isMoving)
        {
            Vector2 diff = Input.mousePosition - mouseOrigin;
            if (Vector2.SqrMagnitude(diff) > 0f)
            {
                Vector3 diffVec3 = new Vector3(diff.x / 1000, 0, diff.y / 1000);
                transform.Translate(transform.position + diffVec3 * Time.deltaTime * 0.02f);
            }
        }
    }
}
