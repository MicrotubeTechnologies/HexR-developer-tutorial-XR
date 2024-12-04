using HaptGlove;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

using static UnityEngine.GraphicsBuffer;

namespace HexR
{
    public class SpecialHaptics : MonoBehaviour
    {
        public PressureTrackerMain RightHandPhysics, LeftHandPhysics;
        public enum Options { FountainEffect, RainDropEffect, HeartBeatEffect }
        public Options TypeOfHaptics;
        private bool RemoveIt = false, IsTriggered = false, ReadyToDrop = true, PressureIn = true;

        public float InTimer = 0.4f, OutTimer = 0f;
        public HeartBeat heartbeat;
        public enum HeartBeat { Regular, Irregular };
        private List<byte[]> fingerAffected = new List<byte[]>();
        private byte[][] totalFingerAffected;
        void Start()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if(TypeOfHaptics == Options.FountainEffect)
            {
                FountainHapticTriggerEnter(other);
            }
            else if(TypeOfHaptics == Options.RainDropEffect)
            {
                RaindropHapticTriggerEnter(other);
            }
            else if(TypeOfHaptics == Options.HeartBeatEffect)
            {
                HeartBeatHapticTriggerEnter(other);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (TypeOfHaptics == Options.FountainEffect)
            {
                FountainHapticTriggerEnter(other);
            }
            else if (TypeOfHaptics == Options.RainDropEffect)
            {
                RaindropHapticTriggerEnter(other);
            }
            else if (TypeOfHaptics == Options.HeartBeatEffect)
            {
                HeartBeatHapticTriggerEnter(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (TypeOfHaptics == Options.FountainEffect)
            {
                FountainHapticTriggerExit(other);
            }
            else if (TypeOfHaptics == Options.RainDropEffect)
            {
                return;
            }
            else if (TypeOfHaptics == Options.HeartBeatEffect)
            {
                return;
            }
        }


        #region Fountain Haptics
        private void FountainHapticTriggerEnter(Collider other)
        {
            if (IsTriggered == false)
            {
                if (other.gameObject.name == "RightGhostPalm")
                {
                    IsTriggered = true;
                    HaptGloveHandler gloveHandler = RightHandPhysics.GetComponent<HaptGloveHandler>();
                    FountainEffect(gloveHandler);
                    StartCoroutine(RemoveFountainHaptic(RightHandPhysics));
                }
                if (other.gameObject.name == "LeftGhostPalm")
                {
                    IsTriggered = true;
                    HaptGloveHandler gloveHandler = LeftHandPhysics.GetComponent<HaptGloveHandler>();
                    FountainEffect(gloveHandler);
                    StartCoroutine(RemoveFountainHaptic(LeftHandPhysics));
                }
            }

            RemoveIt = false;
        }
        private void FountainHapticTriggerExit(Collider other)
        {
            if (other.gameObject.name == "RightGhostPalm")
            {
                RightHandPhysics.RemoveAllVibrations();
                RemoveIt = false;
                IsTriggered = false;
            }
            if (other.gameObject.name == "LeftGhostPalm")
            {
                LeftHandPhysics.RemoveAllVibrations();
                RemoveIt = false;
                IsTriggered = false;
            }
        }
        IEnumerator RemoveFountainHaptic(PressureTrackerMain PressureTracker)
        {
            RemoveIt = true;
            // Wait for the specified delay time
            yield return new WaitForSeconds(0.3f);
            if (RemoveIt == true)
            {
                PressureTracker?.RemoveAllVibrations();
                RemoveIt = false;
                IsTriggered = false;
            }
            else
            {
                StartCoroutine(RemoveFountainHaptic(PressureTracker));
            }
            // Wait for the specified delay time
        }
        public void FountainEffect(HaptGloveHandler gloveHandler)
        {
            byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }, new byte[] { 5, 0 } };
            byte[] btData = gloveHandler.haptics.ApplyHaptics(15, ClutchState, (byte)(30), false);
            gloveHandler.BTSend(btData);
        }
        #endregion

        #region RainDrop Haptics

