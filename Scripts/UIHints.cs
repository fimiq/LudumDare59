using UnityEngine;

public class UIHints : MonoBehaviour
{
    public GameObject hint;

    private void Start()
    {
        hint.SetActive(false);

        PlayerAction.Instance.OnAction += Hide;
    }

    public void Show()
    {
        hint.SetActive(true);
        PlayerAction.Instance.Enable();
    }

    void Hide()
    {
        hint.SetActive(false);
    }
}