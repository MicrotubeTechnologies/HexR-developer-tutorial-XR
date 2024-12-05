using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBreak : MonoBehaviour
{
    public GameObject BrokenCup;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        // Get the MeshRenderer component on the GameObject
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BreakCup()
    {
        GameObject NewBrokenCup = Instantiate(BrokenCup,gameObject.transform);
        meshRenderer.enabled = false;
        StartCoroutine(Restore(NewBrokenCup));
    }
    IEnumerator Restore(GameObject newbrokencup)
    {
        yield return new WaitForSeconds(2f);
        Destroy(newbrokencup);
        meshRenderer.enabled = true;
    }
}
