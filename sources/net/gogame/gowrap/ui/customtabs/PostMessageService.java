package net.gogame.gowrap.p019ui.customtabs;

import android.app.Service;
import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import net.gogame.gowrap.p019ui.customtabs.IPostMessageService.Stub;

/* renamed from: net.gogame.gowrap.ui.customtabs.PostMessageService */
public class PostMessageService extends Service {
    private Stub mBinder = new Stub() {
        public void onMessageChannelReady(ICustomTabsCallback iCustomTabsCallback, Bundle bundle) throws RemoteException {
            iCustomTabsCallback.onMessageChannelReady(bundle);
        }

        public void onPostMessage(ICustomTabsCallback iCustomTabsCallback, String str, Bundle bundle) throws RemoteException {
            iCustomTabsCallback.onPostMessage(str, bundle);
        }
    };

    public IBinder onBind(Intent intent) {
        return this.mBinder;
    }
}
