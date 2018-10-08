using System.Collections.Generic;

public class GoGameSettingsManager : MonoBehaviourSingleton<GoGameSettingsManager>
{
	public List<string> itemsUseShopUI3 = new List<string>();

	public bool UseShopUI3(string spriteName)
	{
		if (string.IsNullOrEmpty(spriteName) || itemsUseShopUI3 == null || itemsUseShopUI3.Count <= 0)
		{
			return false;
		}
		return itemsUseShopUI3.Contains(spriteName);
	}
}
