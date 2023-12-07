using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDropped : MonoBehaviour
{
	public Item item;

	public static GameObject prefItemDrop;
	public static Canvas canvasGame;
	public static Item itemDrop;

	public GameObject pubPrefItemDrop;
	public Canvas pubCanvasGame;

	public GameObject instanceInventory;
	InventoryManager inventory;


    // Start is called before the first frame update
    void Start()
    {
		inventory = instanceInventory.transform.GetComponentInParent<InventoryManager>();
		prefItemDrop = pubPrefItemDrop;
		canvasGame = pubCanvasGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void CollectItem()
	{
		
		if (item != null)
		{
			if (Vector2.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= 3)
			{
			if (BagManager.CountItems(inventory.inventoryItems) < inventory.slotsBag)
				{
					inventory.CollectItem(item);
					Debug.Log(BagManager.CountItems(inventory.inventoryItems));
					Destroy(gameObject);
				}
			}
		}
	}

	public static void DropItem(Item newItem, Transform pos)
	{
		GameObject newItemDrop = Instantiate(prefItemDrop);
		newItemDrop.transform.SetParent(canvasGame.transform);
		newItemDrop.transform.position = pos.position;
		newItemDrop.GetComponent<Image>().sprite = newItem.itemIcon;
		newItemDrop.GetComponent<ItemDropped>().item = newItem;
	}
}
