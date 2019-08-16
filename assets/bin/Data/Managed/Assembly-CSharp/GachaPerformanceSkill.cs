using System;

public class GachaPerformanceSkill : GachaPerformanceBase, SkillGachaDirector.ISectionCommand
{
	public new enum UI
	{
		BTN_SKIP,
		OBJ_RARITY_ROOT,
		OBJ_RARITY_D,
		OBJ_RARITY_C,
		OBJ_RARITY_B,
		OBJ_RARITY_A,
		OBJ_RARITY_S,
		OBJ_RARITY_SS,
		OBJ_RARITY_SSS,
		OBJ_RARITY_LIGHT,
		OBJ_RARITY_TEXT_ROOT,
		TEX_MODEL,
		TEX_INNER_MODEL
	}

	protected override void OnOpen()
	{
		SetActive((Enum)UI.BTN_SKIP, is_visible: true);
		if (AnimationDirector.I != null)
		{
			(AnimationDirector.I as SkillGachaDirector).StartDirection(this);
		}
	}

	void SkillGachaDirector.ISectionCommand.OnShowSkillModel(uint skill_item_id)
	{
		SetRenderSkillItemModel((Enum)UI.TEX_MODEL, skill_item_id, rotation: false, light_rotation: false);
		SetRenderSkillItemSymbolModel((Enum)UI.TEX_INNER_MODEL, skill_item_id, rotation: false);
	}

	void SkillGachaDirector.ISectionCommand.OnHideSkillModel()
	{
		ClearRenderModel((Enum)UI.TEX_MODEL);
		ClearRenderModel((Enum)UI.TEX_INNER_MODEL);
	}

	void SkillGachaDirector.ISectionCommand.OnShowRarity(RARITY_TYPE rarity)
	{
		ShowRarity(rarity);
	}

	void SkillGachaDirector.ISectionCommand.OnHideRarity()
	{
		HideRarity();
	}

	void SkillGachaDirector.ISectionCommand.OnEnd()
	{
		End();
	}
}
