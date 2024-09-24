using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColobScript : MonoBehaviour
{
    public Object dialogue;
    public GameObject dialogueSystem;

    public void interact()
    {
        dialogueSystem.GetComponent<ColobDialogueSys>().loadDialogue(dialogue);
    }
}
