using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    public int health, maxHealth = 100;
    public Image healthBar;
    public bool death = false;
    public float lerpSpeed = 3f;  // Sabit bir lerp hýzý

    private Color healthColor;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  // Saðlýk baþlangýçta maksimum seviyede olacak.
        this.gameObject.GetComponent<Building>().health = health;
        this.gameObject.GetComponent<Building>().maxHealth = maxHealth;


//        UpdateBuildingHealth();  // Baþlangýçta bina saðlýðýný güncelle
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
                death = true;  // Saðlýk sýfýra ulaþtýðýnda ölüm durumunu iþaretle
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
        // Building class'ýnda health ve maxHealth'i güncelle
        var building = this.gameObject.GetComponent<Building>();
        if (building != null)
        {
            building.health = health;
            building.maxHealth = maxHealth;
        }
    }

    private void HealthFillAmountChanger()
    {
        // Health / MaxHealth iþlemini float olarak yap
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
        HealthFillAmountChanger();  // Health bar'ý anýnda güncelle
        HealthBarFiller();  // Saðlýk çubuðunu pürüzsüz geçiþlerle güncelle
        ColorChanger();  // Saðlýk çubuðunun rengini güncelle
    }
}
