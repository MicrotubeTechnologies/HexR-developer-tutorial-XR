using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBitRotate : MonoBehaviour
{
    [Tooltip("Speed of the drill bit rotation in degrees per second.")]
    public float rotationSpeed = 360f;

    private bool isDrilling = false;
    private void Start()
    {
        isDrilling = false;
    }
    void Update()
    {
        // Rotate the drill bit if it's drilling
        if (isDrilling)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Starts the drill bit rotation.
    /// </summary>
    public void StartDrilling()
    {
        Debug.Log("S");
        isDrilling = true;
    }

    /// <summary>
    /// Stops the drill bit rotation.
    /// </summary>
    public void StopDrilling()
    {
        isDrilling = false;
    }
}
