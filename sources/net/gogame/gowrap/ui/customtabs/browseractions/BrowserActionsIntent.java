package net.gogame.gowrap.p019ui.customtabs.browseractions;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.ArrayList;
import java.util.List;

/* renamed from: net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionsIntent */
public class BrowserActionsIntent {
    public static final String ACTION_BROWSER_ACTIONS_OPEN = "android.support.customtabs.browseractions.browser_action_open";
    public static final String EXTRA_APP_ID = "android.support.customtabs.browseractions.APP_ID";
    public static final String EXTRA_MENU_ITEMS = "android.support.customtabs.browseractions.extra.MENU_ITEMS";
    public static final String EXTRA_SELECTED_ACTION_PENDING_INTENT = "android.support.customtabs.browseractions.extra.SELECTED_ACTION_PENDING_INTENT";
    public static final String EXTRA_TYPE = "android.support.customtabs.browseractions.extra.TYPE";
    public static final int ITEM_COPY = 3;
    public static final int ITEM_DOWNLOAD = 2;
    public static final int ITEM_INVALID_ITEM = -1;
    public static final int ITEM_OPEN_IN_INCOGNITO = 1;
    public static final int ITEM_OPEN_IN_NEW_TAB = 0;
    public static final int ITEM_SHARE = 4;
    public static final String KEY_ACTION = "android.support.customtabs.browseractions.ACTION";
    public static final String KEY_ICON = "android.support.customtabs.browseractions.ICON";
    public static final String KEY_TITLE = "android.support.customtabs.browseractions.TITLE";
    public static final int MAX_CUSTOM_ITEMS = 5;
    private static final String TAG = "BrowserActions";
    private static final String TEST_URL = "https://www.example.com";
    public static final int URL_TYPE_AUDIO = 3;
    public static final int URL_TYPE_FILE = 4;
    public static final int URL_TYPE_IMAGE = 1;
    public static final int URL_TYPE_NONE = 0;
    public static final int URL_TYPE_PLUGIN = 5;
    public static final int URL_TYPE_VIDEO = 2;
    public final Intent mIntent;

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionsIntent$BrowserActionsItemId */
    public @interface BrowserActionsItemId {
    }

    @Retention(RetentionPolicy.SOURCE)
    /* renamed from: net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionsIntent$BrowserActionsUrlType */
    public @interface BrowserActionsUrlType {
    }

    /* renamed from: net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionsIntent$Builder */
    public static final class Builder {
        private Context mContext;
        private final Intent mIntent = new Intent(BrowserActionsIntent.ACTION_BROWSER_ACTIONS_OPEN);
        private ArrayList<Bundle> mMenuItems = null;
        private PendingIntent mOnItemSelectedPendingIntent = null;
        private int mType;
        private Uri mUri;

        public Builder(Context context, Uri uri) {
            this.mContext = context;
            this.mUri = uri;
            this.mType = 0;
            this.mMenuItems = new ArrayList<>();
        }

        public Builder setUrlType(int i) {
            this.mType = i;
            return this;
        }

        public Builder setCustomItems(ArrayList<BrowserActionItem> arrayList) {
            if (arrayList.size() >= 5) {
                throw new IllegalStateException("Exceeded maximum toolbar item count of 5");
            }
            int i = 0;
            while (true) {
                int i2 = i;
                if (i2 >= arrayList.size()) {
                    return this;
                }
                if (((BrowserActionItem) arrayList.get(i2)).getTitle() == null) {
                    throw new IllegalArgumentException("Custom item title is null");
                } else if (((BrowserActionItem) arrayList.get(i2)).getAction() == null) {
                    throw new IllegalArgumentException("Custom item action is null");
                } else {
                    this.mMenuItems.add(getBundleFromItem((BrowserActionItem) arrayList.get(i2)));
                    i = i2 + 1;
                }
            }
        }

        public Builder setOnItemSelectedAction(PendingIntent pendingIntent) {
            this.mOnItemSelectedPendingIntent = pendingIntent;
            return this;
        }

        private Bundle getBundleFromItem(BrowserActionItem browserActionItem) {
            Bundle bundle = new Bundle();
            bundle.putString(BrowserActionsIntent.KEY_TITLE, browserActionItem.getTitle());
            bundle.putParcelable(BrowserActionsIntent.KEY_ACTION, browserActionItem.getAction());
            if (browserActionItem.getIcon() != null) {
                bundle.putParcelable(BrowserActionsIntent.KEY_ICON, browserActionItem.getIcon());
            }
            return bundle;
        }

        public BrowserActionsIntent build() {
            this.mIntent.setData(this.mUri);
            this.mIntent.putExtra(BrowserActionsIntent.EXTRA_TYPE, this.mType);
            this.mIntent.putParcelableArrayListExtra(BrowserActionsIntent.EXTRA_MENU_ITEMS, this.mMenuItems);
            this.mIntent.putExtra(BrowserActionsIntent.EXTRA_APP_ID, PendingIntent.getActivity(this.mContext, 0, new Intent(), 0));
            this.mIntent.putExtra(BrowserActionsIntent.EXTRA_SELECTED_ACTION_PENDING_INTENT, this.mOnItemSelectedPendingIntent);
            return new BrowserActionsIntent(this.mIntent);
        }
    }

    public Intent getIntent() {
        return this.mIntent;
    }

    private BrowserActionsIntent(Intent intent) {
        this.mIntent = intent;
    }

    public static void openBrowserAction(Context context, Uri uri) {
        launchIntent(context, new Builder(context, uri).build().getIntent());
    }

