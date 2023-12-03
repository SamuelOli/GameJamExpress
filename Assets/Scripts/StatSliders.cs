using UnityEngine;
using UnityEngine.UI;

public class StatSliders : MonoBehaviour
{
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider staminaSlider;

    private void OnEnable()
    {
        PlayerStats.SetUpStats += SetUpStats;
        PlayerStats.OnPlayerHPChange += UpdateHP;
        PlayerStats.OnPlayerStaminaChange += UpdateStamina;
    }

    private void OnDisable()
    {
        PlayerStats.SetUpStats -= SetUpStats;
        PlayerStats.OnPlayerHPChange -= UpdateHP;
        PlayerStats.OnPlayerStaminaChange -= UpdateStamina;
    }

    private void SetUpStats(float maxHP, float maxStamina)
    {
        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
    }

    private void UpdateHP(float newHP)
    {
        hpSlider.value = newHP;
    }

    private void UpdateStamina(float newStamina)
    {
        staminaSlider.value = newStamina;
    }
}
