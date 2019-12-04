using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePositionReset : MonoBehaviour
{
    public GameObject positionObject;
    float timer = 0;
	bool activated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            transform.SetPositionAndRotation(positionObject.transform.position, positionObject.transform.rotation);// = positionObject.transform;
            timer = 0;
        }
    }

	public bool getActivated()
	{
		return activated;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "LeverUp")
		{
			activated = false;
		}
		else if (other.gameObject.tag == "LeverDown")
		{
			activated = true;
		}
	}
}
