using System;
using UnityEngine;

public class UnitController3D : MonoBehaviour
{
    public UnitData unitData;
    public UnitController3D enemyTarget;

    private float currentHealth;
    private float attackCooldown;
    public GameObject healthBarPrefab;
    private HealthBar healthBarUI;

    public void Initialize(UnitData data)
    {
        unitData = data;
        currentHealth = unitData.maxHealth;
        attackCooldown = 0f;
        
        if (healthBarPrefab != null)
        {
            GameObject bar = Instantiate(healthBarPrefab);
            healthBarUI = bar.GetComponent<HealthBar>();
            healthBarUI.Initialize(transform);
            healthBarUI.UpdateHealth(currentHealth, unitData.maxHealth);
        }
    }

    void Update()
    {
        if (enemyTarget == null) return;

        float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);

        // Rotar hacia el enemigo (mantiene sprite orientado si usas sprites 3D)
        Vector3 lookDir = enemyTarget.transform.position - transform.position;
        lookDir.y = 0f; // Evita inclinar hacia arriba/abajo
        if (lookDir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDir);

        if (distance > unitData.attackRange)
        {
            // Movimiento libre hacia el enemigo
            Vector3 dir = (enemyTarget.transform.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime * 1.5f;
        }
        else
        {
            // Ataque automático
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                enemyTarget.TakeDamage(unitData.attackDamage);
                attackCooldown = unitData.attackSpeed;
            }
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (healthBarUI != null)
        healthBarUI.UpdateHealth(currentHealth, unitData.maxHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        if (healthBarUI != null)
            Destroy(healthBarUI.gameObject);
            
        Destroy(gameObject);
    }
}
