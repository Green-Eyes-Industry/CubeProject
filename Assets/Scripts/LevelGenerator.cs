using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] private Text _levelText;
    [SerializeField] private Text _multiplyText;

    private int _currentLevel;

    private void Start()
    {
        _currentLevel = 1;
    }

    public void NextLevel(ref float scoreMod)
    {
        _currentLevel++;
        scoreMod *= 2;

        _levelText.text = "Level " + _currentLevel;
        _multiplyText.text = "x" + scoreMod;
        StartCoroutine(GetComponent<SceneController>().GenerateWorld());
    }
}