using System;
using System.Text;
using UnityEngine;

public class SmithGrowSkillConfirm : GameSection
{
	private enum UI
	{
		LBL_NAME,
		LBL_MONEY,
		GRD_MATERIAL,
		STR_TITLE_R,
		PNL_MATERIAL_INFO,
		LBL_MATERIAL_2,
		OBJ_MONEY
	}

	private SkillItemInfo baseSkill;

	private SkillItemInfo[] material;

	private int total;

	private bool isRareConfirm;

	private bool isEquipConfirm;

	private bool isExceedConfirm;

	private bool isExceed;

	public override string overrideBackKeyEvent => "CANCEL";

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		baseSkill = (array[0] as SkillItemInfo);
		SkillItemInfo[] array2 = array[1] as SkillItemInfo[];
		material = new SkillItemInfo[array2.Length];
		int i = 0;
		for (int num = array2.Length; i < num; i++)
		{
			material[i] = array2[i];
		}
		total = (int)(baseSkill.growCost * (float)material.Length);
		isEquipConfirm = false;
		isRareConfirm = false;
		int j = 0;
		for (int num2 = material.Length; j < num2; j++)
		{
			if (isRareConfirm && isEquipConfirm && isExceedConfirm)
			{
				break;
			}
			if (!isRareConfirm && GameDefine.IsRare(material[j].tableData.rarity))
			{
				isRareConfirm = true;
			}
			if (!isEquipConfirm && (material[j].isAttached || material[j].isUniqueAttached))
			{
				isEquipConfirm = true;
			}
			if (!isExceedConfirm && material[j].IsExceeded())
			{
				isExceedConfirm = true;
			}
		}
		Array.Sort(material, delegate(SkillItemInfo l, SkillItemInfo r)
		{
			ulong num3 = (ulong)(((long)r.tableData.rarity << 61) + (long)((ulong)(uint)(-1 - (int)r.tableData.id) << 16) + r.level);
			ulong num4 = (ulong)(((long)l.tableData.rarity << 61) + (long)((ulong)(uint)(-1 - (int)l.tableData.id) << 16) + l.level);
			int num5 = (num3 != num4) ? ((num3 > num4) ? 1 : (-1)) : 0;
			if (num5 == 0)
			{
				num5 = ((r.exp != l.exp) ? ((r.exp > l.exp) ? 1 : (-1)) : 0);
				if (num5 == 0)
				{
					num5 = ((r.uniqueID != l.uniqueID) ? ((r.uniqueID > l.uniqueID) ? 1 : (-1)) : 0);
				}
			}
			return num5;
		});
		isExceed = baseSkill.IsLevelMax();
		base.Initialize();
	}

	public override void UpdateUI()
	{
		SetLabelText((Enum)UI.STR_TITLE_R, base.sectionData.GetText("STR_TITLE"));
		SetLabelText((Enum)UI.LBL_NAME, baseSkill.tableData.name);
		SetLabelText((Enum)UI.LBL_MATERIAL_2, (!isExceed) ? base.sectionData.GetText("TEXT_GROW") : base.sectionData.GetText("TEXT_EXCEED"));
		SetActive((Enum)UI.OBJ_MONEY, !isExceed);
		if (!isExceed)
		{
			SetLabelText((Enum)UI.LBL_MONEY, total.ToString());
		}
		SetGrid(UI.GRD_MATERIAL, null, material.Length, reset: false, delegate(int i, Transform t, bool is_recycle)
		{
			SkillItemInfo skillItemInfo = material[i];
			ItemIcon itemIcon = ItemIcon.Create(ItemIcon.GetItemIconType(skillItemInfo.tableData.type), skillItemInfo.tableData.iconID, skillItemInfo.tableData.rarity, t, ELEMENT_TYPE.MAX, skillItemInfo.tableData.GetEnableEquipType(), -1, "DETAIL", i, is_new: false, -1, is_select: false, null, skillItemInfo.isAttached || skillItemInfo.isUniqueAttached);
			Transform ctrl = GetCtrl(UI.PNL_MATERIAL_INFO);
			SetMaterialInfo(itemIcon.transform, REWARD_TYPE.SKILL_ITEM, skillItemInfo.tableID, ctrl);
		});
	}

	private void OnQuery_DECISION()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (isRareConfirm || isEquipConfirm || isExceedConfirm)
		{
			stringBuilder.Append("[BB]");
			stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_CONFIRM"));
			if (isRareConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_RARE"));
			}
			if (isEquipConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_EQUIP"));
			}
			if (isExceedConfirm)
			{
				stringBuilder.AppendLine(base.sectionData.GetText("TEXT_INCLUDE_EXCEED"));
			}
			stringBuilder.AppendLine(string.Empty);
			stringBuilder.Append(base.sectionData.GetText("TEXT_GROW_CONFIRM"));
			GameSection.ChangeEvent("INCLUDE_RARE", stringBuilder.ToString());
		}
		else
		{
			GameSection.StayEvent();
			if (isExceed)
			{
				MonoBehaviourSingleton<SmithManager>.I.SendExceedSkill(baseSkill, material, delegate(SkillItemInfo ret_skill_item, bool isGreat)
				{
					if (ret_skill_item != null)
					{
						SmithManager.ResultData resultData2 = new SmithManager.ResultData
						{
							itemData = ret_skill_item,
							beforeRarity = (int)baseSkill.tableData.rarity,
							beforeMaxLevel = baseSkill.tableData.GetMaxLv(ret_skill_item.exceedCnt),
							beforeExceedCnt = baseSkill.exceedCnt,
							beforeLevel = baseSkill.level,
							beforeExp = baseSkill.exp,
							beforeAtk = baseSkill.atk,
							beforeDef = baseSkill.def,
							beforeHp = baseSkill.hp,
							beforeElemAtk = baseSkill.elemAtk,
							beforeElemDef = baseSkill.elemDef
						};
						GameSection.ChangeStayEvent("DECISION", new object[4]
						{
							resultData2,
							isGreat,
							material,
							isExceed
						});
						MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
						GameSection.ResumeEvent(is_resume: true);
					}
					else
					{
						GameSection.ResumeEvent(is_resume: false);
					}
				});
			}
			else
			{
				MonoBehaviourSingleton<SmithManager>.I.SendGrowSkill(baseSkill, material, delegate(SkillItemInfo ret_skill_item, bool is_great)
				{
					if (ret_skill_item != null)
					{
						SmithManager.ResultData resultData = new SmithManager.ResultData
						{
							itemData = ret_skill_item,
							beforeRarity = (int)baseSkill.tableData.rarity,
							beforeMaxLevel = baseSkill.tableData.GetMaxLv(ret_skill_item.exceedCnt),
							beforeExceedCnt = baseSkill.exceedCnt,
							beforeLevel = baseSkill.level,
							beforeExp = baseSkill.exp,
							beforeAtk = baseSkill.atk,
							beforeDef = baseSkill.def,
							beforeHp = baseSkill.hp,
							beforeElemAtk = baseSkill.elemAtk,
							beforeElemDef = baseSkill.elemDef
						};
						GameSection.ChangeStayEvent("DECISION", new object[4]
						{
							resultData,
							is_great,
							material,
							isExceed
						});
						MonoBehaviourSingleton<UIAnnounceBand>.I.isWait = true;
						GameSection.ResumeEvent(is_resume: true);
					}
					else
					{
						GameSection.ResumeEvent(is_resume: false);
					}
				});
			}
		}
	}

	private void OnQuery_DETAIL()
	{
		int num = (int)GameSection.GetEventData();
		GameSection.SetEventData(new object[2]
		{
			ItemDetailEquip.CURRENT_SECTION.SMITH_SKILL_MATERIAL,
			material[num]
		});
	}

	private void OnQuery_SmithMaterialIncludeRareConfirm_YES()
	{
		bool flag = isRareConfirm;
		bool flag2 = isEquipConfirm;
		bool flag3 = isExceedConfirm;
		isRareConfirm = false;
		isEquipConfirm = false;
		isExceedConfirm = false;
		OnQuery_DECISION();
		isRareConfirm = flag;
		isEquipConfirm = flag2;
		isExceedConfirm = flag3;
	}
}
