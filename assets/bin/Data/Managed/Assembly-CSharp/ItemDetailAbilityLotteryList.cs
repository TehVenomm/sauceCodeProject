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
			((_003CDoInitialize_003Ec__IteratorC5)/*Error near IL_0048: stateMachine*/)._003Cwait_003E__0 = false;
			((_003CDoInitialize_003Ec__IteratorC5)/*Error near IL_0048: stateMachine*/)._003C_003Ef__this.SetAbilities(list);
		});
		while (wait)
		{
			yield return (object)null;
		}
		InitializeBase();
	}
}
