using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <Transform.Translate()>
// P = Po + vt 
// P는 미래의 위치, Po는 현재의 위치, v는 속도 t는 시간

// <등가속도>
// v = vo + at : 
// v는 속도, vo는 가속도, at는 a(가속도) * t(시간)  

// <가속도>
// F = ma
// F는 힘, m은 질량, a는 가속도 
// 질량이 1이면 F는 곧 가속도인 a가 된다. 누군가 뒤에서 물체를 밀면 힘(F)가 발생 이는 곧 가속도 a가 생김. 
public class csPlayerMove3 : MonoBehaviour
{
    // 플레이어 이동할 속력
    public float speed = 5;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        // transform.Translate(dir * speed * Time.deltaTime);

        //  P = Po + vt 공식으로 변경
        transform.position = transform.position + dir * speed * Time.deltaTime;
    }
}