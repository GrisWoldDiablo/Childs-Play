///
/// Author: Alexandre Lepage
/// Date: October 2018
/// Desc: Project for LaSalle College
///

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableInteraction : MonoBehaviour,IPointerEnterHandler,IDeselectHandler,ISelectHandler {

	private bool selected = false;
	public bool Selected { get { return selected; } }

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<Selectable>().Select();
        SoundManager.GetInstance().PlaySoundOneShot(Sound.onButtonOver,0.5f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        GetComponent<Selectable>().OnPointerExit(null);
        selected = false;
    }

	public void OnSelect(BaseEventData eventData)
	{
		selected = true;
	}
}   
