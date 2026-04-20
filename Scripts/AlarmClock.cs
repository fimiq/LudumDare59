using UnityEngine;

public class AlarmClock : MonoBehaviour
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
        Debug.Log("Alarm off");

        GetComponent<AudioSource>().Stop();

        PlayerAction.Instance.Disable();

        G.Get<SequenceController>().Continue();
    }
}