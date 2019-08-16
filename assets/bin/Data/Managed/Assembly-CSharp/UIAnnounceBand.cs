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
			animStart[i].set_enabled(false);
			animStart[i].Sample(1f, isFinished: true);
		}
		int j = 0;
		for (int num2 = animEnd.Length; j < num2; j++)
		{
			animEnd[j].set_enabled(false);
		}
		this.get_gameObject().SetActive(false);
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
		this.get_gameObject().SetActive(true);
		announceQueue.Add(messeage);
		announceQueue.Add(conditionTitle);
	}

	private bool PlayAnnounce()
	{
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			this.get_gameObject().SetActive(false);
			return false;
		}
		if (announceQueue.Count > 0)
		{
			animRoot.SetActive(true);
			label.text = announceQueue[0];
			label.supportEncoding = true;
			announceQueue.RemoveAt(0);
			conditionLabel.text = announceQueue[0];
			announceQueue.RemoveAt(0);
			isDone = true;
			panelChange.UnLock();
			this.StartCoroutine(Direction());
			return true;
		}
		return false;
	}

	private void FinishAnnounce()
	{
		if (!PlayAnnounce())
		{
			animRoot.SetActive(false);
			this.get_gameObject().SetActive(false);
			panelChange.Lock();
			isDone = false;
		}
	}

	protected IEnumerator Direction()
	{
		int m = 0;
		for (int num = animEnd.Length; m < num; m++)
		{
			animEnd[m].ResetToBeginning();
		}
		int n = 0;
		for (int num2 = animStart.Length; n < num2; n++)
		{
			animStart[n].ResetToBeginning();
			animStart[n].PlayForward();
		}
		int l = 0;
		for (int k = animStart.Length; l < k; l++)
		{
			while (animStart[l].get_enabled())
			{
				yield return null;
			}
		}
		yield return (object)new WaitForSeconds(dispTime);
		int num3 = 0;
		for (int num4 = animEnd.Length; num3 < num4; num3++)
		{
			animEnd[num3].PlayForward();
		}
		int j = 0;
		for (int i = animEnd.Length; j < i; j++)
		{
			while (animEnd[j].get_enabled())
			{
				yield return null;
			}
		}
		FinishAnnounce();
	}
}
