using System.Collections.Generic;
using UnityEngine;

public class MapScriptWall : MonoBehaviour
{
	[Tooltip("壁半径")]
	public float wallRadius = 40f;

	private void LateUpdate()
	{
		if (!MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		List<StageObject>.Enumerator enumerator = MonoBehaviourSingleton<StageObjectManager>.I.characterList.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Vector3 vector = enumerator.Current._transform.position - zero;
			if (vector.magnitude > wallRadius)
			{
				enumerator.Current._transform.position = zero + vector.normalized * wallRadius;
			}
		}
	}
}
