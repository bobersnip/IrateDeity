using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickupObject : MonoBehaviour, IPickupObject
{
    [SerializeField] int healAmount;
    
    public void OnPickup(Character character)
    {
        character.Heal(healAmount);
    }
}
