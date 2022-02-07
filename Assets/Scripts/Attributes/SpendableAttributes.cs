using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpendableAttributes
{
    [Header("Health,Mana")]
    public SpendableAttribute[] SpendableAttributesArray = {
    new SpendableAttribute("Health",100),new SpendableAttribute("Mana",100) };


    public bool IsEmpty()
    {
        for (int i = 0; i < SpendableAttributesArray.Length; i++)
        {
            if (SpendableAttributesArray[i].Value != 0)
            {
                return false;
            }
        }
        return true;
    }
    public static SpendableAttributes operator +(SpendableAttributes a, SpendableAttributes b)
    {
        for (int i = 0; i < a.SpendableAttributesArray.Length; i++)
        {
            int valueSum = b.SpendableAttributesArray[i].Value + a.SpendableAttributesArray[i].Value;
            a.SpendableAttributesArray[i].Value=Mathf.Clamp(valueSum, 0, a.SpendableAttributesArray[i].MaxValue);
        }
        return a;
    }
    public static SpendableAttributes operator -(SpendableAttributes a, SpendableAttributes b)
    {
        for (int i = 0; i < a.SpendableAttributesArray.Length; i++)
        {
            int valueSum = a.SpendableAttributesArray[i].Value - b.SpendableAttributesArray[i].Value;
            a.SpendableAttributesArray[i].Value = Mathf.Clamp(valueSum, 0, a.SpendableAttributesArray[i].MaxValue);
        }
        return a;
    }
    public override string ToString()
    {
        string text = "";
        for (int i = 0; i < SpendableAttributesArray.Length; i++)
        {
            text += SpendableAttributesArray[i].ToString() + '\n';
        }
        // string text= "Strength:"+
        return text;
    }
}
