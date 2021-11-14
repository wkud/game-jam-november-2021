using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeController : MonoBehaviour
{

    [SerializeField] private GameObject InitiativeAvatarPrefab;


    public void SpawnAvatar()
    {
        GameObject childObject = Instantiate(InitiativeAvatarPrefab) as GameObject;
        childObject.transform.parent = gameObject.transform;
    }

    internal void Initialize(InitiativeTracker initiativeTracker)
    {
        throw new NotImplementedException();
    }

    internal void OnFinishedTurn()
    {
        throw new NotImplementedException();
    }
}
