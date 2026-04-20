using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonQueue : MonoBehaviour
{
    [SerializeField] private Image _leftButton;
    [SerializeField] private Image _rightButton;
    [SerializeField] private Image _jumpButton;

    private Queue<string> _buttons = new Queue<string>();

    private void Awake()
    {
        G.Register(this);
        UpdateVisuals();
    }

    public void AddButton(string name)
    {
        _buttons.Enqueue(name);
        UpdateVisuals();
    }

    public void RemoveButton()
    {
        if (_buttons.Count > 0)
        {
            _buttons.Dequeue();
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        bool hasLeft = false;
        bool hasRight = false;
        bool hasJump = false;

        foreach (var btn in _buttons)
        {
            if (btn == "left") hasLeft = true;
            if (btn == "right") hasRight = true;
            if (btn == "jump") hasJump = true;
        }

        _leftButton.enabled = hasLeft;
        _rightButton.enabled = hasRight;
        _jumpButton.enabled = hasJump;
    }

    public void ClearQueue()
    {
        _buttons.Clear();
        UpdateVisuals();
    }

    public int GetQueueCount()
    {
        return _buttons.Count;
    }
}