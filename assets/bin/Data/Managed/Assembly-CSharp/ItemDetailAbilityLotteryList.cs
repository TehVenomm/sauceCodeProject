using Network;
using System.Collections;
using System.Collections.Generic;

public class ItemDetailAbilityLotteryList : SmithAbilityChangeLotteryList
{
	protected override IEnumerator DoInitialize()
	{
		bool wait = true;
		CreateEquipItemTable.CreateEquipItemData createEquipItemTable = GameSection.GetEventData() as CreateEquipItemTable.CreateEquipItemData;
		MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(createEquipItemTable.id, delegate(Error error, List<SmithGetAbilityListForCreateModel.Param> list)
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
