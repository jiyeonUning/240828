using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigid; // ����ó���� ���� ���� (���� ��� �� �÷��̾�� �΋H�� ���θ� �Ǵ��ϱ� ����)
    [SerializeField] float speed; // �Ѿ��� �̵��ӵ�
    [SerializeField] Transform target; // �÷��̾��� ��ġ�� ����

    private void Start()
    {
        transform.LookAt(target.position); // �Ѿ��� Ÿ��(�÷��̾�)�� �ٶ󺸰� �ϰ�,
        rigid.velocity = transform.forward * speed; // �ش� �������� �����ǰ� �Ѵ�.
    }

    // �� ������ �������, Ÿ���� ��ġ�� �������� �Լ� SetTarget
    public void SetTarget(Transform target) {  this.target = target;  }

    // =============================================================
    //                  �Ѿ��� �΋H�� ���� Ȯ��


    // ������ �浹�� Ȯ���� �� �ִ� �Լ� OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        // Bullet�� ������Ʈ�� Player�±׸� ���� ������Ʈ�� �浹�Ͽ��� ��,
        if (collision.gameObject.tag == "Player")
        {
            // �÷��̾� ��Ʈ�ѷ��� TakeHit ������Ʈ�� ������ �����Ѵ�.
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.TakeHit();
        }

        // �� if�� ���� ���ο� �������, �Ѿ��� ������Ʈ�� �浹�ϸ� �ش� �Ѿ��� �����ǵ��� Destroy�Լ��� ����Ѵ�.
        Destroy(gameObject);
    }
}
