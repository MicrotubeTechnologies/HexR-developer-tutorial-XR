using UnityEngine;
using HexR;
public class PlanksCheck : MonoBehaviour
{
    private bool PlanksOne = true, PlanksTwo = true;

    [HideInInspector]
    public bool Done = true;
    public GameObject Plank1Object, Plank2Object;
    public FP_Main fP_Main;

    private Vector3 plank1OriginalPos, plank2OriginalPos;
    private Quaternion plank1OriginalRot, plank2OriginalRot;

    private void Start()
    {
        // Store the original positions and rotations of the planks
        plank1OriginalPos = Plank1Object.transform.position;
        plank2OriginalPos = Plank2Object.transform.position;
        plank1OriginalRot = Plank1Object.transform.rotation;
        plank2OriginalRot = Plank2Object.transform.rotation;
    }

    private void OnEnable()
    {
        PlanksOne = true;
        PlanksTwo = true;
        Done = true;
    }

    private void OnDisable()
    {
        // Reset planks to their original positions and rotations
        Plank1Object.transform.position = plank1OriginalPos;
        Plank1Object.transform.rotation = plank1OriginalRot;

        Plank2Object.transform.position = plank2OriginalPos;
        Plank2Object.transform.rotation = plank2OriginalRot;
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Plank1")
        {
            PlanksOne = true;
        }
        if (other.name == "Plank2")
        {
            PlanksTwo = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Plank1")
        {
            PlanksOne = false;
        }
        if (other.name == "Plank2")
        {
            PlanksTwo = false;
        }
    }
    public void Checkit()
    {
        // Check if both planks are gone and call function
        if (!PlanksOne && !PlanksTwo && !Done)
        {
            Done = true;
            fP_Main.TextTwo(); // Trigger the function when both planks are removed
        }
    }
}


