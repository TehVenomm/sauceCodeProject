package net.gogame.gowrap.p019ui.customtabs;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import android.util.Log;
import net.gogame.gowrap.p019ui.customtabs.ICustomTabsCallback.Stub;

/* renamed from: net.gogame.gowrap.ui.customtabs.CustomTabsSessionToken */
public class CustomTabsSessionToken {
    private static final String TAG = "CustomTabsSessionToken";
    private final CustomTabsCallback mCallback = new CustomTabsCallback() {
        public void onNavigationEvent(int i, Bundle bundle) {
            try {
                CustomTabsSessionToken.this.mCallbackBinder.onNavigationEvent(i, bundle);
            } catch (RemoteException e) {
                Log.e(CustomTabsSessionToken.TAG, "RemoteException during ICustomTabsCallback transaction");
            }
        }

        public void extraCallback(String str, Bundle bundle) {
            try {
                CustomTabsSessionToken.this.mCallbackBinder.extraCallback(str, bundle);
            } catch (RemoteException e) {
                Log.e(CustomTabsSessionToken.TAG, "RemoteException during ICustomTabsCallback transaction");
            }
        }

        public void onMessageChannelReady(Bundle bundle) {
            try {
                CustomTabsSessionToken.this.mCallbackBinder.onMessageChannelReady(bundle);
            } catch (RemoteException e) {
                Log.e(CustomTabsSessionToken.TAG, "RemoteException during ICustomTabsCallback transaction");
            }
        }

        public void onPostMessage(String str, Bundle bundle) {
            try {
                CustomTabsSessionToken.this.mCallbackBinder.onPostMessage(str, bundle);
            } catch (RemoteException e) {
                Log.e(CustomTabsSessionToken.TAG, "RemoteException during ICustomTabsCallback transaction");
            }
        }

        public void onRelationshipValidationResult(int i, Uri uri, boolean z, Bundle bundle) {
            try {
                CustomTabsSessionToken.this.mCallbackBinder.onRelationshipValidationResult(i, uri, z, bundle);
            } catch (RemoteException e) {
                Log.e(CustomTabsSessionToken.TAG, "RemoteException during ICustomTabsCallback transaction");
            }
        }
    };
    /* access modifiers changed from: private */
    public final ICustomTabsCallback mCallbackBinder;

    /* renamed from: net.gogame.gowrap.ui.customtabs.CustomTabsSessionToken$DummyCallback */
    static class DummyCallback extends Stub {
        DummyCallback() {
        }

        public void onNavigationEvent(int i, Bundle bundle) {
        }

        public void extraCallback(String str, Bundle bundle) {
        }

        public void onMessageChannelReady(Bundle bundle) {
        }

        public void onPostMessage(String str, Bundle bundle) {
        }

        public void onRelationshipValidationResult(int i, Uri uri, boolean z, Bundle bundle) {
        }

        public IBinder asBinder() {
            return this;
        }
    }

    public static CustomTabsSessionToken getSessionTokenFromIntent(Intent intent) {
        IBinder binder = intent.getExtras().getBinder("android.support.customtabs.extra.SESSION");
        if (binder == null) {
            return null;
        }
        return new CustomTabsSessionToken(Stub.asInterface(binder));
    }

    public static CustomTabsSessionToken createDummySessionTokenForTesting() {
        return new CustomTabsSessionToken(new DummyCallback());
    }

    CustomTabsSessionToken(ICustomTabsCallback iCustomTabsCallback) {
        this.mCallbackBinder = iCustomTabsCallback;
    }

    /* access modifiers changed from: 0000 */
    public IBinder getCallbackBinder() {
        return this.mCallbackBinder.asBinder();
    }

    public int hashCode() {
        return getCallbackBinder().hashCode();
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof CustomTabsSessionToken)) {
            return false;
        }
        return ((CustomTabsSessionToken) obj).getCallbackBinder().equals(this.mCallbackBinder.asBinder());
    }

    public CustomTabsCallback getCallback() {
        return this.mCallback;
    }

    public boolean isAssociatedWith(CustomTabsSession customTabsSession) {
        return customTabsSession.getBinder().equals(this.mCallbackBinder);
    }
}
