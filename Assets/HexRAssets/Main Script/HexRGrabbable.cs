using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexR;
using TMPro;
using UnityEngine.Events;
using System;

namespace HexR
{
    public class HexRGrabbable : MonoBehaviour
    {
        public enum Options { PinchGrab, PalmGrab }
        [Header("General Settings")]
        [Space(10)]
        public Options TypeOfGrab;
        public enum Option { On, Off }
        public Option Gravity;

        [Range(0f, 60f)]
        public float HapticStrength = 10f;

        [Space(5)]
        public UnityEvent OnGrab, OnRelease;


        private GameObject RHandParent, LHandParent;
        private GameObject OriginalParent;

        #region Bool Fields
        bool RThumb, RIndex, RLittle, RMiddle, RRing, RPalm; // if finger is touching
        bool LThumb, LIndex, LLittle, LMiddle, LRing, LPalm;
        bool RThumbHaptics, RIndexHaptics, RLittleHaptics, RMiddleHaptics, RRingHaptics, RPalmHaptics; // if haptics were triggered
        bool LThumbHaptics, LIndexHaptics, LLittleHaptics, LMiddleHaptics, LRingHaptics, LPalmHaptics;
        #endregion

        private FingerUseTracking RfingerUseTracking, LfingeruseTracking;
        private PressureTrackerMain RightPressureTracker, LeftPressureTracker;
        private Rigidbody objectRigidbody;
        [HideInInspector]
        public bool isGrab = false, InvokeEventReady = true;
        private bool ReadyToActivateGrab = true;
        // Start is called before the first frame update
        void Start()
        {
            GameObject RightHand = GameObject.Find("Right Hand Physics");
            GameObject LeftHand = GameObject.Find("Left Hand Physics");

            // Create new empty objects with unique names for right and left hand parents
            RHandParent = new GameObject("RightHandParent");
            LHandParent = new GameObject("LeftHandParent");

            if (RightHand != null) { RfingerUseTracking = RightHand.GetComponent<FingerUseTracking>(); }
            else { Debug.Log("Right hand is not found"); }
            if (RightHand != null) { RightPressureTracker = RightHand.GetComponent<PressureTrackerMain>(); }
            else { Debug.Log("Right pressuretracker is not found"); }
            if (LeftHand != null) { LfingeruseTracking = LeftHand.GetComponent<FingerUseTracking>(); }
            else { Debug.Log("Left hand is not found"); }
            if (LeftHand != null) { LeftPressureTracker = LeftHand.GetComponent<PressureTrackerMain>(); }
            else { Debug.Log("Left pressuretracker is not found"); }

            objectRigidbody = gameObject.GetComponent<Rigidbody>();
            OriginalParent = gameObject.transform.parent.gameObject;

            SetUpBool();

        }

        // Update is called once per frame
        void Update()
        {

            if (TypeOfGrab == Options.PinchGrab)
            {
                if (RThumb)
                {
                    if (RIndex || RMiddle || RRing || RLittle)
                    {
                        isGrab = true;
                        IsGrab(RHandParent, RfingerUseTracking, RightPressureTracker, false);
                    }
                    else
                    {
                        isGrab = false;
                        NotGrab(RightPressureTracker);
                    }

                }
                if (LThumb)
                {
                    if (LIndex || LMiddle || LRing || LLittle)
                    {
                        isGrab = true;
                        IsGrab(LHandParent, LfingeruseTracking, LeftPressureTracker, true);
                    }
                    else
                    {
                        isGrab = false;
                        NotGrab(LeftPressureTracker);
                    }

                }
            }
            else if (TypeOfGrab == Options.PalmGrab)
            {
                if (RPalm)
                {
                    if (RIndex || RMiddle || RRing || RLittle)
                    {
                        isGrab = true;
                        IsGrab(RHandParent, RfingerUseTracking, RightPressureTracker, false);
                    }
                    else
                    {
                        isGrab = false;
                        NotGrab(RightPressureTracker);
                    }

                }
                if (LPalm)
                {
                    if (LIndex || LMiddle || LRing || LLittle)
                    {
                        isGrab = true;
                        IsGrab(LHandParent, LfingeruseTracking, LeftPressureTracker, true);
                    }
                    else
                    {
                        isGrab = false;
                        NotGrab(LeftPressureTracker);
                    }

                }
            }

        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.parent.name == "R_IndexTip" || collision.transform.parent.name == "R_Index_3")
            {
                RIndex = true;
            }
            if (collision.transform.parent.name == "R_LittleTip" || collision.transform.parent.name == "R_Pinky_1")
            {
                RLittle = true;
            }
            if (collision.transform.parent.name == "R_MiddleTip" || collision.transform.parent.name == "R_Middle_3")
            {
                RMiddle = true;
            }
            if (collision.transform.parent.name == "R_RingTip" || collision.transform.parent.name == "R_Ring_3")
            {
                RRing = true;
            }
            if (collision.transform.parent.name == "R_ThumbTip" || collision.transform.parent.name == "R_Thumb_2")
            {
                if (TypeOfGrab == Options.PinchGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    RHandParent.transform.position = contactPoint;
                    RHandParent.transform.parent = collision.transform;
                }
                RThumb = true;
            }
            if (collision.transform.name == "R_Palm" || collision.transform.parent.name == "R_GhostPalm")
            {
                if (TypeOfGrab == Options.PalmGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    RHandParent.transform.position = contactPoint;
                    RHandParent.transform.parent = collision.transform;
                }
                RPalm = true;
            }

