using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VibrateonIpact : MonoBehaviour
{
		void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.name == "Test")
		{
			Handheld.Vibrate();
			Debug.Log("shit");
		}
	}
}


