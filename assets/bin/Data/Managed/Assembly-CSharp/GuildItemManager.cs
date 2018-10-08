using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildItemManager
{
	private static GuildItemManager _instance;

	private readonly List<GuildItemInfoModel.EmblemInfo> _emblemInfos = new List<GuildItemInfoModel.EmblemInfo>();

	private bool _init;

	public static GuildItemManager I => _instance ?? (_instance = new GuildItemManager());

	public List<GuildItemInfoModel.EmblemInfo> GetEmblemInfos()
	{
		return _emblemInfos;
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer1Infos()
	{
		return _emblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 0).ToArray();
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer2Infos()
	{
		return _emblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 1).ToArray();
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer3Infos()
	{
		return _emblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 2).ToArray();
	}

	public string GetItemSprite(int id)
	{
		GuildItemInfoModel.EmblemInfo emblemInfo = _emblemInfos.Find((GuildItemInfoModel.EmblemInfo x) => x.id == id);
		if (emblemInfo == null)
		{
			return string.Empty;
		}
		return emblemInfo.image;
	}

	public int[] RandomEmblem(bool isFree)
	{
		int[] array = new int[3];
		if (isFree)
		{
			GuildItemInfoModel.EmblemInfo[] emblemLayer1Infos = GetEmblemLayer1Infos();
			emblemLayer1Infos = Array.FindAll(emblemLayer1Infos, (GuildItemInfoModel.EmblemInfo x) => x.price == 0);
			array[0] = emblemLayer1Infos[Random.Range(0, emblemLayer1Infos.Length)].id;
			emblemLayer1Infos = GetEmblemLayer2Infos();
			emblemLayer1Infos = Array.FindAll(emblemLayer1Infos, (GuildItemInfoModel.EmblemInfo x) => x.price == 0);
			array[1] = emblemLayer1Infos[Random.Range(0, emblemLayer1Infos.Length)].id;
			emblemLayer1Infos = GetEmblemLayer3Infos();
			emblemLayer1Infos = Array.FindAll(emblemLayer1Infos, (GuildItemInfoModel.EmblemInfo x) => x.price == 0);
			array[2] = emblemLayer1Infos[Random.Range(0, emblemLayer1Infos.Length)].id;
		}
		return array;
	}

	public void Init(Action callback)
	{
		if (_init)
		{
			if (callback != null)
			{
				callback.Invoke();
			}
		}
		else
		{
			_emblemInfos.Clear();
			GuildItemInfoModel.RequestAllItemInfo postData = new GuildItemInfoModel.RequestAllItemInfo();
			Protocol.SendAsync(GuildItemInfoModel.RequestAllItemInfo.path, postData, delegate(GuildItemInfoModel ret)
			{
				if (ret.Error == Error.None)
				{
					foreach (GuildItemInfoModel.EmblemInfo item in ret.result.emblem)
					{
						_emblemInfos.Add(item);
					}
				}
				_init = true;
				if (callback != null)
				{
					callback.Invoke();
				}
			}, string.Empty);
		}
	}
}
