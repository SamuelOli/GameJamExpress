using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private bool doesHealthRegen;
    [SerializeField] private float healthRegen;
    [SerializeField] private float delayToRegenHealth;
    private float healthTimeStamp;
    private bool canHealthRegen;

    [Header("Stamina")]
    [SerializeField] private float maxStamina;
    private float currentStamina;
    [SerializeField] private float staminaCost;
    [SerializeField] private bool doesStaminaRegen;
    [SerializeField] private float staminaRegen;
    [SerializeField] private float delayToRegenStamina;
    private float staminaTimeStamp;
    private bool canStaminaRegen;

    public static Action<float, float> SetUpStats;
    public static Action<float> OnPlayerHPChange;
    public static Action<float> OnPlayerStaminaChange;
    public static Action<bool> OnSprint;
    public static Action OnPlayerDeath;

    private void Start()
    {
        SetUpStats(maxHealth, maxStamina);
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        canStaminaRegen = false;
        canHealthRegen = false;
    }

    private void Update()
    {
        HandleHP();
        HandleStamina();
    }

    private void HandleHP()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            canHealthRegen = false;
            healthTimeStamp = Time.time + delayToRegenHealth;
            currentHealth -= 1f;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnPlayerDeath();
            }

            OnPlayerHPChange(currentHealth);
        }

        if (healthTimeStamp < Time.time)
            canHealthRegen = true;

        RegenHealth();
    }

    private void RegenHealth()
    {
        if (doesHealthRegen && canHealthRegen && currentHealth < maxHealth)
        {
            currentHealth += healthRegen * Time.deltaTime;
            OnPlayerHPChange(currentHealth);
        }
    }

    private void HandleStamina()
    {
        if (Input.GetButton("Sprint"))
        {
            OnSprint(true);
            canStaminaRegen = false;
            currentStamina -= staminaCost * Time.deltaTime;
            if (currentStamina < 0)
                currentStamina = 0;
            OnPlayerStaminaChange(currentStamina);
            staminaTimeStamp = Time.time + delayToRegenStamina;
        }
        else if (Input.GetButtonUp("Sprint"))
            OnSprint(false);

        if (staminaTimeStamp <= Time.time)
            canStaminaRegen = true;

        RegenStamina();
    }

    private void RegenStamina()
    {
        if (doesStaminaRegen && canStaminaRegen && currentStamina < maxStamina)
        {
            currentStamina += staminaRegen * Time.deltaTime;
            if (currentStamina > maxStamina)
                currentStamina = maxStamina;
            OnPlayerStaminaChange(currentStamina); 
        }
    }
}
