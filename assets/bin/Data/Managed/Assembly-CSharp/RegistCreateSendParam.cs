public class RegistCreateSendParam
{
	public string d = string.Empty;

	public string fromCode = string.Empty;

	public string fromParam = string.Empty;

	public string fromAffiliate = string.Empty;

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
