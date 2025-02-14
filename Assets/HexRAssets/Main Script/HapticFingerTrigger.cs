using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HaptGlove;
using UnityEngine.UI;
using TMPro;

namespace HexR
{
    public class HapticFingerTrigger : MonoBehaviour
    {
        private HaptGloveHandler gloveHandler;
        public GameObject HexrLeftOrRight;
        private PressureTrackerMain pressureTrackerMain;
        public HandType handType;
        public FingerType fingertype;
        private Haptics.Finger HapticsFingertype;
        private byte[] clutchStateIn, clutchStateOut;
        private int Pressure;
        public enum FingerType
        {
            Index,
            Middle,
            Ring,
            Thumb,
            Little,
            Palm
        };
        public enum HandType
        {
            Left,
            Right
        };

        //This controls the haptic being send to the individual fingers after it has been triggered by a collider that is
        //on the gameobject that the hand is touching

        // Start is called before the first frame update
        void Start()
        {
            //Debug = GameObject.Find("Panel Debug").GetComponent<Text>();
            Pressure = 0;
            gloveHandler = HexrLeftOrRight.GetComponent<HaptGloveHandler>();
            pressureTrackerMain = HexrLeftOrRight.GetComponent<PressureTrackerMain>();
            if (fingertype == FingerType.Thumb)
            {
                clutchStateIn = new byte[] { 0, 0 };
                clutchStateOut = new byte[] { 0, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Index)
            {
                clutchStateIn = new byte[] { 1, 0 };
                clutchStateOut = new byte[] { 1, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Middle)
            {
                clutchStateIn = new byte[] { 2, 0 };
                clutchStateOut = new byte[] { 2, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Ring)
            {
                clutchStateIn = new byte[] { 3, 0 };
                clutchStateOut = new byte[] { 3, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Little)
            {
                clutchStateIn = new byte[] { 4, 0 };
                clutchStateOut = new byte[] { 4, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
            else if (fingertype == FingerType.Palm)
            {
                clutchStateIn = new byte[] { 5, 0 };
                clutchStateOut = new byte[] { 5, 2 };
                HapticsFingertype = Haptics.Finger.Thumb;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void TriggerFixPressure(byte TargetPressure)
        {
            if (TargetPressure > Pressure)
            {

                int IncreasePressure = (int)TargetPressure - Pressure;

                pressureTrackerMain.TriggerSingleHapticsIncrease(clutchStateIn, IncreasePressure, true);
            }

        }
        public void TriggerVibrationPressure(float Frequency,float Intensity, float PeakRatio, float Speed)
        {
            byte[] btData = gloveHandler.haptics.HEXRVibration(HapticsFingertype, true, Frequency, Intensity, PeakRatio, Speed, Intensity);
            gloveHandler.BTSend(btData);
        }
        public void RemoveVibration()
        {
            byte[] btData = gloveHandler.haptics.HEXRVibration(HapticsFingertype, false, 0, 0, 0, 0, 0);
            gloveHandler.BTSend(btData);
        }
        public void RemoveHaptics()
        {
            if (Pressure != 0)
            {
                pressureTrackerMain.RemoveSingleHaptics(clutchStateOut,true);
            }

        }

    }
}
