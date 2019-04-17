using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//[System.Serializable]
//public class MyEvent : UnityEvent { }

public class OnButtonEvents : MonoBehaviour
{
    [Header("Set the name of you button.")]
    [SerializeField] private string buttonName;
    [Header("Populate the Events")]
    [SerializeField] private UnityEvent onButtonCancel;
    [Header("With button sound?")]
    [SerializeField] private bool playSound = true;

    private void Update()
    {
        if (Input.GetButton(buttonName))
        {
            onButtonCancel.Invoke();
            if (playSound)
            {
                SoundManager.GetInstance().PlaySoundButton();
            }
        }
    }
}
