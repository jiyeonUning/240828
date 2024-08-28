using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 열거형을 통해 게임준비, 게임실행, 게임중지를 생성한다.
    public enum GameState { Ready, Runnig, GameOver }

    [SerializeField] GameState curState; // 현재 게임의 상태를 저장한다
    [SerializeField] TowerController[] towers; // 타워가 여려개 있을 경우, 해당 타워들을 배열로 저장하여 사용해줄 수 있다
    [SerializeField] PlayerController player;

    // <UI 구현>
    [Header("UI")]
    [SerializeField] GameObject readyText; // 시작 시 출력 텍스트 UI
    [SerializeField] GameObject gameOverText; // 게임 종료 시 출력 텍스트 UI

    private void Start()
    {
        // 게임 시작 시, 게임상태는 준비로 고정한다.
        curState = GameState.Ready;
        // 게임 시작 시, 타워 컨트롤러를 가진 오브젝트들의 모든 컴포넌트를 찾아서 저장한다.
        towers = FindObjectsOfType<TowerController>();
        // 게임 시작 시, 플레이어 오브젝트의 컴포넌트를 찾아 저장한다.
        player = FindObjectOfType<PlayerController>();

        // OnDied 이벤트 함수에 GameOver함수를 추가한다.
        player.OnDied += GameOver;

        // UI 시작텍스트 활성화, 종료 텍스트 비활성화
        readyText.SetActive(true);
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        // 게임이 준비상태일 때, 아무 키나 누르게 되면 게임이 시작된다.
        if (curState == GameState.Ready && Input.anyKeyDown) { GameStart(); }

        // 게임종료 시, R을 누르면 재시작할 수 있는 if문
        else if (curState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        { SceneManager.LoadScene("SampleScene"); }
    }

    // ===================================================================================================================

    // 게임이 시작되었을 때 실행되는 함수 GameStart
    void GameStart()
    {
        // 게임 상태가 준비에서 실행으로 바뀌고,
        curState = GameState.Runnig;
        // 모든 타워들은 StartAttack함수를 실행한다.
        foreach (TowerController tower in towers) { tower.StartAttack(); }

        // UI 텍스트 모두 비활성화
        readyText.SetActive(false);
        gameOverText.SetActive(false);
    }

    // 게임을 종료할 때 실행하는 함수 GameOver
    void GameOver()
    {
        // 게임 상태가 실행에서 중지로 바뀌고,
        curState = GameState.GameOver;
        // 모든 타워들은, 총알 발포를 멈추는 함수 StopAttack을 실핸한다.
        foreach (TowerController tower in towers) { tower.StopAttack(); }

        // UI 시작텍스트 비활성화, 종료 텍스트 활성화
        readyText.SetActive(false);
        gameOverText.SetActive(true);
    }


}
