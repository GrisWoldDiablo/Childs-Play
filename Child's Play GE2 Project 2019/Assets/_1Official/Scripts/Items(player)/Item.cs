using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Normal,
    Placeholder
}

public class Item : Player
{
    [Header("Item Option")]
    [SerializeField] private ItemType _itemType = ItemType.Normal;
    [SerializeField] private string itemName;
    [SerializeField, Multiline] private string itemDescription;
    /// <summary>
    /// this is the base value of the Item, when the players buys this item the value is reduce 
    /// but it will go up as this item is upgraded in order to increase its resell value.
    /// </summary>
    [SerializeField] private int value = 1;
    [SerializeField] private int indexInGM;
    [SerializeField] private GameObject rangeGO;
    [SerializeField] private GameObject rangeGOUpgrade;
    [SerializeField] private Item upgradeVersion;

    private AudioSource myAudioSource;

    public GameObject RangeGO { get => rangeGO; }
    public int Value { get => value; set => this.value = value; }
    public string ItemName { get => itemName; }
    public string ItemDescription { get => itemDescription; }
    public int IndexInGM { get => indexInGM; }
    public Item UpgradeVersion { get => upgradeVersion; }
    public GameObject RangeGOUpgrade { get => rangeGOUpgrade; set => rangeGOUpgrade = value; }

    // Start is called before the first frame update
    //void Awake()
    //{
    //    //if (rangeGO != null)
    //    //{
    //    //    rangeGO.SetActive(false);
    //    //}
    //
    //    //indexInGM = 0;
    //    //itemDescription = "This is a placeholder description";
    //}

    private void Awake()
    {
        if (rangeGO != null)
        {
            SetRangeScale(GetComponent<Tower>().Range);
        }
        if (rangeGOUpgrade != null)
        {
            rangeGOUpgrade.SetActive(false);
            if (upgradeVersion != null)
            {
                SetRangeUPScale(upgradeVersion.GetComponent<Tower>().Range);
            }
        }
        if (_itemType == ItemType.Placeholder)
        {
            Destroy(GetComponent<Tower>());
            Destroy(this);
        }
        myAudioSource = GetComponent<AudioSource>();
    }

    public void SetRangeScale(float scaleV3)
    {
        rangeGO.transform.localScale = Vector3.one * scaleV3 * 2.0f;
        rangeGO.transform.localScale = new Vector3(rangeGO.transform.localScale.x,
            Mathf.Clamp(rangeGO.transform.localScale.y * 0.33f, 0.0f, 10.0f), rangeGO.transform.localScale.z);
    }

    public void SetRangeUPScale(float scaleV3)
    {
        rangeGOUpgrade.transform.localScale = Vector3.one * scaleV3 * 2.0f;
        rangeGOUpgrade.transform.localScale = new Vector3(rangeGOUpgrade.transform.localScale.x,
            Mathf.Clamp(rangeGOUpgrade.transform.localScale.y * 0.33f, 0.0f, 10.0f), rangeGOUpgrade.transform.localScale.z);
    }

    public override void TakeDamage(int damageValue)
    {
        base.TakeDamage(damageValue);
        PlaySound();
    }

    private void PlaySound()
    {
        if (myAudioSource.clip != null && !myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(myAudioSource.clip);   
        }
    }
}