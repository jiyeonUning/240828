using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; // ������ �� ���� �΋H���ٸ� �� �̻� ���ư��� ���ϰ� �ϰų�, �Ѿ˰��� �浹�� Ȯ���� �� �ִ�
    [SerializeField] float moveSpeed; // �÷��̾��� �̵��ӵ�
    public event Action OnDied; // �÷��̾ �׾��� ��(�Ѿ˰� �浹���� ��) ���Ǵ� �뵵�� �̺�Ʈ

    private void Update()
    {
        // �Է°��� �޴� �Լ� GetAxis�� ����, �Է¸Ŵ��� Horizontal�� Vertical�� ����� Ű�� ������ �� �࿡ �����Ѵ�.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ������ ������ Ű���� �� ������ �࿡ �־��ְ�,
        Vector3 moveDir = new Vector3(x, 0, z);
        // ������ ũ�Ⱑ 1���� Ŭ ��, ������ �ӵ��� �ش� ������ ���� ȸ���� �� �ִ�.
        if (moveDir.magnitude > 1) { moveDir.Normalize(); }
        // ���������� ����� �����ϴ� ������ٵ� �����, �ش� �������� �̵��ӵ���ŭ �̵��� �� �ִ�.
        rigid.velocity = moveDir * moveSpeed;
    }

    // =================================================================================================
    //                                 �÷��̾ �Ѿ˰� �浹�Ͽ��� ���

    public void TakeHit()
    {
        OnDied?.Invoke();  // �̺�Ʈ �Լ� OnDied�� �κ�ũ�Լ��� ���� ȣ���ϰ�,
        Destroy(gameObject); // �÷��̾��� ������Ʈ�� �����Ѵ�.
    }
}
