using GameCore;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements.Joystick
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        [SerializeField] private float handleRange = 1;
        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform handle = null;

        private Canvas canvas;
        private Camera cam;
        private Vector2 input = Vector2.zero;

        protected virtual void Start()
        {
            HandleRange = handleRange;
            canvas = GetComponentInParent<Canvas>();

            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        }

        private void OnEnable()
        {
            SetToStartPosition();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            input = (eventData.position - position) / (radius * canvas.scaleFactor);

            if (input.magnitude > 1)
            {
                input = input.normalized;
            }

            handle.anchoredPosition = input * radius * handleRange;

            Core.InputController.DeviceInput(input.x, input.y);
        }
        
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            SetToStartPosition();
        }

        private void SetToStartPosition()
        {
            input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;

            Core.InputController.DeviceInput(input.x, input.y);
        }
    }
}