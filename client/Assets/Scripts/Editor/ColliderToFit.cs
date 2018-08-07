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

    [MenuItem("ProjectTools/Building/Remove Collider")]
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
        }
    }

}