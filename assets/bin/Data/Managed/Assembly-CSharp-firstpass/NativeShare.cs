using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NativeShare
{
	private static AndroidJavaClass m_ajc;

	private static AndroidJavaObject m_context;

	private string subject;

	private string text;

	private string title;

	private string targetPackage;

	private string targetClass;

	private List<string> files;

	private List<string> mimes;

	private static AndroidJavaClass AJC
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (m_ajc == null)
			{
				m_ajc = new AndroidJavaClass("com.yasirkula.unity.NativeShare");
			}
			return m_ajc;
		}
	}

	private static AndroidJavaObject Context
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (m_context == null)
			{
				AndroidJavaObject val = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				try
				{
					m_context = val.GetStatic<AndroidJavaObject>("currentActivity");
				}
				finally
				{
					((IDisposable)val)?.Dispose();
				}
			}
			return m_context;
		}
	}

	public NativeShare()
	{
		subject = string.Empty;
		text = string.Empty;
		title = string.Empty;
		targetPackage = string.Empty;
		targetClass = string.Empty;
		files = new List<string>(0);
		mimes = new List<string>(0);
	}

	public NativeShare SetSubject(string subject)
	{
		if (subject != null)
		{
			this.subject = subject;
		}
		return this;
	}

	public NativeShare SetText(string text)
	{
		if (text != null)
		{
			this.text = text;
		}
		return this;
	}

	public NativeShare SetTitle(string title)
	{
		if (title != null)
		{
			this.title = title;
		}
		return this;
	}

	public NativeShare SetTarget(string androidPackageName, string androidClassName = null)
	{
		if (!string.IsNullOrEmpty(androidPackageName))
		{
			targetPackage = androidPackageName;
			if (androidClassName != null)
			{
				targetClass = androidClassName;
			}
		}
		return this;
	}

	public NativeShare AddFile(string filePath, string mime = null)
	{
		if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
		{
			files.Add(filePath);
			mimes.Add(mime ?? string.Empty);
		}
		else
		{
			Debug.LogError((object)("File does not exist at path or permission denied: " + filePath));
		}
		return this;
	}

	public void Share()
	{
		if (files.Count == 0 && subject.Length == 0 && text.Length == 0)
		{
			Debug.LogWarning((object)"Share Error: attempting to share nothing!");
		}
		else
		{
			AJC.CallStatic("Share", new object[8]
			{
				(object)Context,
				targetPackage,
				targetClass,
				files.ToArray(),
				mimes.ToArray(),
				subject,
				text,
				title
			});
		}
	}

	public static bool TargetExists(string androidPackageName, string androidClassName = null)
	{
		if (string.IsNullOrEmpty(androidPackageName))
		{
			return false;
		}
		if (androidClassName == null)
		{
			androidClassName = string.Empty;
		}
		return AJC.CallStatic<bool>("TargetExists", new object[3]
		{
			(object)Context,
			androidPackageName,
			androidClassName
		});
	}

	public static bool FindTarget(out string androidPackageName, out string androidClassName, string packageNameRegex, string classNameRegex = null)
	{
		androidPackageName = null;
		androidClassName = null;
		if (string.IsNullOrEmpty(packageNameRegex))
		{
			return false;
		}
		if (classNameRegex == null)
		{
			classNameRegex = string.Empty;
		}
		string text = AJC.CallStatic<string>("FindMatchingTarget", new object[3]
		{
			(object)Context,
			packageNameRegex,
			classNameRegex
		});
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		int num = text.IndexOf('>');
		if (num <= 0 || num >= text.Length - 1)
		{
			return false;
		}
		androidPackageName = text.Substring(0, num);
		androidClassName = text.Substring(num + 1);
		return true;
	}
}
