using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetBoard : MonoBehaviour, Idamageable
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private TMP_Text _UIhealth;
    public void ReceiveDamage(float damageAmount)
    {
        _currentHealth -= damageAmount;
        _UIhealth.text = $"{_currentHealth}/{_maxHealth}";
        if (_currentHealth <= 0 )
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