        private void RaindropHapticTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "RightGhostPalm" && ReadyToDrop == true)
            {
                ReadyToDrop = false;
                RemoveIt = false;
                HaptGloveHandler gloveHandler = RightHandPhysics.GetComponent<HaptGloveHandler>();
                RaindropEffect(Random.Range(1, 9), gloveHandler);
                StartCoroutine(RestartRaindropHaptic());
                StartCoroutine(RemoveRaindropHaptic(RightHandPhysics));
            }
            if (other.gameObject.name == "LeftGhostPalm" && ReadyToDrop == true)
            {
                ReadyToDrop = false;
                RemoveIt = false;
                HaptGloveHandler gloveHandler = LeftHandPhysics.GetComponent<HaptGloveHandler>();
                RaindropEffect(Random.Range(1, 9), gloveHandler);
                StartCoroutine(RestartRaindropHaptic());
                StartCoroutine(RemoveRaindropHaptic(LeftHandPhysics));
            }
        }
        IEnumerator RestartRaindropHaptic()
        {
            yield return new WaitForSeconds(0.2f);
            ReadyToDrop = true;
        }
        IEnumerator RemoveRaindropHaptic(PressureTrackerMain PressureTracker)
        {
            RemoveIt = true;
            // Wait for the specified delay time
            yield return new WaitForSeconds(0.4f);
            if (RemoveIt == true)
            {
                PressureTracker?.RemoveAllHaptics();
                RemoveIt = false;
            }
            else
            {
                RemoveRaindropHaptic(PressureTracker);
            }
            // Wait for the specified delay time
        }
        public void RaindropEffect(int Pattern, HaptGloveHandler gloveHandler)
        {
            byte Pressure = (byte)40; // 10 to 60

            // ClutchState affecting all indenters
            if (Pattern == 1)
            {
                // thumb Pinky
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 0 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 2)
            {
                // Index middle ring
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 2 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 3)
            {
                // Palm Middle
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 0 }, new byte[] { 3, 2 }, new byte[] { 4, 2 }, new byte[] { 5, 0 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 4)
            {
                // Index Thumb
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 2 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 5)
            {
                // ring middle
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 2 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 6)
            {
                // Palm
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 2 }, new byte[] { 5, 0 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 7)
            {
                //middle little
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 0 }, new byte[] { 3, 2 }, new byte[] { 4, 0 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
            else if (Pattern == 8)
            {
                //Index little
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 0 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 0 }, new byte[] { 5, 2 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, Pressure, false);
                gloveHandler.BTSend(btData);
            }
        }

        #endregion

        #region HeartBeat Pulse Haptics
        private void HeartBeatHapticTriggerEnter(Collider other)
        {
            if (PressureIn == true)
            {
                if (other.name.Contains("Index"))
                {
                    fingerAffected.Add(new byte[] { 1, 0 });
                }
                if (other.name.Contains("Middle"))
                {
                    fingerAffected.Add(new byte[] { 2, 0 });
                }
                if (other.name.Contains("Ring"))
                {
                    fingerAffected.Add(new byte[] { 3, 0 });
                }
                if (other.name.Contains("Pinky") || other.name.Contains("Little"))
                {
                    fingerAffected.Add(new byte[] { 4, 0 });
                }
                foreach (var byteArray in fingerAffected)
                {
                    Debug.Log(string.Join(", ", byteArray));
                }


                if (fingerAffected.Count > 0)
                {
                    totalFingerAffected = fingerAffected.ToArray();
                    HaptGloveHandler gloveHandler = FindParent(other.transform);
                    if(gloveHandler != null)
                    {
                        byte[] btData = gloveHandler.haptics.ApplyHaptics(totalFingerAffected, 40, false);
                        gloveHandler.BTSend(btData);
                        PressureIn = false;
                        StartCoroutine(RemoveHeartBeatHaptic());
                    }
                }
            }
        }
        HaptGloveHandler FindParent(Transform childTransform)
        {
            // This is to determine if Right or Left Hand is touching the Component
            Transform parentTransform = childTransform.parent;

            while (parentTransform != null)
            {
                if (parentTransform.name == "Right Hand Physics")
                {
                    HaptGloveHandler gloveHandler = RightHandPhysics.GetComponent<HaptGloveHandler>();
                    return gloveHandler;
                }
                if (parentTransform.name == "Left Hand Physics")
                {
                    HaptGloveHandler gloveHandler = LeftHandPhysics.GetComponent<HaptGloveHandler>();
                    return gloveHandler;
                }
                parentTransform = parentTransform.parent;
            }
            Debug.Log("No hand is found, check if the name of Hapt Glove Handler is changed");
            return null; // Return null if parent not found
        }
        IEnumerator RemoveHeartBeatHaptic()
        {
            // Wait for the specified delay time
            if (heartbeat == HeartBeat.Regular)
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            }
            fingerAffected.Clear();
            totalFingerAffected = null;
            RightHandPhysics.RemoveAllHaptics();
            LeftHandPhysics.RemoveAllHaptics();
            StartCoroutine(ReadyHeartBeatHaptic());

        }
        IEnumerator ReadyHeartBeatHaptic()
        {
            if (heartbeat == HeartBeat.Regular)
            {
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(0.4f, 0.7f));
            }
            PressureIn = true;
        }
        public void ToggleHeartRegularity()
        {
            if (heartbeat == HeartBeat.Irregular)
            {
                heartbeat = HeartBeat.Regular;
            }
            else
            {
                heartbeat = HeartBeat.Irregular;
            }
        }
        #endregion
    }


    [CustomEditor(typeof(SpecialHaptics))]
    public class HapticEffectControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            // Get reference to the target script
            SpecialHaptics controller = (SpecialHaptics)target;

            // Add fields to assign RightHandPhysics and LeftHandPhysics
            controller.RightHandPhysics = (PressureTrackerMain)EditorGUILayout.ObjectField(
                "Right Hand Physics",
                controller.RightHandPhysics,
                typeof(PressureTrackerMain),
                true // Allow scene objects
            );

            controller.LeftHandPhysics = (PressureTrackerMain)EditorGUILayout.ObjectField(
                "Left Hand Physics",
                controller.LeftHandPhysics,
                typeof(PressureTrackerMain),
                true // Allow scene objects
            );

            // Draw default fields
            controller.TypeOfHaptics = (SpecialHaptics.Options)EditorGUILayout.EnumPopup("Type of Haptics", controller.TypeOfHaptics);

            // Conditional fields for HeartBeatEffect
            if (controller.TypeOfHaptics == SpecialHaptics.Options.HeartBeatEffect)
            {
                // Timers
                controller.InTimer = EditorGUILayout.FloatField("In Timer", controller.InTimer);
                controller.OutTimer = EditorGUILayout.FloatField("Out Timer", controller.OutTimer);
                // Type of Heartbeat
                controller.heartbeat = (SpecialHaptics.HeartBeat)EditorGUILayout.EnumPopup("Heart Beat Type", controller.heartbeat);

            }

            // Save changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
