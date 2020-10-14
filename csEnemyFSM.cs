using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//                  <Coroutine>
// 코루틴은 routine의 복수로서 상호 협력 루틴 (co-coroutine) 
// 일정 시간 동안 내가 잠들어 있을 테니 sleep 그 시간이 끝나면 나를 깨워줘
// 그러면 남은 처리 과정을 이어서 하도록 할게.
// 이때 양보 반환 yield return이라는 키워드를 사용해 시스템에게 깨워줄 시간을 
// 넘겨줌.

// <리턴 데이터>                           /       <대기 시간>
// yeild return null                        다음 프레임까지 대기한다.
// yeild return new WaitSecond(float)       지정된 시간(초)동안 대기한다.
// yeild return new WaitForFixedUpdate()    다음 고정(물리) 프레임까지 대기한다.
// yeild return new WaitForEndOfFrame()     모든 렌더링이 끝날 때까지 대기한다.
// yeild return StartCoroutine(string)      특정 코루틴 함수가 끝날 때까지 대기한다.
// yeild return new WWW(string)             웹 통신 작업이 끝날 때까지 대기한다.
// yeild return new AsyncOperation          비동기 씬 로드가 끝날 때까지 대기한다.
public class csEnemyFSM : MonoBehaviour
{
    // 에너미 상태 상수
        enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    // 에너미 상태 변수
    EnemyState m_State;

    // 플레이어 발견 범위
    public float findDistance = 8f;

    // 플레이어 트랜스폼
    Transform player;

    // 공격 가능 범위
    public float attackDistance = 2f;

    // 캐릭터 콘트롤러 컴포넌트
    CharacterController cc;

    // 이동 속도
    public float moveSpeed = 5f;

    // 누적 시간
    float currentTime = 0;

    // 공격 딜레이 시간
    float attackDelay = 2f;

    // 에너미 공격력
    public int attackPower = 3;

    // 초기 위치 저장용 변수
    Vector3 originPos;

    // 이동 가능 범위
    public float moveDistance = 20f;

    // 에너미의 체력
    public int hp = 15;

    // 에너미의 최대 체력
    int maxHp = 15;

    // 에너미 hp Slider 변수
    public Slider hpSlider;

    private void Start()
    {
        // 최초의 에너미 상태는 대기(idle)로 한다.
        m_State = EnemyState.Idle;

        // 플레이어의 트랜스폼 컴포넌트 받아오기
        player = GameObject.Find("Player").transform;

        cc = GetComponent<CharacterController>();

        // 자신의 초기 위치 저장하기
        originPos = transform.position;
    }
    void Update()
    {
        // 현재 상태를 체크해 해당 상태별로 정해진 기능을 수행하게 하고 싶다.
        switch(m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
           case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }
        // 현재 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        // 만일, 플레이어와의 거리가 액션 시작 범위 이내라면 Move 상태로 전환한다.
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");
        }
    }

    void Move()
    {
        // 만일 현재 위치가 초기 위치에서 이동 가능 범위를 넘어간다면...
        if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // 현재 상태를 복귀(Return)로 전환한다.
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        // 만일 플레이어와의 거리가 공격 범위 밖이라면 플레이어를 향해 이동한다.
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // 이동 방향 설정
            Vector3 dir = (player.position - transform.position).normalized;

            // 캐릭터 콘트롤러를 이용하여 이동하기
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        else
        {
            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");

            // 누적 시간을 공격 딜레이 시간만큼 미리 진행시켜 놓는다.
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        // 만일 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정한 시간마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                player.GetComponent<csPlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;
            }
        }
        // 그렇지 않다면, 현재 상태를 이동(Move)으로 전환한다(재추격 실시).
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;
        }
    }

    void Return()
    {
        // 만일 초기 위치에서의 거리가 0.1f 이상이라면 초기 위치 쪽으로 이동한다.
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);
        }
        // 그렇지 않다면, 자신의 위치를 초기 위치로 조정하고 현재 상태를 대기로 전환한다.
        else
        {
            transform.position = originPos;

            // hp를 다시 회복한다.
         
            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");
        }
    }



    // 데미지 처리용 코루틴 함수
    public void HitEnemy(int hitPower)
    {
        // 만일, 이미 피격 상태이거나 사망 상태 또는 복귀 상태라면 아무런 처리도 하지 않고 함수를 종료
        if(m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        // 플레이어의 공격력만큼 에너미의 체력을 감소시킨다.
        hp -= hitPower;
        // 에너미의 체력이 0보다 크면 피격 상태로 전환한다.
        if(hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any state -> Damaged");
            Damaged();
        }
        // 그렇지 않다면 죽음 상태로 전환한다.
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");
            Die();
        }
    }

    void Damaged()
    {
        // 피격 상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DamageProcess());
    }
    // 데미지 실행 함수
    IEnumerator DamageProcess()
    {
        // 피격 모션 시간만큼 기다린다.
        yield return new WaitForSeconds(0.5f);

        // 현재 상태를 이동 상태로 전환한다.
        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }

    void Die()
    {
        // 진행 중인 피격 코루틴을 중지한다.
        StopAllCoroutines();

        // 죽은 상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // 캐릭터 콘트롤러 컴포넌트를 비활성화시킨다.
        cc.enabled = false;

        // 2초 동안 기다린 후에 자기 자신을 제거한다.
        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }
}
