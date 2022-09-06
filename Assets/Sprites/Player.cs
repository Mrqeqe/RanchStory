using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //存储x方向的值
    private float inputX ;
    //存储y方向的值
    private float inputY ;
    //存储x+y的向量的值
    private Vector2 XAddY_Vector2;
    //存储速度
    public float speed;
    //刚体
    public Rigidbody2D player_rb2D;

    //获取组件
    private void Awake()
    {
        player_rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerInput();
        
    }

    private void FixedUpdate()
    {
        MoveMent();
    } 
    //获取player输入的值
    private void PlayerInput()
    {
        //获取x，与y的值
         inputX = Input.GetAxisRaw("Horizontal");
         inputY = Input.GetAxisRaw("Vertical");
         // Debug.Log("获取了x:"+inputX +"y："+ inputY);
         //对于斜角行动速度平衡
        if(inputX !=0&& inputY != 0)
        {
          inputX = inputX * 0.6f;
          inputY = inputY * 0.6f;
        }
        //计算XAddY_Vector2的值
        XAddY_Vector2 = new Vector2(inputX,inputY);
    }
    
    private void MoveMent()
    {
        //Debug.Log("我开始移动了");
        
        player_rb2D.MovePosition (player_rb2D.position + XAddY_Vector2 * speed * Time.deltaTime); 
    }
}
