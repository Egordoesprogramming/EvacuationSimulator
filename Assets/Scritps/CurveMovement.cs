using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    public HandlePositionReset lever;
    bool up;
    public bool same;

    public GameObject upPos;
    public GameObject downPos;

    Vector3 currentPos;

    float percentage = 0;
    bool moving;

    private void Start()
    {
        moving = false;

        if (same)
        {
            up = lever.getActivated();
            currentPos = upPos.transform.position;
        }

        else
        {
            up = !(lever.getActivated());
            currentPos = downPos.transform.position;
        }

        gameObject.transform.SetPositionAndRotation(currentPos, gameObject.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        checkMove();

        switch(same)
        {
            case true:
                up = lever.getActivated();
                break;
            case false:
                up = !(lever.getActivated());
                break;
        }

        if (moving)
        {
            percentage += Time.deltaTime * 1;

            if (percentage > 1)
            {
                percentage = 1;
                moving = false; ;
            }

            if (up)
            {
                currentPos.y = (1 - percentage) * upPos.transform.position.y + percentage * downPos.transform.position.y;
            }

            else
            {
                currentPos.y = (1 - percentage) * downPos.transform.position.y + percentage * upPos.transform.position.y;
            }

            gameObject.transform.SetPositionAndRotation(currentPos, gameObject.transform.rotation);
        }

    }

    void checkMove()
    {
        if (same)
        {
            if (up != lever.getActivated())
            {
                percentage = 0;
                moving = true;
            }
        }

        else
        {
            if (up == lever.getActivated())
            {
                percentage = 0;
                moving = true;
            }
        }
    }
}
