using UnityEngine;
using UnityEditor;
using System.Collections;

public class ColliderToFit : MonoBehaviour
{

    [MenuItem("ProjectTools/Building/Fit to Children")]
    static void FitToChildren()
    {
        foreach (GameObject rootGameObject in Selection.gameObjects)
        {
            if (!(rootGameObject.GetComponent<BoxCollider>() is BoxCollider))
                continue;
            bool hasBounds = false;
            Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

            Renderer[] renderers = rootGameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                if (hasBounds)
                    bounds.Encapsulate(renderer.bounds);
                else
                {
                    bounds = renderer.bounds;
                    hasBounds = true;
                }
            }
            BoxCollider collider = rootGameObject.GetComponent<BoxCollider>();
            if (collider == null) collider = rootGameObject.AddComponent<BoxCollider>();
            collider.center = bounds.center - rootGameObject.transform.position;
            collider.size = bounds.size;
        }
    }

    [MenuItem("ProjectTools/Building/Remove Collider")]
    static void RemoveAllCollider()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            Collider[] colliders = go.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
                DestroyImmediate(collider);
        }
    }

    [MenuItem("ProjectTools/Building/AddFloatingIconPos")]
    static void AddFloatingIconPos()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            BoxCollider collider = go.GetComponent<BoxCollider>();
            if (collider == null)
            {
                print("pls add box collider first");
                continue;
            }
            GameObject hudGo = new GameObject("FloatingPos");
            hudGo.transform.parent = go.transform;
            //hudGo.transform.localPosition = new Vector3(0, 0, 0);
            hudGo.transform.localPosition = new Vector3(
                0,
                Mathf.Ceil(collider.bounds.size.y * 1.2f),
                0
                );
        }
    }

    [MenuItem("ProjectTools/Building/MergeAndAddLock")]
    static void MergeAndAddLock()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            GameObject merge = new GameObject("building");
            merge.transform.parent = go.transform;
            for (int i = go.transform.childCount - 1; i >= 0; i--)
            {
                go.transform.GetChild(i).parent = merge.transform;
            }
            GameObject lockGo = Instantiate(Resources.Load("lock") as GameObject);
            lockGo.name = "lock";
            Instantiate(lockGo).transform.parent = go.transform;
        }
    }

}