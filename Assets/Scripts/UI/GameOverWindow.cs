using UI.Grid;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private UIItemsGrid _itemsGrid;
        [SerializeField] private UIItemsUsed _itemsUsed;
        [SerializeField] private Button _restartButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        }

        public void Open()
        {
            transform.gameObject.SetActive(true);
        }

        private void OnRestartButtonClick()
        {
            _itemsUsed.Set();
            _itemsGrid.DeleteAllItems();
            _itemsGrid.Set();
            transform.gameObject.SetActive(false);
        }
    }
}