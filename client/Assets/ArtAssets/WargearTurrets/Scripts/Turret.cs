using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Turret : MonoBehaviour
{
	public Transform target;
	public Transform pivot;
	public Transform yaw;
	public Transform[] pitch;
	public bool enablePitch = true;
	public bool enableYaw = true;
	
	public float yawSpeed = 10;
	public float pitchSpeed = 10;
	
	public float dampSpeed = 0.1f;

	public float fireInterval = 1.0f;

	public ParticleSystem[] fireEffects;
	
	float yawVelocity;
	float pitchVelocity;
	
	float timer = 0f;
 
	void Reset() {
		if (transform.childCount == 1) {
			this.pivot = transform.GetChild (0);
			if (pivot.childCount == 1) {
				this.yaw = pivot.GetChild (0);
				var pitch = new List<Transform> ();
				
				foreach (Transform p in yaw) {
					pitch.Add (p);
				}
				this.pitch = pitch.ToArray ();
			}
		}
	}
	

	bool AngleInRange (float A, float D)
	{
		if (A < (360 - D) && A > 180) {
			return false;
		}
		if (A > D && A < 180) {
			return false;
		}
		return true;
	}

	bool AngleBetween (float angle, float A, float B)
	{
		angle = (360 + angle % 360) % 360;
		A = (360 + A % 360) % 360;
		B = (360 + B % 360) % 360;
		if (A < B)
			return A <= angle && angle <= B;
		return A <= angle || angle <= B;
	}

	void Update ()
	{
		if(timer < fireInterval)
			timer += Time.deltaTime;

		if (target == null || yaw == null || pitch == null)
			return;

		//check if zombie is dead
		if(target.tag != "Zombie")
		{
			target = null;
			return;
		}
			

		if(timer >= fireInterval)
		{
			timer = 0;
			Fire();
		}

		if (enableYaw) {
			var LE = Quaternion.LookRotation(yaw.position -target.position).eulerAngles;
			var E = yaw.localEulerAngles;
			E.z = Mathf.SmoothDampAngle (E.z, LE.y, ref yawVelocity, dampSpeed, yawSpeed);
			yaw.localEulerAngles = E;
		}
		if(enablePitch && pitch.Length > 0) {
			var LE = Quaternion.LookRotation(target.position - pitch[0].position).eulerAngles;
			var PE = pitch[0].localEulerAngles;
			PE.x = Mathf.SmoothDampAngle (PE.x, LE.x, ref pitchVelocity, dampSpeed, pitchSpeed);
			foreach (var p in this.pitch) {
				p.localEulerAngles = PE;
			}
		}    
	}

	void Fire()
	{
		if(target == null) return;
		target.SendMessage("TakeDamage");
		//play effect
		for(int i=0;i<fireEffects.Length;i++)
			fireEffects[i].Play();
	}

	private void OnCollisionEnter(Collision other) 
   {
	   if(other.gameObject.tag == "Zombie")
	   {
		   target = other.gameObject.transform;
	   }
	   		
   }

   private void OnCollisionStay(Collision other) 
   {
	   if(target != null) return;
	   if(other.gameObject.tag == "Zombie")
			target = other.gameObject.transform;   
   }

   private void OnCollisionExit(Collision other) 
   {
	   target = null;
   }
}
