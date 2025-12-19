using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    public float stunRadius = 10f;
    public float stunDuration = 3f;
    public float stunCooldown = 10f;

    public GameObject abilityPanel;
    public GameObject abilityOnImage;
    public GameObject abilityOffImage;
    public GameObject cooldownBackground;
    public TMP_Text cooldownText;

    private float cooldownTimer = 0f;
    private bool abilityUnlocked = false;

    void Start()
    {
        if (abilityPanel != null)
            abilityPanel.SetActive(false);

        if (cooldownBackground != null)
            cooldownBackground.SetActive(false);

        if (cooldownText != null)
            cooldownText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (abilityUnlocked && abilityPanel != null && !abilityPanel.activeSelf)
            abilityPanel.SetActive(true);

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;

            if (abilityOnImage != null) abilityOnImage.SetActive(false);
            if (abilityOffImage != null) abilityOffImage.SetActive(true);

            if (cooldownText != null)
            {
                cooldownText.gameObject.SetActive(true);
                cooldownText.text = Mathf.Ceil(cooldownTimer).ToString() + "s";
            }

            if (cooldownBackground != null)
                cooldownBackground.SetActive(true);
        }
        else
        {
            if (abilityUnlocked)
            {
                if (abilityOnImage != null) abilityOnImage.SetActive(true);
                if (abilityOffImage != null) abilityOffImage.SetActive(false);

                if (cooldownText != null)
                    cooldownText.gameObject.SetActive(false);

                if (cooldownBackground != null)
                    cooldownBackground.SetActive(false);
            }
        }

        if (abilityUnlocked && UnityEngine.Input.GetMouseButtonDown(1) && cooldownTimer <= 0f)
        {
            StunEnemies();
            cooldownTimer = stunCooldown;
        }
    }

    void StunEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, stunRadius);
        foreach (var hit in hits)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
                enemy.Stun(stunDuration);
        }
    }

    public void UnlockAbility()
    {
        abilityUnlocked = true;
        cooldownTimer = 0f;

        if (abilityPanel != null)
            abilityPanel.SetActive(true);

        if (abilityOnImage != null) abilityOnImage.SetActive(true);
        if (abilityOffImage != null) abilityOffImage.SetActive(false);

        if (cooldownText != null)
            cooldownText.gameObject.SetActive(false);

        if (cooldownBackground != null)
            cooldownBackground.SetActive(false);
    }
}
