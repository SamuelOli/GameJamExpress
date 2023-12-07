using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour {
	public Item item;

	public Image icon;
	public Text txtName;
	public Text txtLvl;
	public Text txtDescription;
	public Text txtAttribute;

	public delegate void Action (int i);
	public Action btn1;
	public Action btn2;
	public int i;
	public Text txt1;
	public Text txt2;

	bool mouseOver = true;

	public ItemDescription itemEquippedDesc;

    private int linesDesc;
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SetItem (Item newItem, int index, Action newBtn1, Action newBtn2, string newtxt1, string newtxt2) {
		item = newItem;
		SetDrescription ();

		i = index;

		btn1 = newBtn1;
		btn2 = newBtn2;

		txt1.text = newtxt1;
		txt2.text = newtxt2;
	}

	public void SetItem (Item newItem, int index) {
		item = newItem;
		SetDrescription ();

		i = index;
	}

	public void SetDrescription () {
		if (item != null) {
            linesDesc = 0;

			icon.sprite = item.itemIcon;
			txtName.text = item.itemName;
			txtLvl.text = "lvl: " + item.LevelReq.ToString ();
			txtDescription.text = "Description: " + item.Description;

            txtAttribute.text = "";
			for (int i = 0; i < item.Attributes.Count; i++) {
				if (i != 0) { txtAttribute.text += "\n"; }
				txtAttribute.text += item.Attributes[i].MyAttribute.AttributeName + ": " + item.Attributes[i].Value;
			}

            SetDescriptionPosition();
            SetViwerPortSize();
        }
	}

    public void SetDescriptionPosition()
    {
        Canvas.ForceUpdateCanvases();

        float posX = txtLvl.transform.position.x;
        txtLvl.transform.position = new Vector3(posX, CalculateY(txtName), 0);
        txtAttribute.transform.position = new Vector3(posX, CalculateY(txtDescription), 0);
        linesDesc += txtAttribute.cachedTextGenerator.lineCount;
    }

    public float CalculateY(Text txt)
    {
        linesDesc += txt.cachedTextGenerator.lineCount;
        return txt.transform.position.y - 20 - (txt.cachedTextGenerator.lineCount * 28);
    }

    public void SetViwerPortSize()
    {
        float sizeY = (linesDesc * 30) + 100;
        RectTransform viwer = txtName.transform.parent.GetComponent<RectTransform>();
        viwer.sizeDelta = new Vector2(viwer.sizeDelta.x, sizeY);
    }

	public void Clear () {
		icon.sprite = null;
		txtName.text = null;
		txtLvl.text = null;
		txtDescription.text = null;
		txtAttribute.text = null;
		item = null;
		//btn1 = null;
		//btn2 = null;
		//txt1 = null;
		//txt2 = null;

		if(itemEquippedDesc != null){
			itemEquippedDesc.Clear();
		}
		gameObject.SetActive (false);
	}

	public void Button1 () {
		btn1 (i);
	}
	public void Button2 () {
		btn2 (i);
	}

	public void SetBtn1 (Action newEvent, string newTxt) {
		btn1 = newEvent;
		txt1.text = newTxt;
	}
	public void SetBtn2 (Action newEvent, string newTxt) {
		btn2 = newEvent;
		txt2.text = newTxt;
	}

}