using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{

   [SerializeField]private Inventory _inventory;
    [SerializeField] private InventoryUI _inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = new Inventory();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            if(collision.TryGetComponent<ItemWorldHolder>(out ItemWorldHolder itemHolder))
            {
                _inventory.AddItem(itemHolder.GetItem());
                _inventoryUI.AddItem(itemHolder.GetItem());
                Destroy(collision.gameObject);
            }
        }
    }

    // Update is called once per frame

}
