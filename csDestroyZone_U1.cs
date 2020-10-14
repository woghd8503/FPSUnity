using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 물체를 그리기 위한 기본 조건으로 Mesh데이터와 머터리얼 정보가 필요합니다.
// 머터리얼에는 다시 이미지 정보, 색상 등의 정보와 물체를 어떤 style로 그릴지 정의해
// 놓은 Shader 정보가 필요합니다. 따라서 material을 바꾸면 외관을 이루고 있는 material 형태
// 및 어떤 스타일로 그릴지를 정의할 수 있습니다.
public class csDestroyZone_U1 : MonoBehaviour
{
    // 영역 안에 다른 물체가 감지될 경우
    private void OnTriggerEnter(Collider other)
    {
        // 1. 만약 부딪힌 물체가 Bullet이거나 Enemy이라면
        if (other.gameObject.name.Contains("Bullet") ||
            other.gameObject.name.Contains("Enemy"))
        {
            // 2. 부딪힌 물체를 비활성화
            other.gameObject.SetActive(false);
            // 3. 부딪힌 물체가 총알일 경우 총알 리스트에 삽입
            if (other.gameObject.name.Contains("Bullet"))
            {
                //PlayerFire 클래스 얻어오기
                csPlayerFire player = GameObject.Find("Player").GetComponent<csPlayerFire>();
                //리스트에 총알 삽입
                player.bulletObjectPool.Add(other.gameObject);
            }
            else if (other.gameObject.name.Contains("Enemy"))
            {
                // EnemyManager 클래스 얻어오기
                GameObject emObject = GameObject.Find("EnemyManager");
                csEnemyManager manager = emObject.GetComponent<csEnemyManager>();
                // 리스트에 총알 삽입
                manager.enemyObjectPool.Add(other.gameObject);
            }
            // 그 물체를 없애고 싶다.
            /* Destroy(other.gameObject);*/
        }
    }
}
