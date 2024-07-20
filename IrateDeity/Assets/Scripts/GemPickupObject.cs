using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickupObject : MonoBehaviour, IPickupObject
{
    [SerializeField] int amount;

    public void OnPickup(Character character)
    {
        character.level.AddExperience(amount);
    }
}
