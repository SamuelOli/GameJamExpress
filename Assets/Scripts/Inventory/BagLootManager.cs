using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagLootManager : MonoBehaviour
{

	public GameObject bagLoot;
	public GameObject bagLootSlots;

	public Sprite bagLootDefault;
	public GameObject prefSlot;
	public List<Image> slots = new List<Image>();

	public List<Item> bagLootItems = new List<Item>();

	public List<Item> openItems;

	public int slotsBag = 0;

	public InventoryManager inventory;

    private OpenBagLoot obl;

	// Start is called before the first frame update
	void Start()
	{
		if (slotsBag <= 8)
		{
			slotsBag = 8;
			CreateSlot();
			for (int i = bagLootItems.Count; i < slotsBag; i++) { bagLootItems.Add(null); }
		}
		else
		{
            CreateSlot();
			for (int i = bagLootItems.Count; i<slotsBag; i++) { bagLootItems.Add(null); }
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ActiveBagLoot()
	{
		if (slotsBag >= 8)
		{
			LoadBagLoot();
			bagLoot.SetActive(true);
		}
		else
		{
			slotsBag = 8;
			CreateSlot();
			bagLoot.SetActive(true);
		}
	}

	public void CreateSlot()
	{
		for (int i = slots.Count; i < slotsBag; i++)
		{
			GameObject newSlot = Instantiate(prefSlot, bagLootSlots.transform);
			newSlot.SetActive(true);
			slots.Add(newSlot.GetComponent<Image>());
			slots[i].sprite = bagLootDefault;
			slots[i].color = new Color(0, 0, 0, 0.5f);

			slots[i].name = i.ToString();
		}

		//float y = (slotsBag / 5 * 40) + 10;
		//bagLoot.GetComponent<RectTransform>().sizeDelta = new Vector2(170, 90);
		LoadBagLoot();
	}
	public void AddSlot(int capacitySlots)
	{
		slotsBag += capacitySlots;
		CreateSlot();
	}

	public void LoadBagLoot()
	{
		for (int i = bagLootItems.Count; i<slotsBag; i++) { bagLootItems.Add(null); }

		for (int i = 0; i < slotsBag; i++)
		{
			if (bagLootItems[i] != null)
			{
				bagLootItems[i].slot = i;

				slots[i].sprite = bagLootItems[i].itemIcon;
				slots[i].color = new Color(1, 1, 1, 1);
			}
			else
			{
				slots[i].sprite = bagLootDefault;
				slots[i].color = new Color(0, 0, 0, 0.5f);
			}
		}
	}

	public void SetBagLoot(List<Item> list, OpenBagLoot newObl)
	{
        for (int i = 0; i < bagLootItems.Count; i++)
        {
            if (i < list.Count && list[i] != null) { bagLootItems[i] = list[i]; }
        }
        openItems = list;
        obl = newObl;
	}

	public void CollectItem(GameObject slotUsed)
	{
		int i = int.Parse(slotUsed.name);

		CollectItem(i);
	}

	public void CollectItem(int i)
	{
		if (BagManager.CountItems(inventory.inventoryItems) < inventory.slotsBag && i < bagLootItems.Count)
		{
			if (bagLootItems[i] != null)
			{
				inventory.CollectItem(bagLootItems[i]);
                openItems[i] = null;
                bagLootItems[i] = null;
                slots[i].sprite = bagLootDefault;
				slots[i].color = new Color(0, 0, 0, 0.5f);
                
                if(obl.Collect() <= 0) { Close(); }
			}		}
	}

	public void CollectAllItems()
	{
		for (int i = 0; i < bagLootItems.Count; i++)
		{
			if (BagManager.CountItems(inventory.inventoryItems) < inventory.slotsBag) { CollectItem(i); }
			else
			{
				Debug.Log("Bag is full. It was not possible to get all items");
				Close();
				break;
			}
		}
		Close();
	}

	public void Close()
	{
		bagLoot.SetActive(false);
	}
}
