using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class LogView : ConsoleBase
	{
		private static string datePatt = "M/d/yyyy hh:mm:ss tt";

		private static IList<string> events = new List<string>();

		public static void AddLog(string log)
		{
			events.Insert(0, $"{DateTime.Now.ToString(datePatt)}\n{log}\n");
		}

		protected void OnGUI()
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Invalid comparison between Unknown and I4
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_008c: Expected O, but got Unknown
			//IL_008d: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bf: Expected O, but got Unknown
			//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Expected O, but got Unknown
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			if (Button("Back"))
			{
				GoBack();
			}
			if (Input.get_touchCount() > 0)
			{
				Touch touch = Input.GetTouch(0);
				if ((int)touch.get_phase() == 1)
				{
					Vector2 scrollPosition = base.ScrollPosition;
					float y = scrollPosition.y;
					Touch touch2 = Input.GetTouch(0);
					Vector2 deltaPosition = touch2.get_deltaPosition();
					scrollPosition.y = y + deltaPosition.y;
					base.ScrollPosition = scrollPosition;
				}
			}
			base.ScrollPosition = GUILayout.BeginScrollView(base.ScrollPosition, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowFullWidth)
			});
			GUILayout.TextArea(string.Join("\n", events.ToArray()), base.TextStyle, (GUILayoutOption[])new GUILayoutOption[2]
			{
				GUILayout.ExpandHeight(true),
				GUILayout.MaxWidth((float)ConsoleBase.MainWindowWidth)
			});
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
		}
	}
}
