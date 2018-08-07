using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCamera : MonoBehaviour {

    public Transform camera;

    private void Update()
    {
        Vector3 lookPoint = transform.position - camera.transform.position;
        lookPoint.y = camera.transform.position.y;
        transform.LookAt(lookPoint);
    }
}
