﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <충돌>
// 두개의 object가 collider가 있어야되고, 하는는 꼭 rigidbody가 있어야한다.
// Collider 충돌체

// private void OnCollisionEnter(Collision other)
//{
//  //충돌시작
//}

// private void OnCollisionStay(Collision other)
//{
//  //충돌 중
//}

// private void OnCollisionExit(Collision other)
//{
//  //충돌 끝
//}
public class csEnemy : MonoBehaviour
{
    // 필요 속성: 이동 속도
    public float speed = 5;

    // 방향을 전역 변수로 만들어 Start와 Update에서 사용
    Vector3 dir;

    private void Start()
    {
        // Vector3 dir;
        // 0부터 9가지 10개의 값 중에 하나를 랜덤으로 가져온다.
        int randValue = UnityEngine.Random.Range(0, 10);
        // 만약 3보다 작으면 플레이어 방향
        if(randValue < 3)
        {
            //플레이어를 찾아 target으로 하고 싶다.
            GameObject target = GameObject.Find("Player");
            //방향을 구하고 싶다.target-me
            dir = target.transform.position - transform.position;
            //방향의크기를 1로 하고 싶다.
            dir.Normalize();
        }
        // 그렇지 않으면 아래 방향으로 정하고 싶다.
        else
        {
            dir = Vector3.down;
        }
    }
    private void Update()
    {
        // 1. 방향을 구한다.
        /// Vector3 dir = Vector3.down;

        // 2. 이동하고 싶다. 공식 P = PO + vt
        transform.position += dir * speed * Time.deltaTime;
    }

        //충돌시작
    private void OnCollisionEnter(Collision other)
    {
        // 너 죽고
        Destroy(other.gameObject);
        // 나 죽자.
        Destroy(gameObject);
    }
}
