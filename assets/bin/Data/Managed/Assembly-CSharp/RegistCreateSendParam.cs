public class RegistCreateSendParam
{
	public string d = "";

	public string fromCode = "";

	public string fromParam = "";

	public string fromAffiliate = "";

	public void SetAttribute(UserFromAttributeData data)
	{
		if (data != null)
		{
			fromCode = data.fromCode;
			fromParam = data.fromParam;
			fromAffiliate = data.fromAffiliate;
		}
	}
}
