package com.facebook.marketing.internal;

import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.view.View.AccessibilityDelegate;
import com.facebook.FacebookSdk;
import com.facebook.appevents.codeless.CodelessLoggingEventListener.AutoLoggingAccessibilityDelegate;
import com.facebook.appevents.codeless.internal.ViewHierarchy;

public class ButtonIndexingEventListener {
    /* access modifiers changed from: private */
    public static final String TAG = ButtonIndexingEventListener.class.getCanonicalName();

    public static class ButtonIndexingAccessibilityDelegate extends AutoLoggingAccessibilityDelegate {
        @Nullable
        private AccessibilityDelegate existingDelegate;
        private String mapKey;

        public ButtonIndexingAccessibilityDelegate(View view, String str) {
            if (view != null) {
                this.existingDelegate = ViewHierarchy.getExistingDelegate(view);
                this.mapKey = str;
                this.supportButtonIndexing = true;
            }
        }

        public void sendAccessibilityEvent(final View view, int i) {
            if (i == -1) {
                Log.e(ButtonIndexingEventListener.TAG, "Unsupported action type");
            }
            if (this.existingDelegate != null && !(this.existingDelegate instanceof ButtonIndexingAccessibilityDelegate)) {
                this.existingDelegate.sendAccessibilityEvent(view, i);
            }
            final String str = this.mapKey;
            FacebookSdk.getExecutor().execute(new Runnable() {
                public void run() {
                    ButtonIndexingLogger.logIndexing(FacebookSdk.getApplicationId(), view, str, FacebookSdk.getApplicationContext());
                }
            });
        }
    }

    public static ButtonIndexingAccessibilityDelegate getAccessibilityDelegate(View view, String str) {
        return new ButtonIndexingAccessibilityDelegate(view, str);
    }
}
