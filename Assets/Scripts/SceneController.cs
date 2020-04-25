using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    #region PARAMS

    [Header("Links")]
    [SerializeField] public GameObject ruleteObj;

    [SerializeField] private Image _player;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _scoreText;

    [Header("Params")]
    [SerializeField] private float _timeForMatch;
    [SerializeField] private float _ruleteSpeed;
    [SerializeField] private float _scoreModifide;
    [SerializeField] private int _level;

    #endregion

    [HideInInspector] public Color[] holeColors = new Color[]
    {
        Color.white,
        Color.grey,
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.black,
        Color.yellow
    };

    [HideInInspector] public bool[] holeActiveList;
    [HideInInspector] public bool isPlayGame;

    #region PRIVATE

    private int _playerKillCount;
    private float _playerScore;
    private float _tempTimeForMatch;
    private float _timeWinRange;
    private Vector2 _playerStartPosition;

    #endregion

    #region METHODS

    private void Start()
    {
        _playerScore = 0;
        _tempTimeForMatch = _timeForMatch;
        StartCoroutine(GenerateWorld());
        StartCoroutine(StartTimer());
    }

    private void Update()
    {
        if (isPlayGame) ruleteObj.transform.Rotate(new Vector3(0, 0, 1), _ruleteSpeed);
    }

    public IEnumerator GenerateWorld()
    {
        
        ruleteObj.GetComponent<Animation>().Play();
        yield return new WaitForSecondsRealtime(0.15f);
        StopAllCoroutines();
        isPlayGame = true;
        holeActiveList = new bool[holeColors.Length];

        _playerStartPosition = _player.gameObject.transform.position;
        _player.color = holeColors[Random.Range(0, holeColors.Length)];

        _timeForMatch = _tempTimeForMatch;
        _timeWinRange = _timeForMatch;
        _playerKillCount = 1;

        for (int holeId = 0; holeId < ruleteObj.transform.childCount; holeId++)
        {
            holeActiveList[holeId] = true;
            ruleteObj.transform.GetChild(holeId).GetComponent<HoleController>().CreateHole(holeId, holeColors[holeId], this);
        }
        StartCoroutine(StartTimer());
    }

    private void CheckHoleList()
    {
        for (int holeId = 0; holeId < holeActiveList.Length; holeId++) if (holeActiveList[holeId]) return;

        _timeForMatch = 0;
    }

    public void AddScore()
    {
        _playerScore += (5 + (_timeWinRange - _timeForMatch)) * _scoreModifide;
        _timeWinRange = _timeForMatch;
        _playerKillCount += 1;

        _scoreText.text = "Score : " + _playerScore;
        
        if (holeActiveList.Length < _playerKillCount)
        {
            GetComponent<LevelGenerator>().NextLevel(ref _scoreModifide);
            _ruleteSpeed *= 1.1f;
        }
    }

    private void GameEnd()
    {
        isPlayGame = false;
        _scoreText.text = "Score : " + _playerScore;
    }

    private IEnumerator StartTimer()
    {
        while (_timeForMatch > 0)
        {
            _timerText.text = "Timer : " + _timeForMatch;
            yield return new WaitForSecondsRealtime(1f);
            _timeForMatch -= 1;
        }

        _timerText.text = "Timer : " + ((_timeForMatch < 0) ? 0 : _timeForMatch);
        GameEnd();
    }

    public void ReactivatePlayer(int holeId, bool isWin)
    {
        if (isWin) holeActiveList[holeId] = false;
        ReactivatePlayer();
        CheckHoleList();
    }

    public void ReactivatePlayer()
    {
        StartCoroutine(GetNewPlayerColor());
        _player.gameObject.GetComponent<Rigidbody2D>().Sleep();
        _player.gameObject.transform.position = _playerStartPosition;
        _player.gameObject.GetComponent<Rigidbody2D>().WakeUp();
    }

    public IEnumerator GetNewPlayerColor()
    {
        Color newColor = Color.white;
        _player.GetComponent<Animation>().Play();
        yield return new WaitForSecondsRealtime(0.2f);
        for (int colorId = 0; colorId < ruleteObj.transform.childCount; colorId++)
        {
            if (ruleteObj.transform.GetChild(colorId).GetComponent<HoleController>().isActive)
            {
                newColor = holeColors[colorId];
                break;
            }
        }

        _player.color = newColor;
    }

    #endregion
}