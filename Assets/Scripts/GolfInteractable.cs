using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class GolfInteractable : MonoBehaviour
{
    public bool isClub = false;
    public float scaleFactor;
    [SerializeField] bool angled = false;

    private Vector3 startpos;
    private Quaternion startrot;

    /*[SerializeField] private Collider course1;
    [SerializeField] private Collider course2;
    [SerializeField] private Collider walls1;
    [SerializeField] private Collider walls2;*/
    private GameObject plane;

    GameObject[] colliders;

    [HideInInspector]
    public Hand m_ActiveHand = null;

    private void Start()
    {
        startpos = transform.position;
        startrot = transform.rotation;

        colliders = GameObject.FindGameObjectsWithTag("Ground");
        plane = GameObject.FindGameObjectWithTag("BadGround");
    }

    private void Update()
    {
        if (transform.position.y < -10)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            transform.SetPositionAndRotation(startpos, startrot);
        }
    }

    public void resetRotation(Transform handGrab)
    {
        if (isClub)
        {
            transform.rotation = handGrab.rotation;
            transform.position = handGrab.transform.position + handGrab.transform.forward * scaleFactor;
            if (angled)
            {
                transform.position = handGrab.transform.position + handGrab.transform.up * scaleFactor * 0.35f + handGrab.transform.forward * scaleFactor;
            }
        }
    }

    public void ignoreCollisions(bool ignore)
    {
        if (isClub)
        {
            //print("hi im happening");
            //print(ignore);

            Collider[] selfs = transform.gameObject.GetComponents<Collider>();

            foreach (GameObject collider in colliders)
            {
                Physics.IgnoreCollision(selfs[0], collider.GetComponent<Collider>(), ignore);
                Physics.IgnoreCollision(selfs[1], collider.GetComponent<Collider>(), ignore);
            }
            Physics.IgnoreCollision(selfs[0], plane.GetComponent<Collider>(), ignore);
            Physics.IgnoreCollision(selfs[1], plane.GetComponent<Collider>(), ignore);
        }
    }
}
