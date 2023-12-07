using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{

	public static int CountItems(List<Item> list)
	{
		int count = 0;
		for (int i = 0; i < list.Count; i++) 
		{ 
			if (list[i] != null) { count++; }
		}
		return count;
	}

	public static int CountNullItems(List<Item> list)
	{
		int count = 0;
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null) { count++; }
		}
		return count;	}

	public static Item FindItemAt(int index, List<Item> list)
	{
		int j = 0;
		for (int i = 0; i < list.Count && i < index; i++)
		{
			if (j >= index && list[i] !=null ) { return list[i]; }

			if (list[i] == null) { j++; }
		}
		return null;
	}

	public static int FindNullPosition(List<Item> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i] == null)
			{
				return i;
			}
		}
		return 999999;
	}
}
