using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    public Transform m_transform;
    //角色控制器组件
    CharacterController m_ch;
    //角色移动速度
    float m_movSpeed=3.0f;
    //重力
    float m_gravity=9.8f;
    void Start(){
        m_transform=this.transform;
        //获取角色控制器组件
        m_ch=this.GetComponent<CharacterController>();
    }
    void Update(){
        Control();
    }
    void Control(){
        //定义3个值控制移动
        float xm=0, ym=0, zm=0;
        //重力运动
        ym-=m_gravity*Time.deltaTime;
        //前后左右移动

        if(Input.GetKey(KeyCode.A)) {
            xm-=m_movSpeed*Time.deltaTime;
        } else if(Input.GetKey(KeyCode.D)) {
            xm+=m_movSpeed*Time.deltaTime;
        } else if (Input.GetKey(KeyCode.Space)) {
            ym += 0.3f;
        }
        //使用角色控制器提供的Move函数进行移动
        m_ch.Move(m_transform.TransformDirection(new Vector3(xm, ym, zm)));
    }
}
