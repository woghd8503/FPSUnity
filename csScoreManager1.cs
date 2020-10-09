using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 소프트웨어 개발 방법에서 사용되는 디자인 패턴은 프로그램 개발에서 자주 나타나는 과제를 해결하기 위한 방법 중 하나로,
// 과거 소프트웨어 개발 과정에서 발견된 설계의 노하우를 축적하고 이름을 붙여 이후에 재이용하기 좋은 형태로 특정의 규약을
// 묶어 정리한 것입니다. 알고리즘고 같이 프로그램 코드로 바로 변환될 수 있는 형태는 아니지만, 특정한 상황에서 구조적인
// 문제를 해결하는 방식을 설명해줍니다.


// 1. 스코어를 화면에 표시하고 싶다.
// 2. 스코어가 증가됐으니까.
// 3. 에너미를 잡았으니까. 
public class csScoreManager1 : MonoBehaviour
{
    //필요 속성: 점수 UI, 현재 점수, 최고 점수
    //현재 점수 UI
    public Text currentScoreUI;
    //현재 점수
    public int currentScore;
    //최고 점수 UI
    public Text bestScoreUI;
    //최고 점수
    public int bestScore;

    // 싱글턴 객체
    public static csScoreManager Instance = null;

    public int Score
    {
        get
        {
            return currentScore;
        }
        set
        {
            // to do
            // 3. ScoreManager 클래스의 속성에 값을 할당한다.
            currentScore = value;
            // 4. 화면에 현재 점수 표시하기
            currentScoreUI.text = "현재 점수 : " + currentScore;

            // 목표: 최고 점수를 표시하고 싶다.
            // 1. 현재 점수가 최고 점수보다 크니까
            // -> 만약 현재 점수가 최고 점수를 초과했다면"
            if (currentScore > bestScore)
            {
                // 2. 최고 점수를 갱신시킨다.
                bestScore = currentScore;
                // 3. 최고 점수 UI에 표시
                bestScoreUI.text = "최고 점수 : " + bestScore;
                // 목표: 최고 점수를 저장하고 싶다.
                PlayerPrefs.SetInt("Best Score", bestScore);
            }
        }
    }

    // 싱글턴 객체에 값이 없으면 생성된 자기 자신을 할당
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // 목표: 최고 점수를 불러와 bestScore 변수에 할당하고 화면에 표시한다.
        // 순서: 1. 최고 점수를 불러와 bestScore에 넣어주기
        bestScore = PlayerPrefs.GetInt("Best Score", 0);
        // 2.최고 점수를 화면에 표시하기
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }

}
