using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour {
	public PlayerStats playerStats;

	public GameObject equipment;
	public GameObject equipmentSlots;

	public List<Sprite> defaultEquipments;
	public List<Image> slots = new List<Image> ();

	public List<Item> equippedItems = new List<Item> ();

	InventoryManager inventoryManager;

	public ItemDescription itemDesc;

    GameObject slotSelected;

	// Start is called before the first frame update
	void Start () {
		inventoryManager = GetComponent<InventoryManager> ();

		for (int i = 0; i < equippedItems.Count; i++) {
			if (i != 9) {
				slots.Add (equipmentSlots.transform.GetChild (i).GetComponent<Image> ());
				slots[i].name = i.ToString ();
			} else {
				slots.Add (equipmentSlots.transform.GetChild (10).GetComponent<Image> ());
				slots[i].name = i.ToString ();              
			}

		}

        //SetSizeSlots();

		LoadEquipment ();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Open Equipment");
            OpenEquipment();
        }
    }

    public void OpenEquipment()
    {
        equipment.SetActive(true);
    }

    public void SetSizeSlots()
    {
        float x, y;
       /* x = equipmentSlots.transform.GetChild(11).GetComponent<RectTransform>().sizeDelta.x;
        x = slots[11].GetComponent<Renderer>().bounds.size.x;
        y = slots[11].GetComponent<Renderer>().bounds.size.y;
        y = equipmentSlots.transform.GetChild(11).GetComponent<RectTransform>().sizeDelta.y;
        float size = y;

        if (x < y) { size = x; }
        
        //panelJoystick.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * 0.3f, Screen.height * 0.3f);
        Debug.LogWarning(x + "\n" + y);

        

        for (int i=0; i< slots.Count; i++)
        {
            slots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        }
        */
    }
    
    public void SetImage(int i, Sprite icon, float color)
    {
        float x = color;
        if (color == 0) { x = .5f; }
        slots[i].transform.GetComponentInChildren<Image>().sprite = icon;
        slots[i].transform.GetComponentInChildren<Image>().color = new Color(color, color, color, x);
    }

	public void LoadEquipment ()
    {
        SetSlotSelect(null);

        for (int i = 0; i < equippedItems.Count; i++) {
			if (equippedItems[i] == null) {
                SetImage(i, defaultEquipments[i], 0);
			} else {
                SetImage(i, equippedItems[i].itemIcon, 1);
			}
		}
	}

	public void ClearSlot (int i) {
		equippedItems[i] = null;
        SetImage(i, defaultEquipments[i], 0);
    }

	public bool AlreadyEquipped (Item newItem) {
		int index = newItem.gearMainType.GetHashCode ();
		if(equippedItems[index] != null){
			return true;
		}
		return false;
	}

	public void Equip (Item newItem) {
		int index = newItem.gearMainType.GetHashCode ();

        //ADICIONA OS ATRIBUTOS DO ITEM
		/*for (int i = 0; i < newItem.Attributes.Count; i++) {
			playerStats.AddAttribute (newItem.Attributes[i], newItem.Attributes[i].Value);
		}

		if (equippedItems[index] != null) {
			Unequip (newItem.gearMainType.GetHashCode ());
		}

		equippedItems[index] = newItem;
		playerCombat.AttStatsCombat ();*/
        SetImage(index, newItem.itemIcon, 1);
	}

	public void Unequip (int i) {
		if (BagManager.CountItems (inventoryManager.inventoryItems) < inventoryManager.slotsBag) {
			RemoveItem (i);
			inventoryManager.CollectItem (equippedItems[i]);
			inventoryManager.LoadInventory ();

			ClearSlot (i);
		}
		itemDesc.Clear ();
        SetSlotSelect(null);
    }

	public void RemoveItem (int i) {
		for (int j = 0; j < equippedItems[i].Attributes.Count; j++) {
            //REMOVE OS ATRIBUTOS DO ITEM
			//playerStats.AddAttribute (equippedItems[i].Attributes[j], -equippedItems[i].Attributes[j].Value);
		}
	}

	public void BtnSelect (GameObject slotsUnequip) {
		int i = int.Parse (slotsUnequip.name);
        Select(i);
	}

    public void Select(int i)
    {
        if (equippedItems[i] != null)
        {
            itemDesc.gameObject.SetActive(true);
            itemDesc.SetItem(equippedItems[i], i, Unequip, DiscartItem, "Unequip", "Discart");
        }
    }

    public void SetSlotSelect(GameObject newSlot)
    {
        if(slotSelected != null)
        {
            slotSelected.transform.GetChild(0).gameObject.SetActive(false);
        }
        slotSelected = newSlot;
        itemDesc.Clear();
        if (newSlot != null)
        {
            int i = int.Parse(slotSelected.name);
            if (equippedItems[i] != null)
            {
                if (inventoryManager.slotSelected != null)
                {
                    if (inventoryManager.inventoryItems[int.Parse(inventoryManager.slotSelected.name)].gearMainType.GetHashCode() != equippedItems[i].gearMainType.GetHashCode())
                    {
                        inventoryManager.SetSlotSelect(null);
                    }
                }

                slotSelected.transform.GetChild(0).gameObject.SetActive(true);
                BtnSelect(slotSelected);
            }
        }
    }

    public void DiscartItem (int i) {
		ItemDropped.DropItem (equippedItems[i], GameObject.FindGameObjectWithTag("Player").transform);

		RemoveItem (i);
		ClearSlot (i);
		itemDesc.Clear ();
        SetSlotSelect(null);
	}
}