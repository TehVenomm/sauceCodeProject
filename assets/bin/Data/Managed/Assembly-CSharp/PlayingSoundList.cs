using System.Collections.Generic;
using UnityEngine;

public class PlayingSoundList
{
	private const int HIGHEST_PRIORITY = 0;

	private const int LOWEST_PRIORITY = 255;

	private const int MAX_PRIORITY_NUM = 256;

	private const float DEFAULT_INTERVAL = 0.002f;

	private Dictionary<int, List<AudioObject>> playingObjects = new Dictionary<int, List<AudioObject>>();

	private List<AudioObject>[] priorityList = new List<AudioObject>[256];

	public int playingSENum
	{
		get;
		private set;
	}

	public PlayingSoundList()
	{
		for (int i = 0; i < 256; i++)
		{
			priorityList[i] = new List<AudioObject>(20);
		}
	}

	public void AddSE(AudioObject so)
	{
		if (!playingObjects.ContainsKey(so.clipId))
		{
			playingObjects.Add(so.clipId, new List<AudioObject>(20));
		}
		playingObjects[so.clipId].Add(so);
		playingSENum++;
	}

	public void RemoveSE(AudioObject so)
	{
		playingObjects[so.clipId].Remove(so);
		playingSENum--;
	}

	public bool canPlay(int clip_id)
	{
		SETable.Data seData = Singleton<SETable>.I.GetSeData((uint)clip_id);
		float num = 0.002f;
		if (seData != null)
		{
			num = seData.intervalLimit;
		}
		float time = Time.get_time();
		if (playingObjects.ContainsKey(clip_id))
		{
			int count = playingObjects[clip_id].Count;
			for (int i = 0; i < count; i++)
			{
				if (Mathf.Abs(playingObjects[clip_id][i].timeAtPlay - time) < num)
				{
					return false;
				}
			}
		}
		return true;
	}
}
