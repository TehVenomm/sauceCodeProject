using Network;
using System;
using UnityEngine;

public class GachaSkillBannerAnimPattern : UIBehaviour
{
	[SerializeField]
	private UILabel lblName;

	[SerializeField]
	private UILabel lblDescription;

	[SerializeField]
	private UILabel lblSubDescription;

	[SerializeField]
	private UILabel lblAddText_A;

	[SerializeField]
	private UILabel lblAddText_B;

	[SerializeField]
	private UITexture uiTex;

	[SerializeField]
	private UITweenCtrl entryAnimCtrl;

	private bool nowEntryAnim;

	public void Init(int index, SkillItemTable.SkillItemData table, Texture tex, GachaList.GachaPickupAnim anim)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		GachaList.GachaPickupAnim.TextStyle name = anim.name;
		SetFontStyle(lblName.get_transform(), (name.italic == 1) ? 2 : 0);
		SetPatternText(lblName, table.name, name.size, name.toColor(), name.toOutColor());
		GachaList.GachaPickupAnim.TextStyle description = anim.description;
		SetFontStyle(lblDescription.get_transform(), (description.italic == 1) ? 2 : 0);
		SetPatternText(lblDescription, SkillItemInfo.GetExplanationText(table, 1, 0), description.size, description.toColor(), description.toOutColor());
		GachaList.GachaPickupAnim.TextStyle sub = anim.sub;
		SetFontStyle(lblSubDescription.get_transform(), (sub.italic == 1) ? 2 : 0);
		SetPatternText(lblSubDescription, sub.text, sub.size, sub.toColor(), sub.toOutColor());
		GachaList.GachaPickupAnim.TextStyle adda = anim.adda;
		SetFontStyle(lblAddText_A.get_transform(), (adda.italic == 1) ? 2 : 0);
		SetPatternText(lblAddText_A, adda.text, adda.size, adda.toColor(), adda.toOutColor());
		GachaList.GachaPickupAnim.TextStyle addb = anim.addb;
		SetFontStyle(lblAddText_B.get_transform(), (addb.italic == 1) ? 2 : 0);
		SetPatternText(lblAddText_B, addb.text, addb.size, addb.toColor(), addb.toOutColor());
		uiTex.mainTexture = tex;
		this.get_gameObject().SetActive(true);
	}

	public void Finish()
	{
		this.get_gameObject().SetActive(false);
		if (uiTex.mainTexture != null)
		{
			uiTex.mainTexture = null;
		}
	}

	public void AnimStart(bool is_entry, bool is_skip, EventDelegate.Callback end_callback)
	{
		nowEntryAnim = is_entry;
		Transform t = (!nowEntryAnim) ? this.get_transform() : entryAnimCtrl.get_transform();
		ResetTween(t);
		if (is_skip)
		{
			SkipTween(t);
			end_callback?.Invoke();
			nowEntryAnim = false;
		}
		else
		{
			PlayTween(t, forward: true, end_callback, is_input_block: false);
		}
	}

	private void SetPatternText(UILabel lbl, string text, int size, Color text_color, Color out_line_color)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (lbl != null)
		{
			lbl.text = text;
			lbl.color = text_color;
			lbl.effectColor = out_line_color;
			lbl.fontSize = size;
		}
	}

	public void DebugAnimReset()
	{
		Array.ForEach(GetDirectionTweenCtrl().tweens, delegate(UITweener tw)
		{
			tw.set_enabled(false);
		});
		Array.ForEach(GetEntryTweenCtrl().tweens, delegate(UITweener tw)
		{
			tw.set_enabled(false);
		});
		ResetTween(this.get_transform());
		ResetTween(entryAnimCtrl.get_transform());
	}

	public UITweenCtrl GetDirectionTweenCtrl()
	{
		return base.GetComponent<UITweenCtrl>(this.get_transform());
	}

	public UITweenCtrl GetEntryTweenCtrl()
	{
		return base.GetComponent<UITweenCtrl>(entryAnimCtrl.get_transform());
	}
}
