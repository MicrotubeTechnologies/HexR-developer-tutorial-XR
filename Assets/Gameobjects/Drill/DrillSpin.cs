using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSpin : MonoBehaviour
{
    public float spinSpeed = 360f; // Degrees per second
    public float spinDuration = 2f; // Total time to spin (seconds)

    public bool isSpinning = false;
    private void Start()
    {

    }
    private void Update()
    {
        if (isSpinning)
        {
            // Rotate around the Z-axis
            float rotationStep = spinSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationStep, Space.Self);

        }
    }


    public void SpinDrill()
    {
        isSpinning = true;

    }
    public void StopDrill()
    {
        isSpinning = false;

    }
}
