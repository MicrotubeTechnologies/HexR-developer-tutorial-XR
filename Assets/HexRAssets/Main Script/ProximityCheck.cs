using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HaptGlove;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace HexR
{
    public class ProximityCheck : MonoBehaviour
    {
        public PressureTrackerMain rightpressureTrackerMain, leftpressureTrackerMain;
        private bool restart = false;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains("L_Palm") || other.name.Contains("LeftGhostPalm"))
            {
                restart = true;
                leftpressureTrackerMain.IsPhysicsCollisionNear(true);
                removeCollisiontrue(leftpressureTrackerMain);
            }
            else if(other.name.Contains("R_Palm") || other.name.Contains("RightGhostPalm"))
            {
                restart = true;
                rightpressureTrackerMain.IsPhysicsCollisionNear(true);
                removeCollisiontrue(rightpressureTrackerMain);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.name.Contains("L_Palm") || other.name.Contains("LeftGhostPalm"))
            {
                restart = false;
                leftpressureTrackerMain.IsPhysicsCollisionNear(true);
            }
            else if (other.name.Contains("R_Palm") || other.name.Contains("RightGhostPalm"))
            {
                restart = false;
                rightpressureTrackerMain.IsPhysicsCollisionNear(true);
            }
        }
        IEnumerator removeCollisiontrue(PressureTrackerMain pressureTrackerMain)
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(0.5f);
            if(restart == true)
            {
                pressureTrackerMain.IsPhysicsCollisionNear(false);
            }
            else
            {
                restart = true;
                StartCoroutine(removeCollisiontrue(pressureTrackerMain));
            }


        }
        private void OnTriggerExit(Collider other)
        {
            if (other.name.Contains("L_Palm") || other.name.Contains("LeftGhostPalm"))
            {
                leftpressureTrackerMain.IsPhysicsCollisionNear(false);
            }
            else if (other.name.Contains("R_Palm") || other.name.Contains("RightGhostPalm"))
            {
                rightpressureTrackerMain.IsPhysicsCollisionNear(false);
            }
        }
    }
}
