using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBagLoot : MonoBehaviour
{

	public BagLootManager bagLoot;

    private int i;

	public List<Item> items;
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DestroyBag());
        i = items.Count;
	}


	// Update is called once per frame
	void Update()
	{

	}

	public void OpenBagLootManager()
	{
		Debug.Log(" OpenBagLootManager()");
		bagLoot.SetBagLoot(items, this);

		bagLoot.ActiveBagLoot();
	}

    public int Collect()
    {
        i--;
        Debug.Log(i + "\n" + items.Count);
        if(i <= 0) { Destroy(gameObject); }
        return i;
    }

	public IEnumerator DestroyBag()
	{
		yield return new WaitForSeconds(30);
		Destroy(gameObject);	}
}
