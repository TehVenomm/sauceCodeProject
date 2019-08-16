using System;

public class QuestAcceptChangeEquipSet : QuestChangeEquipSet
{
	public override void Initialize()
	{
		base.Initialize();
	}

	protected void OnQuery_QuestAcceptRoomInvalid_EquipChange_OK()
	{
		OnQuery_QuestRoomInvalid_EquipChange_OK();
	}

	private new void OnEnable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Combine(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
	}

	private new void OnDisable()
	{
		InputManager.OnDragAlways = (InputManager.OnTouchDelegate)Delegate.Remove(InputManager.OnDragAlways, new InputManager.OnTouchDelegate(OnDrag));
		nowSectionName = string.Empty;
	}

	private void OnDrag(InputManager.TouchInfo touch_info)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (!(loader == null) && !MonoBehaviourSingleton<UIManager>.I.IsDisable() && CanRotateSection())
		{
			loader.get_transform().Rotate(GameDefine.GetCharaRotateVector(touch_info));
		}
	}

	private bool CanRotateSection()
	{
		string currentSectionName = MonoBehaviourSingleton<GameSceneManager>.I.GetCurrentSectionName();
		if (currentSectionName != null && currentSectionName == "QuestAcceptChangeEquipSet")
		{
			return true;
		}
		return false;
	}
}
