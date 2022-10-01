using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PlayerState     // 玩家动作状态
{
    Idle    = 0,
    Move    = 1,
};

public enum PlayerToward    // 玩家朝向
{
    L = 0,
    R = 1,
};

public class MainCharacterControl : MonoBehaviour
{
    public float speed = 3f;

    public delegate void OnTowardChangehandler(PlayerToward toward);
    public OnTowardChangehandler onTowardChange;

    private PlayerToward _toward;
    public PlayerToward toward
    {
        get { return _toward; }
        set {
            bool isTowardChanged = (_toward != value);
            _toward = value;
            spriteRenderer.flipX = (toward == PlayerToward.R);
            if (isTowardChanged)
                onTowardChange?.Invoke(_toward);
        }
    }

    public PlayerState m_state;

    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Collider2D playerCollider;

    Vector3 tmpVector3 = new Vector3();
    [HideInInspector] public Vector3        currLocalPosition = new Vector3();
    [HideInInspector] public Rigidbody2D    rBody2d;

    private bool isMoveing = false;

    const float SPEED_MUL = 100f;

    void FloorVector3(ref Vector3 vector3)
    {
        vector3.x = Mathf.Floor(vector3.x);
        vector3.y = Mathf.Floor(vector3.y);
        vector3.z = Mathf.Floor(vector3.z);
    }

    void Awake()
    {
        m_state = PlayerState.Idle;
        currLocalPosition = new Vector3(0f, 0f, 0f);
        transform.localPosition = currLocalPosition;
        toward = PlayerToward.L;
        rBody2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    void Update()
    {
    }

    void FixedUpdate()
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

    void ProcessState()
    {
        if (isMoveing) {
            ChangeState(PlayerState.Move);
        } else {
            ChangeState(PlayerState.Idle);
        }
    }

    void ProcessToward()
    {
        bool keyL = Input.GetKey(KeyCode.A);
        bool keyR = Input.GetKey(KeyCode.D);
        if (keyL && !keyR && toward == PlayerToward.R) {
            toward = PlayerToward.L;
        } else if (!keyL && keyR && toward == PlayerToward.L) {
            toward = PlayerToward.R;
        } else {}
    }

    void ProcessMove()
    {
        Vector2 m = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMoveing = (m != Vector2.zero);
        rBody2d.MovePosition(rBody2d.position + m.normalized * Time.deltaTime * speed * SPEED_MUL);
    }

}
