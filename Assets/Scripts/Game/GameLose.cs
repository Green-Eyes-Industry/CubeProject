using UnityEngine;

namespace GES
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameLose : MonoBehaviour
    {
        private LevelMode _levelMode;

        #region Private

        private void Start()
        {
            _levelMode = FindObjectOfType<LevelMode>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player") _levelMode.LoseGame();
        }

        #endregion
    }
}