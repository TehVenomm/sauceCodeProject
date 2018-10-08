using System;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal abstract class MenuBase : ConsoleBase
	{
		private static ShareDialogMode shareDialogMode;

		protected abstract void GetGui();

		protected virtual bool ShowDialogModeSelector()
		{
			return false;
		}

		protected virtual bool ShowBackButton()
		{
			return true;
		}

		protected void HandleResult(IResult result)
		{
			if (result == null)
			{
				base.LastResponse = "Null Response\n";
				LogView.AddLog(base.LastResponse);
			}
			else
			{
				base.LastResponseTexture = null;
				if (!string.IsNullOrEmpty(result.get_Error()))
				{
					base.Status = "Error - Check log for details";
					base.LastResponse = "Error Response:\n" + result.get_Error();
				}
				else if (result.get_Cancelled())
				{
					base.Status = "Cancelled - Check log for details";
					base.LastResponse = "Cancelled Response:\n" + result.get_RawResult();
				}
				else if (!string.IsNullOrEmpty(result.get_RawResult()))
				{
					base.Status = "Success - Check log for details";
					base.LastResponse = "Success Response:\n" + result.get_RawResult();
				}
				else
				{
					base.LastResponse = "Empty Response\n";
				}
				LogView.AddLog(((object)result).ToString());
			}
		}

		protected void OnGUI()
		{
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Invalid comparison between Unknown and I4
			//IL_0063: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007f: Unknown result type (might be due to invalid IL or missing references)
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ae: Expected O, but got Unknown
			//IL_00af: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fe: Expected O, but got Unknown
			if (IsHorizontalLayout())
			{
				GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
				GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			}
			GUILayout.Label(GetType().Name, base.LabelStyle, (GUILayoutOption[])new GUILayoutOption[0]);
			AddStatus();
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
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			if (ShowBackButton())
			{
				AddBackButton();
			}
			AddLogButton();
			if (ShowBackButton())
			{
				GUILayout.Label(GUIContent.none, (GUILayoutOption[])new GUILayoutOption[1]
				{
					GUILayout.MinWidth((float)ConsoleBase.MarginFix)
				});
			}
			GUILayout.EndHorizontal();
			if (ShowDialogModeSelector())
			{
				AddDialogModeButtons();
			}
			GUILayout.BeginVertical((GUILayoutOption[])new GUILayoutOption[0]);
			GetGui();
			GUILayout.Space(10f);
			GUILayout.EndVertical();
			GUILayout.EndScrollView();
		}

		private void AddStatus()
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Expected O, but got Unknown
			GUILayout.Space(5f);
			GUILayout.Box("Status: " + base.Status, base.TextStyle, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowWidth)
			});
		}

		private void AddBackButton()
		{
			GUI.set_enabled(ConsoleBase.MenuStack.Any());
			if (Button("Back"))
			{
				GoBack();
			}
			GUI.set_enabled(true);
		}

		private void AddLogButton()
		{
			if (Button("Log"))
			{
				SwitchMenu(typeof(LogView));
			}
		}

		private void AddDialogModeButtons()
		{
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			foreach (object value in Enum.GetValues(typeof(ShareDialogMode)))
			{
				AddDialogModeButton((int)value);
			}
			GUILayout.EndHorizontal();
		}

		private void AddDialogModeButton(ShareDialogMode mode)
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			bool enabled = GUI.get_enabled();
			GUI.set_enabled(enabled && mode != shareDialogMode);
			if (Button(((Enum)mode).ToString()))
			{
				shareDialogMode = mode;
				Mobile.set_ShareDialogMode(mode);
			}
			GUI.set_enabled(enabled);
		}
	}
}
