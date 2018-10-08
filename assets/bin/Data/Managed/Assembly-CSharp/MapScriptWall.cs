using System.Collections.Generic;
using UnityEngine;

public class MapScriptWall : MonoBehaviour
{
	[Tooltip("壁半径")]
	public float wallRadius = 40f;

	private void LateUpdate()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid())
		{
			Vector3 zero = Vector3.zero;
			List<StageObject> characterList = MonoBehaviourSingleton<StageObjectManager>.I.characterList;
			List<StageObject>.Enumerator enumerator = characterList.GetEnumerator();
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
}
