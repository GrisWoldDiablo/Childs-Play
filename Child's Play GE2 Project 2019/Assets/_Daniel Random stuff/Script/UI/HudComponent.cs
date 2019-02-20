using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudComponent : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private Text _foodTxt;
    private int _foodRemaining;

    // Start is called before the first frame update
    void Start()
    {
        //_foodRemaining = 100 - food.GetComponent<Food>().CurrentPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        _foodRemaining = 100 - food.GetComponent<Food>().CurrentPercentage;
        _foodTxt.text = "Food remainig: " + _foodRemaining + " %";
        Debug.Log(food.GetComponent<Food>().CurrentPercentage);
    }
}
