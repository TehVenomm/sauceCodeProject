using System.Collections.Generic;
using UnityEngine;

public class GoGameSettingsManager : MonoBehaviourSingleton<GoGameSettingsManager>
{
	public List<string> itemsUseShopUI3 = new List<string>();

	public List<int> weaponsWhichPlayerUseItCanFly = new List<int>();

	public List<int> enemysCanFly = new List<int>();

	public Vector3 colliderOfMapScale = new Vector3(1f, 3.5f, 1f);

	public List<long> weaponLimitedNumAbilities = new List<long>();

	public int numAbilityCheck = 4;

	public int limitAbility = 3;

	public bool UseShopUI3(string spriteName)
	{
		if (string.IsNullOrEmpty(spriteName) || itemsUseShopUI3 == null || itemsUseShopUI3.Count <= 0)
		{
			return false;
		}
		return itemsUseShopUI3.Contains(spriteName);
	}
}
