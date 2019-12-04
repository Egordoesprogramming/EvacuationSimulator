using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    bool pickedUp;
    GameObject target;

    public void grab(GameObject t)
    {
        target = t;
        pickedUp = true;
    }

    public void letGo()
    {
        pickedUp = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        pickedUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp)
        {
            transform.SetPositionAndRotation(target.transform.position, target.transform.rotation);
        }
    }
}
