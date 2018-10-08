using UnityEngine;

public class UserFromAttributeData
{
	public string fromCode;

	public string fromParam;

	public string fromAffiliate;

	public void printData()
	{
		Debug.Log((object)("fromCode: " + fromCode + ", fromParam: " + fromParam + ", fromAffiliate: " + fromAffiliate));
	}
}
