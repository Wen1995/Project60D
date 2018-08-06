﻿using System.Collections;
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
        SceneManager.LoadScene(name);
    }

    public static void LoadSceneAsync(string name)
    {
        //TODO
    }
}