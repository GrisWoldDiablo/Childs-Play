using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RandomNodeSelector : AbstractNodeSelector
{
    public override Node GetNextNode()
    {
        if (listOfNodes == null)
        {
            return null;
        }
        return listOfNodes[Random.Range(0, listOfNodes.Count)];
        ////float chance = Random.value;
        //if (Random.value > 0.5)
        //{
        //    return listOfNodes[0];
        //}
        //return listOfNodes[1];
    }
}



