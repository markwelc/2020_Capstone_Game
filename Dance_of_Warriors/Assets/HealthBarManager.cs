using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need UI to handle this

public class HealthBarManager : MonoBehaviour
{

    private Image healthBar;
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    PlayerHealthController healthManager;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        healthManager = FindObjectOfType<PlayerHealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = healthManager.getHealth();
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
