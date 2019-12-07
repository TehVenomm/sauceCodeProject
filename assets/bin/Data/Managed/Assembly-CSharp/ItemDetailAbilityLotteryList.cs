using Network;
using System.Collections;
using System.Collections.Generic;

public class ItemDetailAbilityLotteryList : SmithAbilityChangeLotteryList
{
	protected override IEnumerator DoInitialize()
	{
		bool wait = true;
		CreateEquipItemTable.CreateEquipItemData createEquipItemData = GameSection.GetEventData() as CreateEquipItemTable.CreateEquipItemData;
		MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(createEquipItemData.id, delegate(Error error, List<SmithGetAbilityListForCreateModel.Param> list)
		{
			wait = false;
			SetAbilities(list);
		});
		while (wait)
		{
			yield return null;
		}
		InitializeBase();
	}
}
