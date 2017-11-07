using Lofle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerObject : MonoBehaviour {

    public enum eSprite {
        Idle,
        Run,
        Jump
    }

    [SerializeField]
    private SpriteRenderer _spriteIdle = null;
    [SerializeField]
    private SpriteRenderer _spriteRun = null;
    [SerializeField] 
    private SpriteRenderer _spriteJump = null;

    private StateMachine<playerObject> _stateMachine = null;



    void Start () {
        _stateMachine = new StateMachine<playerObject>(this);
        StartCoroutine(_stateMachine.Coroutine<Idlestate>());
;	}
    //시작하는 상태머신

    private void HideAllsprite() {
        _spriteIdle.enabled = false;
        _spriteRun.enabled = false;
        _spriteJump.enabled = false;
    }

    private void Showsprite(eSprite type) {

        HideAllsprite();

        switch (type) {
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
    // 각각의 enum을 이용하여서 sprite 를 껐다 켰다 하는 부분

    private class Idlestate : State<playerObject>
    {
        protected override void Begin()
        {
            Owner.Showsprite(eSprite.Idle);
        }
        protected override void Update() //상태가 지속될때
        {
        }
        protected override void End()
        {
        }

    }

    private class Runstate : State<playerObject>
    {
        protected override void Begin()
        {
            Owner.Showsprite(eSprite.Run);
        }
        protected override void Update()
        {
        }
        protected override void End()
        {
        }

    }

    private class Jumpstate : State<playerObject>
    {
        protected override void Begin()
        {
            Owner.Showsprite(eSprite.Jump);
        }
        protected override void Update()
        {
        }
        protected override void End()
        {
        }

    }

    // Update is called once per frame

}
