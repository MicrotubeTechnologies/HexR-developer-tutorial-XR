using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HexR;
using TMPro;
using UnityEngine.Events;
public class HexRGrabbable : MonoBehaviour
{
    public enum Options { PinchGrab, PalmGrab }
    public Options TypeOfGrab;

    public UnityEvent OnGrab, OnRelease;


    private GameObject RHandParent, LHandParent;
    private GameObject OriginalParent;
    bool RThumb, RIndex, RLittle, RMiddle, RRing, RPalm;
    bool LThumb, LIndex, LLittle, LMiddle, LRing, LPalm;
    private FingerUseTracking RfingerUseTracking,LfingeruseTracking;
    private Rigidbody objectRigidbody;

    [HideInInspector]
    public bool isGrab = false;
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

        if (LeftHand != null) { LfingeruseTracking = LeftHand.GetComponent<FingerUseTracking>(); }
        else { Debug.Log("Left hand is not found"); }

        objectRigidbody = gameObject.GetComponent<Rigidbody>();
        OriginalParent = gameObject.transform.parent.gameObject;
        RThumb = false; LThumb = false;
        RIndex = false; LIndex = false;
        RMiddle = false; LMiddle = false;
        RRing = false; LRing = false;
        RLittle = false; LLittle = false;

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
                    IsGrab(RHandParent, RfingerUseTracking);
                }
                else
                {
                    NotGrab();
                }
            }
            if (LThumb)
            {
                if (LIndex || LMiddle || LRing || LLittle)
                {

                    IsGrab(LHandParent, LfingeruseTracking);
                }
                else
                {
                    NotGrab();
                }
            }
        }
        else if(TypeOfGrab == Options.PalmGrab)
        {
            if (RPalm)
            {
                if (RIndex || RMiddle || RRing || RLittle)
                {
                    IsGrab(RHandParent, RfingerUseTracking);
                }
                else
                {
                    NotGrab();
                }
            }
            if (LPalm)
            {
                if (LIndex || LMiddle || LRing || LLittle)
                {

                    IsGrab(LHandParent, LfingeruseTracking);
                }
                else
                {
                    NotGrab();
                }
            }
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.parent.name == "R_IndexTip")
        {
            RIndex = true;
        }
        if (collision.transform.parent.name == "R_LittleTip")
        {
            RLittle = true;
        }
        if (collision.transform.parent.name == "R_MiddleTip")
        {
            RMiddle = true;
        }
        if (collision.transform.parent.name == "R_RingTip")
        {
            RRing = true;
        }
        if (collision.transform.parent.name == "R_ThumbTip")
        {
            if(TypeOfGrab == Options.PinchGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                RHandParent.transform.position = contactPoint;
                RHandParent.transform.parent = collision.transform;
            }
            RThumb = true;
        }
        if (collision.transform.name == "R_Palm")
        {
            if (TypeOfGrab == Options.PalmGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                RHandParent.transform.position = contactPoint;
                RHandParent.transform.parent = collision.transform;
            }
            RPalm = true;
        }

        if (collision.transform.parent.name == "L_IndexTip")
        {
            LIndex = true;
        }
        if (collision.transform.parent.name == "L_LittleTip")
        {
            LLittle = true;
        }
        if (collision.transform.parent.name == "L_MiddleTip")
        {
            LMiddle = true;
        }
        if (collision.transform.parent.name == "L_RingTip")
        {
            LRing = true;
        }
        if (collision.transform.parent.name == "L_ThumbTip")
        {
            if(TypeOfGrab == Options.PinchGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                LHandParent.transform.position = contactPoint;
                LHandParent.transform.parent = collision.transform;
            }
            LThumb = true;
        }
        if (collision.transform.name == "L_Palm")
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
        if (collision.transform.parent.name == "R_IndexTip")
        {
            RIndex = true;
        }
        if (collision.transform.parent.name == "R_LittleTip")
        {
            RLittle = true;
        }
        if (collision.transform.parent.name == "R_MiddleTip")
        {
            RMiddle = true;
        }
        if (collision.transform.parent.name == "R_RingTip")
        {
            RRing = true;
        }
        if (collision.transform.parent.name == "R_ThumbTip")
        {
            if (TypeOfGrab == Options.PinchGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                RHandParent.transform.position = contactPoint;
                RHandParent.transform.parent = collision.transform;
            }
            RThumb = true;
        }
        if (collision.transform.name == "R_Palm")
        {
            if (TypeOfGrab == Options.PalmGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                RHandParent.transform.position = contactPoint;
                RHandParent.transform.parent = collision.transform;
            }
            RPalm = true;
        }

        if (collision.transform.parent.name == "L_IndexTip")
        {
            LIndex = true;
        }
        if (collision.transform.parent.name == "L_LittleTip")
        {
            LLittle = true;
        }
        if (collision.transform.parent.name == "L_MiddleTip")
        {
            LMiddle = true;
        }
        if (collision.transform.parent.name == "L_RingTip")
        {
            LRing = true;
        }
        if (collision.transform.parent.name == "L_ThumbTip")
        {
            if (TypeOfGrab == Options.PinchGrab)
            {
                Vector3 contactPoint = collision.ClosestPoint(transform.position);
                LHandParent.transform.position = contactPoint;
                LHandParent.transform.parent = collision.transform;
            }
            LThumb = true;
        }
        if (collision.transform.name == "L_Palm")
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
        if (collision.transform.parent.name == "R_IndexTip")
        {
            RIndex = false;
        }
        if (collision.transform.parent.name == "R_LittleTip")
        {
            RLittle = false;
        }
        if (collision.transform.parent.name == "R_MiddleTip")
        {
            RMiddle = false;
        }
        if (collision.transform.parent.name == "R_RingTip")
        {
            RRing = false;
        }
        if (collision.transform.parent.name == "R_ThumbTip")
        {
            RThumb = false;
        }
        if (collision.transform.name == "R_Palm")
        {
            RPalm = false;
        }

        if (collision.transform.parent.name == "L_IndexTip")
        {
            LIndex = false;
        }
        if (collision.transform.parent.name == "L_LittleTip")
        {
            LLittle = false;
        }
        if (collision.transform.parent.name == "L_MiddleTip")
        {
            LMiddle = false;
        }
        if (collision.transform.parent.name == "L_RingTip")
        {
            LRing = false;
        }
        if (collision.transform.parent.name == "L_ThumbTip")
        {
            LThumb = false;
        }
        if (collision.transform.name == "L_Palm")
        {
            LPalm = false;
        }
    }
    private void IsGrab(GameObject HandParent, FingerUseTracking fingerUseTracking)
    {
        isGrab = true;
        objectRigidbody.isKinematic = true;
        objectRigidbody.useGravity = false;
        objectRigidbody.interpolation = RigidbodyInterpolation.None;
        gameObject.transform.SetParent(HandParent.transform);
        if (isGrab) { OnGrab?.Invoke(); }

        if (fingerUseTracking.isHandOpen() == true)
        {
            NotGrab();
        }

    }
    private void NotGrab()
    {
        isGrab = false;
        objectRigidbody.isKinematic = false;
        objectRigidbody.useGravity = true;
        objectRigidbody.interpolation = RigidbodyInterpolation.Extrapolate;
        gameObject.transform.SetParent(OriginalParent.transform);
        OnRelease?.Invoke();
    }
}
