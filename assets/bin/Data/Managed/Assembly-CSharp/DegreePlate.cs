using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegreePlate : MonoBehaviour
{
	private const int DEGREE_PART = 4;

	private const string PREFAB_NAME = "DegreePlate";

	private const string UNKNOWN_FRAME = "DF_UNKNOWN";

	[SerializeField]
	private UILabel mText;

	[SerializeField]
	private UITexture mFrame;

	private string mFrameName;

	public static void Create(MonoBehaviour call_mono, List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		ResourceObject cachedResourceObject = MonoBehaviourSingleton<ResourceManager>.I.cache.GetCachedResourceObject(RESOURCE_CATEGORY.UI, "DegreePlate");
		if (cachedResourceObject != null)
		{
			(UnityEngine.Object.Instantiate(cachedResourceObject.obj) as GameObject).GetComponent<DegreePlate>().Initialize(degreeIds, isButton, onFinish);
		}
		else
		{
			MonoBehaviourSingleton<ResourceManager>.I.Load(ResourceLoad.GetResourceLoad(call_mono, destroy_notify: true), RESOURCE_CATEGORY.UI, "DegreePlate", delegate(ResourceManager.LoadRequest x, ResourceObject[] y)
			{
				y[0].refCount++;
				(UnityEngine.Object.Instantiate(y[0].obj) as GameObject).GetComponent<DegreePlate>().Initialize(degreeIds, isButton, onFinish);
			}, null);
		}
	}

	public void Initialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		StartCoroutine(DoInitialize(degreeIds, isButton, onFinish));
	}

	private IEnumerator DoInitialize(List<int> degreeIds, bool isButton, Action<DegreePlate> onFinish)
	{
		if (degreeIds == null || 4 != degreeIds.Count)
		{
			onFinish?.Invoke(null);
			yield break;
		}
		DegreeTable.DegreeData data = Singleton<DegreeTable>.I.GetData((uint)degreeIds[0]);
		if (data == null || (data.type != DEGREE_TYPE.FRAME && data.type != DEGREE_TYPE.SPECIAL_FRAME))
		{
			onFinish?.Invoke(null);
			yield break;
		}
		string text;
		if (data.type == DEGREE_TYPE.SPECIAL_FRAME)
		{
			text = "";
		}
		else
		{
			DegreeTable.DegreeData data2 = Singleton<DegreeTable>.I.GetData((uint)degreeIds[1]);
			DegreeTable.DegreeData data3 = Singleton<DegreeTable>.I.GetData((uint)degreeIds[2]);
			DegreeTable.DegreeData data4 = Singleton<DegreeTable>.I.GetData((uint)degreeIds[3]);
			text = data2.name + " " + data3.name + " " + data4.name;
		}
		mText.text = text;
		mText.gameObject.SetActive(value: false);
		yield return StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeIds[0])));
		mText.gameObject.SetActive(value: true);
		SetEnableButtonCollider(isButton);
		onFinish?.Invoke(this);
	}

	public void SetFrame(int degreeId)
	{
		StartCoroutine(_SetFrame(ResourceName.GetDegreeFrameName(degreeId)));
	}

	public void SetUnknownFrame()
	{
		StartCoroutine(_SetFrame("DF_UNKNOWN"));
	}

	private IEnumerator _SetFrame(string frameName)
	{
		if (!(mFrameName == frameName))
		{
			LoadingQueue loadingQueue = new LoadingQueue(this);
			LoadObject frameTexture = loadingQueue.Load(isEventAsset: true, RESOURCE_CATEGORY.DEGREE_FRAME, frameName);
			mFrame.enabled = false;
			if (loadingQueue.IsLoading())
			{
				yield return loadingQueue.Wait();
			}
			mFrame.mainTexture = (frameTexture.loadedObject as Texture);
			mFrameName = frameName;
			mFrame.enabled = true;
		}
	}

	public void SetEnableButtonCollider(bool enable)
	{
		base.transform.GetComponent<Collider>().enabled = enable;
	}
}
