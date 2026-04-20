using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAction : MonoBehaviour
{
    public static PlayerAction Instance;

    public event Action OnAction;

    private Queue<int> _queue = new();
    private bool _processing;

    private bool _enabled = false;
    private float _delay = 0.6f;

    private void Awake()
    {
        Instance = this;
        G.Register(this);
    }

    private void Update()
    {
        if (!_enabled) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _queue.Enqueue(1);
            G.Get<ButtonQueue>()?.AddButton("action");

            if (!_processing)
                StartCoroutine(Process());
        }
    }

    IEnumerator Process()
    {
        _processing = true;

        while (_queue.Count > 0)
        {
            _queue.Dequeue();

            yield return new WaitForSeconds(_delay);

            OnAction?.Invoke();

            G.Get<ButtonQueue>()?.RemoveButton();
        }

        _processing = false;
    }

    public void Enable() => _enabled = true;
    public void Disable() => _enabled = false;

    public void Lock() => _enabled = false;
    public void Unlock() => _enabled = true;

    public void Clear()
    {
        _queue.Clear();
        G.Get<ButtonQueue>()?.ClearQueue();
    }
}