using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudComponent : MonoBehaviour
{
    [SerializeField] private GameObject food;
    [SerializeField] private Text _foodTxt;
    [SerializeField] private Image fillerImage;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text foodPercentageTxt;
    private float foodRemaining;

    public Text MoneyTxt { get => moneyTxt; set => moneyTxt = value; }
    public Text FoodPercentageTxt { get => foodPercentageTxt; set => foodPercentageTxt = value; }
    public float FoodRemaining { get => foodRemaining; set => foodRemaining = value; }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
          _foodRemaining = (float)food.GetComponent<Food>().HitPoints * 0.01f;
          fillerImage.fillAmount = (float)_foodRemaining;    
          //Debug.Log("Current food percentage: " + _foodRemaining);
    }
}
