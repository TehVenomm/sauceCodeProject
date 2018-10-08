using System.Collections.Generic;
using UnityEngine;

public class BulletControllerPairSwordsSoul : BulletControllerBase, IObservable
{
	private List<IObserver> observerList = new List<IObserver>();

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize(bullet, _skillInfoParam, pos, rot);
	}

	public override void OnHit(Collider collider)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (collider.get_gameObject().get_layer() == 11 || collider.get_gameObject().get_layer() == 10)
		{
			NotifyObservers();
		}
	}

	public void RegisterObserver(IObserver observer)
	{
		observerList.Add(observer);
	}

	public void NotifyObservers()
	{
		for (int i = 0; i < observerList.Count; i++)
		{
			observerList[i].OnHit();
		}
	}
}
