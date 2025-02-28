using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRateCollider : MonoBehaviour
{
    public FP_Main fP_Main;
    private bool Done = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        Done = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.Contains("Middle")|| other.name.Contains("Index"))
        {
            if (!Done) { fP_Main.TextSix(); Done = true;  }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Middle") || other.name.Contains("Index"))
        {
            if (!Done) { fP_Main.TextSix(); Done = true; }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name.Contains("Middle") || other.name.Contains("Index"))
        {

        }
    }

    public void Restartbool()
    {
        Done = false;
    }
}
