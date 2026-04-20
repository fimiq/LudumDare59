
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("Timing")]
    [SerializeField] private float _delay = 0.8f;

    [Header("Movement")]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpForce = 5f;



    private Rigidbody _rb;

    private Queue<InputCommand> _queue = new Queue<InputCommand>();
    private bool _isProcessing;

    private float _holdRight;
    private float _holdLeft;
    private float _holdJump;

    private bool _holdingRight;
    private bool _holdingLeft;
    private bool _holdingJump;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        G.Register(this);
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        HandleHold(KeyCode.D, ref _holdingRight, ref _holdRight, Vector3.right, "right");
        HandleHold(KeyCode.A, ref _holdingLeft, ref _holdLeft, Vector3.left, "left");
        HandleJump();
    }

    private void HandleHold(KeyCode key, ref bool holding, ref float holdTime, Vector3 dir, string name)
    {
        if (Input.GetKey(key))
        {
            if (!holding)
            {
                holding = true;
                holdTime = 0f;
                G.Get<ButtonQueue>().AddButton(name);
            }

            holdTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(key) && holding)
        {           
            holding = false;

            EnqueueCommand(new InputCommand
            {
                type = CommandType.Move,
                direction = dir,
                duration = holdTime
            });
        }
    }

 

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!_holdingJump)
            {
                _holdingJump = true;
                _holdJump = 0f;
                G.Get<ButtonQueue>().AddButton("jump");
            }

            _holdJump += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && _holdingJump)
        {           
            _holdingJump = false;

            EnqueueCommand(new InputCommand
            {
                type = CommandType.Jump,
                duration = _holdJump
            });         
        }
    }

    private void EnqueueCommand(InputCommand cmd)
    {
        if (_queue.Count >= 4) return;

        _queue.Enqueue(cmd);

        if (!_isProcessing)
            StartCoroutine(ProcessQueue());
    }

    private IEnumerator ProcessQueue()
    {
        _isProcessing = true;

        while (_queue.Count > 0)
        {
            InputCommand cmd = _queue.Dequeue();

            yield return new WaitForSeconds(_delay);

            if (cmd.type == CommandType.Move)
                yield return Move(cmd);

            if (cmd.type == CommandType.Jump)
                ExecuteJump();

            G.Get<ButtonQueue>().RemoveButton();
        }

        _isProcessing = false;
    }

    private IEnumerator Move(InputCommand cmd)
    {
        float elapsed = 0f;

        while (elapsed < cmd.duration)
        {
            _rb.linearVelocity = new Vector3(cmd.direction.x * _speed, _rb.linearVelocity.y, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
    }

    private void ExecuteJump()
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpForce, _rb.linearVelocity.z);
    }
}

public enum CommandType
{
    Move,
    Jump,
    Action
}

public struct InputCommand
{
    public CommandType type;
    public Vector3 direction;
    public float duration;
}