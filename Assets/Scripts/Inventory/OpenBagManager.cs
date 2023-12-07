using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBagManager : MonoBehaviour
{
    private InventoryManager inventory;
    private EquipmentManager equipment;

    public GameObject background;

    public GameObject Dica;

    // Start is called before the first frame update
    void Start()
    {
        inventory = gameObject.GetComponent<InventoryManager>();
        equipment = gameObject.GetComponent<EquipmentManager>();
        Dica.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Open();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Open()
    {
        background.SetActive(!background.active);
        inventory.StartInventory();
        equipment.LoadEquipment();
    }

    public void Close()
    {
        background.SetActive(false);
    }
}
