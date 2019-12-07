using System;

public class CoopClientCollector
{
	public const int MAX_CLIENT = 8;

	private CoopClient[] clientList = new CoopClient[8];

	public CoopClient GetAt(int idx)
	{
		if (idx < 0 || idx >= 8)
		{
			return null;
		}
		return clientList[idx];
	}

	public void Add(CoopClient client)
	{
		int num = 0;
		int num2 = clientList.Length;
		while (true)
		{
			if (num < num2)
			{
				if (clientList[num] == null)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		clientList[num] = client;
	}

	public void Remove(CoopClient client)
	{
		int num = 0;
		int num2 = clientList.Length;
		while (true)
		{
			if (num < num2)
			{
				if (clientList[num] == client)
				{
					break;
				}
				num++;
				continue;
			}
			return;
		}
		clientList[num] = null;
	}

	public void Clear()
	{
		int i = 0;
		for (int num = clientList.Length; i < num; i++)
		{
			clientList[i] = null;
		}
	}

	public CoopClient Find(Predicate<CoopClient> predicate)
	{
		int i = 0;
		for (int num = clientList.Length; i < num; i++)
		{
			CoopClient coopClient = clientList[i];
			if (!(coopClient == null) && predicate(coopClient))
			{
				return coopClient;
			}
		}
		return null;
	}

	public int IndexOf(CoopClient client)
	{
		int i = 0;
		for (int num = clientList.Length; i < num; i++)
		{
			if (clientList[i].userId == client.userId)
			{
				return i;
			}
		}
		return -1;
	}

	public void ForEach(Action<CoopClient> action)
	{
		int i = 0;
		for (int num = clientList.Length; i < num; i++)
		{
			CoopClient coopClient = clientList[i];
			if (!(coopClient == null))
			{
				action(coopClient);
			}
		}
	}

	public Player FindPlayer(Predicate<Player> predicate)
	{
		int i = 0;
		for (int num = clientList.Length; i < num; i++)
		{
			CoopClient coopClient = clientList[i];
			if (!(coopClient == null))
			{
				Player player = coopClient.GetPlayer();
				if (player != null && predicate(player))
				{
					return player;
				}
			}
		}
		return null;
	}

	public CoopClient FindStageHost(int stage_id)
	{
		return Find((CoopClient c) => c.stageId == stage_id && c.isStageHost);
	}

	public CoopClient FindPartyOwner()
	{
		return Find((CoopClient c) => c.isPartyOwner);
	}

	public CoopClient FindByClientId(int client_id)
	{
		for (int i = 0; i < clientList.Length; i++)
		{
			if (!(clientList[i] == null))
			{
				CoopClient coopClient = clientList[i];
				if (coopClient.clientId == client_id)
				{
					return coopClient;
				}
			}
		}
		return null;
	}

	public CoopClient FindByToken(string token)
	{
		return Find((CoopClient c) => c.userToken == token);
	}

	public CoopClient FindByPlayerId(int player_id)
	{
		return Find((CoopClient c) => c.playerId == player_id);
	}

	public CoopClient FindByUserId(int user_id)
	{
		return Find((CoopClient c) => c.userId == user_id);
	}

	public bool HasSeriesProgress()
	{
		return Find(delegate(CoopClient c)
		{
			if (c.isLeave)
			{
				return false;
			}
			if (c.IsBattleEnd())
			{
				return false;
			}
			if (c.isBattleRetire)
			{
				return false;
			}
			return c.IsStageRequest() && !c.isSeriesProgressEnd;
		}) != null;
	}

	public bool HasLoadingPlayer()
	{
		return FindPlayer((Player p) => p.isLoading) != null;
	}
}
