using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatingTrigger : MonoBehaviour
{
    public FP_Main fP_Main;
    private bool Done = true, NextPart = false, Completed = false;
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
        if (other.name.Contains("Middle") || other.name.Contains("Index"))
        {
            if (!Done) { Done = true; fP_Main.TextFour(); StartCoroutine(Delay()); }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.name.Contains("Middle") || other.name.Contains("Index"))
        {
            if (!Done) { Done = true; fP_Main.TextFour(); StartCoroutine(Delay()); }
            
            if (NextPart && !Completed) { Completed = true; fP_Main.TextFive();  }
        }
    }
    public void Restartbool()
    {
        Done = false;
        NextPart = false;
        Completed = false;
    }
    IEnumerator Delay()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(3f);
        NextPart = true;
    }
}
