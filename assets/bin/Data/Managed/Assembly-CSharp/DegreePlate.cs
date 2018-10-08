using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegreePlate
{
	private const int DEGREE_PART = 4;

	private const string PREFAB_NAME = "DegreePlate";

	private const string UNKNOWN_FRAME = "DF_UNKNOWN";

	[SerializeField]
	private UILabel mText;

	[SerializeField]
	private UITexture mFrame;

	private string mFrameName;

	public DegreePlate()
		: this()
	{
	}

	public static void Create(MonoBehaviour call_mono, List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(RESOURCE_CATEGORY.UI, "DegreePlate");
		if (cachedResourceObject != null)
		{
			DegreePlate component = (Object.Instantiate(cachedResourceObject.obj) as GameObject).GetComponent<DegreePlate>();
			component.Initialize(degreeIds, isButton, onFinish);
		}
		else
		{
			MonoBehaviourSingleton<ResourceManager>.I.Load(ResourceLoad.GetResourceLoad(call_mono, true), RESOURCE_CATEGORY.UI, "DegreePlate", delegate(ResourceManager.LoadRequest x, ResourceObject[] y)
			{
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				y[0].refCount++;
				DegreePlate component2 = (Object.Instantiate(y[0].obj) as GameObject).GetComponent<DegreePlate>();
				component2.Initialize(degreeIds, isButton, onFinish);
			}, null, false, null);
		}
	}

	public void Initialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(DoInitialize(degreeIds, isButton, onFinish));
	}

	private IEnumerator DoInitialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		if (degreeIds == null || degreeIds.Count != 4)
		{
			onFinish?.Invoke(null);
		}
		else
		{
			DegreeTable.DegreeData frameData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[0]);
			if (frameData == null || (frameData.type != DEGREE_TYPE.FRAME && frameData.type != DEGREE_TYPE.SPECIAL_FRAME))
			{
				onFinish?.Invoke(null);
			}
			else
			{
				string degreeText;
				if (frameData.type == DEGREE_TYPE.SPECIAL_FRAME)
				{
					degreeText = string.Empty;
				}
				else
				{
					DegreeTable.DegreeData prefixData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[1]);
					DegreeTable.DegreeData conData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[2]);
					DegreeTable.DegreeData suffixData = Singleton<DegreeTable>.I.GetData((uint)degreeIds[3]);
					degreeText = prefixData.name + " " + conData.name + " " + suffixData.name;
				}
				mText.text = degreeText;
				mText.get_gameObject().SetActive(false);
				yield return (object)this.StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeIds[0])));
				mText.get_gameObject().SetActive(true);
				SetEnableButtonCollider(isButton);
				onFinish?.Invoke(this);
			}
		}
	}

	public void SetFrame(int degreeId)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeId)));
	}

	public void SetUnknownFrame()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		this.StartCoroutine(_SetFrame("DF_UNKNOWN"));
	}

	private IEnumerator _SetFrame(string frameName)
	{
		if (!(mFrameName == frameName))
		{
			LoadingQueue load = new LoadingQueue(this);
			LoadObject frameTexture = load.Load(true, RESOURCE_CATEGORY.DEGREE_FRAME, frameName, false);
			mFrame.set_enabled(false);
			if (load.IsLoading())
			{
				yield return (object)load.Wait();
			}
			mFrame.mainTexture = (frameTexture.loadedObject as Texture);
			mFrameName = frameName;
			mFrame.set_enabled(true);
		}
	}

	public void SetEnableButtonCollider(bool enable)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		this.get_transform().GetComponent<Collider>().set_enabled(enable);
	}
}