            if (collision.transform.parent.name == "L_IndexTip" || collision.transform.parent.name == "R_Index_3")
            {
                LIndex = true;
            }
            if (collision.transform.parent.name == "L_LittleTip" || collision.transform.parent.name == "R_Pinky_1")
            {
                LLittle = true;
            }
            if (collision.transform.parent.name == "L_MiddleTip" || collision.transform.parent.name == "R_Middle_3")
            {
                LMiddle = true;
            }
            if (collision.transform.parent.name == "L_RingTip" || collision.transform.parent.name == "R_Ring_3")
            {
                LRing = true;
            }
            if (collision.transform.parent.name == "L_ThumbTip" || collision.transform.parent.name == "R_Thumb_2")
            {
                if (TypeOfGrab == Options.PinchGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    LHandParent.transform.position = contactPoint;
                    LHandParent.transform.parent = collision.transform;
                }
                LThumb = true;
            }
            if (collision.transform.name == "L_Palm" || collision.transform.parent.name == "L_GhostPalm")
            {
                if (TypeOfGrab == Options.PalmGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    LHandParent.transform.position = contactPoint;
                    LHandParent.transform.parent = collision.transform;
                }
                LPalm = true;
            }
        }
        private void OnTriggerStay(Collider collision)
        {
            if (collision.transform.parent.name == "R_IndexTip" || collision.transform.parent.name == "R_Index_3")
            {
                RIndex = true;
            }
            if (collision.transform.parent.name == "R_LittleTip" || collision.transform.parent.name == "R_Pinky_1")
            {
                RLittle = true;
            }
            if (collision.transform.parent.name == "R_MiddleTip" || collision.transform.parent.name == "R_Middle_3")
            {
                RMiddle = true;
            }
            if (collision.transform.parent.name == "R_RingTip" || collision.transform.parent.name == "R_Ring_3")
            {
                RRing = true;
            }
            if (collision.transform.parent.name == "R_ThumbTip" || collision.transform.parent.name == "R_Thumb_2")
            {
                if (TypeOfGrab == Options.PinchGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    RHandParent.transform.position = contactPoint;
                    RHandParent.transform.parent = collision.transform;
                }
                RThumb = true;
            }
            if (collision.transform.name == "R_Palm" || collision.transform.parent.name == "R_GhostPalm")
            {
                if (TypeOfGrab == Options.PalmGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    RHandParent.transform.position = contactPoint;
                    RHandParent.transform.parent = collision.transform;
                }
                RPalm = true;
            }

            if (collision.transform.parent.name == "L_IndexTip" || collision.transform.parent.name == "L_Index_3")
            {
                LIndex = true;
            }
            if (collision.transform.parent.name == "L_LittleTip" || collision.transform.parent.name == "L_Pinky_1")
            {
                LLittle = true;
            }
            if (collision.transform.parent.name == "L_MiddleTip" || collision.transform.parent.name == "L_Middle_3")
            {
                LMiddle = true;
            }
            if (collision.transform.parent.name == "L_RingTip" || collision.transform.parent.name == "L_Ring_3")
            {
                LRing = true;
            }
            if (collision.transform.parent.name == "L_ThumbTip" || collision.transform.parent.name == "L_Thumb_2")
            {
                if (TypeOfGrab == Options.PinchGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    LHandParent.transform.position = contactPoint;
                    LHandParent.transform.parent = collision.transform;
                }
                LThumb = true;
            }
            if (collision.transform.name == "L_Palm" || collision.transform.parent.name == "L_GhostPalm")
            {
                if (TypeOfGrab == Options.PalmGrab)
                {
                    Vector3 contactPoint = collision.ClosestPoint(transform.position);
                    LHandParent.transform.position = contactPoint;
                    LHandParent.transform.parent = collision.transform;
                }
                LPalm = true;
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.transform.parent.name == "R_IndexTip" || collision.transform.parent.name == "R_Index_3")
            {
                RIndex = false;
            }
            if (collision.transform.parent.name == "R_LittleTip" || collision.transform.parent.name == "R_Pinky_1")
            {
                RLittle = false;
            }
            if (collision.transform.parent.name == "R_MiddleTip" || collision.transform.parent.name == "R_Middle_3")
            {
                RMiddle = false;
            }
            if (collision.transform.parent.name == "R_RingTip" || collision.transform.parent.name == "R_Ring_3")
            {
                RRing = false;
            }
            if (collision.transform.parent.name == "R_ThumbTip" || collision.transform.parent.name == "R_Thumb_2")
            {
                RThumb = false;
            }
            if (collision.transform.name == "R_Palm" || collision.transform.parent.name == "R_GhostPalm")
            {
                RPalm = false;
            }

            if (collision.transform.parent.name == "L_IndexTip" || collision.transform.parent.name == "L_Index_3")
            {
                LIndex = false;
            }
            if (collision.transform.parent.name == "L_LittleTip" || collision.transform.parent.name == "L_Pinky_1")
            {
                LLittle = false;
            }
            if (collision.transform.parent.name == "L_MiddleTip" || collision.transform.parent.name == "L_Middle_3")
            {
                LMiddle = false;
            }
            if (collision.transform.parent.name == "L_RingTip" || collision.transform.parent.name == "L_Ring_3")
            {
                LRing = false;
            }
            if (collision.transform.parent.name == "L_ThumbTip" || collision.transform.parent.name == "L_Thumb_2")
            {
                LThumb = false;
            }
            if (collision.transform.name == "L_Palm" || collision.transform.parent.name == "L_GhostPalm")
            {
                LPalm = false;
            }
        }

