using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace GemShaft.UI
{
    public class LoaderScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);

            var sequence = DOTween.Sequence();
            sequence
                .Append(canvasGroup.DOFade(0f, 0.2f))
                .AppendCallback(() => Destroy(gameObject));
        }
    }
}