    public static void openBrowserAction(Context context, Uri uri, int i, ArrayList<BrowserActionItem> arrayList, PendingIntent pendingIntent) {
        launchIntent(context, new Builder(context, uri).setUrlType(i).setCustomItems(arrayList).setOnItemSelectedAction(pendingIntent).build().getIntent());
    }

    public static void launchIntent(Context context, Intent intent) {
        int i = 0;
        List browserActionsIntentHandlers = getBrowserActionsIntentHandlers(context);
        if (browserActionsIntentHandlers == null || browserActionsIntentHandlers.size() == 0) {
            openFallbackBrowserActionsMenu(context, intent);
            return;
        }
        if (browserActionsIntentHandlers.size() == 1) {
            intent.setPackage(((ResolveInfo) browserActionsIntentHandlers.get(0)).activityInfo.packageName);
        } else {
            ResolveInfo resolveActivity = context.getPackageManager().resolveActivity(new Intent("android.intent.action.VIEW", Uri.parse(TEST_URL)), 65536);
            if (resolveActivity != null) {
                String str = resolveActivity.activityInfo.packageName;
                while (true) {
                    int i2 = i;
                    if (i2 >= browserActionsIntentHandlers.size()) {
                        break;
                    } else if (str.equals(((ResolveInfo) browserActionsIntentHandlers.get(i2)).activityInfo.packageName)) {
                        intent.setPackage(str);
                        break;
                    } else {
                        i = i2 + 1;
                    }
                }
            }
        }
        context.startActivity(intent, null);
    }

    private static List<ResolveInfo> getBrowserActionsIntentHandlers(Context context) {
        return context.getPackageManager().queryIntentActivities(new Intent(ACTION_BROWSER_ACTIONS_OPEN, Uri.parse(TEST_URL)), 131072);
    }

    private static void openFallbackBrowserActionsMenu(Context context, BrowserActionsIntent browserActionsIntent) {
        openFallbackBrowserActionsMenu(context, browserActionsIntent.getIntent());
    }

    private static void openFallbackBrowserActionsMenu(Context context, Intent intent) {
        Uri data = intent.getData();
        int intExtra = intent.getIntExtra(EXTRA_TYPE, 0);
        ArrayList parcelableArrayListExtra = intent.getParcelableArrayListExtra(EXTRA_MENU_ITEMS);
        openFallbackBrowserActionsMenu(context, data, intExtra, parcelableArrayListExtra != null ? parseBrowserActionItems(parcelableArrayListExtra) : null);
    }

    private static void openFallbackBrowserActionsMenu(Context context, Uri uri, int i, List<BrowserActionItem> list) {
    }

    /* JADX WARNING: Removed duplicated region for block: B:11:0x0040  */
    /* JADX WARNING: Removed duplicated region for block: B:13:0x0059  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.util.List<net.gogame.gowrap.p019ui.customtabs.browseractions.BrowserActionItem> parseBrowserActionItems(java.util.ArrayList<android.os.Bundle> r6) {
        /*
            java.util.ArrayList r3 = new java.util.ArrayList
            r3.<init>()
            r0 = 0
            r2 = r0
        L_0x0007:
            int r0 = r6.size()
            if (r2 >= r0) goto L_0x008d
            java.lang.Object r0 = r6.get(r2)
            android.os.Bundle r0 = (android.os.Bundle) r0
            java.lang.String r1 = "android.support.customtabs.browseractions.TITLE"
            java.lang.String r4 = r0.getString(r1)
            java.lang.String r1 = "android.support.customtabs.browseractions.ACTION"
            android.os.Parcelable r1 = r0.getParcelable(r1)
            android.app.PendingIntent r1 = (android.app.PendingIntent) r1
            java.lang.String r5 = "android.support.customtabs.browseractions.ICON"
            android.os.Parcelable r0 = r0.getParcelable(r5)
            android.graphics.Bitmap r0 = (android.graphics.Bitmap) r0
            if (r4 == 0) goto L_0x003e
            if (r1 == 0) goto L_0x003e
            net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionItem r5 = new net.gogame.gowrap.ui.customtabs.browseractions.BrowserActionItem
            r5.<init>(r4, r1)
            if (r0 == 0) goto L_0x0037
            r5.setIcon(r0)
        L_0x0037:
            r3.add(r5)
            int r0 = r2 + 1
            r2 = r0
            goto L_0x0007
        L_0x003e:
            if (r4 == 0) goto L_0x0059
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r3 = "Missing action for item: "
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0059:
            if (r1 == 0) goto L_0x0074
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r3 = "Missing title for item: "
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x0074:
            java.lang.IllegalArgumentException r0 = new java.lang.IllegalArgumentException
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r1.<init>()
            java.lang.String r3 = "Missing title and action for item: "
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.StringBuilder r1 = r1.append(r2)
            java.lang.String r1 = r1.toString()
            r0.<init>(r1)
            throw r0
        L_0x008d:
            return r3
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.p019ui.customtabs.browseractions.BrowserActionsIntent.parseBrowserActionItems(java.util.ArrayList):java.util.List");
    }

    public static String getCreatorPackageName(Intent intent) {
        PendingIntent pendingIntent = (PendingIntent) intent.getParcelableExtra(EXTRA_APP_ID);
        if (pendingIntent == null) {
            return null;
        }
        if (VERSION.SDK_INT >= 17) {
            return pendingIntent.getCreatorPackage();
        }
        return pendingIntent.getTargetPackage();
    }
}
