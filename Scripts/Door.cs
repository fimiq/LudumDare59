using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerAction.Instance.OnAction += Act;
    }

    private void OnDisable()
    {
        if (PlayerAction.Instance != null)
            PlayerAction.Instance.OnAction -= Act;
    }

    void Act()
    {
        transform.Rotate(0, 90, 0);

        PlayerAction.Instance.Disable();

        G.Get<SequenceController>().Continue();
    }
}