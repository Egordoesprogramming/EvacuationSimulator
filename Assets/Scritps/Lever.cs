using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject pivot;
    bool rotating;
    bool up;
    float rotation;

    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        rotating = false;
        up = true;
        rotation = 90;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotating)
        {
            if (up)
            {
                rotation += Time.deltaTime * 90;

                if (rotation > 90)
                {
                    rotation = 90;
                    rotating = false;
                    activated = false;
                }
            }

            else
            {
                rotation -= Time.deltaTime * 90;

                if (rotation < -90)
                {
                    rotation = -90;
                    rotating = false;
                    activated = true;

                }
            }

            pivot.transform.localRotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    public void pull()
    {
        if (!rotating)
        {
            rotating = true;
            up = !up;
        }
    }

    public bool getActivated()
    {
        return activated;
    }
}
