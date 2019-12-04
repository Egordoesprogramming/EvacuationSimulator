using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPMove : MonoBehaviour
{
    bool up;

    bool activated;

    float maxHeight;
    float minHeight;
    float currentHeight;

    // Start is called before the first frame update
    void Start()
    {
        up = true;
        maxHeight = transform.position.y;
        minHeight = maxHeight * 2;
        currentHeight = maxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            currentHeight += Time.deltaTime * 0.3f;
        }

        else
        {
            currentHeight -= Time.deltaTime * 0.3f;
        }

        currentHeight = Mathf.Min(maxHeight, Mathf.Max(minHeight, currentHeight));

        if (currentHeight == minHeight)
            activated = true;
        else
            activated = false;


        transform.SetPositionAndRotation(new Vector3(transform.position.x, currentHeight, transform.position.z), transform.rotation);
    }

    public void ascend()
    {
        up = true;
    }

    public void descend()
    {
        up = false;
    }

    public bool getActivated()
    {
        return activated;
    }
}
