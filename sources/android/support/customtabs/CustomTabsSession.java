package android.support.customtabs;

import android.app.PendingIntent;
import android.content.ComponentName;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.widget.RemoteViews;
import java.util.List;

public final class CustomTabsSession {
    private static final String TAG = "CustomTabsSession";
    private final ICustomTabsCallback mCallback;
    private final ComponentName mComponentName;
    private final Object mLock = new Object();
    private final ICustomTabsService mService;

    CustomTabsSession(ICustomTabsService iCustomTabsService, ICustomTabsCallback iCustomTabsCallback, ComponentName componentName) {
        this.mService = iCustomTabsService;
        this.mCallback = iCustomTabsCallback;
        this.mComponentName = componentName;
    }

    /* access modifiers changed from: 0000 */
    public IBinder getBinder() {
        return this.mCallback.asBinder();
    }

    /* access modifiers changed from: 0000 */
    public ComponentName getComponentName() {
        return this.mComponentName;
    }

    public boolean mayLaunchUrl(Uri uri, Bundle bundle, List<Bundle> list) {
        try {
            return this.mService.mayLaunchUrl(this.mCallback, uri, bundle, list);
        } catch (RemoteException e) {
            return false;
        }
    }

    public int postMessage(String str, Bundle bundle) {
        int i;
        synchronized (this.mLock) {
            try {
                i = this.mService.postMessage(this.mCallback, str, bundle);
            } catch (RemoteException e) {
                i = -2;
            }
        }
        return i;
    }

    public boolean requestPostMessageChannel(Uri uri) {
        try {
            return this.mService.requestPostMessageChannel(this.mCallback, uri);
        } catch (RemoteException e) {
            return false;
        }
    }

    public boolean setActionButton(@NonNull Bitmap bitmap, @NonNull String str) {
        Bundle bundle = new Bundle();
        bundle.putParcelable("android.support.customtabs.customaction.ICON", bitmap);
        bundle.putString("android.support.customtabs.customaction.DESCRIPTION", str);
        Bundle bundle2 = new Bundle();
        bundle2.putBundle("android.support.customtabs.extra.ACTION_BUTTON_BUNDLE", bundle);
        try {
            return this.mService.updateVisuals(this.mCallback, bundle2);
        } catch (RemoteException e) {
            return false;
        }
    }

    public boolean setSecondaryToolbarViews(@Nullable RemoteViews remoteViews, @Nullable int[] iArr, @Nullable PendingIntent pendingIntent) {
        Bundle bundle = new Bundle();
        bundle.putParcelable("android.support.customtabs.extra.EXTRA_REMOTEVIEWS", remoteViews);
        bundle.putIntArray("android.support.customtabs.extra.EXTRA_REMOTEVIEWS_VIEW_IDS", iArr);
        bundle.putParcelable("android.support.customtabs.extra.EXTRA_REMOTEVIEWS_PENDINGINTENT", pendingIntent);
        try {
            return this.mService.updateVisuals(this.mCallback, bundle);
        } catch (RemoteException e) {
            return false;
        }
    }

    @Deprecated
    public boolean setToolbarItem(int i, @NonNull Bitmap bitmap, @NonNull String str) {
        Bundle bundle = new Bundle();
        bundle.putInt("android.support.customtabs.customaction.ID", i);
        bundle.putParcelable("android.support.customtabs.customaction.ICON", bitmap);
        bundle.putString("android.support.customtabs.customaction.DESCRIPTION", str);
        Bundle bundle2 = new Bundle();
        bundle2.putBundle("android.support.customtabs.extra.ACTION_BUTTON_BUNDLE", bundle);
        try {
            return this.mService.updateVisuals(this.mCallback, bundle2);
        } catch (RemoteException e) {
            return false;
        }
    }
}
