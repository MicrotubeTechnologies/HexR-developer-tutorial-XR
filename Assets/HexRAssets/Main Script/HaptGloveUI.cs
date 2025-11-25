using System.Collections;
using System.Collections.Generic;
using HaptGlove;
using UnityEngine;
using TMPro;
using UnityEngine.Android;

namespace HexR
{
    public class HaptGloveUI : MonoBehaviour
    {
        private HaptGloveHandler LeftHandPhysics, RightHandPhysics;
        private TextMeshProUGUI RightBtText, LeftBtText;
        private HaptGloveManager haptGloveManager;

        // Track which hand is currently controlled
        private bool isLeftHandControlled = false;

        void Start()
        {
            try
            {
                haptGloveManager = gameObject.GetComponent<HaptGloveManager>();
            }
            catch
            {
                Debug.Log("HaptGlove manager is not found.");
            }

            if (haptGloveManager != null)
            {
                RightBtText = haptGloveManager.RightBtText;
                LeftBtText = haptGloveManager.LeftBtText;
                LeftHandPhysics = haptGloveManager.leftHand;
                RightHandPhysics = haptGloveManager.rightHand;
            }
            else
            {
                Debug.Log("Please place HaptGloveManager in the same gameObject as HaptGloveUIOpenXR");
            }
        }

        public void ConnectRightBT()
        {
            CheckAndRequestPermissions();
            isLeftHandControlled = false;
            RightBtText.text = "Searching for device...";
            RightHandPhysics.GetComponent<HaptGloveHandler>().BTConnection();
        }

        public void ConnectLeftBT()
        {
/*            LeftBtText.text = "Entered";
            RevokePermissions();
            LeftBtText.text += " 2";
            CheckAndRequestPermissions();*/
            LeftBtText.text += " 2";
            isLeftHandControlled = true;
            LeftBtText.text = "Searching for device...";
            LeftHandPhysics.GetComponent<HaptGloveHandler>().BTConnection();
        }

        public void CheckAndRequestPermissions()
        {
            // Only activate if device is Android 12 (API 31) or greater
            if (Application.platform == RuntimePlatform.Android && GetAndroidApiLevel() >= 31)
            {
                // Request the necessary permissions
                Permission.RequestUserPermissions(new string[] {
                            Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                            "android.permission.BLUETOOTH_CONNECT"
                        });
            }
        }

        // Helper method to get the Android API level
        private int GetAndroidApiLevel()
        {
            string osVersion = SystemInfo.operatingSystem;
            // Extract the API level from the version string (e.g., "Android 12.0.0 / API 31")
            int apiLevel = 0;
            if (osVersion.Contains("API"))
            {
                string[] versionParts = osVersion.Split(' ');
                foreach (string part in versionParts)
                {
                    if (part.StartsWith("API"))
                    {
                        int.TryParse(part.Substring(4), out apiLevel);
                        break;
                    }
                }
            }
            return apiLevel;
        }
        private void RevokePermissions()
        {
            using (AndroidJavaClass activityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject activity = activityClass.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject packageManager = activity.Call<AndroidJavaObject>("getPackageManager"))
                    {
                        string packageName = activity.Call<string>("getPackageName");
                        using (AndroidJavaObject permissionManager = activity.Call<AndroidJavaObject>("getSystemService", "permission"))
                        {
                            permissionManager.Call("revokeRuntimePermission", packageName, "android.permission.BLUETOOTH_CONNECT", activity);
                        }
                    }
                }
            }
        }
    }
}