        private void IsGrab(GameObject HandParent, FingerUseTracking fingerUseTracking, PressureTrackerMain ThePressureTracker, bool IsLeft)
        {
            ThePressureTracker?.HandGrabbingCheck(true);
            gameObject.transform.SetParent(HandParent.transform);

            #region Rigidbody Settings
            objectRigidbody.isKinematic = true;
            objectRigidbody.useGravity = false;
            objectRigidbody.interpolation = RigidbodyInterpolation.None;
            #endregion

            //Trigger Events
            if (isGrab && InvokeEventReady)
            {
                OnGrab?.Invoke();
                InvokeEventReady = false;
            }

            TriggerHaptics(ThePressureTracker, IsLeft);

            StartCoroutine(ResetGrab(fingerUseTracking, ThePressureTracker));
        }
        private void NotGrab(PressureTrackerMain ThePressureTracker)
        {
            ThePressureTracker?.HandGrabbingCheck(false); // change grab state back to false
            objectRigidbody.isKinematic = false;
            if (Gravity == Option.On) { objectRigidbody.useGravity = true; }
            objectRigidbody.interpolation = RigidbodyInterpolation.Extrapolate;

            gameObject.transform.SetParent(OriginalParent.transform);

            if (!InvokeEventReady)
            {
                OnRelease?.Invoke();
                InvokeEventReady = true;
                RemoveHaptics(ThePressureTracker);
            }
        }
        private void TriggerHaptics(PressureTrackerMain pressureTrackerMain, bool IsLeft)
        {
            if(ReadyToActivateGrab)
            {
                ReadyToActivateGrab = false;
                if (HapticStrength != 0)
                {
                    byte[][] ClutchState = new byte[0][]; // Start with an empty array

                    if (IsLeft) // Left hand hexr trigger
                    {
                        // Check and update left hand fingers directly
                        bool[] fingerStates = { LThumb, LIndex, LMiddle, LRing, LLittle, LPalm };

                        for (int i = 0; i < fingerStates.Length; i++)
                        {
                            switch (i)
                            {
                                case 0: // Thumb
                                    UpdateHapticState(ref LThumbHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 1: // Index
                                    UpdateHapticState(ref LIndexHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 2: // Middle
                                    UpdateHapticState(ref LMiddleHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 3: // Ring
                                    UpdateHapticState(ref LRingHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 4: // Little
                                    UpdateHapticState(ref LLittleHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 5: // Palm
                                    UpdateHapticState(ref LPalmHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        // Check and update right hand fingers directly
                        bool[] fingerStates = { RThumb, RIndex, RMiddle, RRing, RLittle, RPalm };

                        for (int i = 0; i < fingerStates.Length; i++)
                        {
                            switch (i)
                            {
                                case 0: // Thumb
                                    UpdateHapticState(ref RThumbHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 1: // Index
                                    UpdateHapticState(ref RIndexHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 2: // Middle
                                    UpdateHapticState(ref RMiddleHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 3: // Ring
                                    UpdateHapticState(ref RRingHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 4: // Little
                                    UpdateHapticState(ref RLittleHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                                case 5: // Palm
                                    UpdateHapticState(ref RPalmHaptics, fingerStates[i], ref ClutchState, i);
                                    break;
                            }
                        }
                    } // Right hand hexr trigger

                    // Send haptics data if there are any clutch states
                    if (ClutchState.Length > 0)
                    {
                        pressureTrackerMain.TriggerCustomHapticsIncrease(ClutchState, (byte)HapticStrength);
                    }
                }
                ReadyToActivateGrab = true;
            }

        }
        private void RemoveHaptics(PressureTrackerMain pressureTrackerMain)
        {
            if (HapticStrength == 0 ) return;

            pressureTrackerMain.RemoveAllHaptics();

            // Reset all haptic states
            LThumbHaptics = LIndexHaptics = LMiddleHaptics = LRingHaptics = LLittleHaptics = LPalmHaptics = false;
            RThumbHaptics = RIndexHaptics = RMiddleHaptics = RRingHaptics = RLittleHaptics = RPalmHaptics = false;
        }

        IEnumerator ResetGrab(FingerUseTracking fingerUseTracking, PressureTrackerMain ThePressureTracker)
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(0.2f);
            if (fingerUseTracking.isHandOpen() == true)
            {
                NotGrab(ThePressureTracker);
            }
            else
            {
                isGrab = false;
                StartCoroutine(ResetGrab(fingerUseTracking, ThePressureTracker));
            }

        }
        private void OnValidate()
        {
            // Snap HapticStrength to the nearest increment of 10
            HapticStrength = Mathf.Round(HapticStrength / 10) * 10;
        }

        private void SetUpBool()
        {
            RThumb = false; LThumb = false;
            RIndex = false; LIndex = false;
            RMiddle = false; LMiddle = false;
            RRing = false; LRing = false;
            RLittle = false; LLittle = false;
            RThumbHaptics = false; LThumbHaptics = false;
            RIndexHaptics = false; LIndexHaptics = false;
            RMiddleHaptics = false; LMiddleHaptics = false;
            RRingHaptics = false; LRingHaptics = false;
            RLittleHaptics = false; LLittleHaptics = false;
        }

        #region Helper functions
        private void UpdateHapticState(ref bool hapticState, bool fingerState, ref byte[][] clutchState, int fingerIndex)
        {
            if (fingerState && !hapticState) // Finger activated + no haptic
            {
                hapticState = true;
                AddToClutchState(ref clutchState, fingerIndex, 0);
            }
            else if (!fingerState && hapticState) // Finger deactivated + haptic active
            {
                hapticState = false;
                AddToClutchState(ref clutchState, fingerIndex, 2);
            }
        }

        private void AddToClutchState(ref byte[][] clutchState, int fingerIndex, byte state)
        {
            Array.Resize(ref clutchState, clutchState.Length + 1);
            clutchState[clutchState.Length - 1] = new byte[] { (byte)fingerIndex, state };
        }

        #endregion
    }
}
