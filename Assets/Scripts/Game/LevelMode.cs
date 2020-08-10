using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GES
{
    public class LevelMode : MonoBehaviour
    {
        [Header("Connections")]
        [SerializeField] private Text _timerText;
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Transform _spawnPlayerPosition;
        [SerializeField] private Canvas _menuCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Canvas _winLevelCanvas;
        [SerializeField] private Canvas _loseLevelCanvas;

        private int _timerValue;

        [HideInInspector] public bool isTimerStarted;

        #region Methods

        public void StartGame()
        {
            isTimerStarted = true;
            _menuCanvas.enabled = false;
            _loseLevelCanvas.enabled = false;
            _gameCanvas.enabled = true;
            _timerValue = 0;
            MovePlayerToStart();
            StartCoroutine(UpdateTimer());
        }

        public void FinishGame()
        {
            _gameCanvas.enabled = false;
            _winLevelCanvas.enabled = true;
            isTimerStarted = false;
        }

        public void LoseGame()
        {
            _gameCanvas.enabled = false;
            _loseLevelCanvas.enabled = true;
            isTimerStarted = false;
        }

        public void PauseButton()
        {
            isTimerStarted = false;
            StopCoroutine(UpdateTimer());
        }

        public void ResumeGameButton()
        {
            isTimerStarted = true;
            StartCoroutine(UpdateTimer());
        }

        public void NextLevel()
        {
            int nextLevelId = PlayerPrefs.GetInt("SelectedLevel", 1) + 1;
            if (nextLevelId < 9)
            {
                SceneManager.LoadSceneAsync(nextLevelId);
                PlayerPrefs.SetInt("SelectedLevel", nextLevelId);
            }
            else SceneManager.LoadSceneAsync(0);
        }

        #endregion

        #region Private

        private void MovePlayerToStart()
        {
            PlayerMovemant player = FindObjectOfType<PlayerMovemant>();

            if (player != null) player.transform.position = _spawnPlayerPosition.position;
            else Instantiate(_playerObject, _spawnPlayerPosition.position, Quaternion.identity);
        }

        private IEnumerator UpdateTimer()
        {
            while (isTimerStarted)
            {
                _timerValue++;
                _timerText.text = _timerValue.ToString();
                yield return new WaitForSecondsRealtime(1);
            }
        }

        #endregion

    }
}