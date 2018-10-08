using Network;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemDetailAbilityLotteryList : SmithAbilityChangeLotteryList
{
	protected unsafe override IEnumerator DoInitialize()
	{
		bool wait = true;
		CreateEquipItemTable.CreateEquipItemData createEquipItemTable = GameSection.GetEventData() as CreateEquipItemTable.CreateEquipItemData;
		MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(createEquipItemTable.id, new Action<Error, List<SmithGetAbilityListForCreateModel.Param>>((object)/*Error near IL_0048: stateMachine*/, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		while (wait)
		{
			yield return (object)null;
		}
		InitializeBase();
	}
}
