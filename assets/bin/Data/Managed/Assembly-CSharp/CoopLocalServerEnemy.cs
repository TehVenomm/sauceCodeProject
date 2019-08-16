using UnityEngine;

public class CoopLocalServerEnemy
{
	private CoopLocalServerEnemyPop owner;

	private float popTime;

	public int sid
	{
		get;
		private set;
	}

	public int popIndex => owner.popIndex;

	public uint enemyId => owner.data.enemyID;

	public bool bossFlag => owner.data.bossFlag;

	public bool bigMonsterFlag => owner.data.bigMonsterFlag;

	public CoopLocalServerEnemy(CoopLocalServerEnemyPop epop, float pop_time)
	{
		owner = epop;
		popTime = pop_time;
	}

	public bool IsPop()
	{
		return sid > 0;
	}

	public bool IsReady()
	{
		return popTime <= Time.get_time();
	}

	public void Pop(int sid)
	{
		this.sid = sid;
	}

	public void Out(float pop_time)
	{
		sid = 0;
		popTime = pop_time;
	}

	public override string ToString()
	{
		return "sid=" + sid + ",idx=" + popIndex + ",enemyId=" + enemyId + ",popTime=" + popTime + "/" + IsReady();
	}
}
