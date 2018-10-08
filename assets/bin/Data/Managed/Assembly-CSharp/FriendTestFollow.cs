using Network;
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

	private void OnQuery_SEND()
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
				MonoBehaviourSingleton<FriendManager>.I.SendFollowUser(list, delegate(Error err, List<int> follow_list)
				{
					if (err == Error.None)
					{
						if (follow_list.Count > 0)
						{
							string names = string.Empty;
							follow_list.ForEach(delegate(int user_id)
							{
								string text = names;
								names = text + "「" + user_id + "」\n";
							});
							DispatchEvent("SUCCESS", names + "をフォロ\u30fcしました");
						}
					}
					else
					{
						DispatchEvent("SUCCESS", "エラ\u30fcが発生しました");
					}
				});
			}
		}
	}
}
