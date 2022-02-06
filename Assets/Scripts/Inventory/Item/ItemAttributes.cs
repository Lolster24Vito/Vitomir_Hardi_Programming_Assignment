using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemAttributes
{
    [Header("Strength,Dexterity,Agility,Inteligence,Luck")]
    public ItemAttribute[] itemAttributes = {
    new ItemAttribute("Strength"),new ItemAttribute("Dexterity"),
    new ItemAttribute("Agility"),new ItemAttribute("Inteligence"),new ItemAttribute("Luck")
    };
    
    
  

    public override string ToString()
    {
        string text="";
        for(int i = 0; i < itemAttributes.Length; i++)
        {
            text += itemAttributes[i].ToString() + '\n';
        }
       // string text= "Strength:"+
        return text;
    }
    public static ItemAttributes operator +(ItemAttributes a, ItemAttributes b)
    {
        for(int i = 0; i < a.itemAttributes.Length; i++)
        {
            a.itemAttributes[i].Value += b.itemAttributes[i].Value;
        }
        return a;
    }
    public static ItemAttributes operator -(ItemAttributes a, ItemAttributes b)
    {
        for (int i = 0; i < a.itemAttributes.Length; i++)
        {
            a.itemAttributes[i].Value -= b.itemAttributes[i].Value;
        }
        return a;
    }
    public bool IsEmpty()
    {
        for(int i=0; i< itemAttributes.Length; i++)
        {
            if (itemAttributes[i].Value != 0)
            {
                return false;
            }
        }
        return true;
    }
}


//ItemAtributes consists of a list of attributes
//Item attribute = string  name and int value,Equals by name;
//

