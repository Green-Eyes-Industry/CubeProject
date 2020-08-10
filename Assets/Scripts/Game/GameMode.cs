using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GES
{
    public class GameMode : MonoBehaviour
    {
        [Header("Connections")]
        [SerializeField] private Transform _parentButtons;

        [Header("Parameters")]
        [SerializeField] private Color _closeColor;

        private enum PrefsNames { LevelOpen }
        private int _selectedLevel;

        #region Methods

        public void SelectLevel(int level)
        {
            if (PlayerPrefs.GetInt(PrefsNames.LevelOpen.ToString() + level, 0) == 1)
            {
                _selectedLevel = level;
                LoadSelectedLevel();
            }
        }

        public void LoadSelectedLevel()
        {
            PlayerPrefs.SetInt("SelectedLevel", _selectedLevel);
            SceneManager.LoadSceneAsync(_selectedLevel);
        }

        #endregion

        #region Private

        private void Start()
        {
            PlayerPrefs.SetInt(PrefsNames.LevelOpen.ToString() + 1, 1);
            _selectedLevel = FindLastPlayedLevel();
        }

        private int FindLastPlayedLevel()
        {
            int lastOpened = 1;
            for (int levelId = _parentButtons.transform.childCount; levelId > 0; levelId--)
            {
                if (PlayerPrefs.GetInt(PrefsNames.LevelOpen.ToString() + levelId, 0) == 1) lastOpened = levelId;
            }
            return lastOpened;
        }

        #endregion
    }
}