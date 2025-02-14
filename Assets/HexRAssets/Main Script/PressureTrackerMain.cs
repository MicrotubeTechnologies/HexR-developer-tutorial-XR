
using UnityEngine;
using System.Threading.Tasks;
using HaptGlove;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.XR.Interaction.Toolkit;
namespace HexR
{
    public class PressureTrackerMain : MonoBehaviour
    {


        [HideInInspector]
        public int ThumbPressure, IndexPressure, MiddlePressure, RingPressure, LittlePressure, PalmPressure, TankPressure;
        [HideInInspector]
        public HaptGloveHandler gloveHandler;
        private HaptGloveManager haptGloveManager;
        [HideInInspector]
        public bool HandGrabbing, PokeHovering, CollisionNearHand;
        //This is the central control for the pressure on each finger
        //As we want them to be within 60 KPA

        // Start is called before the first frame update
        void Start()
        {
            haptGloveManager = gameObject?.GetComponentInParent<HaptGloveManager>();
            gloveHandler = gameObject.GetComponent<HaptGloveHandler>();
            ThumbPressure = 0;
            IndexPressure = 0;
            MiddlePressure = 0;
            RingPressure = 0;
            LittlePressure = 0;
            PalmPressure = 0;
            TankPressure = 0;
            CollisionNearHand = false;
            HandGrabbing = false;
            PokeHovering = false;

        }

        // Update is called once per frame
        void Update()
        {

            int[] AirPressure = gloveHandler?.GetAirPressure();
            if (AirPressure != null)
            {
                ThumbPressure = ((int)Math.Round(AirPressure[0] / 100000.0) * 100000) - 100000;
                IndexPressure = ((int)Math.Round(AirPressure[1] / 100000.0) * 100000) - 100000;
                MiddlePressure = ((int)Math.Round(AirPressure[2] / 100000.0) * 100000) - 100000;
                RingPressure = ((int)Math.Round(AirPressure[3] / 100000.0) * 100000) - 100000;
                LittlePressure = ((int)Math.Round(AirPressure[4] / 100000.0) * 100000) - 100000;
                PalmPressure = ((int)Math.Round(AirPressure[5] / 100000.0) * 100000) - 100000;
                TankPressure = ((int)Math.Round(AirPressure[6] / 100000.0) * 100000) - 100000;
            }
        }

        #region Hand Proximity Test

        public void HandGrabbingCheck(bool IsHandGrabbing)
        {
            HandGrabbing = IsHandGrabbing;
        }
        public bool IsPhysicsCollisionNear(bool CollisionNearHand)
        {
            //uses proximmity checker to check if hand is near
            return CollisionNearHand;
        }

        #endregion

        #region Basic Haptics Functions For Single Haptics Trigger

        // Single Finger Haptics increase.
        // Set a TargetPressure of 0 - 1

        #region Simple Single haptics Functions
        public void SingleThumbHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            { 
                // byte[] btData = gloveHandler.haptics.ApplyHaptics(new byte[] { 0, 0 }, (byte)TargetPressure, false);

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Thumb,true, TargetPressure,1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleIndexHaptic(int TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Index, true, TargetPressure,1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleMiddleHaptic(int TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Middle, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleRingHaptic(int TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Ring, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePinkyHaptic(int TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Pinky, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePalmHaptic(int TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Palm, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);
            }
        }
        public void RemoveThumbHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Thumb, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveIndexHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Index, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveMiddleHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Middle, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemoveRingHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Ring, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemovePinkyHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Pinky, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        public void RemovePalmHaptics()
        {
            byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Palm, false, 0, 1);
            gloveHandler.BTSend(btData);
        }
        #endregion

        public void CustomSingleHaptics(Haptics.Finger finger, bool states, float intensity, float speed, bool ByPassHandCheck )
        {
            if(!ByPassHandCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, states, intensity, speed);
                gloveHandler.BTSend(btData);
            }
            else if(ByPassHandCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, states, intensity, speed);
                gloveHandler.BTSend(btData);
            }
        }

