using Network;
using System;
using System.Collections.Generic;

public class BasicGatherPointObject : GatherPointObject
{
	public unsafe override void Gather()
	{
		if (CoopWebSocketSingleton<KtbWebSocket>.IsValidConnected())
		{
			base.isGathered = true;
			UpdateView();
			FieldManager i = MonoBehaviourSingleton<FieldManager>.I;
			uint pointID = base.pointData.pointID;
			if (_003C_003Ef__am_0024cache0 == null)
			{
				_003C_003Ef__am_0024cache0 = new Action<bool, FieldGatherRewardList>((object)null, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
			}
			i.SendFieldGather((int)pointID, _003C_003Ef__am_0024cache0);
		}
	}

	public override void CheckGather()
	{
		base.isGathered = true;
		List<int> currentFieldPointIdList = MonoBehaviourSingleton<FieldManager>.I.currentFieldPointIdList;
		if (currentFieldPointIdList != null)
		{
			int i = 0;
			for (int count = currentFieldPointIdList.Count; i < count; i++)
			{
				if (base.pointData.pointID == currentFieldPointIdList[i])
				{
					base.isGathered = false;
					break;
				}
			}
		}
		base.CheckGather();
	}

	public override void UpdateView()
	{
		base.UpdateView();
		if (gimmick != null)
		{
			gimmick.OnNotify(base.isGathered);
		}
	}
}
