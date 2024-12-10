using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSpin : MonoBehaviour
{
    public float spinSpeed = 360f; // Degrees per second
    public float spinDuration = 2f; // Total time to spin (seconds)
    private AudioSource m_Source;
    private bool isSpinning = false;
    private void Start()
    {
        m_Source = gameObject.GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (isSpinning)
        {
            // Rotate around the Z-axis
            float rotationStep = spinSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationStep, Space.Self);
            if(!m_Source.isPlaying)
            {
                m_Source.Play();
            }
        }
    }


    public void SpinDrill()
    {
        isSpinning = true;
        m_Source.Play();
    }
    public void StopDrill()
    {
        isSpinning = false;
        m_Source.Stop();
    }
}
