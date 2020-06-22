using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    Player p;

    public Image healthbar;
    public Image[] ammo;
    public TextMeshProUGUI score;

    public GameObject DeathCanvas;

    private void Awake()
    {
        p = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (p.mode == Gamemode.Multiplayer)
        {
            score.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        UpdateAmmo();
    }

    public void UpdateHealth()
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

    public void UpdateScore(int _score)
    {
        score.text = $"Score: {_score}";
    }
}
