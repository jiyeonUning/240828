using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float bulletTime; // 총알을 생성하는 주기 시간값
    [SerializeField] float remainTime; // 다음 총알을 생성할 때 까지 걸리는 시간값
    [SerializeField] GameObject bulletPrefab; // 생성할 총알의 프리팹을 가져오는 용도

    private void Start()
    {
        // Player 태그를 가지고 있는 오브젝트를 찾아서
        GameObject playerOBJ = GameObject.FindGameObjectWithTag("Player");
        
        // 찾아낸 오브젝트의 트렌스폼 값을 target에 저장함으로써, Player 태그를 가진 오브젝트의 위치, 회전, 크기 값을 저장한다.
        target = playerOBJ.GetComponent<Transform>();
        target = playerOBJ.transform;
    }

    // =========================================================================================================================

    private void Update()
    {
        // 공격중이 아닐 경우, 총알생성을 하지 않는다.
        if ( isAttacking == false ) return;

        remainTime -= Time.deltaTime; // 다음 총알의 생성까지 걸리는 시간을 실시간으로 차감해준다

        if (remainTime <= 0) // 전부 차감될 경우,
        {
            // bulletPrefab에 저장한 프리팹값을, 지정된 위치에 오브젝트로 생성하고, 그 값을 bulletGameOBJ에 저장한다.
            GameObject bulletGameOBJ = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // bulletGameOBJ에 저장한 오브젝트를 Bullet클래스에 넣어주고,
            Bullet bullet = bulletGameOBJ.GetComponent<Bullet>();

            // 저장한 오브젝트는 위에서 저장해둔 타겟을 목표로 하게 한다.
            bullet.SetTarget(target);

            // 위 과정이 끝난 후, bulletTime의 값을 -> 값이 모두 차감된 remainTime 값에 넣어줌으로써,
            // 해당 과정을 반복한다.
            remainTime = bulletTime;
        }
    }

    // 발포기능의 동작 여부
    [SerializeField] bool isAttacking;
    public void StartAttack() { isAttacking = true; } // 공격 개시
    public void StopAttack() { isAttacking = false; } // 공격 중지
}
