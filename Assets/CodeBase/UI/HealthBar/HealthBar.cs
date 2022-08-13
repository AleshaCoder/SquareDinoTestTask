using Logic.Enemy;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private MonoBehaviour _IHealth;

        private IHealth Health => (IHealth)_IHealth;

        public void Hide() => gameObject.SetActive(false);

        public void Show() => gameObject.SetActive(true);

        private void Start()
        {
            _slider.maxValue = Health.MaxHP;
            _slider.value = Health.CurrentHP;
        }

        private void OnEnable() => Health.OnHealthChanged += _slider.SetValueWithoutNotify;

        private void OnDisable() => Health.OnHealthChanged -= _slider.SetValueWithoutNotify;
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_IHealth == null)
                return;
            if (_IHealth is IHealth)
                return;

            Debug.LogError(_IHealth.name + " needs to implement " + nameof(IHealth));
            _IHealth = null;
        }
#endif
    }
}
