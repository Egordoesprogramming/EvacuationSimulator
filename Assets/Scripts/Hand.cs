using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    private GolfInteractable m_CurrentInteractable = null;
    private List<GolfInteractable> m_ContactInteractables = new List<GolfInteractable>();

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }


    // Update is called once per frame
    private void Update()
    {
        //Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            Pickup();
        }
        //Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }

        m_ContactInteractables.Add(other.gameObject.GetComponent<GolfInteractable>());
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
        {
            return;
        }

        m_ContactInteractables.Remove(other.gameObject.GetComponent<GolfInteractable>());
    }

    public void Pickup()
    {
        // Get nearest
        m_CurrentInteractable = GetNearestInteractable();
        // Null check
        if (!m_CurrentInteractable)
        {
            return;
        }
        //Already held, check
        if (m_CurrentInteractable.m_ActiveHand)
        {
            if (m_CurrentInteractable.isClub)
            {
                m_Pose.transform.rotation = m_CurrentInteractable.m_ActiveHand.transform.rotation;
                m_Pose.transform.position = m_CurrentInteractable.m_ActiveHand.transform.position + m_CurrentInteractable.m_ActiveHand.transform.forward * (m_CurrentInteractable.scaleFactor * 0.5f);

                print("My rotation: " + transform.rotation + ", other Hand rotation: " + m_CurrentInteractable.m_ActiveHand.transform.rotation);
            }
            //m_CurrentInteractable.m_ActiveHand.Drop();
        }
        if (m_CurrentInteractable.isClub)
        {
            m_CurrentInteractable.ignoreCollisions(true);
        }

        // Position
        m_CurrentInteractable.transform.position = transform.position;

        m_CurrentInteractable.resetRotation(transform);
        //move the golf club forward according to hand rotation and position
        //m_CurrentInteractable.transform.position = transform.position + transform.forward * 0.3f;

        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
    }

    public void Drop()
    {
        // Null check
        if (!m_CurrentInteractable)
        {
            return;
        }

        if (m_CurrentInteractable.isClub)
        {
            m_CurrentInteractable.ignoreCollisions(false);
        }

        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        // Detach
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private GolfInteractable GetNearestInteractable()
    {
        GolfInteractable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (GolfInteractable interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
