using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 lastSafePosition;
    float timer;
    bool incannon = false;

    public GameObject cannon;
    public GameObject cannonDestination;

    public float power = 1;

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.25f)
        {
            gameObject.GetComponent<Rigidbody>().velocity *= 0.9f;
        }
        else if (GolfClub.timesincehit > 0.1f)
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindObjectsOfType<GolfClub>()[0].GetComponent<Collider>(), true);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindObjectsOfType<GolfClub>()[1].GetComponent<Collider>(), true);
        }
        if (gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.05f)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindObjectsOfType<GolfClub>()[0].GetComponent<Collider>(), false);
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), GameObject.FindObjectsOfType<GolfClub>()[1].GetComponent<Collider>(), false);
        }

        cannonthings();
        print(timer);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "BadGround" && gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.05f)
        {
            transform.position = lastSafePosition;
            //GolfClub.shotCount++;
        }
        if (collision.gameObject.tag == "Ground" && gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.005f)
        {
            lastSafePosition = transform.position;
        }
    }

    void cannonthings()
    {
        //if (incannon)
        {
            if (timer <= 0)
            {
               // incannon = false;
                gameObject.transform.position = cannon.transform.position;
                gameObject.GetComponent<Rigidbody>().AddForce(-cannon.transform.forward * power, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CannonBall")
        {
            //incannon = true;
            //timer = 2.0f;
            //gameObject.transform.position = gameObject.transform.position + Vector3.down * 2;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cannon Teleporter")
        {
            gameObject.transform.position = cannonDestination.transform.position;
            timer = 2.0f;
            //incannon = true;
        }

        if (other.gameObject.tag == "Cannon Destonation")
        {
            timer -= Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 2.0f;
    }
}
