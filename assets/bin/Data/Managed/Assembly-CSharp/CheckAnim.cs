using UnityEngine;

public class CheckAnim : MonoBehaviour
{
	public Animation m_Target;

	public CheckAnim()
		: this()
	{
	}

	private void OnGUI()
	{
		if (!(m_Target == null) && GUILayout.Button("play", (GUILayoutOption[])new GUILayoutOption[0]))
		{
			m_Target.Stop();
			m_Target.Play();
		}
	}
}
