using System;
using System.Collections;
using UnityEngine;

public class UIDropAnnounceItem : MonoBehaviour
{
	[SerializeField]
	protected UILabel itemName;

	[SerializeField]
	protected UITweener[] animStart;

	[SerializeField]
	protected UITweener[] animEnd;

	private Transform _transform;

	private Vector3Interpolator anim = new Vector3Interpolator();

	private bool isStop;

	protected Action<UIDropAnnounceItem> onEndCallback;

	protected void Awake()
	{
		int i = 0;
		for (int num = animStart.Length; i < num; i++)
		{
			animStart[i].enabled = false;
			animStart[i].Sample(1f, true);
		}
		int j = 0;
		for (int num2 = animEnd.Length; j < num2; j++)
		{
			animEnd[j].enabled = false;
		}
		_transform = base.transform;
	}

	public void StartAnnounce(string text, Color color, bool stop, Action<UIDropAnnounceItem> end_callback)
	{
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		itemName.text = text;
		itemName.color = color;
		onEndCallback = end_callback;
		isStop = stop;
		int i = 0;
		for (int num = animStart.Length; i < num; i++)
		{
			animStart[i].ResetToBeginning();
			animStart[i].PlayForward();
		}
		StartCoroutine(Direction());
	}

	protected IEnumerator Direction()
	{
		int n = 0;
		for (int m = animStart.Length; n < m; n++)
		{
			while (animStart[n].enabled)
			{
				yield return (object)null;
			}
		}
		while (isStop)
		{
			yield return (object)null;
		}
		yield return (object)new WaitForSeconds(0.3f);
		if (onEndCallback != null)
		{
			onEndCallback(this);
		}
		int l = 0;
		for (int k = animEnd.Length; l < k; l++)
		{
			animEnd[l].ResetToBeginning();
			animEnd[l].PlayForward();
		}
		int j = 0;
		for (int i = animEnd.Length; j < i; j++)
		{
			while (animEnd[j].enabled)
			{
				yield return (object)null;
			}
		}
		base.gameObject.SetActive(false);
	}

	public void MovePos(bool stop, Vector3 pos, float time)
	{
		anim.Set(time, _transform.localPosition, pos, null, default(Vector3), null);
		anim.Play();
		isStop = stop;
	}

	private void LateUpdate()
	{
		if (anim.IsPlaying())
		{
			_transform.localPosition = anim.Update();
		}
	}
}
