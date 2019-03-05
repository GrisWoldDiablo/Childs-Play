using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Food : Player
{
    [SerializeField] private GameObject[] pieces;

    private int initialHP;
    private int currentPercentage = 100;

    public int CurrentPercentage { get => currentPercentage; private set => currentPercentage = value; } 

    void Start()
    {
        initialHP = HitPoints;
        pieces = GameObject.FindGameObjectsWithTag("FoodPieces");
    }

    new protected void Update()
    {
        if (currentPercentage == 100 - (HitPoints * 100 / initialHP))
        {
            return;
        }
        currentPercentage = 100 - (HitPoints * 100 / initialHP) ;
        int qtyToRemove = (int)Mathf.Floor(pieces.Length * (currentPercentage / 100.0f));
        for (int i = pieces.Length - 1; i >= pieces.Length - qtyToRemove; i--)
        {
            if (pieces[i] != null)
            {
                //Debug.Log($"Destroying:{pieces[i].name}");
                Destroy(pieces[i]);
            }
        }
        base.Update();
    }

    protected override void Die()
    {
        AlexGMTest.GetInstance().GameOver();
        base.Die();
    }
    
}
