package net.gogame.gowrap.ui.customtabs.browseractions;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ResolveInfo;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.util.ArrayList;
import java.util.List;

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
    public @interface BrowserActionsItemId {
    }

    @Retention(RetentionPolicy.SOURCE)
    public @interface BrowserActionsUrlType {
    }

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
            this.mMenuItems = new ArrayList();
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
            while (i < arrayList.size()) {
                if (((BrowserActionItem) arrayList.get(i)).getTitle() == null) {
                    throw new IllegalArgumentException("Custom item title is null");
                } else if (((BrowserActionItem) arrayList.get(i)).getAction() == null) {
                    throw new IllegalArgumentException("Custom item action is null");
                } else {
                    this.mMenuItems.add(getBundleFromItem((BrowserActionItem) arrayList.get(i)));
                    i++;
                }
            }
            return this;
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
                for (int i = 0; i < browserActionsIntentHandlers.size(); i++) {
                    if (str.equals(((ResolveInfo) browserActionsIntentHandlers.get(i)).activityInfo.packageName)) {
                        intent.setPackage(str);
                        break;
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

    public static List<BrowserActionItem> parseBrowserActionItems(ArrayList<Bundle> arrayList) {
        List<BrowserActionItem> arrayList2 = new ArrayList();
        int i = 0;
        while (i < arrayList.size()) {
            Bundle bundle = (Bundle) arrayList.get(i);
            String string = bundle.getString(KEY_TITLE);
            PendingIntent pendingIntent = (PendingIntent) bundle.getParcelable(KEY_ACTION);
            Bitmap bitmap = (Bitmap) bundle.getParcelable(KEY_ICON);
            if (string != null && pendingIntent != null) {
                BrowserActionItem browserActionItem = new BrowserActionItem(string, pendingIntent);
                if (bitmap != null) {
                    browserActionItem.setIcon(bitmap);
                }
                arrayList2.add(browserActionItem);
                i++;
            } else if (string != null) {
                throw new IllegalArgumentException("Missing action for item: " + i);
            } else if (pendingIntent != null) {
                throw new IllegalArgumentException("Missing title for item: " + i);
            } else {
                throw new IllegalArgumentException("Missing title and action for item: " + i);
            }
        }
        return arrayList2;
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
