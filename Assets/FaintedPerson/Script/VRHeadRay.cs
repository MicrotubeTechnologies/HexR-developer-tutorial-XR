using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRHeadRay : MonoBehaviour
{
    public Camera vrCamera; // VR camera reference
    public GameObject projectilePrefab; // Prefab of the projectile
    private GameObject[] projectileArray; // Stores projectiles
    private int projectileCounter;
    private float triggerInterval = 0.3f; // Interval between projectile shots
    private float timeSinceLastTrigger = 0f; // Timer for shooting
    public TextMeshPro debugPanel; // UI text for debugging

    private void Start()
    {
        projectileArray = new GameObject[200];
        projectileCounter = 0;
    }

    private void Update()
    {
        // Get the position and forward direction of the VR camera
        Vector3 cameraPosition = vrCamera.transform.position;
        Vector3 cameraForward = vrCamera.transform.forward;

        // Update the debug panel
        if (debugPanel != null)
        {
            debugPanel.text = $"Camera Position: {cameraPosition}\nCamera Forward: {cameraForward}";
        }

        // Accumulate elapsed time
        timeSinceLastTrigger += Time.deltaTime;

        // Check if the trigger interval has passed
        if (timeSinceLastTrigger >= triggerInterval)
        {
            // Reset the timer
            timeSinceLastTrigger = 0f;

            // Manage the projectile array
            if (projectileCounter == projectileArray.Length)
            {
                projectileCounter = 0;
            }

            // Destroy old projectile if it exists
            if (projectileArray[projectileCounter] != null)
            {
                Destroy(projectileArray[projectileCounter]);
            }

            // Instantiate and configure the projectile
            projectileArray[projectileCounter] = Instantiate(
                projectilePrefab,
                cameraPosition + cameraForward * 0.5f, // Offset slightly forward from the camera
                Quaternion.LookRotation(cameraForward) // Face the same direction as the camera
            );

            Rigidbody projectileRigidbody = projectileArray[projectileCounter].GetComponent<Rigidbody>();
            if (projectileRigidbody != null)
            {
                float projectileSpeed = 10f; // Adjust the speed as needed
                projectileRigidbody.AddForce(cameraForward * projectileSpeed, ForceMode.VelocityChange);
            }
            else
            {
                Debug.LogWarning("Projectile does not have a Rigidbody component!");
            }

            // Automatically destroy the projectile after 2 seconds
            Destroy(projectileArray[projectileCounter], 2f);

            projectileCounter++;
        }
    }
}


