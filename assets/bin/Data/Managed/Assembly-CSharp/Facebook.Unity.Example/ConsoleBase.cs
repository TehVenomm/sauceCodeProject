using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Facebook.Unity.Example
{
	internal class ConsoleBase
	{
		private const int DpiScalingFactor = 160;

		private static Stack<string> menuStack = new Stack<string>();

		private string status = "Ready";

		private string lastResponse = string.Empty;

		private Vector2 scrollPosition = Vector2.get_zero();

		private float? scaleFactor;

		private GUIStyle textStyle;

		private GUIStyle buttonStyle;

		private GUIStyle textInputStyle;

		private GUIStyle labelStyle;

		protected static int ButtonHeight => (!Constants.get_IsMobile()) ? 24 : 60;

		protected static int MainWindowWidth => (!Constants.get_IsMobile()) ? 700 : (Screen.get_width() - 30);

		protected static int MainWindowFullWidth => (!Constants.get_IsMobile()) ? 760 : Screen.get_width();

		protected static int MarginFix => (!Constants.get_IsMobile()) ? 48 : 0;

		protected static Stack<string> MenuStack
		{
			get
			{
				return menuStack;
			}
			set
			{
				menuStack = value;
			}
		}

		protected string Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

		protected Texture2D LastResponseTexture
		{
			get;
			set;
		}

		protected string LastResponse
		{
			get
			{
				return lastResponse;
			}
			set
			{
				lastResponse = value;
			}
		}

		protected Vector2 ScrollPosition
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				return scrollPosition;
			}
			set
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0002: Unknown result type (might be due to invalid IL or missing references)
				scrollPosition = value;
			}
		}

		protected float ScaleFactor
		{
			get
			{
				if (!scaleFactor.HasValue)
				{
					scaleFactor = Screen.get_dpi() / 160f;
				}
				return scaleFactor.Value;
			}
		}

		protected int FontSize => (int)Math.Round((double)(ScaleFactor * 16f));

		protected GUIStyle TextStyle
		{
			get
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Expected O, but got Unknown
				//IL_0046: Unknown result type (might be due to invalid IL or missing references)
				if (textStyle == null)
				{
					textStyle = new GUIStyle(GUI.get_skin().get_textArea());
					textStyle.set_alignment(0);
					textStyle.set_wordWrap(true);
					textStyle.set_padding(new RectOffset(10, 10, 10, 10));
					textStyle.set_stretchHeight(true);
					textStyle.set_stretchWidth(false);
					textStyle.set_fontSize(FontSize);
				}
				return textStyle;
			}
		}

		protected GUIStyle ButtonStyle
		{
			get
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Expected O, but got Unknown
				if (buttonStyle == null)
				{
					buttonStyle = new GUIStyle(GUI.get_skin().get_button());
					buttonStyle.set_fontSize(FontSize);
				}
				return buttonStyle;
			}
		}

		protected GUIStyle TextInputStyle
		{
			get
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Expected O, but got Unknown
				if (textInputStyle == null)
				{
					textInputStyle = new GUIStyle(GUI.get_skin().get_textField());
					textInputStyle.set_fontSize(FontSize);
				}
				return textInputStyle;
			}
		}

		protected GUIStyle LabelStyle
		{
			get
			{
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Expected O, but got Unknown
				if (labelStyle == null)
				{
					labelStyle = new GUIStyle(GUI.get_skin().get_label());
					labelStyle.set_fontSize(FontSize);
				}
				return labelStyle;
			}
		}

		public ConsoleBase()
			: this()
		{
		}//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)


		protected virtual void Awake()
		{
			Application.set_targetFrameRate(60);
		}

		protected bool Button(string label)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Expected O, but got Unknown
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Expected O, but got Unknown
			return GUILayout.Button(label, ButtonStyle, (GUILayoutOption[])new GUILayoutOption[2]
			{
				GUILayout.MinHeight((float)ButtonHeight * ScaleFactor),
				GUILayout.MaxWidth((float)MainWindowWidth)
			});
		}

		protected void LabelAndTextField(string label, ref string text)
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			//IL_004e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0053: Expected O, but got Unknown
			GUILayout.BeginHorizontal((GUILayoutOption[])new GUILayoutOption[0]);
			GUILayout.Label(label, LabelStyle, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MaxWidth(200f * ScaleFactor)
			});
			text = GUILayout.TextField(text, TextInputStyle, (GUILayoutOption[])new GUILayoutOption[1]
			{
				GUILayout.MaxWidth((float)(MainWindowWidth - 150))
			});
			GUILayout.EndHorizontal();
		}

		protected bool IsHorizontalLayout()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Invalid comparison between Unknown and I4
			return (int)Screen.get_orientation() == 3;
		}

		protected void SwitchMenu(Type menuClass)
		{
			menuStack.Push(GetType().Name);
			SceneManager.LoadScene(menuClass.Name);
		}

		protected void GoBack()
		{
			if (menuStack.Any())
			{
				SceneManager.LoadScene(menuStack.Pop());
			}
		}
	}
}
