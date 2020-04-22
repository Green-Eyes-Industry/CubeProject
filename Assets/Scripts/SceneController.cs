using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    #region PARAMS

    [Header("Links")]
    [SerializeField] public GameObject ruleteObj;

    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    [Header("Params")]
    [SerializeField] private float _timeForMatch;
    [SerializeField] private float _ruleteSpeed;
    [SerializeField] private float _scoreModifide;

    #endregion

    #region PRIVATE

    private bool _isPlayGame;
    private float _playerScore;

    #endregion

    #region METHODS

    private void Start()
    {
        _isPlayGame = true;
        _playerScore = 0;
        GenerateWorld();
        StartCoroutine(UpdateTimeUI());
    }

    private void Update()
    {
        if (_isPlayGame)
        {
            ruleteObj.transform.Rotate(new Vector3(0, 0, 1), _ruleteSpeed);
        }
    }

    private void GenerateWorld()
    {
        for (int holeId = 0; holeId < ruleteObj.transform.childCount; holeId++) ruleteObj.transform.GetChild(holeId).GetComponent<HoleController>().CreateHole();
    }

    private void GameEnd()
    {
        Debug.LogError("Game End");

        _isPlayGame = false;

        for (int holeId = 0; holeId < ruleteObj.transform.childCount; holeId++)
        {
            if (!ruleteObj.transform.GetChild(holeId).GetComponent<HoleController>().isActive) _playerScore += 1 * _scoreModifide;
        }

        _scoreText.text = "Score : " + _playerScore;
    }

    private IEnumerator UpdateTimeUI()
    {
        while (_timeForMatch > 0)
        {
            _timerText.text = "Timer : " + _timeForMatch;
            yield return new WaitForSecondsRealtime(1f);
            _timeForMatch -= 1;
        }

        GameEnd();
    }

    #endregion
}