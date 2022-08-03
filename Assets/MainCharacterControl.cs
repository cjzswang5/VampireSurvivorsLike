using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState     // 玩家动作状态
{
    Idle = 0,
    Move = 1,
};

public enum PlayerToward    // 玩家朝向
{
    Left = 0,
    Right = 1,
};

public class MainCharacterControl : MonoBehaviour
{
    public float speed = 3f;

    public delegate void OnTowardChangehandler(PlayerToward m_toward);
    public OnTowardChangehandler onTowardChange;

    public PlayerState    m_state;

    private PlayerToward  toward;
    public PlayerToward   m_toward
    {
        get { return toward; }
        set {
            toward = value;
            onTowardChange?.Invoke(toward);
        }
    }

    Vector3 tmpVector3 = new Vector3();
    Vector3 currLocalPosition = new Vector3();

    Animator animator;
    SpriteRenderer spriteRenderer;

    const float SPEED_MUL = 100f;

    void FloorVector3(ref Vector3 vector3)
    {
        vector3.x = Mathf.Floor(vector3.x);
        vector3.y = Mathf.Floor(vector3.y);
        vector3.z = Mathf.Floor(vector3.z);
    }

    void OnTowardChange(PlayerToward playerToward)
    {
        spriteRenderer.flipX = (m_toward == PlayerToward.Right);
    }

    void Awake()
    {
        m_state = PlayerState.Idle;
        currLocalPosition = new Vector3(0f, 0f, 0f);
        transform.localPosition = currLocalPosition;
        animator = gameObject.GetComponent<Animator>();
        m_toward = PlayerToward.Left;
        onTowardChange += OnTowardChange;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
    }

    void Update()
    {
        ProcessState();
        ProcessMove();
        ProcessToward();
    }

    void ChangeState(PlayerState newState)
    {
        if (newState != m_state)
        {
            m_state = newState;
            switch (m_state) {
                case PlayerState.Idle:
                    animator.Play("idle");
                    break;
                case PlayerState.Move:
                    animator.Play("move");
                    break;
                default:
                    break;
            }
        }
    }

    bool isMoveing = false;
    bool IsMoving()
    {
        isMoveing = Input.GetKey(KeyCode.W);
        isMoveing = isMoveing || Input.GetKey(KeyCode.S);
        isMoveing = isMoveing || Input.GetKey(KeyCode.A);
        isMoveing = isMoveing || Input.GetKey(KeyCode.D);
        return isMoveing;
    }

    void ProcessState()
    {
        if (IsMoving()) {
            ChangeState(PlayerState.Move);
        } else {
            ChangeState(PlayerState.Idle);
        }
    }

    void ProcessToward()
    {
        bool keyL = Input.GetKey(KeyCode.A);
        bool keyR = Input.GetKey(KeyCode.D);
        if (keyL && !keyR && m_toward == PlayerToward.Right) {
            m_toward = PlayerToward.Left;
        } else if (!keyL && keyR && m_toward == PlayerToward.Left) {
            m_toward = PlayerToward.Right;
        } else {}
    }

    void ProcessMove()
    {
        if (Input.GetKey(KeyCode.W)) {
            ProcessMoveKey(KeyCode.W);
        }
        if (Input.GetKey(KeyCode.S)) {
            ProcessMoveKey(KeyCode.S);
        }
        if (Input.GetKey(KeyCode.A)) {
            ProcessMoveKey(KeyCode.A);
        }
        if (Input.GetKey(KeyCode.D)) {
            ProcessMoveKey(KeyCode.D);
        }
    }

    void ProcessMoveKey(KeyCode keyCode)
    {
        tmpVector3.Set(0f, 0f, 0f);
        switch (keyCode) {
            case KeyCode.W:
                tmpVector3.y =  1f;
                break;
            case KeyCode.S:
                tmpVector3.y = -1f;
                break;
            case KeyCode.A:
                tmpVector3.x = -1f;
                break;
            case KeyCode.D:
                tmpVector3.x =  1f;
                break;
            default:
                break;
        }
        tmpVector3 = tmpVector3.normalized * speed * Time.deltaTime * SPEED_MUL;
        currLocalPosition += tmpVector3;
        tmpVector3 = currLocalPosition;
        FloorVector3(ref tmpVector3);
        transform.localPosition = tmpVector3;
    }
}
