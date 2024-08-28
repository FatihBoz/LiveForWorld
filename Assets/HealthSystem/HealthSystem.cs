using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int health, maxHealth =100;

    public Image healthBar;

    public bool death = false;

    public float lerpSpeed;

    Color healthColor;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Building>().health = health; //!NULL REFERENCE EXCEPTION
        this.gameObject.GetComponent<Building>().maxHealth = maxHealth;

        

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        ClassicHealthDisplay();

        
    }

    public void Damage(int damage)
    {
        if (health > 0)  
            health -= damage;
    }

    public void Heal(int heal)
    {
        if (health < maxHealth)
            health += heal;
    }
    public void HealthFillAmountChanger() { 
        
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if (health < 0)
        {
            death = true;
            health = 0;
        }

        healthBar.fillAmount = health / maxHealth;
    
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health/maxHealth, lerpSpeed);
    }

    public void ColorChanger()
    {
        healthColor = Color.Lerp(Color.red, Color.green, (health/maxHealth));
        healthBar.color = healthColor;
    }

    public void ClassicHealthDisplay()
    {
        HealthFillAmountChanger();

        lerpSpeed = 3f * Time.deltaTime;

        HealthBarFiller();
        ColorChanger();
    }


}
