using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfClub : MonoBehaviour
{

    public static int shotCount = 0;
    public static float timesincehit;

    void Update()
    {
        timesincehit += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ball>() != null)
        {
            shotCount++;
            timesincehit = 0;

            GameObject.Find("StrokeCount").GetComponent<TextMesh>().text = ": " + shotCount.ToString();
        }
    }
}
