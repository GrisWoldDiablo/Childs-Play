using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Exit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool  onPanel;
    public bool OnPanel { get => onPanel; set => onPanel = value; }

    //[SerializeField] private GameObject thisPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Shop.GetInstance().OnButton)
        {
            GameManager.GetInstance().DeselectTile();
            //Debug.Log("Shop will be deselcted now");
        }
    }
}
