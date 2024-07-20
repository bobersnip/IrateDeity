using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour, IPickupObject
{
    [SerializeField] int value = 10;
    public void OnPickup(Character character)
    {
        character.coins.Add(value);
    }
}
