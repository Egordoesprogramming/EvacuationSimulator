using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Teleporter : MonoBehaviour
{

    public GameObject m_Pointer;
    public SteamVR_Action_Boolean m_TeleportAction;
    public LineRenderer line;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private bool m_HasPosition = false;

    private bool m_isTeleporting = false;
    private float m_FadeTime = 0.2f;


    // Start is called before the first frame update
    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Pointer
        m_HasPosition = UpdatePointer();
        m_Pointer.SetActive(m_HasPosition);
        //Teleport Line
        if(m_HasPosition)
        {
            if(line.enabled == false)
            {
                line.enabled = true;
            }
            Vector3 tpPos = new Vector3(m_Pointer.transform.position.x, m_Pointer.transform.position.y, m_Pointer.transform.position.z);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, tpPos);
        }
        else if(!m_HasPosition)
        {
            line.enabled = false;
        }
        

        //Teleport
        if(m_TeleportAction.GetStateUp(m_Pose.inputSource))
        {
            TryTeleport();
        }
    }

    private void TryTeleport()
    {
        //Check for valid position, and if teleporting
        if (!m_HasPosition || m_isTeleporting)
        {
            return;
        }

        //Get camera rig and head position
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        //Figure out translation
        Vector3 groundPosiiton = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translateVector = m_Pointer.transform.position - groundPosiiton;

        //Move
        StartCoroutine(MoveRig(cameraRig, translateVector));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        //Flag
        m_isTeleporting = true;

        //Fade to black
        SteamVR_Fade.Start(Color.black, m_FadeTime, true);

        //Apply translation
        yield return new WaitForSeconds(m_FadeTime);
        cameraRig.position += translation;

        //Fade back to clear
        SteamVR_Fade.Start(Color.clear, m_FadeTime, true);

        //De-flag var
        m_isTeleporting = false;
    }

    private bool UpdatePointer()
    {
        //Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        //If it's hit
        if(Physics.Raycast(ray, out hit))
        {
            m_Pointer.transform.position = hit.point;
            return true;
        }
        //If it's not hit
        return false;
    }
}
