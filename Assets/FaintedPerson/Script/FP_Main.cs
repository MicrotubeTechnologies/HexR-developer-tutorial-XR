using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FP_Main : MonoBehaviour
{
    public TypewriterEffect typewriterEffect;
    public BreatingTrigger breatingTrigger;
    public HeartRateCollider heartRateCollider;
    public PlanksCheck planksCheck;
    private AudioSource audioSource;
    public AudioSource BackGroundMusic;
    public AudioClip Clip1, Clip2, Clip3, Clip4, Clip5, Clip6, MachineSound;
    public GameObject HeartRateParticle;
    public Animator anim;
    public TextMeshProUGUI Robotext;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            StartCoroutine(StartDialogue());
        }
        if (Input.GetKey(KeyCode.W))
        {
            TextThree();
        }
        if (Input.GetKey(KeyCode.E))
        {
            TextFour();
        }
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        BackGroundMusic.Stop();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        anim.SetBool("Open_Anim", true);
        ReadjustPositionRotation();
        StartCoroutine(StartDialogue());
    }
    private void OnDisable()
    {
        Robotext.text = "";
        BackGroundMusic.Play();
        anim.SetBool("Open_Anim", false);
        audioSource.Stop();
        audioSource.clip = MachineSound;

    }
    private void ReadjustPositionRotation()
    {
        // Reset the local position and rotation
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private IEnumerator StartDialogue()
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(3f);
        // Queue the dialogue lines
        typewriterEffect.ShowText("Hey there, can you help me? My partner was hit by falling planks and is now unconscious. Can you help move the planks from him?");
        audioSource.clip = Clip1;
        audioSource.Play();
        planksCheck.Done = false;

    }
    private IEnumerator Text2()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }
        // Wait for the specified delay time
        yield return new WaitForSeconds(1f);
        // Add a callback after the first text is displayed
        typewriterEffect.ShowText("Great! Now, let's check if he's breathing and has a pulse. I'll guide you through it step by step.");
        audioSource.clip = Clip2;
        audioSource.Play();
        StartCoroutine(Text3());
    }
    private IEnumerator Text3()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }
        // Wait for the specified delay time
        yield return new WaitForSeconds(1f);
        // Add a callback after the first text is displayed
        typewriterEffect.ShowText("First, place your index and middle finger near his nose and mouth to check if he’s breathing. Hold it steady for a few seconds.");
        audioSource.clip = Clip3;
        audioSource.Play();
        while (audioSource.isPlaying) // Wait until the audio stops
        {
            yield return null;
        }
        breatingTrigger.Restartbool();
    }
    private IEnumerator Text4()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }
        // Wait for the specified delay time
        yield return new WaitForSeconds(1f);
        // Add a callback after the first text is displayed
        typewriterEffect.ShowText("Alright keep it steady, I am beginning to feel some light breathing.");
        audioSource.clip = Clip4;
        audioSource.Play();
    }
    private IEnumerator Text5()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }

        heartRateCollider.Restartbool();
        string TheText = "Okay, great! He’s breathing, so we’ll focus on his pulse next. Now, gently place two fingers on his wrist to feel for a pulse. I’ll analyze it for you.";
        typewriterEffect.ShowText(TheText);
        audioSource.clip = Clip5;
        audioSource.Play();
        while (audioSource.isPlaying) // Wait until the audio stops
        {
            yield return null;
        }
        heartRateCollider.Restartbool();
    }
    private IEnumerator Text6()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }

        string TheText = "Analyzing now... Processing vital signs... and 85 BPM—that’s within a normal range. It seems like his heart is stable for now. I’ve already called for an ambulance. Thank you for your assistance.";
        typewriterEffect.ShowText(TheText);
        HeartRateParticle.layer = 0;
        audioSource.clip = Clip6;
        audioSource.Play();
        StartCoroutine(Endanimation());
    }
    public void TextFive()
    {
        StartCoroutine(Text5());
    }
    public void TextTwo()
    {
        StartCoroutine(Text2());
    }
    public void TextThree()
    {
        StartCoroutine(Text3());
    }
    public void TextFour()
    {
        StartCoroutine(Text4());
    }
    public void TextSix()
    {
        StartCoroutine(Text6());
    }
    private IEnumerator Endanimation()
    {
        // Wait until the current dialogue finishes typing
        while (typewriterEffect.isTyping)
        {
            yield return null; // Wait for the next frame without starting another coroutine
        }
        HeartRateParticle.layer = 9;

    }
}
