using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


namespace Helpers
{


    public class ButtonUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public UnityEvent OnEnter;
        public UnityEvent OnExit;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (OnEnter != null) OnEnter.Invoke();
        }

      

     
        public void OnPointerUp(PointerEventData eventData)
        {
            if (OnExit != null) OnExit.Invoke();
        }

    }
}