using UnityEngine;

public class FieldGimmickCandyWoodObject : FieldGimmickObject
{
	public enum STATE
	{
		None,
		Low,
		Middle,
		High
	}

	private const string ANIM_STATE_LOW = "CMN_candy01_low";

	private const string ANIM_STATE_MIDDLE = "CMN_candy01_middle";

	private const string ANIM_STATE_HIGH = "CMN_candy01_high";

	private readonly int LOW_ANIM_HASH = Animator.StringToHash("CMN_candy01_low");

	private readonly int MIDDLE_ANIM_HASH = Animator.StringToHash("CMN_candy01_middle");

	private readonly int HIGH_ANIM_HASH = Animator.StringToHash("CMN_candy01_high");

	private STATE state;

	public Transform transSwitching;

	private Animator anim;

	public override string GetObjectName()
	{
		return "CandyWood";
	}

	public override void Initialize(FieldMapTable.FieldGimmickPointTableData pointData)
	{
		base.Initialize(pointData);
		transSwitching = Utility.FindChild(modelTrans, "switching");
		anim = transSwitching.GetComponent<Animator>();
		SyncWoodStateAndView();
	}

	public override void OnNotify(object value)
	{
		GrowthGatherPointObject.GrowthInfo growthInfo = value as GrowthGatherPointObject.GrowthInfo;
		if (growthInfo != null)
		{
			STATE sTATE = STATE.None;
			sTATE = ((growthInfo.current == 0) ? STATE.Low : ((growthInfo.current != growthInfo.max) ? STATE.Middle : STATE.High));
			if (sTATE != state)
			{
				state = sTATE;
				SyncWoodStateAndView();
			}
		}
	}

	private void SyncWoodStateAndView()
	{
		int num = 0;
		switch (state)
		{
		case STATE.None:
		case STATE.Low:
			num = LOW_ANIM_HASH;
			break;
		case STATE.Middle:
			num = MIDDLE_ANIM_HASH;
			break;
		case STATE.High:
			num = HIGH_ANIM_HASH;
			break;
		}
		if (anim.HasState(0, num))
		{
			anim.Play(num);
		}
	}
}
