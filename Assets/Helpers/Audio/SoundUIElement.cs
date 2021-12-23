using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Helpers
{



    public class SoundUIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] AudioClip onClick, onEnter, onExit;
        [SerializeField] float volume = 0.3f;


        public void OnPointerClick(PointerEventData eventData)
        {
          if(onClick != null)  SoundCreator.creator.CreateSound(onClick, Sound.SoundType.ui,  volume: volume);
         
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null) SoundCreator.creator.CreateSound(onEnter, Sound.SoundType.ui,  volume: volume);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null) SoundCreator.creator.CreateSound(onExit, Sound.SoundType.ui, volume: volume);
        }

       
    }
}