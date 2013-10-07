using UnityEngine;
using System.Collections;

public class Armor : Abilities
{
    public float _armorAmount;
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