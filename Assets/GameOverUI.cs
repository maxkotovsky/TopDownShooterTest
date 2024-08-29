using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Gamecore
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private Button _restart;

        private CanvasGroup _comonCanvasGroup;
        private RectTransform _commonRectTransform;

        private Sequence _animateScreenIn;
        private Sequence _animateScreenOut;

        private void Initialize()
        {
            _comonCanvasGroup = GetComponent<CanvasGroup>();
            _commonRectTransform = GetComponent<RectTransform>();
            _restart.onClick.AddListener(OnButtonClicked);
        }
        void Start()
        {
            Initialize();
            AnimateScreenIn();
        }

        public void OnButtonClicked()
        {
            AnimateScreenOut();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void TransformsOverride()
        {
            _commonRectTransform.localScale = new Vector3(1.1f, 1.1f, 1f);
            _comonCanvasGroup.alpha = 0f;
        }

        private void AnimateScreenIn()
        {
            TransformsOverride();
            _animateScreenIn = DOTween.Sequence();
            _animateScreenIn.Append(_commonRectTransform.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack))
                            .Append(_comonCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutBack));
        }

        private void AnimateScreenOut()
        {
            _animateScreenOut = DOTween.Sequence();
            _animateScreenOut.Append(_commonRectTransform.transform.DOScale(1.1f, 0.5f).SetEase(Ease.InBack))
                             .Append(_comonCanvasGroup.DOFade(0, 0.5f).SetEase(Ease.OutBack));
        }
    }
}
