using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemAttributes
{
    public ItemAttribute[] itemAttributes = {
    new ItemAttribute("Strength"),new ItemAttribute("Dexterity"),
    new ItemAttribute("Agility"),new ItemAttribute("Inteligence")
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
}


//ItemAtributes consists of a list of attributes
//Item attribute = string  name and int value,Equals by name;
//

