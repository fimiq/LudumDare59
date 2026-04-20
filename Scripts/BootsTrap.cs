using Unity.VisualScripting;
using UnityEngine;

public class BootsTrap : MonoBehaviour
{
    private void Awake()
    {
        G.Register(FindObjectOfType<PlayerController>());
        G.Register(FindObjectOfType<PlayerAction>());
        G.Register(FindObjectOfType<ButtonQueue>());
        G.Register(FindObjectOfType<SequenceController>());
    }
}