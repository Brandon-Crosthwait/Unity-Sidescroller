using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHp;
    private int currentHP;

    void Start()
    {
        currentHP = enemyHp;
    }

    void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
    }
}
