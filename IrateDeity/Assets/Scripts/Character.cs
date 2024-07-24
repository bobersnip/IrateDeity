using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Character : MonoBehaviour
{
    [SerializeField] public int currentHp = 10;
    [SerializeField] public int currentMp = 10;

    private float hpRegenTimer = 0f;
    private float mpRegenTimer = 0f;

    [SerializeField] StatusBar hpBar;
    [SerializeField] StatusBar mpBar;

    [HideInInspector] public Coins coins;
    private bool isDead = false;

    [SerializeField] public float hpRegenRate = 1f;
    [SerializeField] public float hpRegenBase = 10f;
    [SerializeField] public float mpRegenRate = 1f;
    [SerializeField] public float mpRegenBase = 10f;

    // Stats
    [HideInInspector] public Level level;

    [SerializeField] public int maxHp = 10;
    [SerializeField] public int maxMp = 10;


    public int armor = 0; // defense
    private float speed = 8; // movement speed
    [SerializeField] int dexterity = 5;
    [SerializeField] int wisdom = 5;
    [SerializeField] int vitality = 5;
    [SerializeField] int attack = 5;


    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        hpBar.SetState(currentHp, maxHp);
        mpBar.SetState(currentMp, maxMp);

    }

    private void Update()
    {
        hpRegenTimer += Time.deltaTime;
        if (hpRegenTimer > (hpRegenBase / hpRegenRate))
        {
            Heal(vitality);
            hpRegenTimer = 0;
        }
        mpRegenTimer += Time.deltaTime;
        if (mpRegenTimer > (mpRegenBase / mpRegenRate))
        {
            ManaGain(wisdom);
            mpRegenTimer = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        ApplyArmor(ref damage);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            GetComponent<CharacterGameOver>().GameOver();
            isDead = true;
        }

        hpBar.SetState(currentHp, maxHp);
    }

    private void ApplyArmor(ref int damage)
    {
        damage -= armor;
        if (damage < 0)
        {
            damage = 0;
        }
    }

    public void Heal(int amount)
    {
        if (currentHp <= 0)
        {
            return;
        }

        currentHp += amount;
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        hpBar.SetState(currentHp, maxHp);
    }

    public void ManaGain(int amount)
    {
        if (currentMp <= 0)
        {
            return;
        }

        currentMp += amount;
        if (currentMp > maxMp)
        {
            currentMp = maxMp;
        }

        mpBar.SetState(currentMp, maxMp);
    }

    public float GetMovementSpeed()
    {
        return speed;
    }

    public int GetAttack()
    {
        return attack;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public int GetDefense()
    {
        return armor;
    }

    public int GetDexterity()
    {
        return dexterity;
    }

    public int GetWisdom()
    {
        return wisdom;
    }

    public int GetVitality()
    {
        return vitality;
    }
}




