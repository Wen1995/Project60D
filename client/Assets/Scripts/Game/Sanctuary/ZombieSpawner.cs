﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour {

    public float zombieSpeed = 2.0f;
    public int zombieHP = 3;
    public float spwanInterval = 1.0f;

    public Transform spawnPos;
    public Transform container;

    public int zombieMaxNum = 30;
    float timer = 0;
    ISubPool zombiePool = null;

    public Transform target;
    List<ZombieController> zombieList = new List<ZombieController>();

    bool startFlag = false;

    private void Awake() 
    {
        ObjectPoolSingleton.Instance.RegisterComPool<ZombieController>(Resources.Load<GameObject>("Prefabs/Zombie/One"));
        zombiePool = ObjectPoolSingleton.Instance.GetPool<ZombieController>();

        FacadeSingleton.Instance.RegisterEvent("RefreshZombie", ClearDeadZombie);
    }
	
	void Update () {
        if (zombiePool == null) return;
        if(zombieList.Count >= zombieMaxNum) return;
        if(startFlag == false) return;

        timer += Time.deltaTime;
        if (timer >= spwanInterval)
        {
            timer = 0;
            
            int index = Random.Range(0, spawnPos.transform.childCount);
            ZombieController zombie = zombiePool.Take(spawnPos.GetChild(index).position, Quaternion.identity, container) as ZombieController;
            zombie.SetZombieProperty(zombieSpeed, zombieHP);
            zombie.SetTarget(target);
            zombieList.Add(zombie);
        }
	}

    public void StartSpwaning()
    {
        startFlag = true;
    }

    public void StopSpwaning()
    {
        startFlag = false;
    }

    void ClearDeadZombie(NDictionary args = null)
    {
        List<int> record = new List<int>();
        for(int i=0;i<zombieList.Count;i++)
            record.Add(i);
        for(int i=record.Count-1;i>=0;i--)
            zombieList.RemoveAt(i);
    }

}
