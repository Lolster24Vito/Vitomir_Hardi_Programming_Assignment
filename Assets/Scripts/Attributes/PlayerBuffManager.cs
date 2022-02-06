using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffManager : MonoBehaviour
{



    private static PlayerBuffManager _instance;

    public static PlayerBuffManager Instance { get { return _instance; } }


    public static event Action OnBuffChangeAttributes;

    private PlayerAttributeManager _playerAttribute;

    private ItemAttributes _buffAttributes=new ItemAttributes();
    public ItemAttributes PlayerBuffAttributes { get => _buffAttributes; }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        _playerAttribute = GetComponent<PlayerAttributeManager>();
    }
    // Start is called before the first frame update

    public void UseItem(ItemConsumables itemConsumables)
    {
        switch (itemConsumables.EnumConsumeType)
        {
            case EnumConsumeEffectType.HoldBonus:
                HoldBonus(itemConsumables);
                break;
            case EnumConsumeEffectType.RampValue:
                RampValue(itemConsumables);
                break;
            case EnumConsumeEffectType.TickValue:
                TickValue(itemConsumables);
                break;

            default:
                break;
        }
    }
    private void HoldBonus(ItemConsumables itemConsumables)
    {
        if (!itemConsumables.CharacterAttributes.IsEmpty())
        {
            StartCoroutine(HoldCharacterAttributeIncrease(itemConsumables.CharacterAttributes,itemConsumables.ConsumeEndTimeSeconds));
        }
        if (!itemConsumables.SpendableAttributes.IsEmpty())
        {
            _playerAttribute.spendableAttributes += itemConsumables.SpendableAttributes;
            OnBuffChangeAttributes?.Invoke();
        }
    }
    private void TickValue(ItemConsumables itemConsumables)
    {
        if (!itemConsumables.CharacterAttributes.IsEmpty())
        {
            StartCoroutine(TickChangeValue(itemConsumables.CharacterAttributes, itemConsumables.ConsumeEndTimeSeconds,itemConsumables.TickTimeToAddNewAttributes));
        }
        if (!itemConsumables.SpendableAttributes.IsEmpty())
        {
            StartCoroutine(TickChangeValue(itemConsumables.SpendableAttributes, itemConsumables.ConsumeEndTimeSeconds,itemConsumables.TickTimeToAddNewAttributes));
        }
    }
    private void RampValue(ItemConsumables itemConsumables)
    {
        if (!itemConsumables.CharacterAttributes.IsEmpty())
        {
            StartCoroutine(RampValue(itemConsumables.CharacterAttributes, itemConsumables.ConsumeEndTimeSeconds, itemConsumables.RampValueMaxSeconds));
        }
        if (!itemConsumables.SpendableAttributes.IsEmpty())
        {
            StartCoroutine(RampValue(itemConsumables.SpendableAttributes, itemConsumables.ConsumeEndTimeSeconds, itemConsumables.RampValueMaxSeconds));

        }
    }
    IEnumerator HoldCharacterAttributeIncrease(ItemAttributes itemAttributes,float consumeTimeSeconds)
    {

        _buffAttributes += itemAttributes;
        OnBuffChangeAttributes?.Invoke();
        yield return new WaitForSeconds(consumeTimeSeconds);
        _buffAttributes -= itemAttributes;
        OnBuffChangeAttributes?.Invoke();

    }
    IEnumerator RampValue(ItemAttributes itemAttributes, float consumeTimeSeconds,float rampValueSecondsEnd)
    {

        ItemAttributes attributeHelper = new ItemAttributes();

        for (float f = 0f; f < 1f; f += Time.deltaTime / rampValueSecondsEnd)
        {
            for (int i = 0; i < itemAttributes.itemAttributes.Length; i++)
            {
                attributeHelper.itemAttributes[i].Value = (int)(Mathf.Lerp(0, itemAttributes.itemAttributes[i].Value, f));
            }

            _buffAttributes += attributeHelper;
            OnBuffChangeAttributes?.Invoke();
            yield return null;
            _buffAttributes -= attributeHelper;

        }
        _buffAttributes += itemAttributes;
        OnBuffChangeAttributes?.Invoke();
        yield return new WaitForSeconds(consumeTimeSeconds);
        _buffAttributes -= itemAttributes;
        OnBuffChangeAttributes?.Invoke();
    }
    IEnumerator RampValue(SpendableAttributes spendableAttributes, float consumeTimeSeconds, float rampValueSecondsEnd)
    {

        SpendableAttributes attributeHelper = new SpendableAttributes();
        for (float f = 0f; f < 1f; f += Time.deltaTime / rampValueSecondsEnd)
        {
            for (int i = 0; i < spendableAttributes.SpendableAttributesArray.Length; i++)
            {
                
                attributeHelper.SpendableAttributesArray[i].Value = (int)(Mathf.Lerp(0, spendableAttributes.SpendableAttributesArray[i].Value, f));

            }
            _playerAttribute.spendableAttributes += attributeHelper;
            OnBuffChangeAttributes?.Invoke();
            yield return null;
            _playerAttribute.spendableAttributes -= attributeHelper;

        }
        _playerAttribute.spendableAttributes += spendableAttributes;
        OnBuffChangeAttributes?.Invoke();
       
    }

    IEnumerator TickChangeValue(ItemAttributes characterAttributes, float consumeEndTimeSeconds, float tickTimeToAddNewAttributes)
    {
        float timeSpent = 0f;
        ItemAttributes addedAttributes=new ItemAttributes();
        while(timeSpent<= consumeEndTimeSeconds)
        {
            _buffAttributes += characterAttributes;
            addedAttributes += characterAttributes;
            timeSpent += tickTimeToAddNewAttributes;
            OnBuffChangeAttributes?.Invoke();
            yield return new WaitForSeconds(tickTimeToAddNewAttributes);
        }
        _buffAttributes -= addedAttributes;
            OnBuffChangeAttributes?.Invoke();


    }
    IEnumerator TickChangeValue(SpendableAttributes spendableAttributes, float consumeEndTimeSeconds, float tickTimeToAddNewAttributes)
    {
        float timeSpent = 0f;
        while (timeSpent <= consumeEndTimeSeconds)
        {
            _playerAttribute.spendableAttributes+= spendableAttributes;
            timeSpent += tickTimeToAddNewAttributes;
            OnBuffChangeAttributes?.Invoke();
            yield return new WaitForSeconds(tickTimeToAddNewAttributes);
        }
    }


}
