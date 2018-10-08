package net.gree.unitywebview;

import android.webkit.JavascriptInterface;
import com.unity3d.player.UnityPlayer;

class WebViewPluginInterface {
    private String mGameObject;

    public WebViewPluginInterface(String str) {
        this.mGameObject = str;
    }

    @JavascriptInterface
    public void call(String str) {
        UnityPlayer.UnitySendMessage(this.mGameObject, "CallFromJS", str);
    }
}
