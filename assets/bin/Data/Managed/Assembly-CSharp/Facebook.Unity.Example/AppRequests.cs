using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal class AppRequests : MenuBase
	{
		private string requestMessage = string.Empty;

		private string requestTo = string.Empty;

		private string requestFilter = string.Empty;

		private string requestExcludes = string.Empty;

		private string requestMax = string.Empty;

		private string requestData = string.Empty;

		private string requestTitle = string.Empty;

		private string requestObjectID = string.Empty;

		private int selectedAction;

		private string[] actionTypeStrings = new string[4]
		{
			"NONE",
			((Enum)0).ToString(),
			((Enum)1).ToString(),
			((Enum)2).ToString()
		};

		protected unsafe override void GetGui()
		{
			//IL_018f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0194: Expected O, but got Unknown
			//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c7: Expected O, but got Unknown
			//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Expected O, but got Unknown
			//IL_0227: Unknown result type (might be due to invalid IL or missing references)
			if (Button("Select - Filter None"))
			{
				FacebookDelegate<IAppRequestResult> val = new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/);
				FB.AppRequest("Test Message", (IEnumerable<string>)null, (IEnumerable<object>)null, (IEnumerable<string>)null, (int?)null, string.Empty, string.Empty, val);
			}
			if (Button("Select - Filter app_users"))
			{
				List<object> list = new List<object>();
				list.Add("app_users");
				List<object> list2 = list;
				FB.AppRequest("Test Message", (IEnumerable<string>)null, (IEnumerable<object>)list2, (IEnumerable<string>)null, (int?)0, string.Empty, string.Empty, new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			if (Button("Select - Filter app_non_users"))
			{
				List<object> list = new List<object>();
				list.Add("app_non_users");
				List<object> list3 = list;
				FB.AppRequest("Test Message", (IEnumerable<string>)null, (IEnumerable<object>)list3, (IEnumerable<string>)null, (int?)0, string.Empty, string.Empty, new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
			}
			LabelAndTextField("Message: ", ref requestMessage);
			LabelAndTextField("To (optional): ", ref requestTo);
			LabelAndTextField("Filter (optional): ", ref requestFilter);
			LabelAndTextField("Exclude Ids (optional): ", ref requestExcludes);
			LabelAndTextField("Filters: ", ref requestExcludes);
			LabelAndTextField("Max Recipients (optional): ", ref requestMax);
			LabelAndTextField("Data (optional): ", ref requestData);
			LabelAndTextField("Title (optional): ", ref requestTitle);
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.Label("Request Action (optional): ", base.LabelStyle, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MaxWidth(200f * base.ScaleFactor)
			});
			selectedAction = GUILayout.Toolbar(selectedAction, actionTypeStrings, base.ButtonStyle, (GUILayoutOption[])new GUILayoutOption[2]
			{
				GUILayout.MinHeight((float)ConsoleBase.ButtonHeight * base.ScaleFactor),
				GUILayout.MaxWidth((float)(ConsoleBase.MainWindowWidth - 150))
			});
			GUILayout.EndHorizontal();
			LabelAndTextField("Request Object ID (optional): ", ref requestObjectID);
			if (Button("Custom App Request"))
			{
				OGActionType? selectedOGActionType = GetSelectedOGActionType();
				if (selectedOGActionType.HasValue)
				{
					FB.AppRequest(requestMessage, selectedOGActionType.Value, requestObjectID, (IEnumerable<string>)((!string.IsNullOrEmpty(requestTo)) ? requestTo.Split(',') : null), requestData, requestTitle, new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
				else
				{
					FB.AppRequest(requestMessage, (IEnumerable<string>)((!string.IsNullOrEmpty(requestTo)) ? requestTo.Split(',') : null), (IEnumerable<object>)((!string.IsNullOrEmpty(requestFilter)) ? requestFilter.Split(',').OfType<object>().ToList() : null), (IEnumerable<string>)((!string.IsNullOrEmpty(requestExcludes)) ? requestExcludes.Split(',') : null), (int?)((!string.IsNullOrEmpty(requestMax)) ? int.Parse(requestMax) : 0), requestData, requestTitle, new FacebookDelegate<IAppRequestResult>((object)this, (IntPtr)(void*)/*OpCode not supported: LdFtn*/));
				}
			}
		}

		private OGActionType? GetSelectedOGActionType()
		{
			string a = actionTypeStrings[selectedAction];
			if (a == ((Enum)0).ToString())
			{
				return 0;
			}
			if (a == ((Enum)1).ToString())
			{
				return 1;
			}
			if (a == ((Enum)2).ToString())
			{
				return 2;
			}
			return null;
		}
	}
}
