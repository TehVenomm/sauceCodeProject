using System;

public class LoungeMoveNPC : HomeCharacterBase
{
	private NPCTable.NPCData npcData;

	private OutGameSettingsManager.HomeScene.NPC npcInfo;

	public void SetNPCData(NPCTable.NPCData data)
	{
		npcData = data;
	}

	public void SetNPCInfo(OutGameSettingsManager.HomeScene.NPC npcInfo)
	{
		this.npcInfo = npcInfo;
	}

	protected override ModelLoaderBase LoadModel()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		bool useSpecialModel = false;
		if (npcData.specialModelID > 0)
		{
			useSpecialModel = true;
		}
		return npcData.LoadModel(this.get_gameObject(), true, true, null, useSpecialModel);
	}

	protected unsafe override void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_01;
		string loopAnim = npcInfo.GetLoopAnim();
		if (!string.IsNullOrEmpty(loopAnim))
		{
			default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
		}
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, new Action<PlayerAnimCtrl, PLCA>((object)this, (IntPtr)(void*)/*OpCode not supported: LdVirtFtn*/), null, new Action<PlayerAnimCtrl, PLCA>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
	}

	protected override void InitCollider()
	{
	}
}
