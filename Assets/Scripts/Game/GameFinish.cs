using UnityEngine;

namespace GES
{
    [RequireComponent(typeof(BoxCollider))]
    public class GameFinish : MonoBehaviour
    {
        private LevelMode _levelMode;

        #region Private

        private void Start()
        {
            _levelMode = FindObjectOfType<LevelMode>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                _levelMode.FinishGame();
                Destroy(gameObject);
            }
        }

        #endregion
    }
}