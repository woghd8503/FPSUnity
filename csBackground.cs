using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // 배경 스크롤은 한 장을 회전시키려고 합니다 이때 이를 가능하게 해주는 것이 material offset 속성.
public class csBackground : MonoBehaviour
{
    // Background Material
    public Material bgMaterial;
    // 스크롤 속도
    public float scrollSpeed = 0.2f;

    //1.살아 있는 동안 계속 하고 싶다.
    void Update()
    {
        //2.방향이 필요하다.
        Vector2 direction = Vector2.up;

        //3.스크롤하고 싶다. P = PO + vt
        bgMaterial.mainTextureOffset += direction * scrollSpeed * Time.deltaTime;
    }
}
