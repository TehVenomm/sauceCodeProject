using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnnounceBand : MonoBehaviourSingleton<UIAnnounceBand>
{
	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected UILabel label;

	[SerializeField]
	protected UILabel conditionLabel;

	[SerializeField]
	protected float dispTime = 3f;

	[SerializeField]
	protected UITweener[] animStart;

	[SerializeField]
	protected UITweener[] animEnd;

	[SerializeField]
	protected GameObject animRoot;

	private bool isDone;

	public bool isWait;

	private List<string> announceQueue = new List<string>();

	private bool isStartable => !isDone && !isWait;

	protected override void Awake()
	{
		base.Awake();
		InitAnim();
	}

	private void InitAnim()
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
		base.gameObject.SetActive(false);
		animRoot.SetActive(false);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (isDone)
		{
			InitAnim();
			announceQueue.Clear();
			isDone = false;
			panelChange.Lock();
		}
	}

	private void LateUpdate()
	{
		if (isStartable)
		{
			PlayAnnounce();
		}
	}

	public void SetAnnounce(string messeage, string conditionTitle)
	{
		base.gameObject.SetActive(true);
		announceQueue.Add(messeage);
		announceQueue.Add(conditionTitle);
	}

	private bool PlayAnnounce()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
			return false;
		}
		if (announceQueue.Count > 0)
		{
			animRoot.SetActive(true);
			label.text = announceQueue[0];
			announceQueue.RemoveAt(0);
			conditionLabel.text = announceQueue[0];
			announceQueue.RemoveAt(0);
			isDone = true;
			panelChange.UnLock();
			StartCoroutine(Direction());
			return true;
		}
		return false;
	}

	private void FinishAnnounce()
	{
		if (!PlayAnnounce())
		{
			animRoot.SetActive(false);
			base.gameObject.SetActive(false);
			panelChange.Lock();
			isDone = false;
		}
	}

	protected IEnumerator Direction()
	{
		int i3 = 0;
		for (int n3 = animEnd.Length; i3 < n3; i3++)
		{
			animEnd[i3].ResetToBeginning();
		}
		int i2 = 0;
		for (int n2 = animStart.Length; i2 < n2; i2++)
		{
			animStart[i2].ResetToBeginning();
			animStart[i2].PlayForward();
		}
		int n = 0;
		for (int m = animStart.Length; n < m; n++)
		{
			while (animStart[n].enabled)
			{
				yield return (object)null;
			}
		}
		yield return (object)new WaitForSeconds(dispTime);
		int l = 0;
		for (int k = animEnd.Length; l < k; l++)
		{
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
		FinishAnnounce();
	}
}
