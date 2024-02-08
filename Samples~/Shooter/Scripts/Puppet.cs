using System.Collections;
using TMPro;
using UnityEngine;

namespace BaseTool.Samples.Shooter
{
    public class Puppet : MonoBehaviour, IDamageable
    {
        [SerializeField]
        protected TMP_Text _textPrefab;

        [SerializeField]
        protected float _duration = 1;

        [SerializeField, Range(0, 1)]
        protected float _randomize = 1;

        [SerializeField]
        protected Vector3 _offset;

        [SerializeField]
        protected AnimationCurve _ease = AnimationCurve.Linear(0, 0, 1, 1);

        public void TakeDamages(double damages)
        {
            StartCoroutine(DisplayDamages(damages));
        }

        protected IEnumerator DisplayDamages(double damages)
        {
            var text = Instantiate(_textPrefab);
            text.text = $"{damages}";
            Destroy(text.gameObject, 2);

            var position = transform.position + _offset;
            var endPosition = position + 1f * Vector3.up;
            text.transform.position = position;

            Cooldown cd = 1;
            cd.Reset();
            while (!cd.IsReady)
            {
                text.transform.position = Vector3.Lerp(position, endPosition, 1 - cd.TimeLeftPercent);
                text.transform.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.zero, _ease.Evaluate(1 - cd.TimeLeftPercent));
                cd.Update();
                yield return null;
            }
        }
    }
}
