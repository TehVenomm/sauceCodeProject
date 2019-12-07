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
			animStart[i].Sample(1f, isFinished: true);
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
			base.gameObject.SetActive(value: true);
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
		int l = 0;
		int k;
		for (k = animStart.Length; l < k; l++)
		{
			while (animStart[l].enabled)
			{
				yield return null;
			}
		}
		while (isStop)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.3f);
		if (onEndCallback != null)
		{
			onEndCallback(this);
		}
		int m = 0;
		for (int num = animEnd.Length; m < num; m++)
		{
			animEnd[m].ResetToBeginning();
			animEnd[m].PlayForward();
		}
		k = 0;
		for (l = animEnd.Length; k < l; k++)
		{
			while (animEnd[k].enabled)
			{
				yield return null;
			}
		}
		base.gameObject.SetActive(value: false);
	}

	public void MovePos(bool stop, Vector3 pos, float time)
	{
		anim.Set(time, _transform.localPosition, pos);
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
