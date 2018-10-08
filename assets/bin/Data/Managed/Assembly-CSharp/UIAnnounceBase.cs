using System.Collections;
using UnityEngine;

public class UIAnnounceBase<T> : MonoBehaviourSingleton<T> where T : DisableNotifyMonoBehaviour
{
	[SerializeField]
	protected FontStyle style;

	[SerializeField]
	protected UIStaticPanelChanger panelChange;

	[SerializeField]
	protected UITweener[] starAnim;

	[SerializeField]
	protected UITweener[] effAnim;

	[SerializeField]
	protected UITweener[] loopAnim;

	[SerializeField]
	protected UITweener[] endAnim;

	private IEnumerator routineWork;

	protected virtual float GetDispSec()
	{
		return 3f;
	}

	protected override void Awake()
	{
		base.Awake();
		InitAnim();
	}

	private void InitAnim()
	{
		int i = 0;
		for (int num = starAnim.Length; i < num; i++)
		{
			starAnim[i].set_enabled(false);
			starAnim[i].ResetToBeginning();
		}
		int j = 0;
		for (int num2 = effAnim.Length; j < num2; j++)
		{
			effAnim[j].set_enabled(false);
			effAnim[j].Sample(1f, true);
		}
		int k = 0;
		for (int num3 = loopAnim.Length; k < num3; k++)
		{
			loopAnim[k].set_enabled(false);
		}
		int l = 0;
		for (int num4 = endAnim.Length; l < num4; l++)
		{
			endAnim[l].set_enabled(false);
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (routineWork != null)
		{
			if (panelChange != null)
			{
				panelChange.Lock();
			}
			routineWork = null;
			InitAnim();
		}
	}

	protected bool AnnounceStart(Player player)
	{
		if (player == null)
		{
			return false;
		}
		if (!TutorialStep.HasAllTutorialCompleted() && !(player is Self))
		{
			return false;
		}
		return AnnounceStart();
	}

	protected bool AnnounceStart()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (!this.get_gameObject().get_activeInHierarchy())
		{
			return false;
		}
		if (routineWork != null)
		{
			this.StopCoroutine(routineWork);
		}
		else if (panelChange != null)
		{
			panelChange.UnLock();
		}
		routineWork = Direction();
		this.StartCoroutine(routineWork);
		return true;
	}

	protected IEnumerator Direction()
	{
		int i4 = 0;
		for (int n4 = this.endAnim.Length; i4 < n4; i4++)
		{
			this.endAnim[i4].set_enabled(false);
		}
		int i3 = 0;
		for (int n3 = this.starAnim.Length; i3 < n3; i3++)
		{
			this.starAnim[i3].ResetToBeginning();
			this.starAnim[i3].PlayForward();
		}
		int i2 = 0;
		for (int n2 = this.effAnim.Length; i2 < n2; i2++)
		{
			this.effAnim[i2].ResetToBeginning();
			this.effAnim[i2].PlayForward();
		}
		int n = 0;
		for (int m = this.loopAnim.Length; n < m; n++)
		{
			this.loopAnim[n].ResetToBeginning();
			this.loopAnim[n].PlayForward();
		}
		yield return (object)new WaitForSeconds(this.GetDispSec());
		int l = 0;
		for (int k = this.endAnim.Length; l < k; l++)
		{
			this.endAnim[l].ResetToBeginning();
			this.endAnim[l].PlayForward();
		}
		int j = 0;
		for (int i = this.endAnim.Length; j < i; j++)
		{
			while (this.endAnim[j].get_enabled())
			{
				yield return (object)null;
			}
		}
		if (this.panelChange != null)
		{
			this.panelChange.Lock();
		}
		this.routineWork = null;
	}
}
