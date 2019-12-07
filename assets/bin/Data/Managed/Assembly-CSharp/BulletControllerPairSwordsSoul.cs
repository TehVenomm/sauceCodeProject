using System.Collections.Generic;
using UnityEngine;

public class BulletControllerPairSwordsSoul : BulletControllerBase, IObservable
{
	private List<IObserver> observerList = new List<IObserver>();

	public override void Initialize(BulletData bullet, SkillInfo.SkillParam _skillInfoParam, Vector3 pos, Quaternion rot)
	{
		base.Initialize(bullet, _skillInfoParam, pos, rot);
	}

	public override void OnHit(Collider collider)
	{
		if (collider.gameObject.layer == 11 || collider.gameObject.layer == 10)
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
