using UnityEngine;
using System.Collections;

public class Health : Abilities
{
    public float _healthAmount;
    public float _healSpeed;
    
    
    void OnMouseDown()
    {
        CheckInput();
    }
    
    void OnMouseUp()
    {
        CheckInput();
    }
}