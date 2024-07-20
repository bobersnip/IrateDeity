using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Character : MonoBehaviour
{
    [SerializeField] public int maxHp = 10;
    [SerializeField] public int currentHp = 10;

    [SerializeField] public float hpRegenRate = 1f;
    [SerializeField] public float hpRegenBase = 10f;
    private float hpRegenTimer = 0f;

    public int armor = 0;

    [SerializeField] StatusBar hpBar;

    [HideInInspector] public Level level;
    [HideInInspector] public Coins coins;

    private bool isDead = false;

    private void Awake()
    {
        level = GetComponent<Level>();
        coins = GetComponent<Coins>();
    }

    private void Start()
    {
        hpBar.SetState(currentHp, maxHp);
    }

    private void Update()
    {
        hpRegenTimer += Time.deltaTime;
        if (hpRegenTimer > (hpRegenBase / hpRegenRate))
        {
            Heal(5);
            hpRegenTimer = 0;
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
}
