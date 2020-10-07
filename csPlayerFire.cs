using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GetButtonDown 버튼을 누를 때 / GetButtonUp 뗄 때 / GetButton 게속 누르고 있을 때
public class csPlayerFire : MonoBehaviour
{
    // 총알을 생산할 공장
    public GameObject bulletFactory;
    // 총구
    public GameObject firePosition;

    private void Update()
    {
        // 목표: 사용자가 발사 버튼을 누르면 총알을 발사하고 싶다.
        // 순서: 1. 사용자가 발사 버튼을 누르면
        // 만약 사용자가 발사 버튼을 누르면
        if(Input.GetButtonDown("Fire1"))
        {
            // 2. 총알 공장에서 총알을 만든다.
            GameObject bullet = Instantiate(bulletFactory);

            // 3. 총알을 발사한다. bullet아, 너의 속성 또는 기능 중에 transform 좀 줘봐. Transform아, 너의 속성 또는 기능 중에 position 좀 줘봐.
            bullet.transform.position = firePosition.transform.position;
        }
    }
}
