using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {
    
    public float _damageDealt;
    public float _speed;
    
    protected bool _doubleDamage;
    protected bool _tripleDamage;
    
    
    void Start()
    {
        if (_doubleDamage)
        {
            _damageDealt *= 2;
        }
        
        else if (_tripleDamage)
        {
            _damageDealt *= 3;
        }
    }
}