using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_alarm_play : MonoBehaviour
{

    public AudioSource tickSource;
    // Start is called before the first frame update
    void Start()
    {
        tickSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        tickSource.Play();
    }
}
