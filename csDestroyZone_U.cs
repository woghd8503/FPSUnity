using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csDestroyZone_U : MonoBehaviour
{
    // 영역 안에 다른 물체가 감지될 경우
    private void OnTriggerEnter(Collider other)
    {
        // 그 물체를 없애고 싶다.
        Destroy(other.gameObject);
    }
}
