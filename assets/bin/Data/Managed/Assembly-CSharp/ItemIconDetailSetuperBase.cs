using System;
using UnityEngine;

public class ItemIconDetailSetuperBase : MonoBehaviour
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

	public virtual void Set(object[] data = null)
	{
		if (inActiveRootAry != null && inActiveRootAry.Length != 0)
		{
			Array.ForEach(inActiveRootAry, delegate(GameObject obj)
			{
				if (obj != null)
				{
					obj.gameObject.SetActive(value: false);
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
		spBG.enabled = is_visible;
	}

	public virtual void SetupSelectNumberSprite(int select_number)
	{
		if (!(selectSP == null))
		{
			int num = select_number - 1;
			bool flag = num >= 0 && num < SPR_SKILL_MATERIAL_NUMBER.Length;
			infoRootAry[0].SetActive(flag);
			if (flag)
			{
				selectSP.spriteName = SPR_SKILL_MATERIAL_NUMBER[num];
				selectSP.enabled = true;
			}
		}
	}
}
