using System.Collections;
using System.Collections.Generic;
using com.nkm.common.proto.client;
using UnityEngine;

public class Main : MonoBehaviour {

    const string HOST = "192.168.90.74";
    //const string HOST = "127.0.0.1";
    const int PORT = 8000;

    public bool SendMsg = false;
    public bool Connect = false;
    private bool isConnected = false;
    ManagerNet managerNet;

    ObjectPoolSingleton objectPool = null;
    Cube cube = null;
    SubPoolCom<Cube> subPool;

    private void OnValidate()
    {
        if (Connect && !isConnected)
        {
            managerNet = ManagerNet.Instance;
            managerNet.BeginConnect(NetType.Netty, HOST, PORT);
            isConnected = true;
        }
        if(SendMsg)
            OnSend();
    }

    void OnSend()
    {
        var builder = TCSLogin.CreateBuilder();
        builder.Account = "wen";
        TCSLogin login = builder.Build();
        byte[] data = login.ToByteArray();
        managerNet.SendNetMsg(NetType.Netty, (short)Cmd.LOGIN, data);
    }

    private void Start()
    {
        objectPool = ObjectPoolSingleton.Instance;
        GameObject prefab = Resources.Load<GameObject>("Cube");
        objectPool.RegisterComPool<Cube>(prefab);
        ISubPool pool = objectPool.GetPool<Cube>();
        cube = pool.Take() as Cube;
        cube.transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (cube)
        {
            cube.transform.Translate(Vector3.forward * Time.deltaTime);
            if (cube.transform.position.z >= 2)
            {
                ISubPool pool = objectPool.GetPool<Cube>();
                pool.Restore(cube);
                cube = null;
            }
        }
    }
}
