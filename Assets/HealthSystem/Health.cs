using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public int health, maxHealth = 100;
    public Image healthBar;
    public bool death = false;
    public float lerpSpeed = 3f;  // Sabit bir lerp h�z�

    private Color healthColor;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  // Sa�l�k ba�lang��ta maksimum seviyede olacak.
        this.gameObject.GetComponent<Building>().health = health;
        this.gameObject.GetComponent<Building>().maxHealth = maxHealth;


//        UpdateBuildingHealth();  // Ba�lang��ta bina sa�l���n� g�ncelle
    }

    // Update is called once per frame
    void Update()
    {
        ClassicHealthDisplay();
    }

    public void Damage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                death = true;  // Sa�l�k s�f�ra ula�t���nda �l�m durumunu i�aretle
            }
        }
    }

    public void Heal(int heal)
    {
        if (health < maxHealth)
        {
            health += heal;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
    }

    private void UpdateBuildingHealth()
    {
        // Building class'�nda health ve maxHealth'i g�ncelle
        var building = this.gameObject.GetComponent<Building>();
        if (building != null)
        {
            building.health = health;
            building.maxHealth = maxHealth;
        }
    }

    private void HealthFillAmountChanger()
    {
        // Health / MaxHealth i�lemini float olarak yap
        float healthPercent = (float)health / maxHealth;
        healthBar.fillAmount = healthPercent;
    }

    private void HealthBarFiller()
    {
        float healthPercent = (float)health / maxHealth;
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, healthPercent, lerpSpeed * Time.deltaTime);
    }

    private void ColorChanger()
    {
        float healthPercent = (float)health / maxHealth;
        healthColor = Color.Lerp(Color.red, Color.green, healthPercent);
        healthBar.color = healthColor;
    }

    private void ClassicHealthDisplay()
    {
        HealthFillAmountChanger();  // Health bar'� an�nda g�ncelle
        HealthBarFiller();  // Sa�l�k �ubu�unu p�r�zs�z ge�i�lerle g�ncelle
        ColorChanger();  // Sa�l�k �ubu�unun rengini g�ncelle
    }
}
