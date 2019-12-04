using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PPPTrigger : MonoBehaviour
{
    public GameObject ppButton;
    private bool firstFrame = false;

    private void Update()
    {
        if (gameObject.GetComponent<Collider>().bounds.Contains(SteamVR_Render.Top().head.position))
        {
            ppButton.GetComponent<PPMove>().descend();
            firstFrame = true;
        }
        else if (firstFrame)
        {
            ppButton.GetComponent<PPMove>().ascend();
            firstFrame = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ppButton.GetComponent<PPMove>().descend();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ppButton.GetComponent<PPMove>().ascend();
        }
    }
}
