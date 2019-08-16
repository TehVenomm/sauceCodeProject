package android.support.p000v4.app;

import android.app.Notification;
import android.app.Notification.BigPictureStyle;
import android.app.Notification.BigTextStyle;
import android.app.Notification.InboxStyle;
import android.app.PendingIntent;
import android.content.Context;
import android.graphics.Bitmap;
import android.os.Bundle;
import android.os.Parcelable;
import android.support.annotation.RequiresApi;
import android.support.p000v4.app.NotificationCompatBase.Action;
import android.support.p000v4.app.NotificationCompatBase.Action.Factory;
import android.support.p000v4.app.RemoteInputCompatBase.RemoteInput;
import android.util.Log;
import android.util.SparseArray;
import android.widget.RemoteViews;
import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

@RequiresApi(16)
/* renamed from: android.support.v4.app.NotificationCompatJellybean */
class NotificationCompatJellybean {
    static final String EXTRA_ALLOW_GENERATED_REPLIES = "android.support.allowGeneratedReplies";
    static final String EXTRA_DATA_ONLY_REMOTE_INPUTS = "android.support.dataRemoteInputs";
    private static final String KEY_ACTION_INTENT = "actionIntent";
    private static final String KEY_DATA_ONLY_REMOTE_INPUTS = "dataOnlyRemoteInputs";
    private static final String KEY_EXTRAS = "extras";
    private static final String KEY_ICON = "icon";
    private static final String KEY_REMOTE_INPUTS = "remoteInputs";
    private static final String KEY_TITLE = "title";
    public static final String TAG = "NotificationCompat";
    private static Class<?> sActionClass;
    private static Field sActionIconField;
    private static Field sActionIntentField;
    private static Field sActionTitleField;
    private static boolean sActionsAccessFailed;
    private static Field sActionsField;
    private static final Object sActionsLock = new Object();
    private static Field sExtrasField;
    private static boolean sExtrasFieldAccessFailed;
    private static final Object sExtrasLock = new Object();

    /* renamed from: android.support.v4.app.NotificationCompatJellybean$Builder */
    public static class Builder implements NotificationBuilderWithBuilderAccessor, NotificationBuilderWithActions {

        /* renamed from: b */
        private android.app.Notification.Builder f15b;
        private List<Bundle> mActionExtrasList = new ArrayList();
        private RemoteViews mBigContentView;
        private RemoteViews mContentView;
        private final Bundle mExtras;

        public Builder(Context context, Notification notification, CharSequence charSequence, CharSequence charSequence2, CharSequence charSequence3, RemoteViews remoteViews, int i, PendingIntent pendingIntent, PendingIntent pendingIntent2, Bitmap bitmap, int i2, int i3, boolean z, boolean z2, int i4, CharSequence charSequence4, boolean z3, Bundle bundle, String str, boolean z4, String str2, RemoteViews remoteViews2, RemoteViews remoteViews3) {
            this.f15b = new android.app.Notification.Builder(context).setWhen(notification.when).setSmallIcon(notification.icon, notification.iconLevel).setContent(notification.contentView).setTicker(notification.tickerText, remoteViews).setSound(notification.sound, notification.audioStreamType).setVibrate(notification.vibrate).setLights(notification.ledARGB, notification.ledOnMS, notification.ledOffMS).setOngoing((notification.flags & 2) != 0).setOnlyAlertOnce((notification.flags & 8) != 0).setAutoCancel((notification.flags & 16) != 0).setDefaults(notification.defaults).setContentTitle(charSequence).setContentText(charSequence2).setSubText(charSequence4).setContentInfo(charSequence3).setContentIntent(pendingIntent).setDeleteIntent(notification.deleteIntent).setFullScreenIntent(pendingIntent2, (notification.flags & 128) != 0).setLargeIcon(bitmap).setNumber(i).setUsesChronometer(z2).setPriority(i4).setProgress(i2, i3, z);
            this.mExtras = new Bundle();
            if (bundle != null) {
                this.mExtras.putAll(bundle);
            }
            if (z3) {
                this.mExtras.putBoolean(NotificationCompatExtras.EXTRA_LOCAL_ONLY, true);
            }
            if (str != null) {
                this.mExtras.putString(NotificationCompatExtras.EXTRA_GROUP_KEY, str);
                if (z4) {
                    this.mExtras.putBoolean(NotificationCompatExtras.EXTRA_GROUP_SUMMARY, true);
                } else {
                    this.mExtras.putBoolean(NotificationManagerCompat.EXTRA_USE_SIDE_CHANNEL, true);
                }
            }
            if (str2 != null) {
                this.mExtras.putString(NotificationCompatExtras.EXTRA_SORT_KEY, str2);
            }
            this.mContentView = remoteViews2;
            this.mBigContentView = remoteViews3;
        }

