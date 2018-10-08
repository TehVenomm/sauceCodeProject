using UnityEngine;

public class QuestRoomSettingsOption : MonoBehaviour
{
	public Color texEnableColor;

	public Color disableColor;

	[Header("[0]通常時 : [1]グレ\u30fc表示時")]
	public string[] buttonSpriteName;

	[Header("[0]通常時 : [1]グレ\u30fc表示時")]
	public string[] frameSpriteName;

	public UILabel[] lbls;

	public UIButton btn;

	public UISprite spr;

	public UITexture tex;

	public void SetShowOption(bool is_enable)
	{
		int num = (lbls != null) ? lbls.Length : 0;
		if (is_enable)
		{
			int i = 0;
			for (int num2 = num; i < num2; i++)
			{
				if ((Object)lbls[i] != (Object)null)
				{
					lbls[i].color = Color.white;
				}
			}
			btn.GetComponent<BoxCollider>().enabled = true;
			btn.normalSprite = (btn.pressedSprite = buttonSpriteName[0]);
			spr.spriteName = frameSpriteName[0];
			tex.color = texEnableColor;
		}
		else
		{
			int j = 0;
			for (int num3 = num; j < num3; j++)
			{
				if ((Object)lbls[j] != (Object)null)
				{
					lbls[j].color = disableColor;
				}
			}
			btn.GetComponent<BoxCollider>().enabled = false;
			btn.normalSprite = (btn.pressedSprite = buttonSpriteName[1]);
			spr.spriteName = frameSpriteName[1];
			tex.color = disableColor;
		}
		btn.UpdateColor(true);
	}
}
