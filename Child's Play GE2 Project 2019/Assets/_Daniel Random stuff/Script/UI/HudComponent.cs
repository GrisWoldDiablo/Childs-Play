using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudComponent : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private Text _foodTxt;
    [SerializeField] private Image fillerImage;
    private float _foodRemaining;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
          _foodRemaining = (float)food.GetComponent<Food>().HitPoints * 0.01f;
          fillerImage.fillAmount = (float)_foodRemaining;    
          Debug.Log("Current food percentage: " + _foodRemaining);
    }
}
