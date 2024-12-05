    using System;
using System.Collections;
using System.Collections.Generic;
using HaptGlove;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit.UI.BodyUI;
using System.Linq;
using UnityEngine.UI;
using Unity.VisualScripting;
namespace HexR
{
    public class HaptGloveManager : MonoBehaviour
    {
        public bool isQuest;

        public HaptGloveHandler leftHand;
        public HaptGloveHandler rightHand;
        public GameObject HandMenu;
        public GameObject NewHandMenu;

        public GameObject btIndicator_L;
        public GameObject btIndicator_R, pumpIndicator_L, pumpIndicator_R, HexRPanel;
        private string bluetoothLog;
        public TextMeshProUGUI RightBtText, LeftBtText;
        void Start()
        {
            if (isQuest)
            {
                leftHand.isQuest = true;
                rightHand.isQuest = true;
            }
            else
            {
                leftHand.isQuest = false;
                rightHand.isQuest = false;
            }

            leftHand.onBluetoothConnected += HaptGlove_OnConnected;
            leftHand.onBluetoothConnectionFailed += HaptGlove_OnConnectedFailed;
            leftHand.onBluetoothDisconnected += HaptGlove_OnDisconnected;
            leftHand.onPumpAction += HaptGlove_OnPumpAction;

            rightHand.onBluetoothConnected += HaptGlove_OnConnected;
            rightHand.onBluetoothConnectionFailed += HaptGlove_OnConnectedFailed;
            rightHand.onBluetoothDisconnected += HaptGlove_OnDisconnected;
            rightHand.onPumpAction += HaptGlove_OnPumpAction;

            //Add all layers that you want to interact with HaptGlove
            AddHaptGloveInteractableLayer("HaptGloveInteractable");
        }


        private void HaptGlove_OnConnected(HaptGloveHandler.HandType hand)
        {
            if (hand == HaptGloveHandler.HandType.Left)
            {
                btIndicator_L.SetActive(true);
                LeftBtText.text = "Left Glove Connected";
                bluetoothLog = "Left glove connected: " + "HaptGLove " + hand.ToString();
                StartCoroutine(Pump(leftHand.GetComponent<HaptGloveHandler>()));
            }
            else if (hand == HaptGloveHandler.HandType.Right)
            {
                btIndicator_R.SetActive(true);
                RightBtText.text = "Right Glove Connected";
                bluetoothLog = "Right glove connected: " + "HaptGLove " + hand.ToString();
                StartCoroutine(Pump(rightHand.GetComponent<HaptGloveHandler>()));
            }

        }

        IEnumerator Pump(HaptGloveHandler haptGloveHandler)
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(2f);

            haptGloveHandler.AirPressureSourceControl();
        }

        private void HaptGlove_OnConnectedFailed(HaptGloveHandler.HandType hand)
        {
            if (hand == HaptGloveHandler.HandType.Left)
            {
                btIndicator_L.SetActive(false);
                LeftBtText.text = "Connection failed, try again";
                bluetoothLog = "Left glove connection failed: " + "HaptGlove " + hand.ToString();
            }
            else if (hand == HaptGloveHandler.HandType.Right)
            {
                btIndicator_R.SetActive(false);
                RightBtText.text = "Connection failed, try again";
                bluetoothLog = "Right glove connection failed: " + "HaptGlove " + hand.ToString();
            }
            HexRPanel.SetActive(true);
        }

        private void HaptGlove_OnDisconnected(HaptGloveHandler.HandType hand)
        {
            if (hand == HaptGloveHandler.HandType.Left)
            {
                btIndicator_L.SetActive(false);
                LeftBtText.text = "HexR Left Disconnected";
                bluetoothLog = "Left glove disconnected: " + "HaptGlove " + hand.ToString();
            }
            else if (hand == HaptGloveHandler.HandType.Right)
            {
                btIndicator_R.SetActive(false);
                RightBtText.text = "HexR Right Disconnected";
                bluetoothLog = "Right glove disconnected: " + "HaptGlove " + hand.ToString();
            }
            HexRPanel.SetActive(true);
        }

        private void HaptGlove_OnPumpAction(HaptGloveHandler.HandType hand, bool state)
        {
            if (hand == HaptGloveHandler.HandType.Left)
            {
                LeftBtText.text = "Left Glove Ready";
                if (state)
                    pumpIndicator_L.SetActive(true);
                else
                    pumpIndicator_L.SetActive(false);
            }
            else if (hand == HaptGloveHandler.HandType.Right)
            {
                RightBtText.text = "Right Glove Ready";
                if (state)
                    pumpIndicator_R.SetActive(true);
                else
                    pumpIndicator_R.SetActive(false);
            }
        }

        public void AddHaptGloveInteractableLayer(string layerName)
        {
            leftHand.hapticsInteratableLayers.Add(LayerMask.NameToLayer(layerName));
            rightHand.hapticsInteratableLayers.Add(LayerMask.NameToLayer(layerName));
        }

