using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObjectSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _genericItem;
    [SerializeField] Vector3 _droppedItemOffset;


    [SerializeField] ItemPermanentUse _permanentUseItem;
    [SerializeField] ItemConsumables _consumableItem;
    [SerializeField] Item _genericNonStackableItem;
    [SerializeField] Item _genericInfiniteStackableItem;
    [SerializeField] Item _stackableWithMaxLimitItem;
    [SerializeField] ItemEquipable _helmet;
    [SerializeField] ItemEquipable _torso;
    [SerializeField] ItemEquipable _weapon;
    [SerializeField] ItemEquipable _shield;
    [SerializeField] ItemEquipable _helmet2;

    // Update is called once per frame
    void Start()
    {
        SpawnObject(_permanentUseItem);
        SpawnObject(_consumableItem);
        SpawnObject(_genericNonStackableItem);
        SpawnObject(_genericInfiniteStackableItem);
        SpawnObject(_stackableWithMaxLimitItem);
        SpawnObject(_helmet);
        SpawnObject(_torso);
        SpawnObject(_weapon);
        SpawnObject(_shield);
        SpawnObject(_helmet2);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnObject(_helmet2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnObject(_permanentUseItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnObject(_consumableItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SpawnObject(_genericNonStackableItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SpawnObject(_genericInfiniteStackableItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SpawnObject(_stackableWithMaxLimitItem);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SpawnObject(_helmet);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SpawnObject(_torso);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SpawnObject(_weapon);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnObject(_shield);
        }
    }


    void SpawnObject(Item item)
    {
        Vector3 currentPosition = transform.position;
        Vector3 dropPosition = new Vector3(currentPosition.x + _droppedItemOffset.x, currentPosition.y + _droppedItemOffset.y, currentPosition.z);
        GameObject droppedItem = Instantiate(_genericItem, dropPosition, Quaternion.identity);
        droppedItem.GetComponent<ItemWorld>().SetItem(item);
        droppedItem.GetComponent<ItemWorld>().SetAmount(1);
    }
}
