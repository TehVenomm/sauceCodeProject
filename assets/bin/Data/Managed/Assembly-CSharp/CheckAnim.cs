using UnityEngine;

public class CheckAnim : MonoBehaviour
{
	public Animation m_Target;

	private void OnGUI()
	{
		if (!(m_Target == null) && GUILayout.Button("play"))
		{
			m_Target.Stop();
			m_Target.Play();
		}
	}
}
