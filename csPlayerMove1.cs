using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <life Cycle Function>
// Start 1번 태어남 / Upadate 계속 살다가 / OnDestroy 1번 죽다

// <Time.deltaTime>
// deltaTime 시간이 변한 값 즉, 시스템 간의 동기화를 위해 사용. 컴퓨터 사양에 따라 속도차이를 방지.
// 이동, 회전하는 애니메이션 처리, 크기 변환하는 애니메이션 처리에 사용.

public class csPlayerMove1 : MonoBehaviour
{
    // 플레이어 이동할 속력
    public float speed = 5;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        print(h);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