        public void addAction(Action action) {
            this.mActionExtrasList.add(NotificationCompatJellybean.writeActionAndGetExtras(this.f15b, action));
        }

        public Notification build() {
            Notification build = this.f15b.build();
            Bundle extras = NotificationCompatJellybean.getExtras(build);
            Bundle bundle = new Bundle(this.mExtras);
            for (String str : this.mExtras.keySet()) {
                if (extras.containsKey(str)) {
                    bundle.remove(str);
                }
            }
            extras.putAll(bundle);
            SparseArray buildActionExtrasMap = NotificationCompatJellybean.buildActionExtrasMap(this.mActionExtrasList);
            if (buildActionExtrasMap != null) {
                NotificationCompatJellybean.getExtras(build).putSparseParcelableArray(NotificationCompatExtras.EXTRA_ACTION_EXTRAS, buildActionExtrasMap);
            }
            if (this.mContentView != null) {
                build.contentView = this.mContentView;
            }
            if (this.mBigContentView != null) {
                build.bigContentView = this.mBigContentView;
            }
            return build;
        }

        public android.app.Notification.Builder getBuilder() {
            return this.f15b;
        }
    }

    NotificationCompatJellybean() {
    }

    public static void addBigPictureStyle(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor, CharSequence charSequence, boolean z, CharSequence charSequence2, Bitmap bitmap, Bitmap bitmap2, boolean z2) {
        BigPictureStyle bigPicture = new BigPictureStyle(notificationBuilderWithBuilderAccessor.getBuilder()).setBigContentTitle(charSequence).bigPicture(bitmap);
        if (z2) {
            bigPicture.bigLargeIcon(bitmap2);
        }
        if (z) {
            bigPicture.setSummaryText(charSequence2);
        }
    }

    public static void addBigTextStyle(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor, CharSequence charSequence, boolean z, CharSequence charSequence2, CharSequence charSequence3) {
        BigTextStyle bigText = new BigTextStyle(notificationBuilderWithBuilderAccessor.getBuilder()).setBigContentTitle(charSequence).bigText(charSequence3);
        if (z) {
            bigText.setSummaryText(charSequence2);
        }
    }

