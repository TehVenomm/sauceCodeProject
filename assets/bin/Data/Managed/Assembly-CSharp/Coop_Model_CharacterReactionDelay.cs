using System.Collections.Generic;

public class Coop_Model_CharacterReactionDelay : Coop_Model_ObjectSyncPositionBase
{
	public List<Character.DelayReactionInfo> reactionInfoList = new List<Character.DelayReactionInfo>();

	public Coop_Model_CharacterReactionDelay()
	{
		base.packetType = PACKET_TYPE.CHARACTER_REACTION_DELAY;
	}
}
