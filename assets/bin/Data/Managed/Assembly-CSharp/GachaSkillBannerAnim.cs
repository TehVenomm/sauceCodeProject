using Network;
using UnityEngine;

public class GachaSkillBannerAnim : UIBehaviour
{
	private GachaSkillBannerAnimPattern[] pattern;

	private int targetIndex;

	public void Init(int anim_index, SkillItemTable.SkillItemData table, Texture tex, GachaList.GachaPickupAnim anim)
	{
		if (pattern == null)
		{
			pattern = this.GetComponentsInChildren<GachaSkillBannerAnimPattern>();
		}
		if (pattern == null || pattern.Length <= anim_index)
		{
			Log.Error("Skill Gacha Anim Pattern is not Found!");
			if (pattern != null)
			{
				Log.Error("index = " + anim_index);
			}
			return;
		}
		int i = 0;
		for (int num = pattern.Length; i < num; i++)
		{
			pattern[i].Finish();
		}
		GachaSkillBannerAnimPattern gachaSkillBannerAnimPattern = pattern[anim_index];
		targetIndex = anim_index;
		gachaSkillBannerAnimPattern.Init(targetIndex, table, tex, anim);
	}

	public void Entry(bool is_skip, EventDelegate.Callback end_callback)
	{
		if (pattern != null)
		{
			pattern[targetIndex].AnimStart(is_entry: true, is_skip, end_callback);
		}
	}

	public void WaitAndNextPickup(EventDelegate.Callback end_callback)
	{
		if (pattern != null)
		{
			pattern[targetIndex].AnimStart(is_entry: false, is_skip: false, end_callback);
		}
	}
}
