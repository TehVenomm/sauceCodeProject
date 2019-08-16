package com.facebook.marketing.internal;

import android.app.Activity;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.View;
import android.view.View.AccessibilityDelegate;
import android.view.ViewGroup;
import android.view.ViewTreeObserver.OnGlobalLayoutListener;
import android.view.ViewTreeObserver.OnScrollChangedListener;
import android.widget.ImageView;
import android.widget.TextView;
import com.facebook.FacebookException;
import com.facebook.FacebookSdk;
import com.facebook.appevents.codeless.CodelessLoggingEventListener.AutoLoggingAccessibilityDelegate;
import com.facebook.appevents.codeless.internal.ViewHierarchy;
import com.facebook.internal.Utility;
import java.lang.ref.WeakReference;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map.Entry;
import java.util.Set;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class ButtonIndexer {
    /* access modifiers changed from: private */
    public static final String TAG = ButtonIndexer.class.getCanonicalName();
    private Set<Activity> activitiesSet = new HashSet();
    private HashSet<String> delegateSet = new HashSet<>();
    private final Handler uiThreadHandler = new Handler(Looper.getMainLooper());
    private Set<ViewProcessor> viewProcessors = new HashSet();

    protected static class ViewProcessor implements OnGlobalLayoutListener, OnScrollChangedListener, Runnable {
        private static volatile float displayDensity = -1.0f;
        public static volatile Set<String> loadedKeySet = new HashSet();
        private final String activityName;
        private HashSet<String> delegateSet;
        private final Handler handler;
        private WeakReference<View> rootView;
        private HashMap<String, WeakReference<View>> viewMap = new HashMap<>();

        public ViewProcessor(View view, String str, HashSet<String> hashSet, Handler handler2) {
            this.rootView = new WeakReference<>(view);
            this.handler = handler2;
            this.activityName = str;
            this.delegateSet = hashSet;
            if (displayDensity < 0.0f) {
                displayDensity = view.getContext().getResources().getDisplayMetrics().density;
            }
            this.handler.postDelayed(this, 200);
        }

        private void attachListener(View view, String str) {
            if (view != null) {
                try {
                    AccessibilityDelegate existingDelegate = ViewHierarchy.getExistingDelegate(view);
                    boolean z = existingDelegate != null;
                    boolean z2 = z && (existingDelegate instanceof AutoLoggingAccessibilityDelegate);
                    boolean z3 = z2 && ((AutoLoggingAccessibilityDelegate) existingDelegate).getSupportButtonIndexing();
                    if (this.delegateSet.contains(str)) {
                        return;
                    }
                    if (!z || !z2 || !z3) {
                        view.setAccessibilityDelegate(ButtonIndexingEventListener.getAccessibilityDelegate(view, str));
                        this.delegateSet.add(str);
                    }
                } catch (FacebookException e) {
                    Log.e(ButtonIndexer.TAG, "Failed to attach auto logging event listener.", e);
                }
            }
        }

        private void process() {
            View view = (View) this.rootView.get();
            if (view != null) {
                attachListeners(view);
            }
        }

        public void attachListeners(View view) {
            JSONObject clickableElementsOfView = getClickableElementsOfView(view, -1, this.activityName, false);
            if (clickableElementsOfView != null) {
                ButtonIndexingLogger.logAllIndexing(clickableElementsOfView, this.activityName);
            }
            for (Entry entry : this.viewMap.entrySet()) {
                attachListener((View) ((WeakReference) entry.getValue()).get(), (String) entry.getKey());
            }
        }

        @Nullable
        public JSONObject getClickableElementsOfView(View view, int i, String str, boolean z) {
            String str2 = str + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + String.valueOf(i);
            if (view == null) {
                return null;
            }
            JSONObject jSONObject = new JSONObject();
            try {
                boolean isClickableView = ViewHierarchy.isClickableView(view);
                if (isClickableView) {
                    this.viewMap.put(str2, new WeakReference(view));
                }
                if ((!(view instanceof TextView) && !(view instanceof ImageView)) || (!z && !isClickableView)) {
                    JSONArray jSONArray = new JSONArray();
                    if (view instanceof ViewGroup) {
                        ViewGroup viewGroup = (ViewGroup) view;
                        int childCount = viewGroup.getChildCount();
                        for (int i2 = 0; i2 < childCount; i2++) {
                            JSONObject clickableElementsOfView = getClickableElementsOfView(viewGroup.getChildAt(i2), i2, str2, z || isClickableView);
                            if (clickableElementsOfView != null) {
                                jSONArray.put(clickableElementsOfView);
                            }
                        }
                    }
                    if (jSONArray.length() > 0) {
                        JSONObject basicInfoOfView = ViewHierarchy.setBasicInfoOfView(view, jSONObject);
                        basicInfoOfView.put("childviews", jSONArray);
                        return basicInfoOfView;
                    }
                    return null;
                } else if (loadedKeySet.contains(str2)) {
                    return null;
                } else {
                    loadedKeySet.add(str2);
                    return ViewHierarchy.setAppearanceOfView(view, ViewHierarchy.setBasicInfoOfView(view, jSONObject), displayDensity);
                }
            } catch (JSONException e) {
                Utility.logd(ButtonIndexer.TAG, (Exception) e);
            }
        }

        public void onGlobalLayout() {
            process();
        }

        public void onScrollChanged() {
            process();
        }

        public void run() {
            RemoteConfig remoteConfigWithoutQuery = RemoteConfigManager.getRemoteConfigWithoutQuery(FacebookSdk.getApplicationId());
            if (remoteConfigWithoutQuery != null && remoteConfigWithoutQuery.getEnableButtonIndexing()) {
                process();
            }
        }
    }

    /* access modifiers changed from: private */
    public void processViews() {
        for (Activity activity : this.activitiesSet) {
            this.viewProcessors.add(new ViewProcessor(activity.getWindow().getDecorView().getRootView(), activity.getClass().getSimpleName(), this.delegateSet, this.uiThreadHandler));
        }
    }

    private void startTracking() {
        if (Thread.currentThread() == Looper.getMainLooper().getThread()) {
            processViews();
        } else {
            this.uiThreadHandler.post(new Runnable() {
                public void run() {
                    ButtonIndexer.this.processViews();
                }
            });
        }
    }

    public void add(Activity activity) {
        if (Thread.currentThread() != Looper.getMainLooper().getThread()) {
            throw new FacebookException("Can't add activity to ButtonIndexer on non-UI thread");
        }
        this.activitiesSet.add(activity);
        this.delegateSet.clear();
        startTracking();
    }

    public void remove(Activity activity) {
        if (Thread.currentThread() != Looper.getMainLooper().getThread()) {
            throw new FacebookException("Can't remove activity from ButtonIndexer on non-UI thread");
        }
        this.activitiesSet.remove(activity);
        this.viewProcessors.clear();
        this.delegateSet.clear();
    }
}
