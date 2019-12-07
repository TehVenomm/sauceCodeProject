using Network;

public class Coop_Model_StagePlayerPop : Coop_Model_Base
{
	public int sid;

	public bool isSelf;

	public CharaInfo charaInfo;

	public StageObjectManager.CreatePlayerInfo.ExtentionInfo extentionInfo;

	public StageObjectManager.PlayerTransferInfo transferInfo;

	public Coop_Model_StagePlayerPop()
	{
		base.packetType = PACKET_TYPE.STAGE_PLAYER_POP;
	}

	public override string ToString()
	{
		string arg = "";
		arg = arg + ",sid=" + sid;
		arg = arg + ",isSelf=" + isSelf.ToString();
		arg = arg + ",charaInfo=" + charaInfo;
		arg = arg + ",extentionInfo=" + extentionInfo;
		arg = arg + ",transferInfo=" + transferInfo;
		return base.ToString() + arg;
	}
}
