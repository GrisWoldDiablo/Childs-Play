using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudComponent : MonoBehaviour
{
    [SerializeField] private Food food;
    [SerializeField] private Image fillerImage;
    [SerializeField] private Text moneyTxt;
    [SerializeField] private Text foodPercentageTxt;
    [SerializeField] private Text warmUpText;
    private float foodRemaining;

    public Text MoneyTxt { get => moneyTxt; set => moneyTxt = value; }
    public Text FoodPercentageTxt { get => foodPercentageTxt; set => foodPercentageTxt = value; }
    public float FoodRemaining { get => foodRemaining; set => foodRemaining = value; }
    public Text WarmUpText { get => warmUpText; set => warmUpText = value; }
    public Image FillerImage { get => fillerImage; set => fillerImage = value; }

    // Start is called before the first frame update
    void Start()
    {
        //foodRemaining = food.GetComponent<Food>().CurrentPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        //if (food != null)
        //{
        //    foodRemaining = food.GetComponent<Food>().CurrentPercentage; 
        //}
        //else
        //{
        //    foodRemaining = 100.0f;
        //}
        //fillerImage.fillAmount = 1.0f - foodRemaining / 100.0f;
    }
}
