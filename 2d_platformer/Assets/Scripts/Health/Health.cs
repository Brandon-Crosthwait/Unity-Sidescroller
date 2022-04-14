using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    private int characterSelected;
    private HealthHelper hHelper = new HealthHelper();
    //SerializedFiled allows to be seen in the editor
    [SerializeField] private float startingHealth;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numFlashes;
    private SpriteRenderer spriteRend;

    //holds the player's current health. get set/being public is there so it can only curently be grabbed from Healthbar.cs
    public float currentHealth;
    private void Awake()
    {
        //health Character
        characterSelected = PlayerPrefs.GetInt("CharacterAnimatorOverriderID");
        if(characterSelected == 3)
        {
            startingHealth = 4;
        }
        //Sets the player health to the max health
        initializeStartingHealth();
        spriteRend = GetComponent<SpriteRenderer>();
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
            StartCoroutine(Invulnerability());
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

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6,7, true);
        Physics2D.IgnoreLayerCollision(7,8, true);
        for (int i = 0; i < numFlashes; i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6,7, false);
        Physics2D.IgnoreLayerCollision(7,8, false);
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
        PlayerPrefs.SetString("Health", health.ToString());
    }

    public void IncreaseHealth(ref float health, float maxHealth)
    {
        health = Mathf.Clamp(health + 1, 0, maxHealth);
        PlayerPrefs.SetString("Health", health.ToString());
    }

    public void IncreaseHealth(ref float health, float amount, float maxHealth)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        PlayerPrefs.SetString("Health", health.ToString());
    }

    public void takeDamage(ref float health, float damage, float maxHealth)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth);
        PlayerPrefs.SetString("Health", health.ToString());
    }
}