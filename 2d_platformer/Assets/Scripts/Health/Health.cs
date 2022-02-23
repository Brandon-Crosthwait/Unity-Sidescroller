using UnityEngine;

public class Health : MonoBehaviour
{
    private HealthHelper hHelper = new HealthHelper();
    //SerializedFiled allows to be seen in the editor
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;

    //holds the player's current health. get set/being public is there so it can only curently be grabbed from Healthbar.cs
    public float currentHealth;
    private void Awake()
    {
        //Sets the player health to the max health
        initializeStartingHealth();
    }

    public float getStartingHealth() 
    {
        return startingHealth;
    }

    public void setStartingHealth(float health) 
    {
        startingHealth = health;
    }

    public void initializeStartingHealth() 
    {
       hHelper.initializeStartingHealth(ref currentHealth, startingHealth); 
    }

    public void TakeDamage(float _damage)
    {
        hHelper.takeDamage(ref currentHealth, _damage, startingHealth);

        if (currentHealth > 0)
        {
            SoundManager.instance.PlaySound(hurtSound);
            //damage taken, but player is still alive
        }
        else
        {
            SoundManager.instance.PlaySound(deathSound);
            GetComponent<PlayerMovement>().SetCanMove(false);
            GetComponent<PlayerMovement>().PlayerDeath();
            //Leave blank for now
        }
    }

    // Method used to increase the player's health when they pick up a HealthCollectable
    public void IncreaseHealth(float healthToGive)
    {
        hHelper.IncreaseHealth(ref currentHealth, healthToGive, startingHealth);
    }

    public void IncreaseHealth()
    {
        hHelper.IncreaseHealth(ref currentHealth, startingHealth);
    }

    // Update is called once per frame
    /*void Update()
    {
        //Test input to take damage.
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
            GetComponent<PlayerMovement>().GetHit();
        }  
    }*/
}

public class HealthHelper
{
    public void initializeStartingHealth(ref float health, float startingHealth)
    {
        health = startingHealth;
    }

    public void IncreaseHealth(ref float health, float maxHealth)
    {
        health = Mathf.Clamp(health + 1, 0, maxHealth);
    }

    public void IncreaseHealth(ref float health, float amount, float maxHealth)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
    }

    public void takeDamage(ref float health, float damage, float maxHealth)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
    }
}