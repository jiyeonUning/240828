using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; // 물리처리를 위한 참조 (발포 기능 및 플레이어와 부딫힘 여부를 판단하기 위함)
    [SerializeField] float speed; // 총알의 이동속도
    [SerializeField] Transform target; // 플레이어의 위치값 참조

    private void Start()
    {
        transform.LookAt(target.position); // 총알이 타겟(플레이어)를 바라보게 하고,
        rigid.velocity = transform.forward * speed; // 해당 방향으로 발포되게 한다.
    }

    // 위 참조를 기반으로, 타겟의 위치를 가져오는 함수 SetTarget
    public void SetTarget(Transform target) {  this.target = target;  }

    // =============================================================
    //                  총알의 부딫힘 여부 확인


    // 물리적 충돌을 확인할 수 있는 함수 OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        // Bullet의 오브젝트가 Player태그를 가진 오브젝트와 충돌하였을 때,
        if (collision.gameObject.tag == "Player")
        {
            // 플레이어 컨트롤러의 TakeHit 컴포넌트를 가져와 실행한다.
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }

        // 위 if문 실행 여부와 관계없이, 총알이 오브젝트와 충돌하면 해당 총알이 삭제되도록 Destroy함수를 사용한다.
        Destroy(gameObject);
    }
}
