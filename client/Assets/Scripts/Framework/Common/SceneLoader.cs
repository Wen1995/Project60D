using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Implement of loading scene
/// </summary>
public class SceneLoader{

    public static string CurrentScene {
        get { return SceneManager.GetActiveScene().name; }
    }

    public static void LoadScene(string name)
    {
        UnityEngine.Debug.Log(string.Format("Leaving scene{0}", CurrentScene));
        SceneManager.LoadScene(name);
    }

    public static AsyncOperation LoadSceneAsync(string name)
    {
        UnityEngine.Debug.Log(string.Format("Leaving scene{0}", CurrentScene));
        return SceneManager.LoadSceneAsync(name);
    }
}
