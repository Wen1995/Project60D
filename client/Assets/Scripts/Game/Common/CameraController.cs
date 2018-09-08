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

    public float zoomInMax;
    public float zoomOutMax;

    [Tooltip("Rect Area To Constrict Camera's Movement")]
    public Transform bottomLeft;
    public Transform topRight;

    //Movement state flag
    private Vector3 mouseOrigin;
    private Vector3 cameraOrigin;
    private float curZoomDistance = 0;
    
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
                FacadeSingleton.Instance.SendEvent("CloseInteraction");
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
        
        if(Input.touchCount == 2)
        {
            var touchPos0 = Input.GetTouch(0);
            var touchPos1 = Input.GetTouch(1);
            var preTouch0 = touchPos0.position - touchPos0.deltaPosition;
            var preTouch1 = touchPos1.position - touchPos1.deltaPosition;
            float preTouchDeltaMag = (preTouch0 - preTouch1).magnitude;
            float curTouchDeltaMag = (touchPos0.position - touchPos1.position).magnitude;
            if(curTouchDeltaMag > preTouchDeltaMag)
                CameraZomming(0.1f);
            else if(curTouchDeltaMag < preTouchDeltaMag)
                CameraZomming(-0.1f);
        }
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
        float newZoomDistance = curZoomDistance + zoomDiff * zoomSpeed;
        if(newZoomDistance <= zoomInMax && newZoomDistance >= -zoomOutMax)
        {
            transform.Translate(Vector3.forward * zoomDiff * zoomSpeed);
            curZoomDistance = newZoomDistance;
        }
    }

    /// <summary>
    /// Restriant camera in certain area
    /// </summary>
    public void CameraRestriant()
    {
        //camera.ViewportToWorldPoint()
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeft.position.x, topRight.position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bottomLeft.position.z, topRight.position.z)
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
            hitGo.SendMessage("OnClickDown", SendMessageOptions.DontRequireReceiver);
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
                hitGo.SendMessage("OnClickUp", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    #endregion

}
