using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// transform아(.) 네가 갖고 있는 속성(내부 변수) 또는 기능(함수) 중에서 position 좀 줘봐. 
// 거기에 dir 곱하기 speed하고 Time아(.) 네가 갖고 있는 속성 또는 기능 중에서 deltaTime 좀 줘봐.
// 변수 transform 뒤에 붚는 점(.)의 의미는 해당 변수(객체)가 같고 있는 속성 또는 기능을 사용하겠다는 것
public class csPlayerMove4 : MonoBehaviour
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
        //  transform.position = transform.position + dir * speed * Time.deltaTime;
        transform.position += dir * speed * Time.deltaTime;
    }
}