        public string[] GetHaptGloveInteractableLayer()
        {
            int[] layers = rightHand.hapticsInteratableLayers.ToArray();
            string[] layerNames = new string[layers.Length];

            for (int i = 0; i < layers.Length; i++)
            {
                layerNames[i] = LayerMask.LayerToName(layers[i]);
            }

            return layerNames;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(HaptGloveManager))]
    public class HexRSettingEditorGUI : Editor
    {
        public override void OnInspectorGUI()
        {

            // Get reference to the target script
            HaptGloveManager controller = (HaptGloveManager)target;

            controller.rightHand = (HaptGloveHandler)EditorGUILayout.ObjectField("Right Hand Physics",controller.rightHand,
                typeof(HaptGloveHandler), // Corrected type
                true
            );

            controller.leftHand = (HaptGloveHandler)EditorGUILayout.ObjectField("Left Hand Physics",controller.leftHand,
                typeof(HaptGloveHandler), // Corrected type
                true
            );

            controller.HandMenu = (GameObject)EditorGUILayout.ObjectField("HexR Hand Menu",controller.HandMenu, typeof(GameObject), true);

            // Add vertical spacing
            GUILayout.Space(15); // Adds 10 pixels of space

            controller.btIndicator_L = (GameObject)EditorGUILayout.ObjectField("btIndicator_L", controller.btIndicator_L, typeof(GameObject), true);
            controller.btIndicator_R = (GameObject)EditorGUILayout.ObjectField("btIndicator_R", controller.btIndicator_R, typeof(GameObject), true);
            controller.pumpIndicator_L = (GameObject)EditorGUILayout.ObjectField("pumpIndicator_L", controller.pumpIndicator_L, typeof(GameObject), true);
            controller.pumpIndicator_R = (GameObject)EditorGUILayout.ObjectField("pumpIndicator_R", controller.pumpIndicator_R, typeof(GameObject), true);
            controller.LeftBtText = (TextMeshProUGUI)EditorGUILayout.ObjectField("LeftBtText", controller.LeftBtText, typeof(TextMeshProUGUI), true);
            controller.RightBtText = (TextMeshProUGUI)EditorGUILayout.ObjectField("RightBtText", controller.RightBtText, typeof(TextMeshProUGUI), true);


            // Add vertical spacing
            GUILayout.Space(15); // Adds 10 pixels of space

            if (GUILayout.Button("Auto Set Up HexR"))
            {
                try
                {
                    controller.rightHand = GameObject.Find("Right Hand Physics").GetComponent<HaptGloveHandler>(); // Replace with the name of your target object
                    controller.leftHand = GameObject.Find("Left Hand Physics").GetComponent<HaptGloveHandler>(); // Replace with the name of your target object
                    Debug.Log("Right Hand Physics Found And Assigned.");
                }
                catch
                {
                    Debug.Log("HaptGloveHandler Not Found Remember to assign them.");
                }

                try
                {
                    controller.NewHandMenu = Instantiate(controller.HandMenu);
                    DestroyImmediate(controller.HandMenu );
                    controller.NewHandMenu.transform.SetParent(GameObject.Find("Camera Offset").transform);
                    controller.NewHandMenu.transform.localPosition = Vector3.zero;
                    controller.HandMenu = controller.NewHandMenu;
                    // Directly find inactive GameObjects
                    controller.btIndicator_L = GameObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(obj => obj.name == "Bluetooth Indicator L");
                    controller.pumpIndicator_L = GameObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(obj => obj.name == "Pump Indicator L");
                    controller.btIndicator_R = GameObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(obj => obj.name == "Bluetooth Indicator R");
                    controller.pumpIndicator_R = GameObject.FindObjectsOfType<GameObject>(true).FirstOrDefault(obj => obj.name == "Pump Indicator R");
                    controller.LeftBtText = GameObject.FindObjectsOfType<TextMeshProUGUI>(true).FirstOrDefault(obj => obj.name == "Left HexR Text");
                    controller.RightBtText = GameObject.FindObjectsOfType<TextMeshProUGUI>(true).FirstOrDefault(obj => obj.name == "Right HexR Text");

                    Debug.Log("HexR Hand Menu Set Up Complete");
                }
                catch
                {
                    Debug.Log("HexR panel is not set up, Manual Set up needed");

                }

                try
                {
                    GameObject LeftXR = GameObject.Find("Left Hand Interaction Visual");
                    GameObject RightXR = GameObject.Find("Right Hand Interaction Visual");
                    PhysicsHandTracking LeftP = controller.leftHand.gameObject.GetComponent<PhysicsHandTracking>();
                    PhysicsHandTracking RightP = controller.rightHand.gameObject.GetComponent<PhysicsHandTracking>();
                    LeftP.handRoot = LeftXR.transform.Find("L_Wrist");
                    RightP.handRoot = RightXR.transform.Find("R_Wrist");
                    EditorUtility.SetDirty(LeftP); // Mark as dirty to save changes
                    EditorUtility.SetDirty(RightP); // Mark as dirty to save changes

                }
                catch
                {
                    Debug.Log("XR hand is linked not linked to Physics hand tracking, manual link needed, Drag the hand root of your vr hand to the left and right physicshandtracking script");
                }
                EditorUtility.SetDirty(controller); // Mark as dirty to save changes
            }
            // Save changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }

#endif
}
