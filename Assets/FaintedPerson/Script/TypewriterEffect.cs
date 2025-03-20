using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speechText; // The TextMeshPro component for the speech bubble
    [SerializeField] private float typingSpeed = 0.05f; // Speed at which each character appears

    private Coroutine typingCoroutine;
    private Queue<string> messageQueue = new Queue<string>();
    public bool isTyping = false; // Tracks whether the typewriter effect is currently typing


    [Header("References")]
    [Tooltip("The TextMeshProUGUI or Text component containing the text.")]
    public GameObject SpeechBubble;

    [Header("Settings")]
    [Tooltip("Base height of the RectTransform.")]
    public float baseHeight = 50f;

    [Tooltip("Additional height per line of text.")]
    public float heightPerLine = 20f;

    [Tooltip("Minimum height of the RectTransform.")]
    public float minHeight = 50f;

    [Tooltip("Maximum height of the RectTransform (0 for no limit).")]
    public float maxHeight = 0f;

    private RectTransform rectTransform;


    private void Update()
    {
        rectTransform = SpeechBubble.GetComponent<RectTransform>();
        AdjustHeight();
    }

    private void AdjustHeight()
    {
        if (speechText == null) return;

        // Calculate the number of lines in the text
        int lineCount = speechText.textInfo.lineCount;

        // Calculate the new height
        float newHeight = baseHeight + (lineCount * heightPerLine);

        // Clamp the height between minHeight and maxHeight (if maxHeight > 0)
        newHeight = Mathf.Max(newHeight, minHeight);
        if (maxHeight > 0)
        {
            newHeight = Mathf.Min(newHeight, maxHeight);
        }

        // Apply the new height to the RectTransform
        Vector2 sizeDelta = rectTransform.sizeDelta;
        sizeDelta.y = newHeight;
        rectTransform.sizeDelta = sizeDelta;
    }

/// <summary>
/// Adds a message to the queue and starts the typewriter effect if not already typing.
/// </summary>
public void ShowText(string text)
    {
        messageQueue.Enqueue(text);

        if (!isTyping) // Only start typing if no other text is currently being typed
        {
            StartNextMessage();
        }
    }

    /// <summary>
    /// Starts typing the next message in the queue.
    /// </summary>
    private void StartNextMessage()
    {
        if (messageQueue.Count > 0) // Ensure there are messages in the queue
        {
            string nextMessage = messageQueue.Dequeue();
            typingCoroutine = StartCoroutine(TypeText(nextMessage));
        }
    }

    /// <summary>
    /// Coroutine to display the text one character at a time.
    /// </summary>
    private IEnumerator TypeText(string text)
    {
        isTyping = true; // Mark as typing
        speechText.text = ""; // Clear the text field

        foreach (char letter in text.ToCharArray())
        {
            speechText.text += letter; // Add one character at a time
            yield return new WaitForSeconds(typingSpeed); // Wait for the typing speed duration
        }

        isTyping = false; // Typing finished
        typingCoroutine = null;

        // Check if there are more messages in the queue and start the next one
        StartNextMessage();
    }

    /// <summary>
    /// Clears the message queue and stops any current typing.
    /// </summary>
    public void ClearMessages()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        messageQueue.Clear();
        speechText.text = "";
        isTyping = false;
    }
}

