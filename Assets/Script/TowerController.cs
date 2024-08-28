using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform target;

    [SerializeField] float bulletTime; // �Ѿ��� �����ϴ� �ֱ� �ð���
    [SerializeField] float remainTime; // ���� �Ѿ��� ������ �� ���� �ɸ��� �ð���
    [SerializeField] GameObject bulletPrefab; // ������ �Ѿ��� �������� �������� �뵵

    private void Start()
    {
        // Player �±׸� ������ �ִ� ������Ʈ�� ã�Ƽ�
        GameObject playerOBJ = GameObject.FindGameObjectWithTag("Player");
        
        // ã�Ƴ� ������Ʈ�� Ʈ������ ���� target�� ���������ν�, Player �±׸� ���� ������Ʈ�� ��ġ, ȸ��, ũ�� ���� �����Ѵ�.
        target = playerOBJ.GetComponent<Transform>();
        target = playerOBJ.transform;
    }

    // =========================================================================================================================

    private void Update()
    {
        // �������� �ƴ� ���, �Ѿ˻����� ���� �ʴ´�.
        if ( isAttacking == false ) return;

        remainTime -= Time.deltaTime; // ���� �Ѿ��� �������� �ɸ��� �ð��� �ǽð����� �������ش�

        if (remainTime <= 0) // ���� ������ ���,
        {
            // bulletPrefab�� ������ �����հ���, ������ ��ġ�� ������Ʈ�� �����ϰ�, �� ���� bulletGameOBJ�� �����Ѵ�.
            GameObject bulletGameOBJ = Instantiate(bulletPrefab, transform.position, transform.rotation);

            // bulletGameOBJ�� ������ ������Ʈ�� BulletŬ������ �־��ְ�,
            Bullet bullet = bulletGameOBJ.GetComponent<Bullet>();

            // ������ ������Ʈ�� ������ �����ص� Ÿ���� ��ǥ�� �ϰ� �Ѵ�.
            bullet.SetTarget(target);

            // �� ������ ���� ��, bulletTime�� ���� -> ���� ��� ������ remainTime ���� �־������ν�,
            // �ش� ������ �ݺ��Ѵ�.
            remainTime = bulletTime;
        }
    }

    // ��������� ���� ����
    [SerializeField] bool isAttacking;
    public void StartAttack() { isAttacking = true; } // ���� ����
    public void StopAttack() { isAttacking = false; } // ���� ����
}
