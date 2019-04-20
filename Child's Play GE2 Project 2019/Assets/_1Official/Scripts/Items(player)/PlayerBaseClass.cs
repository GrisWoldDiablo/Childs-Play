using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass : MonoBehaviour
{
    [Header("Player Option")]
    [SerializeField] private int hitPoints = 100;
    public int HitPoints { get => hitPoints; set => hitPoints = value; }

    protected AudioSource myAudioSource;

    protected void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    protected virtual void Die()
    {
        if (this == GameManager.GetInstance().SelectedItem)
        {
            GameManager.GetInstance().DeselectTile();
        }
        Destroy(this.gameObject);
    }

    public virtual void TakeDamage(int damageValue)
    {
        this.hitPoints -= damageValue;

        if (this.HitPoints <= 0)
        {
            Die();
            return;
        }
        PlaySound();
    }

    protected void PlaySound()
    {
        if (myAudioSource.clip != null && !myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(myAudioSource.clip);
        }
    }

}
