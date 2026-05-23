using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 5f;

    [Header("Combat Stats")]
    public float attackDamage = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 7f;

    [Header("Visual Feedback (Görsel Tepkiler)")]
    [Tooltip("Hasar arttıkça fiziksel olarak büyüyecek olan Yay/Silah modeli")]
    public Transform weaponModel;
    [Tooltip("Can arttıkça daha da kaslı/iri görünecek olan Karakter modeli")]
    public Transform playerModel;

    public void UpgradeHealth(float amount)
    {
        maxHealth += amount;

        // Görsel Tepki: Karakter her can geliştirmesinde %5 büyür
        if (playerModel != null)
        {
            playerModel.localScale += new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    public void UpgradeMoveSpeed(float amount)
    {
        moveSpeed += amount;
    }

    public void UpgradeAttackDamage(float amount)
    {
        attackDamage += amount;

        // Görsel Tepki: Hasar her arttığında silah/yay %10 büyür
        if (weaponModel != null)
        {
            weaponModel.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    public void UpgradeAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }
}