using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; // 움직일 때 벽에 부딫혔다면 더 이상 나아가지 못하게 하거나, 총알과의 충돌을 확인할 수 있다
    [SerializeField] float moveSpeed; // 플레이어의 이동속도
    public event Action OnDied; // 플레이어가 죽었을 때(총알과 충돌했을 때) 사용되는 용도의 이벤트

    private void Update()
    {
        // 입력값을 받는 함수 GetAxis를 통해, 입력매니저 Horizontal과 Vertical에 저장된 키를 가져와 각 축에 저장한다.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 위에서 가져온 키값을 각 방향의 축에 넣어주고,
        Vector3 moveDir = new Vector3(x, 0, z);
        // 백터의 크기가 1보다 클 때, 일정한 속도로 해당 방향을 향해 회전할 수 있다.
        if (moveDir.magnitude > 1) { moveDir.Normalize(); }
        // 물리엔진의 기능을 수행하는 리지드바디를 사용해, 해당 방향으로 이동속도만큼 이동할 수 있다.
        rigid.velocity = moveDir * moveSpeed;
    }

    // =================================================================================================
    //                                 플레이어가 총알과 충돌하였을 경우

    public void TakeHit()
    {
        OnDied?.Invoke();  // 이벤트 함수 OnDied를 인보크함수를 통해 호출하고,
        Destroy(gameObject); // 플레이어의 오브젝트를 삭제한다.
    }
}