        // Single Finger vibrations increase.
        
        public void CustomSingleVibrations(Haptics.Finger finger, bool states, float intensity, float frequency,float PeakRatio,float Speed,float endIntensity, bool ByPassHandCheck)
        {
            if (!ByPassHandCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, states, frequency, intensity, PeakRatio, Speed, endIntensity);
                gloveHandler.BTSend(btData);
            }
            else if (ByPassHandCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, states, frequency, intensity, PeakRatio, Speed, endIntensity);
                gloveHandler.BTSend(btData);
            }
        }

        // These Functions are depreciating 
        public void TriggerSingleHapticsIncrease(byte[] FingerTypeByte, int TargetPressure, bool ByPassHandInteractionCheck)
        {
            if (IsHandNear() == true || ByPassHandInteractionCheck == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure
                byte[] btData = gloveHandler.haptics.ApplyHaptics(FingerTypeByte, (byte)TargetPressure, false);
                gloveHandler.BTSend(btData);
            }
        }
        public void RemoveSingleHaptics(byte[] FingerTypeByte, bool ByPassHandInteractionCheck)
        {
            if (IsHandNear() == true || ByPassHandInteractionCheck == true)
            {
                byte[] btData = gloveHandler.haptics.ApplyHaptics(FingerTypeByte, (byte)60, false);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerSingleVibrations(byte[] FingerTypeByte, byte Frequency, byte HapticStrength, bool ByPassHandInteractionCheck)
        {
            if (IsHandNear() == true || ByPassHandInteractionCheck == true)
            {

                byte[] btData = gloveHandler.haptics.ApplyHaptics(Frequency, FingerTypeByte, HapticStrength, false);
                gloveHandler.BTSend(btData);

            }
        }
        public void RemoveSingleVibration(byte[] FingerTypeByte, byte Frequency)
        {

            byte[] btData = gloveHandler.haptics.ApplyHaptics(Frequency, FingerTypeByte, 60, false);
            gloveHandler.BTSend(btData);
        }
        #endregion

        #region Basic Haptics Function For Multiple Trigger
        public void TriggerAllHapticsIncrease(int TargetPressure)
        {
            if (IsHandNear())
            {
                // ClutchState affecting all indenters
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }
                            , new byte[] { 5, 0 }};
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, (byte)TargetPressure, false);
                gloveHandler.BTSend(btData);

            }
        }
        public void TriggerCustomHapticsIncrease(byte[][] FingerTypeByte, int TargetPressure)
        {
            if (IsHandNear())
            {
                // ClutchState affecting all indenters
                byte[] btData = gloveHandler.haptics.ApplyHaptics(FingerTypeByte, (byte)TargetPressure, false);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerPinchPressure(int TargetPressure)
        {
            //Index and Thumb
            if (IsHandNear())
            {
                // ClutchState affecting all indenters
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 } };
                byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, (byte)TargetPressure, false);
                gloveHandler.BTSend(btData);

                //Update Pressure status
                ThumbPressure = ThumbPressure + TargetPressure;
                IndexPressure = IndexPressure + TargetPressure;
            }
        }
        public void TriggerAllVibrations(int VibrationStrength)
        {
            if (IsHandNear())
            {
                // ClutchState affecting all indenters
                byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }
                            , new byte[] { 5, 0 }};
                byte[] btData = gloveHandler.haptics.ApplyHaptics((byte)VibrationStrength, ClutchState, (byte)30, false);
                gloveHandler.BTSend(btData);

            }
        }
        public void RemoveAllHaptics()
        {
            // ClutchState 1st Number: 0 = Thumb, 1 = Index, 2 = Middle, 3 = Ring, 4 = Little, 5 = Palm
            // ClutchState 2nd Number: 0 = Pressure In , 2 = Pressure Out

            byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 2 }
                            , new byte[] { 5, 2 }};

            byte[] btData = gloveHandler.haptics.ApplyHaptics((byte)0, ClutchState, (byte)60, false);
            gloveHandler.BTSend(btData);
            ThumbPressure = 0; IndexPressure = 0; MiddlePressure = 0; RingPressure = 0; LittlePressure = 0; PalmPressure = 0;
        }
        public void RemoveAllVibrations()
        {
            // ClutchState 1st Number: 0 = Thumb, 1 = Index, 2 = Middle, 3 = Ring, 4 = Little, 5 = Palm
            // ClutchState 2nd Number: 0 = Pressure In , 2 = Pressure Out

            byte[][] ClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }, new byte[] { 4, 2 }
                            , new byte[] { 5, 2 }};

            byte[] btData = gloveHandler.haptics.ApplyHaptics((byte)60, ClutchState, (byte)60, false);
            gloveHandler.BTSend(btData);
            ThumbPressure = 0; IndexPressure = 0; MiddlePressure = 0; RingPressure = 0; LittlePressure = 0; PalmPressure = 0;
        }

        public async void TriggerPulseIt()
        {
            // pressure is first increase by 10 and after a delay it is reduce by 10
            // Currently used in drum scene to simulate pulse hitting a drum
            await PulseAllFinger();
        }
        public async Task PulseAllFinger()
        {
            // add pressure
            if (ThumbPressure + 10 < 60)
            {
                ThumbPressure = ThumbPressure + 10;
            }
            if (IndexPressure + 10 < 60)
            {
                IndexPressure = IndexPressure + 10;
            }
            if (MiddlePressure + 10 < 60)
            {
                MiddlePressure = MiddlePressure + 10;
            }
            if (RingPressure + 10 < 60)
            {
                RingPressure = RingPressure + 10;
            }
            if (LittlePressure + 10 < 60)
            {
                LittlePressure = LittlePressure + 10;
            }
            if (PalmPressure + 10 < 60)
            {
                PalmPressure = PalmPressure + 10;
            }

            byte[][] ClutchState = new byte[][] { new byte[] { 0, 0 }, new byte[] { 1, 0 }, new byte[] { 2, 0 }, new byte[] { 3, 0 }, new byte[] { 4, 0 }
                            , new byte[] { 5, 0 }};

            byte[] btData = gloveHandler.haptics.ApplyHaptics(ClutchState, (byte)10, false);
            gloveHandler.BTSend(btData);
            // Wait for 0.3 seconds
            await Task.Delay(100);

            // Remove pressure
            if (10 <= ThumbPressure && ThumbPressure <= 60)
            {
                ThumbPressure = ThumbPressure - 10;
            }
            if (10 <= IndexPressure && IndexPressure <= 60)
            {
                IndexPressure = IndexPressure - 10;
            }
            if (10 <= MiddlePressure && MiddlePressure <= 60)
            {
                MiddlePressure = MiddlePressure - 10;
            }
            if (10 <= RingPressure && RingPressure <= 60)
            {
                RingPressure = RingPressure - 10;
            }
            if (10 <= LittlePressure && LittlePressure <= 60)
            {
                LittlePressure = LittlePressure - 10;
            }
            if (10 <= PalmPressure && PalmPressure <= 60)
            {
                PalmPressure = PalmPressure - 10;
            }

            byte[][] RemoveClutchState = new byte[][] { new byte[] { 0, 2 }, new byte[] { 1, 2 }, new byte[] { 2, 2 }, new byte[] { 3, 2 }
                                , new byte[] { 4, 2 } , new byte[] { 5, 2 }};

            byte[] RbtData = gloveHandler.haptics.ApplyHaptics(RemoveClutchState, (byte)10, false);
            gloveHandler.BTSend(RbtData);
        }

        #endregion

        #region Helpers

        private bool IsHandNear()
        {
            //Only trigger haptics if hand is near

            if (HandGrabbing == true || PokeHovering == true || CollisionNearHand == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}

