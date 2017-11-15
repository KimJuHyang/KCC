using Lofle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    public enum eSprite
    {
        Idle,
        Run,
        Jump
    }

    [SerializeField]
    private Rigidbody2D _rigidbody = null;

    [SerializeField]
    private SpriteRenderer _spriteIdle = null;
    [SerializeField]
    private SpriteRenderer _spriteRun = null;
    [SerializeField]
    private SpriteRenderer _spriteJump = null;

    private StateMachine<PlayerObject> _stateMachine = null;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _jump = 10.0f;

    private bool isJump
    {
        get
        {
            return 0 != _rigidbody.velocity.y;
        }
    }

    void Start()
    {   

        _stateMachine = new StateMachine<PlayerObject>(this);
        StartCoroutine(_stateMachine.Coroutine<IdleState>());
    }


    private void Jump()
    {
        if (!isJump)
        {
            _rigidbody.AddForce(new Vector2(0, _jump));
        }
    }


    private void Move(bool bLeft)
    {
        _rigidbody.AddForce(new Vector2((bLeft ? -_speed : _speed), 0));
    }

    private void LookAt(bool bLeft)
    {
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, bLeft ? 180 : 0, transform.rotation.z));
    }

    private void HideAllSprite()
    {
        _spriteIdle.enabled = false;
        _spriteRun.enabled = false;
        _spriteJump.enabled = false;
    }


    private void ShowSprite(eSprite type)
    {
        HideAllSprite();

        switch (type)
        {
            case eSprite.Idle:
                _spriteIdle.enabled = true;
                break;

            case eSprite.Run:
                _spriteRun.enabled = true;
                break;

            case eSprite.Jump:
                _spriteJump.enabled = true;
                break;
        }
    }

    private class IdleState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Idle);
        }

        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Invoke<RunState>();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Invoke<JumpState>();
            }
        }

        protected override void End()
        {
        }
    }

    private class RunState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.ShowSprite(eSprite.Run);
        }

        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Owner.Move(true);
                Owner.LookAt(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Owner.Move(false);
                Owner.LookAt(false);
            }
            else
            {
                Invoke<IdleState>();
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Invoke<JumpState>();
            }
        }

        protected override void End()
        {
        }
    }

    private class JumpState : State<PlayerObject>
    {
        protected override void Begin()
        {
            Owner.Jump();
            Owner.ShowSprite(eSprite.Jump);
        }

        protected override void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Owner.Move(true);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                Owner.Move(false);
            }

            if (!Owner.isJump)
            {
                Invoke<IdleState>();
            }
        }

        protected override void End()
        {
        }
    }
}