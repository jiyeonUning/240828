using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �������� ���� �����غ�, ���ӽ���, ���������� �����Ѵ�.
    public enum GameState { Ready, Runnig, GameOver }

    [SerializeField] GameState curState; // ���� ������ ���¸� �����Ѵ�
    [SerializeField] TowerController[] towers; // Ÿ���� ������ ���� ���, �ش� Ÿ������ �迭�� �����Ͽ� ������� �� �ִ�
    [SerializeField] PlayerController player;

    // <UI ����>
    [Header("UI")]
    [SerializeField] GameObject readyText; // ���� �� ��� �ؽ�Ʈ UI
    [SerializeField] GameObject gameOverText; // ���� ���� �� ��� �ؽ�Ʈ UI

    private void Start()
    {
        // ���� ���� ��, ���ӻ��´� �غ�� �����Ѵ�.
        curState = GameState.Ready;
        // ���� ���� ��, Ÿ�� ��Ʈ�ѷ��� ���� ������Ʈ���� ��� ������Ʈ�� ã�Ƽ� �����Ѵ�.
        towers = FindObjectsOfType<TowerController>();
        // ���� ���� ��, �÷��̾� ������Ʈ�� ������Ʈ�� ã�� �����Ѵ�.
        player = FindObjectOfType<PlayerController>();

        // OnDied �̺�Ʈ �Լ��� GameOver�Լ��� �߰��Ѵ�.
        player.OnDied += GameOver;

        // UI �����ؽ�Ʈ Ȱ��ȭ, ���� �ؽ�Ʈ ��Ȱ��ȭ
        readyText.SetActive(true);
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        // ������ �غ������ ��, �ƹ� Ű�� ������ �Ǹ� ������ ���۵ȴ�.
        if (curState == GameState.Ready && Input.anyKeyDown) { GameStart(); }

        // �������� ��, R�� ������ ������� �� �ִ� if��
        else if (curState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        { SceneManager.LoadScene("SampleScene"); }
    }

    // ===================================================================================================================

    // ������ ���۵Ǿ��� �� ����Ǵ� �Լ� GameStart
    void GameStart()
    {
        // ���� ���°� �غ񿡼� �������� �ٲ��,
        curState = GameState.Runnig;
        // ��� Ÿ������ StartAttack�Լ��� �����Ѵ�.
        foreach (TowerController tower in towers) { tower.StartAttack(); }

        // UI �ؽ�Ʈ ��� ��Ȱ��ȭ
        readyText.SetActive(false);
        gameOverText.SetActive(false);
    }

    // ������ ������ �� �����ϴ� �Լ� GameOver
    void GameOver()
    {
        // ���� ���°� ���࿡�� ������ �ٲ��,
        curState = GameState.GameOver;
        // ��� Ÿ������, �Ѿ� ������ ���ߴ� �Լ� StopAttack�� �����Ѵ�.
        foreach (TowerController tower in towers) { tower.StopAttack(); }

        // UI �����ؽ�Ʈ ��Ȱ��ȭ, ���� �ؽ�Ʈ Ȱ��ȭ
        readyText.SetActive(false);
        gameOverText.SetActive(true);
    }


}
