using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float _health = 100;
    private float health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, 100 - _radiation);
            healthBar.fillAmount = _health / 100;
            healthText.text = _health.ToString("f0");
        }
    }
    private float _radiation = 0;
    private float radiation
    {
        get { return _radiation; }
        set
        {
            _radiation = Mathf.Clamp(value, 0, 100);
            radiationBar.fillAmount = _radiation / 100;

            health = health;
        }
    }
    private float _hunger = 100;
    private float hunger
    {
        get { return _hunger; }
        set
        {
            _hunger = Mathf.Clamp(value, 0, 100);
            hungerBar.fillAmount = _hunger / 100;
            hungerText.text = _hunger.ToString("f0");
        }
    }

    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Image radiationBar;

    [SerializeField] private Image hungerBar;
    [SerializeField] private TextMeshProUGUI hungerText;

    private void Awake()
    {
        health = 100;
        radiation = 0;
        hunger = 100;
    }
    private void Update()
    {
        if(health < 100 - radiation - 1)
        {
            Heal(Time.deltaTime * 0.5f);
            DepleteHunger(Time.deltaTime * 0.5f);
        }
        DepleteHunger(Time.deltaTime * 0.025f);
    }
    public void Heal(float amount)
    {
        health += amount;
    }
    public void Damage(float amount)
    {
        health -= amount;
    }
    public void Eat(float amount)
    {
        hunger += amount;
    }
    public void DepleteHunger(float amount)
    {
        hunger -= amount;
    }
    public void Radiate(float amount)
    {
        if (Player.Instance.inventorySystem.armorSlot.item != null)
            amount *= 0.15f;

        radiation += amount;
    }
    public void UnRadiate(float amount)
    {
        radiation -= amount;
    } 
}