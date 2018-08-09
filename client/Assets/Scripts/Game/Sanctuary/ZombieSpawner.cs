using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public Transform[] spawnPos;
    float deltaTime = 1.0f;
    float timer = 0;
    ISubPool zombiePool = null;

	// Use this for initialization
	void Start () {
        ObjectPoolSingleton.Instance.GetPool<ZombieController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (zombiePool == null) return;
        timer += Time.deltaTime;
        if (timer >= deltaTime)
        {
            timer = 0;
            zombiePool.Take();
        }
	}
}
