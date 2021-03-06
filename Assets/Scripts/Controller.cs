using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private const float DEFAULT_SPEED = 3f;
    private const float ACCELERATE_SPEED = 0.6f;
    private const float MAX_SPEED = 7f;
    private const float DEFAULT_JUMP_SPEED = 12f;

    private Transform camera_transform;

    private Transform m_transform;
    private CharacterController m_controller;

    private Vector3 offset;

    // 角色移动速度
    public float xSpeed = 0;
    private float ySpeed = 0;
    public bool isLeftHold = false;
    public bool isRightHold = false;

    // 重力
    private float gravityDec = 0.3f;
    // 摩擦力
    private float frictionDec = 0.1f;

    // 角色状态
    private bool isHanging = false;

    void Start()
    {
        camera_transform = Camera.main.transform;
        m_transform = this.transform;

        offset = camera_transform.position - m_transform.position;

        m_controller = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 v = MoveCalculate();
        CollisionFlags flags = m_controller.Move(m_transform.TransformDirection(v));

        StateControl(flags);

        camera_transform.position = m_transform.position + offset;
    }

    private void StateControl(CollisionFlags flags)
    {
        if (flags.HasFlag(CollisionFlags.Below))
        {
            isHanging = false;
            ySpeed = 0;
        } else
        {
            isHanging = true;
        }

        if (flags.HasFlag(CollisionFlags.Above))
        {
            if (ySpeed > 0) ySpeed = 0;
        }
    }

    private Vector3 MoveCalculate()
    {
        float x = 0, y = 0, z = 0;

        x += HorizontalMove();

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isHanging) ySpeed = DEFAULT_JUMP_SPEED;
        }

        y += VerticalMove();

        return new Vector3(x, y, z);
    }

    private float VerticalMove()
    {
        if (!isHanging) return 0;

        ySpeed -= gravityDec;
        return ySpeed * Time.deltaTime;
    }

    private float HorizontalMove()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isLeftHold = true;
            xSpeed -= ACCELERATE_SPEED;
            if (xSpeed < -MAX_SPEED) xSpeed = -MAX_SPEED;
        } else
        {
            isLeftHold = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            isRightHold = true;
            xSpeed += ACCELERATE_SPEED;
            if (xSpeed > MAX_SPEED) xSpeed = MAX_SPEED;
        } else
        {
            isRightHold = false;
        }
        
        // 摩擦力计算
        if (xSpeed > 0)
        {
            xSpeed -= frictionDec;
            if (xSpeed < 0) xSpeed = 0;
        } else if (xSpeed < 0)
        {
            xSpeed += frictionDec;
            if (xSpeed > 0) xSpeed = 0;
        }

        return xSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject o = other.gameObject;
        if (o.tag == "Enemy_Head")
        {
            Vector3 size = gameObject.GetComponent<Renderer>().bounds.size;
            Vector3 position = transform.position;
            Vector3 closestPoint = other.ClosestPoint(position);
            if (closestPoint.y * 3 <= position.y * 3 - size.y)
            {
                Destroy(o);
            }
            else
            {
                Time.timeScale = 0;
            }
        }

        //if (collision.gameObject.name != "mario") return;

        //if (collision.impulse.x != 0)
        //{
        //    Time.timeScale = 0;
        //    // 游戏结束
        //}

        //if (collision.impulse.y < 0)
        //{
        //    // 怪物死亡
        //    Destroy(gameObject);
        //}
    }
}
