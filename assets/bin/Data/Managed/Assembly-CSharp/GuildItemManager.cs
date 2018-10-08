using Network;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GuildItemManager
{
	private static GuildItemManager _instance;

	private List<GuildItemInfoModel.EmblemInfo> mEmblemInfos = new List<GuildItemInfoModel.EmblemInfo>();

	public static GuildItemManager I
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GuildItemManager();
			}
			return _instance;
		}
	}

	public bool Inited
	{
		get;
		private set;
	}

	public List<GuildItemInfoModel.EmblemInfo> GetEmblemInfos()
	{
		return mEmblemInfos;
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer1Infos()
	{
		return mEmblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 0).ToArray();
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer2Infos()
	{
		return mEmblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 1).ToArray();
	}

	public GuildItemInfoModel.EmblemInfo[] GetEmblemLayer3Infos()
	{
		return mEmblemInfos.FindAll((GuildItemInfoModel.EmblemInfo x) => x.type == 2).ToArray();
	}

	public string GetItemSprite(int id)
	{
		GuildItemInfoModel.EmblemInfo emblemInfo = mEmblemInfos.Find((GuildItemInfoModel.EmblemInfo x) => x.id == id);
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
		Inited = false;
		mEmblemInfos.Clear();
		GuildItemInfoModel.RequestAllItemInfo post_data = new GuildItemInfoModel.RequestAllItemInfo();
		Protocol.Send(GuildItemInfoModel.RequestAllItemInfo.path, post_data, delegate(GuildItemInfoModel ret)
		{
			if (ret.Error == Error.None)
			{
				foreach (GuildItemInfoModel.EmblemInfo item in ret.result.emblem)
				{
					mEmblemInfos.Add(item);
				}
			}
			Inited = true;
			if (callback != null)
			{
				callback();
			}
		}, string.Empty);
	}
}
