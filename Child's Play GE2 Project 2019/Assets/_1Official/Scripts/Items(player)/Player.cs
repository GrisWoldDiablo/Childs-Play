using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBaseClass
{
    private void OnDestroy()
    {
        if (PlayerManager.GetInstance() != null)
        {
            PlayerManager.GetInstance().RemovePlayer(this);
        }
    }
}
