using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialouge", menuName = "NPC Dialouge")]

public class NPCDialouge: ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialougeLines;
    public float typingSpeed = 0.05f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}