using Network;
using System;
using System.Collections.Generic;

public class FriendTestFollow : GameSection
{
	private string follow_user_id = string.Empty;

	private string follow_user_id_2 = string.Empty;

	private string follow_user_id_3 = string.Empty;

	public override void Initialize()
	{
		base.Initialize();
	}

	private unsafe void OnQuery_SEND()
	{
		if (!string.IsNullOrEmpty(follow_user_id) || !string.IsNullOrEmpty(follow_user_id_2) || !string.IsNullOrEmpty(follow_user_id_3))
		{
			List<int> list = new List<int>();
			if (int.TryParse(follow_user_id, out int result))
			{
				list.Add(result);
			}
			if (int.TryParse(follow_user_id_2, out result))
			{
				list.Add(result);
			}
			if (int.TryParse(follow_user_id_3, out result))
			{
				list.Add(result);
			}
			if (list.Count != 0)
			{
				MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, new Action<Error, List<int>>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
		}
	}
}
