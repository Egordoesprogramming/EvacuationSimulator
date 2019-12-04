using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarmCollision : MonoBehaviour
{
    public AudioSource sound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Fire_Alarm1" || collision.gameObject.name == "Fire_Alarm2" || collision.gameObject.name == "Fire_Alarm3")
        {
            sound.Play();
			Debug.Log("Hello World");
        }

	}

	/*private void OnTriggerEnter(Collider other)
	{
		sound.Play();
		Debug.Log("Hello");
	}*/
}