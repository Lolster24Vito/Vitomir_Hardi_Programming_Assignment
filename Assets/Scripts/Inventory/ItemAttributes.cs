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

[System.Serializable]
public class ItemAttribute: System.IEquatable<ItemAttribute>
{
    private string _name;
    public string Name { get=>_name; }
    public int Value;

    public ItemAttribute(string name)
    {
        _name = name;
    }

    public bool Equals(ItemAttribute other)
    {
        return Name.Equals(other.Name);
    }
    public override string ToString()
    {
        return Name + ": " + Value;
    }
}

//ItemAtributes consists of a list of attributes
//Item attribute = string  name and int value,Equals by name;
//

