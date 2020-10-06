using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input class : 사용자의 입력에 따른 방향 설정 등..
// 3D에서는 물체를 배치하기 위한 좌표계가 그 축에 따라 왼속 좌표계와 오른손 좌표계로 나뉜다. XYZ
public class csPlayerMove2 : MonoBehaviour
{
    // 플레이어 이동할 속력
    public float speed = 5;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.right * h + Vector3.up * v;
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
