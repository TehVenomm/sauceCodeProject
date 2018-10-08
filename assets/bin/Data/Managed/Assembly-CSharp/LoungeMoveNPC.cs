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
		bool useSpecialModel = false;
		if (npcData.specialModelID > 0)
		{
			useSpecialModel = true;
		}
		return npcData.LoadModel(base.gameObject, true, true, null, useSpecialModel);
	}

	protected override void InitAnim()
	{
		PLCA default_anim = PLCA.IDLE_01;
		string loopAnim = npcInfo.GetLoopAnim();
		if (!string.IsNullOrEmpty(loopAnim))
		{
			default_anim = PlayerAnimCtrl.StringToEnum(loopAnim);
		}
		animCtrl = PlayerAnimCtrl.Get(animator, default_anim, OnAnimPlay, null, base.OnAnimEnd);
	}

	protected override void InitCollider()
	{
	}
}
