using UnityEngine;
using System;
using TMPro;
using UnityEditor;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text dialogueBox;

    public UnityEvent<Dialogue> OnDialogueStarted;
    public UnityEvent<Dialogue> OnDialogueEnded;

    private Dialogue currentDialogue;
    private int currentSentenceIndex;


    void Start()
    {
        OnDialogueStarted.AddListener(DebugStartDialogue);
        OnDialogueEnded.AddListener(DebugEndDialogue);
    }

    private void DebugEndDialogue(Dialogue dialogue)
    {
        Debug.Log($"START DIALOGUE: {dialogue}");
    }

    private void DebugStartDialogue(Dialogue dialogue)
    {
        Debug.Log($"END DIALOGUE: {dialogue}");
    }

    public void StartDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
        currentSentenceIndex = 0;

        OnDialogueStarted.Invoke(dialogue);
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (currentDialogue == null) return;
        if (currentSentenceIndex >= currentDialogue.sentences.Length)
        {
            EndDialogue();
            return;
        }

        dialogueBox.text = currentDialogue.sentences[currentSentenceIndex];
        currentDialogue.OnShowSentence?.Invoke();

        currentSentenceIndex++;

    }
    private void EndDialogue()
    {
        OnDialogueEnded.Invoke(currentDialogue);

        currentDialogue = null;
        dialogueBox.text = "";
    }

    public void DebugStartDialogue()
    {
        var dialogue = new Dialogue
        {
            sentences = new[]
            {
                "Hi",
                "Text example",
                "Bla Bla Bla Bla Bla",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla commodo lacus elit, vel eleifend quam laoreet non. Praesent imperdiet diam quis eleifend egestas. Quisque eu arcu odio. Nulla pellentesque, mi vulputate tincidunt interdum, eros eros elementum augue, eget bibendum tellus augue sed neque. Ut sollicitudin enim massa, sit amet vulputate est pulvinar in. Pellentesque porttitor at risus et fringilla. Suspendisse consectetur eu lorem nec pharetra. Pellentesque eget velit nec lorem sodales tempor non id mauris. Phasellus consequat pellentesque ipsum, laoreet scelerisque nunc suscipit non.",
                "Mauris et lacus iaculis, dignissim nibh nec, consectetur velit. In sit amet quam eu orci porta malesuada. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam ac ultricies ipsum. Sed elementum sem ac nisi porttitor, a vulputate nunc tempus. Integer ut vestibulum velit. Vivamus eget posuere sapien, quis consequat dui. Ut blandit, odio et malesuada aliquam, lectus mauris consectetur dui, eu lobortis massa nulla eu lectus. Nulla dignissim scelerisque cursus.",
                "Donec tempus, nibh non commodo varius, elit mi placerat est, nec suscipit augue enim sed ligula. Donec placerat accumsan posuere. Nam pellentesque consectetur odio vitae bibendum. Vivamus a scelerisque nisl. Curabitur nunc lacus, faucibus at finibus dapibus, pharetra vel dolor. Sed hendrerit augue quis sagittis mattis. Integer tristique, orci fermentum cursus fermentum, neque ex cursus lorem, ac congue tortor nulla eget nisi. Mauris scelerisque neque sit amet imperdiet volutpat. Duis justo augue, interdum ac neque ut, eleifend porta mi. Quisque arcu dui, dapibus in ultricies vitae, porttitor euismod nibh. Suspendisse iaculis metus at urna sodales pharetra. Duis ac vehicula nisl. ",
            }
        };

        StartDialogue(dialogue);
    }
}

[Serializable]
public class Dialogue
{
    public string[] sentences;

    public UnityEvent OnShowSentence;
}
