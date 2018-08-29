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

    //Movement state flag
    private Vector3 mouseOrigin;
    private Vector3 cameraOrigin;
    private float curZoomDistance;
    
    //Mouse state flag
    private bool isOverUI = false;
    private bool isDragging = false;
    private bool isPress = false;

    //click event related
    private RaycastHit hit;
    private GameObject hitGo;
    private int layerMask;

    Vector2 oldTouchPos0;
    Vector2 oldTouchPos1;


    private void Awake()
    {
        cameraOrigin = transform.position;
        layerMask = LayerMask.GetMask("Building");
    }

    private void Update()
    {
        isOverUI = UICamera.isOverUI;

        #if UNITY_ANDROID || UNITY_IOS
            ProcessTouch();
        #else
            ProcessMouse();
        #endif
        
    }


    void ProcessMouse()
    {
        if (!isOverUI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseOrigin = Input.mousePosition;
                isPress = true;
                OnClickDown();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnClickUp();
                ClearState();
            }


            if (isPress)
            {
                Vector3 diff = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                if (diff.x != 0 || diff.y != 0)
                {
                    if(!isDragging) isDragging = true;
                    CameraDragging(diff);
                }
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                CameraZomming(Input.GetAxis("Mouse ScrollWheel"));
            }
        }

        CameraRestriant();
    }

    void ProcessTouch()
    {
        if(isOverUI) return;
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            isPress = true;
            OnClickDown();
        }

        if (Input.GetMouseButtonUp(0))
        {
            OnClickUp();
            ClearState();
        }

        if (isPress)
        {
            Vector3 diff = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
            if (diff.x != 0 || diff.y != 0)
            {
                if(!isDragging) isDragging = true;
                CameraDragging(diff);
            }
        }
        
        if(Input.touchCount > 1)
        {
            if(Input.GetTouch(0).phase ==  TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                var touchPos0 = Input.GetTouch(0).position;
                var touchPos1 = Input.GetTouch(1).position;
                if(IsEnlarge(oldTouchPos0, oldTouchPos1, touchPos0, touchPos1))
                {
                    CameraZomming(0.1f);
                }
                else
                {
                    CameraZomming(-0.1f);
                }
                oldTouchPos0 = touchPos0;
                oldTouchPos1 = touchPos1;
            }
        }
    }

    bool IsEnlarge(Vector2 oldPos0, Vector2 oldPos1, Vector2 newPos0, Vector2 newPos1)
    {
        float oldLen = (oldPos0.x - oldPos1.x) * (oldPos0.x - oldPos1.x) + (oldPos0.y - oldPos1.y) * (oldPos0.y - oldPos1.y);
        float newLen = (newPos0.x - newPos1.x) * (newPos0.x - newPos1.x) + (newPos0.y - newPos1.y) * (newPos0.y - newPos1.y);

        if(newLen > oldLen)
            return true;
        else
            return false;
    }

    void ClearState()
    {
        isPress = false;
        isDragging = false;
    }


    #region CameraMovement

    public void CameraDragging(Vector3 diff)
    {
        Vector3 movement = new Vector3(diff.x, diff.y, diff.y);
        transform.Translate(-movement * dragSpeed);
        mouseOrigin = Input.mousePosition;
    }

    public void CameraZomming(float zoomDiff)
    {
        if (Mathf.Abs(curZoomDistance + zoomDiff * zoomSpeed) < zoomArea)
        {
            curZoomDistance += zoomDiff;
            transform.Translate(Vector3.forward * zoomDiff * zoomSpeed);
        }
    }

    /// <summary>
    /// Restriant camera in certain area
    /// </summary>
    public void CameraRestriant()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraOrigin.x - movingArea.x, cameraOrigin.x + movingArea.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, cameraOrigin.z - movingArea.y, cameraOrigin.z + movingArea.y)
            );
    }
    #endregion

    #region Click Event
    private void OnClickDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200, layerMask))
        {
            hitGo = hit.collider.gameObject;
            hitGo.SendMessage("OnPress", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnClickUp()
    {
        if (isDragging) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200, layerMask))
        {
            if (hitGo == hit.collider.gameObject)
            {
                hitGo.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    #endregion

}
