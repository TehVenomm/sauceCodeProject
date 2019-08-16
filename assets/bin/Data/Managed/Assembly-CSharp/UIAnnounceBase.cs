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
		OnAfterAwake();
		base.Awake();
		InitAnim();
		OnBeforeAwake();
	}

	protected void Start()
	{
		OnStart();
	}

	private void InitAnim()
	{
		OnBeforeInitAnimation();
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
			effAnim[j].Sample(1f, isFinished: true);
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
		OnAfterInitAnimation();
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
		OnBeforeStartAnimation();
		int k = 0;
		for (int num = endAnim.Length; k < num; k++)
		{
			endAnim[k].set_enabled(false);
		}
		int l = 0;
		for (int num2 = starAnim.Length; l < num2; l++)
		{
			starAnim[l].ResetToBeginning();
			starAnim[l].PlayForward();
		}
		int m = 0;
		for (int num3 = effAnim.Length; m < num3; m++)
		{
			effAnim[m].ResetToBeginning();
			effAnim[m].PlayForward();
		}
		int n = 0;
		for (int num4 = loopAnim.Length; n < num4; n++)
		{
			loopAnim[n].ResetToBeginning();
			loopAnim[n].PlayForward();
		}
		yield return (object)new WaitForSeconds(GetDispSec());
		int num5 = 0;
		for (int num6 = endAnim.Length; num5 < num6; num5++)
		{
			endAnim[num5].ResetToBeginning();
			endAnim[num5].PlayForward();
		}
		int j = 0;
		for (int i = endAnim.Length; j < i; j++)
		{
			while (endAnim[j].get_enabled())
			{
				yield return null;
			}
		}
		if (panelChange != null)
		{
			panelChange.Lock();
		}
		routineWork = null;
		OnAfterAnimation();
	}

	protected virtual void OnBeforeAwake()
	{
	}

	protected virtual void OnAfterAwake()
	{
	}

	protected virtual void OnStart()
	{
	}

	protected virtual void OnBeforeInitAnimation()
	{
	}

	protected virtual void OnAfterInitAnimation()
	{
	}

	protected virtual void OnBeforeStartAnimation()
	{
	}

	protected virtual void OnAfterAnimation()
	{
	}
}
