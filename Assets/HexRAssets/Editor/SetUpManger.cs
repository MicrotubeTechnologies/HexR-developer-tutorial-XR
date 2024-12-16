using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace HexR
{
    public class QuickLinksManager : EditorWindow
    {
        private string OpenXRGithubLink = "https://github.com/MicrotubeTechnologies/HexR-developer-tutorial-XR";
        private string MetaOVRGithubLink = "https://github.com/MicrotubeTechnologies/HexR-Developer-Tutorial-Meta-OVR";
        private string MicrotubeWebsiteLink = "https://microtube.tech/hexr-glove/";

        // Menu item to show the Quick Links window
        [MenuItem("HexR Menu/Quick Links")]
        public static void ShowQuickLinksWindow()
        {
            GetWindow<QuickLinksManager>("Quick Links");
        }

        private void OpenExternalLink(string link)
        {
            Application.OpenURL(link);
        }

        private void OnGUI()
        {
            GUILayout.Label("Quick Links", EditorStyles.boldLabel);

            if (GUILayout.Button("OpenXR Documentation"))
            {
                OpenExternalLink(OpenXRGithubLink);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("MetaOVR Documentation"))
            {
                OpenExternalLink(MetaOVRGithubLink);
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Microtube Website"))
            {
                OpenExternalLink(MicrotubeWebsiteLink);
            }
        }
    }

    public class HexRSetupManager : EditorWindow
    {
        private static readonly string[] RequiredPackages = {
            "com.unity.xr.arfoundation",
            "com.unity.xr.openxr",
            "com.unity.xr.interaction.toolkit",
            "com.unity.xr.hands",
            "com.unity.textmeshpro"
        };

        private AddRequest addRequest;

        // Menu item to show the HexR Set Up window
        [MenuItem("HexR Menu/HexR Set Up")]
        public static void ShowSetupWindow()
        {
            GetWindow<HexRSetupManager>("HexR Set Up");
        }

        private void InstallRequiredPackages()
        {
            foreach (var package in RequiredPackages)
            {
                Debug.Log($"Checking and installing package: {package}");
                addRequest = Client.Add(package);
                EditorApplication.update += Progress;
            }
        }

        private void Progress()
        {
            if (addRequest.IsCompleted)
            {
                if (addRequest.Status == StatusCode.Success)
                {
                    Debug.Log($"Package {addRequest.Result.packageId} installed successfully.");
                }
                else if (addRequest.Status >= StatusCode.Failure)
                {
                    Debug.LogError($"Failed to install package: {addRequest.Error.message}");
                }

                EditorApplication.update -= Progress;
            }
        }

        private void OnGUI()
        {
            GUILayout.Label("HexR Set Up", EditorStyles.boldLabel);

            if (GUILayout.Button("Install Required Packages"))
            {
                InstallRequiredPackages();
            }
        }
    }
}

