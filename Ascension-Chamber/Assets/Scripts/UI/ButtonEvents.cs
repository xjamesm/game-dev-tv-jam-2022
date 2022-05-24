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

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHighLight?.Invoke();
        }
    } 
}
