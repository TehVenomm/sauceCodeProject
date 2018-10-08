using System;
using System.Collections;
using UnityEngine;

public class UIDropAnnounceItem
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

	public UIDropAnnounceItem()
		: this()
	{
	}

	protected void Awake()
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		int i = 0;
		for (int num = animStart.Length; i < num; i++)
		{
			animStart[i].set_enabled(false);
			animStart[i].Sample(1f, true);
		}
		int j = 0;
		for (int num2 = animEnd.Length; j < num2; j++)
		{
			animEnd[j].set_enabled(false);
		}
		_transform = this.get_transform();
	}

	public void StartAnnounce(string text, Color color, bool stop, Action<UIDropAnnounceItem> end_callback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_gameObject().get_activeSelf())
		{
			this.get_gameObject().SetActive(true);
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
		this.StartCoroutine(Direction());
	}

	protected IEnumerator Direction()
	{
		int n = 0;
		for (int m = animStart.Length; n < m; n++)
		{
			while (animStart[n].get_enabled())
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
			while (animEnd[j].get_enabled())
			{
				yield return (object)null;
			}
		}
		this.get_gameObject().SetActive(false);
	}

	public void MovePos(bool stop, Vector3 pos, float time)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		anim.Set(time, _transform.get_localPosition(), pos, null, default(Vector3), null);
		anim.Play();
		isStop = stop;
	}

	private void LateUpdate()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (anim.IsPlaying())
		{
			_transform.set_localPosition(anim.Update());
		}
	}
}
