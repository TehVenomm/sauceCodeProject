using System;
using System.Collections.Generic;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class GameGroups : MenuBase
	{
		private string gamerGroupName = "Test group";

		private string gamerGroupDesc = "Test group for testing.";

		private string gamerGroupPrivacy = "closed";

		private string gamerGroupCurrentGroup = string.Empty;

		protected unsafe override void GetGui()
		{
			if (Button("Game Group Create - Closed"))
			{
				FB.GameGroupCreate("Test game group", "Test description", "CLOSED", new FacebookDelegate<IGroupCreateResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			if (Button("Game Group Create - Open"))
			{
				FB.GameGroupCreate("Test game group", "Test description", "OPEN", new FacebookDelegate<IGroupCreateResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			LabelAndTextField("Group Name", ref gamerGroupName);
			LabelAndTextField("Group Description", ref gamerGroupDesc);
			LabelAndTextField("Group Privacy", ref gamerGroupPrivacy);
			if (Button("Call Create Group Dialog"))
			{
				CallCreateGroupDialog();
			}
			LabelAndTextField("Group To Join", ref gamerGroupCurrentGroup);
			bool enabled = GUI.get_enabled();
			GUI.set_enabled(enabled && !string.IsNullOrEmpty(gamerGroupCurrentGroup));
			if (Button("Call Join Group Dialog"))
			{
				CallJoinGroupDialog();
			}
			GUI.set_enabled(enabled && FB.get_IsLoggedIn());
			if (Button("Get All App Managed Groups"))
			{
				CallFbGetAllOwnedGroups();
			}
			if (Button("Get Gamer Groups Logged in User Belongs to"))
			{
				CallFbGetUserGroups();
			}
			if (Button("Make Group Post As User"))
			{
				CallFbPostToGamerGroup();
			}
			GUI.set_enabled(enabled);
		}

		private void GroupCreateCB(IGroupCreateResult result)
		{
			HandleResult(result);
			if (result.get_GroupId() != null)
			{
				gamerGroupCurrentGroup = result.get_GroupId();
			}
		}

		private void GetAllGroupsCB(IGraphResult result)
		{
			if (!string.IsNullOrEmpty(result.get_RawResult()))
			{
				base.LastResponse = result.get_RawResult();
				IDictionary<string, object> resultDictionary = result.get_ResultDictionary();
				if (resultDictionary.ContainsKey("data"))
				{
					List<object> list = (List<object>)resultDictionary["data"];
					if (list.Count > 0)
					{
						Dictionary<string, object> dictionary = (Dictionary<string, object>)list[0];
						gamerGroupCurrentGroup = (string)dictionary["id"];
					}
				}
			}
			if (!string.IsNullOrEmpty(result.get_Error()))
			{
				base.LastResponse = result.get_Error();
			}
		}

		private unsafe void CallFbGetAllOwnedGroups()
		{
			FB.API(FB.get_AppId() + "/groups", 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
		}

		private unsafe void CallFbGetUserGroups()
		{
			FB.API("/me/groups?parent=" + FB.get_AppId(), 0, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)null);
		}

		private unsafe void CallCreateGroupDialog()
		{
			FB.GameGroupCreate(gamerGroupName, gamerGroupDesc, gamerGroupPrivacy, new FacebookDelegate<IGroupCreateResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private unsafe void CallJoinGroupDialog()
		{
			FB.GameGroupJoin(gamerGroupCurrentGroup, new FacebookDelegate<IGroupJoinResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
		}

		private unsafe void CallFbPostToGamerGroup()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["message"] = "herp derp a post";
			FB.API(gamerGroupCurrentGroup + "/feed", 1, new FacebookDelegate<IGraphResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/), (IDictionary<string, string>)dictionary);
		}
	}
}
