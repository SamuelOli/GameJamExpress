using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ()]
public class Attribute : ScriptableObject {
	public string AttributeName;
	[Header ("Example +50% STR >> +#VALUE#% STR")]
	public string AttributeDisplay; //Example +50% STR >> +#VALUE#% STR so the code replace #VALUE# with the value.
}

[System.Serializable]
public class BasicAttribute {
	public Attribute MyAttribute;
	public float Value;
	public float ValueMultiplier = 1;

	public float GetValue () {
		return Value * ValueMultiplier;
	}

	public BasicAttribute (Attribute myAttribute, float value, float valueMultiplier) {
		MyAttribute = myAttribute;
		Value = value;
		ValueMultiplier = valueMultiplier;
	}
	public BasicAttribute (BasicAttribute attribute) {
		MyAttribute = attribute.MyAttribute;
		Value = attribute.Value;
		ValueMultiplier = attribute.ValueMultiplier;
	}
}

[System.Serializable]
public class ListBasicAttribute {
	public List<BasicAttribute> listAttributes;

	public float GetBasicAttribute (string name) {
		int i = 0;
		for (i = 0; i < listAttributes.Count; i++) {
			if (listAttributes[i].MyAttribute.AttributeName.Equals (name)) {
				return listAttributes[i].Value;
			}
		}
		return 0;
	}
}