using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public PlayerStats playerStats;


    public GameObject inventory;
    public GameObject inventorySlots;

    public Sprite inventoryDefault;
    public GameObject prefSlot;
    public List<Image> slots = new List<Image>();

    public List<Item> inventoryItems = new List<Item>();

    public int slotsBag = 0;

    EquipmentManager equipmentManager;

    public ItemDescription itemDesc;

    public GameObject slotSelected;

    //public ItemDescription itemEquippedDesc;

    // Start is called before the first frame update
    void Start()
    {
        if (slotsBag <= 20)
        {
            slotsBag = 20;
        }
        CreateSlot();
        equipmentManager = gameObject.GetComponent<EquipmentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("Open Inventory");
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        inventory.SetActive(true);
    }

    public void SetImage(int i, Sprite icon, float color)
    {
        float x = color;
        if (color == 0) { x = .5f; }
        //slots[i].transform.GetChild(0).GetComponent<Image>().sprite = icon;
        //slots[i].transform.GetChild(0).GetComponent<Image>().color = new Color(color, color, color, x);
        slots[i].transform.GetComponentInChildren<Image>().sprite = icon;
        slots[i].transform.GetComponentInChildren<Image>().color = new Color(color, color, color, x);
    }

    public void StartInventory()
    {
        if (slotsBag >= 20)
        {
            LoadInventory();
        }
        else
        {
            slotsBag = 20;
            CreateSlot();
            LoadInventory();
        }
    }

    public void CreateSlot()
    {
        for (int i = slots.Count; i < slotsBag; i++)
        {
            GameObject newSlot = Instantiate(prefSlot, inventorySlots.transform);
            newSlot.SetActive(true);
            slots.Add(newSlot.GetComponent<Image>());
            SetImage(i, inventoryDefault, 0);

            slots[i].name = i.ToString();
        }

        //float y = (slotsBag / 5 * 40) + 10;
        //inventory.GetComponent<RectTransform> ().sizeDelta = new Vector2 (210, y);
        LoadInventory();
    }
    public void AddSlot(int capacitySlots)
    {
        slotsBag += capacitySlots;
        CreateSlot();
    }

    public void LoadInventory()
    {
        SetSlotSelect(null);
        itemDesc.Clear();

        for (int i = inventoryItems.Count; i < slotsBag; i++) { inventoryItems.Add(null); }

        if (slotsBag >= 20)
        {
            for (int i = 0; i < slotsBag; i++)
            {
                if (inventoryItems[i] != null)
                {
                    SetImage(i, inventoryItems[i].itemIcon, 1);
                }
                else
                {
                    SetImage(i, inventoryDefault, 0);
                }
            }
        }
        else
        {
            inventory.SetActive(false);
        }
    }

    public void CollectItem(Item newItem)
    {
        if (BagManager.CountItems(inventoryItems) < slotsBag)
        {
            int i = BagManager.FindNullPosition(inventoryItems);
            inventoryItems[i] = newItem;
            LoadInventory();
        }
        else
        {
            Debug.LogError("Bag limit exceeded");
        }
    }

    public void UseItem(GameObject slotUsed)
    {
        int i = int.Parse(slotUsed.name);

        UseItem(i);
    }

    public void UseItem(int i)
    {
        if (inventoryItems[i] != null)
        {
            itemDesc.gameObject.SetActive(true);
            itemDesc.SetItem(inventoryItems[i], i, EquipItem, DiscartItem, "Equip", "Discart");
            if (equipmentManager.AlreadyEquipped(inventoryItems[i]))
            {
                equipmentManager.Select(inventoryItems[i].gearMainType.GetHashCode());
            }
        }
    }

    public void SetSlotSelect(GameObject newSlot)
    {
        if (slotSelected != null)
        {
            slotSelected.transform.GetChild(0).gameObject.SetActive(false);
        }
        slotSelected = newSlot;
        itemDesc.Clear();
        if (newSlot != null)
        {
            int i = int.Parse(slotSelected.name);
            if (inventoryItems[i] != null)
            {
                if (equipmentManager.AlreadyEquipped(inventoryItems[i]))
                {
                    equipmentManager.SetSlotSelect(equipmentManager.slots[inventoryItems[i].gearMainType.GetHashCode()].gameObject);
                }
                else
                {
                    equipmentManager.SetSlotSelect(null);
                }

                slotSelected.transform.GetChild(0).gameObject.SetActive(true);
                UseItem(slotSelected);
            }
        }
    }

    public void EquipItem(GameObject slotUsed)
    {
        int i = int.Parse(slotUsed.name);

        EquipItem(i);
    }
    public void EquipItem(int i)
    {
        if (inventoryItems[i] != null)
        {
            equipmentManager.Equip(inventoryItems[i]);
            inventoryItems.RemoveAt(i);
            LoadInventory();
        }
        itemDesc.Clear();
    }

    public void DiscartItem(int i)
    {
        Debug.Log("Drop");
        ItemDropped.DropItem(inventoryItems[i], GameObject.FindGameObjectWithTag("Player").transform);

        inventoryItems.RemoveAt(i);
        LoadInventory();
        itemDesc.Clear();
    }

    public void Close()
    {
        itemDesc.Clear();
        inventory.SetActive(false);
    }
}