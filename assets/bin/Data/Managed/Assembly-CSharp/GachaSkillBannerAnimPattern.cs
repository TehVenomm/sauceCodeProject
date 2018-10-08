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
		GachaList.GachaPickupAnim.TextStyle name = anim.name;
		SetFontStyle(lblName.transform, (name.italic == 1) ? FontStyle.Italic : FontStyle.Normal);
		SetPatternText(lblName, table.name, name.size, name.toColor(), name.toOutColor());
		GachaList.GachaPickupAnim.TextStyle description = anim.description;
		SetFontStyle(lblDescription.transform, (description.italic == 1) ? FontStyle.Italic : FontStyle.Normal);
		SetPatternText(lblDescription, SkillItemInfo.GetExplanationText(table, 1), description.size, description.toColor(), description.toOutColor());
		GachaList.GachaPickupAnim.TextStyle sub = anim.sub;
		SetFontStyle(lblSubDescription.transform, (sub.italic == 1) ? FontStyle.Italic : FontStyle.Normal);
		SetPatternText(lblSubDescription, sub.text, sub.size, sub.toColor(), sub.toOutColor());
		GachaList.GachaPickupAnim.TextStyle adda = anim.adda;
		SetFontStyle(lblAddText_A.transform, (adda.italic == 1) ? FontStyle.Italic : FontStyle.Normal);
		SetPatternText(lblAddText_A, adda.text, adda.size, adda.toColor(), adda.toOutColor());
		GachaList.GachaPickupAnim.TextStyle addb = anim.addb;
		SetFontStyle(lblAddText_B.transform, (addb.italic == 1) ? FontStyle.Italic : FontStyle.Normal);
		SetPatternText(lblAddText_B, addb.text, addb.size, addb.toColor(), addb.toOutColor());
		uiTex.mainTexture = tex;
		base.gameObject.SetActive(true);
	}

	public void Finish()
	{
		base.gameObject.SetActive(false);
		if ((UnityEngine.Object)uiTex.mainTexture != (UnityEngine.Object)null)
		{
			uiTex.mainTexture = null;
		}
	}

	public void AnimStart(bool is_entry, bool is_skip, EventDelegate.Callback end_callback)
	{
		nowEntryAnim = is_entry;
		Transform t = (!nowEntryAnim) ? base.transform : entryAnimCtrl.transform;
		ResetTween(t, 0);
		if (is_skip)
		{
			SkipTween(t, true, 0);
			end_callback?.Invoke();
			nowEntryAnim = false;
		}
		else
		{
			PlayTween(t, true, end_callback, false, 0);
		}
	}

	private void SetPatternText(UILabel lbl, string text, int size, Color text_color, Color out_line_color)
	{
		if ((UnityEngine.Object)lbl != (UnityEngine.Object)null)
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
			tw.enabled = false;
		});
		Array.ForEach(GetEntryTweenCtrl().tweens, delegate(UITweener tw)
		{
			tw.enabled = false;
		});
		ResetTween(base.transform, 0);
		ResetTween(entryAnimCtrl.transform, 0);
	}

	public UITweenCtrl GetDirectionTweenCtrl()
	{
		return GetComponent<UITweenCtrl>(base.transform);
	}

	public UITweenCtrl GetEntryTweenCtrl()
	{
		return GetComponent<UITweenCtrl>(entryAnimCtrl.transform);
	}
}
