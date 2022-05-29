using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI
{
    public class ButtonEvents : MonoBehaviour, IPointerEnterHandler
    {
        public UnityEvent OnHighLight;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(button.interactable)
                OnHighLight?.Invoke();
        }
    } 
}
