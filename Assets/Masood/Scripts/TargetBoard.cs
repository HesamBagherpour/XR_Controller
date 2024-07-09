using System.Collections;
using TMPro;
using UnityEngine;

public class TargetBoard : MonoBehaviour, Idamageable
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private TMP_Text _UIhealth;

    public void ReceiveDamage(RaycastHit hit, float damageAmount)
    {
        ImpactHandler.GenerateHitImpact(hit);
        _currentHealth -= damageAmount;
        _UIhealth.text = $"{_currentHealth}/{_maxHealth}";
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        _UIhealth.text = $"{_currentHealth}/{_maxHealth}";
    }

}
