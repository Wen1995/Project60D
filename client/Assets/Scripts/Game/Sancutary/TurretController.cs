using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : Controller , IPoolUnit{

    public Transform target;
    public Transform pivot;
    public Transform yaw;
    public Transform[] pitch;
    public bool enablePitch = true;
    public bool enableYaw = true;
    public float shootingDistance;

    public float yawSpeed = 10;
    public float pitchSpeed = 10;

    public float dampSpeed = 0.1f;

    float yawVelocity;
    float pitchVelocity;



    void Reset()
    {
        if (transform.childCount == 1)
        {
            this.pivot = transform.GetChild(0);
            if (pivot.childCount == 1)
            {
                this.yaw = pivot.GetChild(0);
                var pitch = new List<Transform>();

                foreach (Transform p in yaw)
                {
                    pitch.Add(p);
                }
                this.pitch = pitch.ToArray();
            }

        }
    }


    bool AngleInRange(float A, float D)
    {
        if (A < (360 - D) && A > 180)
        {
            return false;
        }
        if (A > D && A < 180)
        {
            return false;
        }
        return true;
    }

    bool AngleBetween(float angle, float A, float B)
    {
        angle = (360 + angle % 360) % 360;
        A = (360 + A % 360) % 360;
        B = (360 + B % 360) % 360;
        if (A < B)
            return A <= angle && angle <= B;
        return A <= angle || angle <= B;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) >= shootingDistance)
        {
            target = null;
            DetectTarget();
        }


        if (target == null || yaw == null || pitch == null)
            return;

        if (enableYaw)
        {
            var LE = Quaternion.LookRotation(target.position - yaw.position).eulerAngles;
            var E = yaw.localEulerAngles;
            E.z = Mathf.SmoothDampAngle(E.z, LE.y, ref yawVelocity, dampSpeed, yawSpeed);
            yaw.localEulerAngles = E;
        }
        if (enablePitch && pitch.Length > 0)
        {
            var LE = Quaternion.LookRotation(target.position - pitch[0].position).eulerAngles;
            var PE = pitch[0].localEulerAngles;
            PE.x = Mathf.SmoothDampAngle(PE.x, LE.x, ref pitchVelocity, dampSpeed, pitchSpeed);
            foreach (var p in this.pitch)
            {
                p.localEulerAngles = PE;
            }
        }
    }

    /// <summary>
    /// Find a new target in shooting area
    /// </summary>
    void DetectTarget()
    {

    }

    #region IPoolUnit member
    public void OnRestore()
    {
        throw new System.NotImplementedException();
    }

    public void OnTake()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void Restore()
    {
        throw new System.NotImplementedException();
    }

    public UnitState State()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
