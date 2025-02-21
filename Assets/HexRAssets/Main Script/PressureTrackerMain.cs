
using UnityEngine;
using System.Threading.Tasks;
using HaptGlove;
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
        public void IsPhysicsCollisionNear(bool Selection)
        {
            //uses proximmity checker to check if hand is near
            CollisionNearHand = Selection;
        }

        #endregion

        #region Basic Haptics Functions For Single Haptics Trigger

        // Single Finger Haptics increase.
        // Set a TargetPressure of 0 - 1
        public void SingleThumbHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            { 
                // byte[] btData = gloveHandler.haptics.ApplyHaptics(new byte[] { 0, 0 }, (byte)TargetPressure, false);

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Thumb,true, TargetPressure,1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleIndexHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Index, true, TargetPressure,1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleMiddleHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Middle, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SingleRingHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Ring, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePinkyHaptic(float TargetPressure)
        {
            if (IsHandNear() == true)
            {
                // btData contains the instruction for which haptics to be triggered and the incremented pressure

                byte[] btData = gloveHandler.haptics.HEXRPressure(Haptics.Finger.Pinky, true, TargetPressure, 1);
                gloveHandler.BTSend(btData);

            }
        }
        public void SinglePalmHaptic(float TargetPressure)
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


        /// <summary>
        /// Applies haptic feedback to a single channel with the specified pressure.
        /// </summary>
        /// <param name="States">True = Pressure In, False = Pressure Out </param>
        /// /// <param name="TargetPressure">The pressure level (0-1) for the haptic effect.</param>
        /// /// <param name="Speed">The time it takes for pressure to reach target pressure, 1 = fastest, 0 = slowest.</param>
        /// /// <param name="ByPassHandCheck">True to ignore if hand is near the object to trigger haptics.</param>
        public void CustomSingleHaptics(Haptics.Finger finger, bool States, float TargetPressure, float Speed, bool ByPassHandCheck )
        {
            if(!ByPassHandCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, States, TargetPressure, Speed);
                gloveHandler.BTSend(btData);
            }
            else if(ByPassHandCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRPressure(finger, States, TargetPressure, Speed);
                gloveHandler.BTSend(btData);
            }
        }

        /// <summary>
        /// Applies vibration feedback to a single channel with the specified pressure and frequency.
        /// </summary>
        /// <param name="States">True = Pressure In, False = Pressure Out </param>
        /// /// <param name="TargetPressure">The pressure level (0.1-1) for the haptic effect.</param>
        /// /// <param name="frequency">Vibration frequency 0.1-40hz</param>
        /// /// <param name="ByPassHandCheck">True to ignore if hand is near the object to trigger haptics.</param>
        public void CustomSingleVibrations(Haptics.Finger finger, bool States, float TargetPressure, float frequency, bool ByPassHandCheck)
        {
            if (!ByPassHandCheck && IsHandNear())
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, States, frequency, TargetPressure);
                gloveHandler.BTSend(btData);
            }
            else if (ByPassHandCheck)
            {
                byte[] btData = gloveHandler.haptics.HEXRVibration(finger, States, frequency, TargetPressure);
                gloveHandler.BTSend(btData);
            }
        }

        #endregion

        #region Basic Haptics Function For Multiple Trigger
        public void TriggerAllHapticsIncrease(float TargetPressure)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };
                bool[] TheBool = new bool[] { true, true, true, true, true, true };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerCustomHapticsIncrease(bool[] TheBool, float TargetPressure)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        }
        public void TriggerPinchPressure(float TargetPressure)
        {
            //Index and Thumb
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index };

                float[] ThePressure = new float[] { TargetPressure, TargetPressure };
                float[] TheSpeed = new float[] { 1, 1 };
                bool[] TheBool = new bool[] { true, true };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }
        }
        public void RemovePinchPressure()
        {

            //Index and Thumb
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index };

                float[] ThePressure = new float[] { 0, 0 };
                float[] TheSpeed = new float[] { 1, 1 };
                bool[] TheBool = new bool[] { false, false };

                byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);
                gloveHandler.BTSend(btData);
            }

        }
        public void TriggerAllVibrations(float VibrationStrength)
        {
            if (IsHandNear())
            {
                Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

                float[] TheFrequency = new float[] { VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength, VibrationStrength };
                float[] ThePressure = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
                bool[] TheBool = new bool[] { true, true, true, true, true, true };

                byte[] btData = gloveHandler.haptics.HEXRVibration(AllFingers, TheBool, TheFrequency, ThePressure);
                gloveHandler.BTSend(btData);

            }
        }
        public void RemoveAllHaptics()
        {

            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

            float[] ThePressure = new float[] { 0, 0, 0, 0, 0, 0 };
            float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };
            bool[] TheBool = new bool[] { false, false, false, false, false, false };

            byte[] btData = gloveHandler.haptics.HEXRPressure(AllFingers, TheBool, ThePressure, TheSpeed);

            gloveHandler.BTSend(btData);

        }
        public void RemoveAllVibrations()
        {
            Haptics.Finger[] AllFingers = new Haptics.Finger[] { Haptics.Finger.Thumb, Haptics.Finger.Index, Haptics.Finger.Middle, Haptics.Finger.Ring, Haptics.Finger.Pinky, Haptics.Finger.Palm };

            float[] TheFrequency = new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            float[] ThePressure = new float[] { 0f, 0f, 0f, 0f, 0f, 0f };
            float[] ThePeakRatio = new float[] { 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f };
            float[] TheSpeed = new float[] { 1, 1, 1, 1, 1, 1 };
            bool[] TheBool = new bool[] { false, false, false, false, false, false };

            byte[] btData = gloveHandler.haptics.HEXRVibration(AllFingers, TheBool, TheFrequency, ThePressure, ThePeakRatio, TheSpeed, ThePressure);
            gloveHandler.BTSend(btData);
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

