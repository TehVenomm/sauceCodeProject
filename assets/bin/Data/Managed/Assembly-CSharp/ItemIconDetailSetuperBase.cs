using System;
using UnityEngine;

public class ItemIconDetailSetuperBase
{
	[SerializeField]
	private UISprite spBG;

	[SerializeField]
	private UILabel lblName;

	public GameObject[] infoRootAry;

	[SerializeField]
	private GameObject[] inActiveRootAry;

	public static readonly string[] SPR_SKILL_MATERIAL_NUMBER = new string[10]
	{
		"MagiSynthNum01",
		"MagiSynthNum02",
		"MagiSynthNum03",
		"MagiSynthNum04",
		"MagiSynthNum05",
		"MagiSynthNum06",
		"MagiSynthNum07",
		"MagiSynthNum08",
		"MagiSynthNum09",
		"MagiSynthNum10"
	};

	protected virtual UISprite selectSP => null;

	public ItemIconDetailSetuperBase()
		: this()
	{
	}

	public virtual void Set(object[] data = null)
	{
		if (inActiveRootAry != null && inActiveRootAry.Length > 0)
		{
			Array.ForEach(inActiveRootAry, delegate(GameObject obj)
			{
				//IL_000d: Unknown result type (might be due to invalid IL or missing references)
				if (obj != null)
				{
					obj.get_gameObject().SetActive(false);
				}
			});
		}
	}

	public void SetName(string text)
	{
		lblName.text = text;
	}

	public void SetVisibleBG(bool is_visible)
	{
		spBG.set_enabled(is_visible);
	}

	public void SetupSelectNumberSprite(int select_number)
	{
		if (!(selectSP == null))
		{
			int num = select_number - 1;
			bool flag = num >= 0 && num < SPR_SKILL_MATERIAL_NUMBER.Length;
			infoRootAry[0].SetActive(flag);
			if (flag)
			{
				selectSP.spriteName = SPR_SKILL_MATERIAL_NUMBER[num];
				selectSP.set_enabled(true);
			}
		}
	}
}
