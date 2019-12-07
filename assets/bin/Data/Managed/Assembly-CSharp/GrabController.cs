using System.Collections.Generic;
using UnityEngine;

public class GrabController
{
	private List<Player> grabbedPlayers = new List<Player>(4);

	private float duration;

	private float grabStartTime;

	public bool releaseByWeakHit
	{
		get;
		private set;
	}

	public bool releaseBySpWeakHit
	{
		get;
		private set;
	}

	public int releaseActionId
	{
		get;
		private set;
	}

	public DrainAttackInfo drainAtkInfo
	{
		get;
		private set;
	}

	public bool IsGrabing()
	{
		return grabbedPlayers.Count > 0;
	}

	public void AddGrabbedObject(Player player)
	{
		grabbedPlayers.Add(player);
	}

	public void ReleaseAll(float angle, float power)
	{
		for (int i = 0; i < grabbedPlayers.Count; i++)
		{
			if (!grabbedPlayers[i].isDead && grabbedPlayers[i].actionID == (Character.ACTION_ID)29)
			{
				grabbedPlayers[i].ActGrabbedEnd(angle, power);
			}
		}
		grabbedPlayers.Clear();
		releaseByWeakHit = false;
		releaseBySpWeakHit = false;
	}

	private void Grab(float _duration, Player _player, int releaseActId, bool _releaseByWeakHit, bool _releaseBySpWeakHit, DrainAttackInfo _drainAtkInfo)
	{
		if (!IsGrabing())
		{
			grabStartTime = Time.realtimeSinceStartup;
			duration = _duration;
			releaseActionId = releaseActId;
			releaseByWeakHit = _releaseByWeakHit;
			releaseBySpWeakHit = _releaseBySpWeakHit;
			drainAtkInfo = _drainAtkInfo;
		}
		AddGrabbedObject(_player);
	}

	public void Grab(Player _player, GrabInfo info, DrainAttackInfo _drainAtkInfo)
	{
		Grab(info.duration, _player, info.releaseAttackId, info.releaseByWeakHit, info.releaseByWeaponWeakHit, _drainAtkInfo);
	}

	public bool IsReadyForRelease()
	{
		if (IsGrabing())
		{
			return Time.realtimeSinceStartup - grabStartTime >= duration;
		}
		return false;
	}

	public bool IsAliveGrabbedPlayerAll()
	{
		if (grabbedPlayers == null)
		{
			return false;
		}
		for (int i = 0; i < grabbedPlayers.Count; i++)
		{
			if (grabbedPlayers[i] != null && !grabbedPlayers[i].isDead)
			{
				return true;
			}
		}
		return false;
	}
}
