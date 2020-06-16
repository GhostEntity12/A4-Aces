using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class PlayerUI : MonoBehaviour
{
    Player p;

    public Image healthbar;
    public Image[] ammo;

    private void Awake()
    {
        p = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        UpdateHealth();
        UpdateAmmo();
    }

    private void UpdateHealth()
    {
        healthbar.fillAmount = p.currentHealth / p.maxHealth;
    }

    public void UpdateAmmo()
    {
        for (int i = 0; i < ammo.Length; i++)
        {
            ammo[i].gameObject.SetActive(p.currentAmmo > i);
        }
    }
}
