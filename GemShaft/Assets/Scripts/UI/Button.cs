using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GemShaft.UI
{
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        private RectTransform rectTransform;

        private readonly Subject<Unit> clicked = new();
        private readonly Subject<Unit> pressed = new();
        private readonly Subject<Unit> released = new();
        private readonly Subject<Unit> entered = new();
        private readonly Subject<Unit> exited = new();

        public IObservable<Unit> Clicked => clicked;
        public IObservable<Unit> Pressed => pressed;
        public IObservable<Unit> Released => released;
        public IObservable<Unit> Entered => entered;
        public IObservable<Unit> Exited => exited;

        public RectTransform RectTransform => rectTransform ??= GetComponent<RectTransform>();
        public bool Interactable { get; set; }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            pressed.OnNext(default);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            released.OnNext(default);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            clicked.OnNext(default);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            entered.OnNext(default);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            exited.OnNext(default);
        }
    }
}