    public static void addInboxStyle(NotificationBuilderWithBuilderAccessor notificationBuilderWithBuilderAccessor, CharSequence charSequence, boolean z, CharSequence charSequence2, ArrayList<CharSequence> arrayList) {
        InboxStyle bigContentTitle = new InboxStyle(notificationBuilderWithBuilderAccessor.getBuilder()).setBigContentTitle(charSequence);
        if (z) {
            bigContentTitle.setSummaryText(charSequence2);
        }
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            bigContentTitle.addLine((CharSequence) it.next());
        }
    }

    public static SparseArray<Bundle> buildActionExtrasMap(List<Bundle> list) {
        SparseArray<Bundle> sparseArray = null;
        int size = list.size();
        for (int i = 0; i < size; i++) {
            Bundle bundle = (Bundle) list.get(i);
            if (bundle != null) {
                if (sparseArray == null) {
                    sparseArray = new SparseArray<>();
                }
                sparseArray.put(i, bundle);
            }
        }
        return sparseArray;
    }

    private static boolean ensureActionReflectionReadyLocked() {
        if (sActionsAccessFailed) {
            return false;
        }
        try {
            if (sActionsField == null) {
                sActionClass = Class.forName("android.app.Notification$Action");
                sActionIconField = sActionClass.getDeclaredField("icon");
                sActionTitleField = sActionClass.getDeclaredField("title");
                sActionIntentField = sActionClass.getDeclaredField(KEY_ACTION_INTENT);
                sActionsField = Notification.class.getDeclaredField("actions");
                sActionsField.setAccessible(true);
            }
        } catch (ClassNotFoundException e) {
            Log.e(TAG, "Unable to access notification actions", e);
            sActionsAccessFailed = true;
        } catch (NoSuchFieldException e2) {
            Log.e(TAG, "Unable to access notification actions", e2);
            sActionsAccessFailed = true;
        }
        return !sActionsAccessFailed;
    }

    /* JADX WARNING: No exception handlers in catch block: Catch:{  } */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static android.support.p000v4.app.NotificationCompatBase.Action getAction(android.app.Notification r8, int r9, android.support.p000v4.app.NotificationCompatBase.Action.Factory r10, android.support.p000v4.app.RemoteInputCompatBase.RemoteInput.Factory r11) {
        /*
            r6 = 0
            java.lang.Object r7 = sActionsLock
            monitor-enter(r7)
            java.lang.Object[] r0 = getActionObjectsLocked(r8)     // Catch:{ IllegalAccessException -> 0x003f }
            if (r0 == 0) goto L_0x004a
            r1 = r0[r9]
            android.os.Bundle r0 = getExtras(r8)     // Catch:{ IllegalAccessException -> 0x003f }
            if (r0 == 0) goto L_0x0050
            java.lang.String r2 = "android.support.actionExtras"
            android.util.SparseArray r0 = r0.getSparseParcelableArray(r2)     // Catch:{ IllegalAccessException -> 0x003f }
            if (r0 == 0) goto L_0x0050
            java.lang.Object r0 = r0.get(r9)     // Catch:{ IllegalAccessException -> 0x003f }
            android.os.Bundle r0 = (android.os.Bundle) r0     // Catch:{ IllegalAccessException -> 0x003f }
            r5 = r0
        L_0x0021:
            java.lang.reflect.Field r0 = sActionIconField     // Catch:{ IllegalAccessException -> 0x003f }
            int r2 = r0.getInt(r1)     // Catch:{ IllegalAccessException -> 0x003f }
            java.lang.reflect.Field r0 = sActionTitleField     // Catch:{ IllegalAccessException -> 0x003f }
            java.lang.Object r3 = r0.get(r1)     // Catch:{ IllegalAccessException -> 0x003f }
            java.lang.CharSequence r3 = (java.lang.CharSequence) r3     // Catch:{ IllegalAccessException -> 0x003f }
            java.lang.reflect.Field r0 = sActionIntentField     // Catch:{ IllegalAccessException -> 0x003f }
            java.lang.Object r4 = r0.get(r1)     // Catch:{ IllegalAccessException -> 0x003f }
            android.app.PendingIntent r4 = (android.app.PendingIntent) r4     // Catch:{ IllegalAccessException -> 0x003f }
            r0 = r10
            r1 = r11
            android.support.v4.app.NotificationCompatBase$Action r0 = readAction(r0, r1, r2, r3, r4, r5)     // Catch:{ IllegalAccessException -> 0x003f }
            monitor-exit(r7)     // Catch:{ all -> 0x004d }
        L_0x003e:
            return r0
        L_0x003f:
            r0 = move-exception
            java.lang.String r1 = "NotificationCompat"
            java.lang.String r2 = "Unable to access notification actions"
            android.util.Log.e(r1, r2, r0)     // Catch:{ all -> 0x004d }
            r0 = 1
            sActionsAccessFailed = r0     // Catch:{ all -> 0x004d }
        L_0x004a:
            monitor-exit(r7)     // Catch:{ all -> 0x004d }
            r0 = r6
            goto L_0x003e
        L_0x004d:
            r0 = move-exception
            monitor-exit(r7)     // Catch:{ all -> 0x004d }
            throw r0
        L_0x0050:
            r5 = r6
            goto L_0x0021
        */
        throw new UnsupportedOperationException("Method not decompiled: android.support.p000v4.app.NotificationCompatJellybean.getAction(android.app.Notification, int, android.support.v4.app.NotificationCompatBase$Action$Factory, android.support.v4.app.RemoteInputCompatBase$RemoteInput$Factory):android.support.v4.app.NotificationCompatBase$Action");
    }

    public static int getActionCount(Notification notification) {
        int i;
        synchronized (sActionsLock) {
            Object[] actionObjectsLocked = getActionObjectsLocked(notification);
            i = actionObjectsLocked != null ? actionObjectsLocked.length : 0;
        }
        return i;
    }

    private static Action getActionFromBundle(Bundle bundle, Factory factory, RemoteInput.Factory factory2) {
        boolean z = false;
        Bundle bundle2 = bundle.getBundle("extras");
        if (bundle2 != null) {
            z = bundle2.getBoolean(EXTRA_ALLOW_GENERATED_REPLIES, false);
        }
        return factory.build(bundle.getInt("icon"), bundle.getCharSequence("title"), (PendingIntent) bundle.getParcelable(KEY_ACTION_INTENT), bundle.getBundle("extras"), RemoteInputCompatJellybean.fromBundleArray(BundleUtil.getBundleArrayFromBundle(bundle, KEY_REMOTE_INPUTS), factory2), RemoteInputCompatJellybean.fromBundleArray(BundleUtil.getBundleArrayFromBundle(bundle, KEY_DATA_ONLY_REMOTE_INPUTS), factory2), z);
    }

    private static Object[] getActionObjectsLocked(Notification notification) {
        synchronized (sActionsLock) {
            if (!ensureActionReflectionReadyLocked()) {
                return null;
            }
            try {
                Object[] objArr = (Object[]) sActionsField.get(notification);
                return objArr;
            } catch (IllegalAccessException e) {
                Log.e(TAG, "Unable to access notification actions", e);
                sActionsAccessFailed = true;
                return null;
            }
        }
    }

    public static Action[] getActionsFromParcelableArrayList(ArrayList<Parcelable> arrayList, Factory factory, RemoteInput.Factory factory2) {
        if (arrayList == null) {
            return null;
        }
        Action[] newArray = factory.newArray(arrayList.size());
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= newArray.length) {
                return newArray;
            }
            newArray[i2] = getActionFromBundle((Bundle) arrayList.get(i2), factory, factory2);
            i = i2 + 1;
        }
    }

    private static Bundle getBundleForAction(Action action) {
        Bundle bundle = new Bundle();
        bundle.putInt("icon", action.getIcon());
        bundle.putCharSequence("title", action.getTitle());
        bundle.putParcelable(KEY_ACTION_INTENT, action.getActionIntent());
        Bundle bundle2 = action.getExtras() != null ? new Bundle(action.getExtras()) : new Bundle();
        bundle2.putBoolean(EXTRA_ALLOW_GENERATED_REPLIES, action.getAllowGeneratedReplies());
        bundle.putBundle("extras", bundle2);
        bundle.putParcelableArray(KEY_REMOTE_INPUTS, RemoteInputCompatJellybean.toBundleArray(action.getRemoteInputs()));
        return bundle;
    }

    public static Bundle getExtras(Notification notification) {
        synchronized (sExtrasLock) {
            if (sExtrasFieldAccessFailed) {
                return null;
            }
            try {
                if (sExtrasField == null) {
                    Field declaredField = Notification.class.getDeclaredField("extras");
                    if (!Bundle.class.isAssignableFrom(declaredField.getType())) {
                        Log.e(TAG, "Notification.extras field is not of type Bundle");
                        sExtrasFieldAccessFailed = true;
                        return null;
                    }
                    declaredField.setAccessible(true);
                    sExtrasField = declaredField;
                }
                Bundle bundle = (Bundle) sExtrasField.get(notification);
                if (bundle == null) {
                    bundle = new Bundle();
                    sExtrasField.set(notification, bundle);
                }
                return bundle;
            } catch (IllegalAccessException e) {
                Log.e(TAG, "Unable to access notification extras", e);
                sExtrasFieldAccessFailed = true;
                return null;
            } catch (NoSuchFieldException e2) {
                Log.e(TAG, "Unable to access notification extras", e2);
                sExtrasFieldAccessFailed = true;
                return null;
            }
        }
    }

    public static ArrayList<Parcelable> getParcelableArrayListForActions(Action[] actionArr) {
        if (actionArr == null) {
            return null;
        }
        ArrayList<Parcelable> arrayList = new ArrayList<>(actionArr.length);
        for (Action bundleForAction : actionArr) {
            arrayList.add(getBundleForAction(bundleForAction));
        }
        return arrayList;
    }

    public static Action readAction(Factory factory, RemoteInput.Factory factory2, int i, CharSequence charSequence, PendingIntent pendingIntent, Bundle bundle) {
        RemoteInput[] remoteInputArr;
        RemoteInput[] remoteInputArr2;
        boolean z = false;
        if (bundle != null) {
            remoteInputArr = RemoteInputCompatJellybean.fromBundleArray(BundleUtil.getBundleArrayFromBundle(bundle, NotificationCompatExtras.EXTRA_REMOTE_INPUTS), factory2);
            remoteInputArr2 = RemoteInputCompatJellybean.fromBundleArray(BundleUtil.getBundleArrayFromBundle(bundle, EXTRA_DATA_ONLY_REMOTE_INPUTS), factory2);
            z = bundle.getBoolean(EXTRA_ALLOW_GENERATED_REPLIES);
        } else {
            remoteInputArr = null;
            remoteInputArr2 = null;
        }
        return factory.build(i, charSequence, pendingIntent, bundle, remoteInputArr, remoteInputArr2, z);
    }

    public static Bundle writeActionAndGetExtras(android.app.Notification.Builder builder, Action action) {
        builder.addAction(action.getIcon(), action.getTitle(), action.getActionIntent());
        Bundle bundle = new Bundle(action.getExtras());
        if (action.getRemoteInputs() != null) {
            bundle.putParcelableArray(NotificationCompatExtras.EXTRA_REMOTE_INPUTS, RemoteInputCompatJellybean.toBundleArray(action.getRemoteInputs()));
        }
        if (action.getDataOnlyRemoteInputs() != null) {
            bundle.putParcelableArray(EXTRA_DATA_ONLY_REMOTE_INPUTS, RemoteInputCompatJellybean.toBundleArray(action.getDataOnlyRemoteInputs()));
        }
        bundle.putBoolean(EXTRA_ALLOW_GENERATED_REPLIES, action.getAllowGeneratedReplies());
        return bundle;
    }
}
