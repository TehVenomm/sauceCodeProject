using System;
using UnityEngine;

namespace App.Scripts.GoGame
{
	public class AtlasPackDefinition
	{
		[Serializable]
		public class GoOptDefinition
		{
			public string Name;

			public Shader Shader;

			public TextAsset[] Child;
		}

		public GoOptDefinition[] Defines;

		public AtlasPackDefinition()
			: this()
		{
		}
	}
}
