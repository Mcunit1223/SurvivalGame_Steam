using UnityEngine;
using System.Collections;

public class StatManagement : MonoBehaviour {
    // Bar Textures
    public Texture2D emptyHealthBar;
    public Texture2D healthBar;
    public Texture2D emptyHungerBar;
    public Texture2D hungerBar;
    public Texture2D emptyThirstBar;
    public Texture2D thirstBar;
    public Texture2D emptyStaminaBar;
    public Texture2D staminaBar;

    // Stat Values
    public int maxHealth;
    public int maxHunger;
    public int maxThirst;
    public int maxStamina;
    public float health;
    public float hunger;
    public float thirst;
    public float stamina;

    // GUI Values
    Vector2 size = new Vector2(240, 40);
    Vector2 healthPos = new Vector2(20, 20);
    Vector2 hungerPos = new Vector2(20, 60);
    Vector2 thirstPos = new Vector2(20, 100);
    Vector2 staminaPos = new Vector2(20, 140);

    // Controllers
    public bool canJump = true;
    public bool canSprint = true;

    // Fall Rates
    public float healthFallRate;
    public float hungerFallRate;
    public float thirstFallRate;
    public float staminaFallRate;
    public float jumpFallRate;

    void Start()
    {
        health = maxHealth;
        hunger = maxHunger;
        thirst = maxThirst;
        stamina = maxStamina;
    }

    void OnGUI()
    {
        // Health GUI
        GUI.BeginGroup(new Rect(healthPos.x, healthPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyHealthBar);

        GUI.BeginGroup(new Rect(0, 0, size.x * health, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), healthBar);

        GUI.EndGroup();
        GUI.EndGroup();

        // Hunger GUI
        GUI.BeginGroup(new Rect(hungerPos.x, hungerPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyHungerBar);

        GUI.BeginGroup(new Rect(0, 0, size.x * hunger, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), hungerBar);

        GUI.EndGroup();
        GUI.EndGroup();

        // Thirst GUI
        GUI.BeginGroup(new Rect(thirstPos.x, thirstPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyThirstBar);

        GUI.BeginGroup(new Rect(0, 0, size.x * thirst, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), thirstBar);

        GUI.EndGroup();
        GUI.EndGroup();

        // Stamina GUI
        GUI.BeginGroup(new Rect(staminaPos.x, staminaPos.y, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyStaminaBar);

        GUI.BeginGroup(new Rect(0, 0, size.x * stamina, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), staminaBar);

        GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        // DepleteHunger
        if(hunger > 0)
        {
            hunger -= Time.deltaTime / hungerFallRate;
        }

        if(hunger < 0)
        {
            hunger = 0;
        }

        if(hunger > 1)
        {
            hunger = 1;
        }

        // Deplete Thirst
        if(thirst > 0)
        {
            thirst -= Time.deltaTime / thirstFallRate;
        }

        if(thirst < 0)
        {
            thirst = 0;
        }

        if(thirst > 1)
        {
            thirst = 1;
        }

        // Deplete Stamina
        if(canSprint && Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= Time.deltaTime / staminaFallRate;
        }
        else
        {
            stamina += Time.deltaTime / staminaFallRate * 2;
        }

        // Deplete Health
        if(hunger <= 0)
        {
            health -= Time.deltaTime / healthFallRate;
        }

        if(thirst <= 0)
        {
            health -= Time.deltaTime / healthFallRate;
        }

        // Reset Boolean Values
        canJump = stamina > jumpFallRate ? true : false;
        canSprint = stamina > 0 ? true : false;
    }

    public void jump()
    {
        stamina -= jumpFallRate;
    }
}
