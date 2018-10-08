using GooglePlayGames.Android;
using GooglePlayGames.BasicApi;
using GooglePlayGames.Native;
using GooglePlayGames.OurUtils;
using UnityEngine;

namespace GooglePlayGames
{
	internal class PlayGamesClientFactory
	{
		internal static IPlayGamesClient GetPlatformPlayGamesClient(PlayGamesClientConfiguration config)
		{
			if (Application.get_isEditor())
			{
				Logger.d("Creating IPlayGamesClient in editor, using DummyClient.");
				return new DummyClient();
			}
			Logger.d("Creating Android IPlayGamesClient Client");
			return new NativeClient(config, new AndroidClient());
		}
	}
}
