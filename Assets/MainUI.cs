using TMPro;
using UnityEngine;

namespace Gamecore
{
    public class MainUI : MonoBehaviour
    {
        PlayerUnit _player;

        [SerializeField]
        private TMP_Text _healthVaue;

        public void SetPlayerUnit(PlayerUnit player)
        {
            _player = player;
        }
        void Update()
        {
            if (_player != null)
            {
                _healthVaue.text = _player.GetCurrentHealth().ToString();
            }
        }
    }
}
