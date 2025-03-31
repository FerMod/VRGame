using UnityEngine;
using System;

namespace Garitto
{
    public class DialogueManager : MonoBehaviour
    {
        public event Action<Dialogue> OnDialogueStarted;
        public event Action<string> OnShowSentence;
        public event Action<Dialogue> OnDialogueEnded;

        private Dialogue currentDialogue = null;
        private int currentSentenceIndex = 0;

        public void StartDialogue(Dialogue dialogue)
        {
            if (dialogue == null) return;

            currentDialogue = dialogue;
            currentSentenceIndex = 0;

            OnDialogueStarted?.Invoke(dialogue);
            NextSentence();
        }
        public void NextSentence()
        {
            if (currentDialogue == null) return;
            if (currentSentenceIndex >= currentDialogue.sentences.Length)
            {
                EndDialogue();
                return;
            }

            var text = currentDialogue.sentences[currentSentenceIndex];
            OnShowSentence?.Invoke(text);

            currentSentenceIndex++;
        }

        public void EndDialogue()
        {
            if (currentDialogue == null) return;
            OnDialogueEnded.Invoke(currentDialogue);

            currentDialogue = null;
        }
    }

    [Serializable]
    public class Dialogue
    {
        public string[] sentences = new string[] { };
        public DialogueOption[] options;

        public bool HasOptions => options != null && options.Length > 0;
    }

    [Serializable]
    public class DialogueOption
    {
        public DialogueOption(string text, Dialogue nextDialogue)
        {
            this.text = text;
            this.nextDialogue = nextDialogue;
        }

        public string text;
        public Dialogue nextDialogue = null;

        public bool HasNextDialogue => nextDialogue != null;
    }
}
