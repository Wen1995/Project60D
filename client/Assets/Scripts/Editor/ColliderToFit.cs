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
            //collider.center = rootGameObject.transform.position;
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

    
     private delegate void ChangePrefab(GameObject go);
        private const int SelectionThresholdForProgressBar = 20;
        private static bool showProgressBar;
        private static int changedObjectsCount;
 
        [MenuItem("GameObject/Apply Changes To Selected Prefabs %j", false, 100)]
        private static void ApplyPrefabs()
        {
            SearchPrefabConnections(ApplyToSelectedPrefabs);
        }
 
        [MenuItem("GameObject/Revert Changes Of Selected Prefabs", false, 100)]
        private static void ResetPrefabs()
        {
            SearchPrefabConnections(RevertToSelectedPrefabs);
        }
 
        [MenuItem("GameObject/Apply Changes To Selected Prefabs", true)]
        [MenuItem("GameObject/Revert Changes Of Selected Prefabs", true)]
        private static bool IsSceneObjectSelected()
        {
            return Selection.activeTransform != null;
        }
 
        //Look for connections
        private static void SearchPrefabConnections(ChangePrefab changePrefabAction)
        {
            GameObject[] selectedTransforms = Selection.gameObjects;
            int numberOfTransforms = selectedTransforms.Length;
            showProgressBar = numberOfTransforms >= SelectionThresholdForProgressBar;
            changedObjectsCount = 0;
            //Iterate through all the selected gameobjects
            try
            {
                for (int i = 0; i < numberOfTransforms; i++)
                {
                    var go = selectedTransforms[i];
                    if (showProgressBar)
                    {
                        EditorUtility.DisplayProgressBar("Update prefabs", "Updating prefab " + go.name + " (" + i + "/" + numberOfTransforms + ")",
                            (float)i / (float)numberOfTransforms);
                    }
                    IterateThroughObjectTree(changePrefabAction, go);
                }
            }
            finally
            {
                if (showProgressBar)
                {
                    EditorUtility.ClearProgressBar();
                }
                Debug.LogFormat("{0} Prefab(s) updated", changedObjectsCount);
            }
        }
 
        private static void IterateThroughObjectTree(ChangePrefab changePrefabAction, GameObject go)
        {
            var prefabType = PrefabUtility.GetPrefabType(go);
            //Is the selected gameobject a prefab?
            if (prefabType == PrefabType.PrefabInstance || prefabType == PrefabType.DisconnectedPrefabInstance)
            {
                var prefabRoot = PrefabUtility.FindRootGameObjectWithSameParentPrefab(go);
                if (prefabRoot != null)
                {
                    changePrefabAction(prefabRoot);
                    changedObjectsCount++;
                    return;
                }
            }
            // If not a prefab, go through all children
            var transform = go.transform;
            var children = transform.childCount;
            for (int i = 0; i < children; i++)
            {
                var childGo = transform.GetChild(i).gameObject;
                IterateThroughObjectTree(changePrefabAction, childGo);
            }
        }
 
        //Apply    
        private static void ApplyToSelectedPrefabs(GameObject go)
        {
            var prefabAsset = PrefabUtility.GetPrefabParent(go);
            if (prefabAsset == null)
            {
                return;
            }
            PrefabUtility.ReplacePrefab(go, prefabAsset, ReplacePrefabOptions.ConnectToPrefab);
        }
 
        //Revert
        private static void RevertToSelectedPrefabs(GameObject go)
        {
            PrefabUtility.ReconnectToLastPrefab(go);
            PrefabUtility.RevertPrefabInstance(go);
        }

}