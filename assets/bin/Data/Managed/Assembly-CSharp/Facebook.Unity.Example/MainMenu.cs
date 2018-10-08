using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal sealed class MainMenu : MenuBase
	{
		protected override bool ShowBackButton()
		{
			return false;
		}

		protected unsafe override void GetGui()
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ec: Expected O, but got Unknown
			//IL_0136: Unknown result type (might be due to invalid IL or missing references)
			//IL_0140: Unknown result type (might be due to invalid IL or missing references)
			bool enabled = GUI.get_enabled();
			if (Button("FB.Init"))
			{
				FB.Init(new InitDelegate((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), new HideUnityDelegate((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (string)null);
				base.Status = "FB.Init() called with " + FB.get_AppId();
			}
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUI.set_enabled(enabled && FB.get_IsInitialized());
			if (Button("Login"))
			{
				CallFBLogin();
				base.Status = "Login called";
			}
			GUI.set_enabled(FB.get_IsLoggedIn());
			if (Button("Get publish_actions"))
			{
				CallFBLoginForPublish();
				base.Status = "Login (for publish_actions) called";
			}
			if (Button("Logout"))
			{
				CallFBLogout();
				base.Status = "Logout called";
			}
			GUILayout.Label(GUIContent.none, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MinWidth((float)ConsoleBase.MarginFix)
			});
			GUILayout.EndHorizontal();
			GUI.set_enabled(enabled && FB.get_IsInitialized());
			if (Button("Share Dialog"))
			{
				SwitchMenu(typeof(DialogShare));
			}
			bool enabled2 = GUI.get_enabled();
			GUI.set_enabled(enabled && (int)AccessToken.get_CurrentAccessToken() != 0 && AccessToken.get_CurrentAccessToken().get_Permissions().Contains("publish_actions"));
			if (Button("Game Groups"))
			{
				SwitchMenu(typeof(GameGroups));
			}
			GUI.set_enabled(enabled2);
			if (Button("App Requests"))
			{
				SwitchMenu(typeof(AppRequests));
			}
			if (Button("Graph Request"))
			{
				SwitchMenu(typeof(GraphRequest));
			}
			if (Constants.get_IsWeb() && Button("Pay"))
			{
				SwitchMenu(typeof(Pay));
			}
			if (Button("App Events"))
			{
				SwitchMenu(typeof(AppEvents));
			}
			if (Button("App Links"))
			{
				SwitchMenu(typeof(AppLinks));
			}
			if (Constants.get_IsMobile() && Button("App Invites"))
			{
				SwitchMenu(typeof(AppInvites));
			}
			if (Constants.get_IsMobile() && Button("Access Token"))
			{
				SwitchMenu(typeof(AccessTokenMenu));
			}
			GUI.set_enabled(enabled);
		}

		private unsafe void CallFBLogin()
		{
			List<string> list = new List<string>();
			list.Add("public_profile");
			list.Add("email");
			list.Add("user_friends");
			FB.LogInWithReadPermissions((IEnumerable<string>)list, new FacebookDelegate<ILoginResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private unsafe void CallFBLoginForPublish()
		{
			List<string> list = new List<string>();
			list.Add("publish_actions");
			FB.LogInWithPublishPermissions((IEnumerable<string>)list, new FacebookDelegate<ILoginResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private void CallFBLogout()
		{
			FB.LogOut();
		}

		private void OnInitComplete()
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			base.Status = "Success - Check log for details";
			base.LastResponse = "Success Response: OnInitComplete Called\n";
			string log = $"OnInitCompleteCalled IsLoggedIn='{FB.get_IsLoggedIn()}' IsInitialized='{FB.get_IsInitialized()}'";
			LogView.AddLog(log);
			if ((int)AccessToken.get_CurrentAccessToken() != 0)
			{
				LogView.AddLog(AccessToken.get_CurrentAccessToken().ToString());
			}
		}

		private void OnHideUnity(bool isGameShown)
		{
			base.Status = "Success - Check log for details";
			base.LastResponse = $"Success Response: OnHideUnity Called {isGameShown}\n";
			LogView.AddLog("Is game shown: " + isGameShown);
		}
	}
}
