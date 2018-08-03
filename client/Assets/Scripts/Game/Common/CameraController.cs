using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Apply draging and zomming controll
/// </summary>

public class CameraController : MonoBehaviour {

    public Vector2 movingArea;
    public float zoomArea;
    public float dragSpeed;
    public float zoomSpeed;

    private bool isMoving = false;
    private Vector3 mouseOrigin;
    private Vector3 lastDragVec;
    private Vector3 curDragVec;
    private Vector3 cameraOrigin;
    private float curZoomDistance;
    
    private void Awake()
    {
        cameraOrigin = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            lastDragVec = new Vector3(0, 0, 0);
            isMoving = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
            

        if (isMoving)
        {
            Vector3 diff = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            if (diff.x != 0 || diff.y != 0)
            {
                Vector3 newPos = new Vector3(diff.x, diff.y, diff.y);
                transform.Translate(-newPos * dragSpeed);
                mouseOrigin = Input.mousePosition;
            }
        }

        //restriant camera in certain area
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraOrigin.x - movingArea.x, cameraOrigin.x + movingArea.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, cameraOrigin.z - movingArea.y, cameraOrigin.z + movingArea.y)
            );

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float diff = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            if (Mathf.Abs(curZoomDistance + diff) < zoomArea)
            {
                curZoomDistance += diff;
                transform.Translate(Vector3.forward * diff);
            }
        }
    }

    private void ApplyRayCast()
    { }
}
