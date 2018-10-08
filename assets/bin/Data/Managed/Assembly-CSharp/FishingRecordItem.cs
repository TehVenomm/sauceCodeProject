using Network;
using UnityEngine;

public class FishingRecordItem : UIBehaviour
{
	private enum UI
	{
		SPR_STATE_UPDATE,
		SPR_STATE_NEW,
		LBL_LIST_NUMBER,
		LBL_LIST_NAME,
		LBL_RECORD_SIZE_VALUE,
		LBL_RECORD_NUM_VALUE,
		SPR_CROWN
	}

	private enum eValueState
	{
		None,
		New,
		Update
	}

	[SerializeField]
	private string[] crownSpriteName;

	[SerializeField]
	private Color[] crownTextColor;

	private GatherItemRecord record;

	private string prefsKey;

	private eValueState valueState;

	public void Setup(Transform t, GatherItemRecord rec)
	{
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		record = rec;
		prefsKey = "gik_" + record.listId.ToString();
		valueState = eValueState.None;
		if (PlayerPrefs.HasKey(prefsKey))
		{
			int @int = PlayerPrefs.GetInt(prefsKey);
			if (@int < record.maxValue)
			{
				valueState = eValueState.Update;
			}
		}
		else
		{
			valueState = eValueState.New;
		}
		SetActive(t, UI.SPR_STATE_UPDATE, valueState == eValueState.Update);
		SetActive(t, UI.SPR_STATE_NEW, valueState == eValueState.New);
		SetLabelText(t, UI.LBL_LIST_NUMBER, $"#{record.listId}");
		SetLabelText(t, UI.LBL_LIST_NAME, record.name);
		SetColor(t, UI.LBL_RECORD_SIZE_VALUE, crownTextColor[record.maxCrownType]);
		SetLabelText(t, UI.LBL_RECORD_SIZE_VALUE, $"{record.GetSizeString()}cm");
		SetLabelText(t, UI.LBL_RECORD_NUM_VALUE, $"{record.num:#,0}");
		if (record.GetCrownType() != 0)
		{
			SetSprite(t, UI.SPR_CROWN, crownSpriteName[record.maxCrownType]);
			SetActive(t, UI.SPR_CROWN, true);
		}
		else
		{
			SetActive(t, UI.SPR_CROWN, false);
		}
	}

	public bool SaveState()
	{
		if (valueState == eValueState.None)
		{
			return false;
		}
		PlayerPrefs.SetInt(prefsKey, record.maxValue);
		return true;
	}
}
