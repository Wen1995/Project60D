﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRendererController : MonoBehaviour {


    public Transform ModelPos;
    new public Camera camera;
    private GameObject targetGo;

    public GameObject Open(GameObject prefab)
    {
        if (targetGo) DestroyModel();
        camera.gameObject.SetActive(true);
        targetGo = Instantiate(prefab);
        targetGo.transform.parent = ModelPos;
        targetGo.transform.localPosition = Vector3.zero;
        targetGo.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return targetGo;
    }

    public void Close()
    {
        camera.gameObject.SetActive(false);
        DestroyModel();
    }

    public void DestroyModel()
    {
        if (targetGo == null) return;
        Destroy(targetGo);
        targetGo = null;
    }
}
