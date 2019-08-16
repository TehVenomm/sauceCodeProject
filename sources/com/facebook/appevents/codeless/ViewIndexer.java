package com.facebook.appevents.codeless;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.graphics.Bitmap.Config;
import android.graphics.Canvas;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.Nullable;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import com.facebook.AccessToken;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import com.facebook.GraphRequest.Callback;
import com.facebook.GraphResponse;
import com.facebook.LoggingBehavior;
import com.facebook.appevents.codeless.internal.Constants;
import com.facebook.appevents.codeless.internal.UnityReflection;
import com.facebook.appevents.codeless.internal.ViewHierarchy;
import com.facebook.appevents.internal.ActivityLifecycleTracker;
import com.facebook.appevents.internal.AppEventUtility;
import com.facebook.internal.InternalSettings;
import com.facebook.internal.Logger;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import java.io.ByteArrayOutputStream;
import java.lang.ref.WeakReference;
import java.util.Locale;
import java.util.Timer;
import java.util.TimerTask;
import java.util.concurrent.Callable;
import java.util.concurrent.FutureTask;
import java.util.concurrent.TimeUnit;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class ViewIndexer {
    private static final String APP_VERSION_PARAM = "app_version";
    private static final String PLATFORM_PARAM = "platform";
    private static final String REQUEST_TYPE = "request_type";
    private static final String SUCCESS = "success";
    /* access modifiers changed from: private */
    public static final String TAG = ViewIndexer.class.getCanonicalName();
    private static final String TREE_PARAM = "tree";
    private static ViewIndexer instance;
    private WeakReference<Activity> activityReference;
    /* access modifiers changed from: private */
    public Timer indexingTimer;
    /* access modifiers changed from: private */
    public String previousDigest = null;
    /* access modifiers changed from: private */
    public final Handler uiThreadHandler = new Handler(Looper.getMainLooper());

    private static class ScreenshotTaker implements Callable<String> {
        private WeakReference<View> rootView;

        public ScreenshotTaker(View view) {
            this.rootView = new WeakReference<>(view);
        }

        public String call() throws Exception {
            View view = (View) this.rootView.get();
            if (view == null || view.getWidth() == 0 || view.getHeight() == 0) {
                return "";
            }
            Bitmap createBitmap = Bitmap.createBitmap(view.getWidth(), view.getHeight(), Config.RGB_565);
            view.draw(new Canvas(createBitmap));
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            createBitmap.compress(CompressFormat.JPEG, 10, byteArrayOutputStream);
            return Base64.encodeToString(byteArrayOutputStream.toByteArray(), 2);
        }
    }

    public ViewIndexer(Activity activity) {
        this.activityReference = new WeakReference<>(activity);
        instance = this;
    }

    @Nullable
    public static GraphRequest buildAppIndexingRequest(String str, AccessToken accessToken, String str2, String str3) {
        if (str == null) {
            return null;
        }
        GraphRequest newPostRequest = GraphRequest.newPostRequest(accessToken, String.format(Locale.US, "%s/app_indexing", new Object[]{str2}), null, null);
        Bundle parameters = newPostRequest.getParameters();
        if (parameters == null) {
            parameters = new Bundle();
        }
        parameters.putString(TREE_PARAM, str);
        parameters.putString(APP_VERSION_PARAM, AppEventUtility.getAppVersion());
        parameters.putString(PLATFORM_PARAM, "android");
        parameters.putString(REQUEST_TYPE, str3);
        if (str3.equals(Constants.APP_INDEXING)) {
            parameters.putString(Constants.DEVICE_SESSION_ID, ActivityLifecycleTracker.getCurrentDeviceSessionID());
        }
        newPostRequest.setParameters(parameters);
        newPostRequest.setCallback(new Callback() {
            public void onCompleted(GraphResponse graphResponse) {
                Logger.log(LoggingBehavior.APP_EVENTS, ViewIndexer.TAG, "App index sent to FB!");
            }
        });
        return newPostRequest;
    }

    /* access modifiers changed from: private */
    public void sendToServer(final String str, String str2) {
        FacebookSdk.getExecutor().execute(new Runnable() {
            public void run() {
                String applicationId = FacebookSdk.getApplicationId();
                String md5hash = Utility.md5hash(str);
                AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
                if (md5hash == null || !md5hash.equals(ViewIndexer.this.previousDigest)) {
                    GraphRequest buildAppIndexingRequest = ViewIndexer.buildAppIndexingRequest(str, currentAccessToken, applicationId, Constants.APP_INDEXING);
                    if (buildAppIndexingRequest != null) {
                        GraphResponse executeAndWait = buildAppIndexingRequest.executeAndWait();
                        try {
                            JSONObject jSONObject = executeAndWait.getJSONObject();
                            if (jSONObject != null) {
                                if (jSONObject.has("success") && jSONObject.getString("success") == ServerProtocol.DIALOG_RETURN_SCOPES_TRUE) {
                                    Logger.log(LoggingBehavior.APP_EVENTS, ViewIndexer.TAG, "Successfully send UI component tree to server");
                                    ViewIndexer.this.previousDigest = md5hash;
                                }
                                if (jSONObject.has(Constants.APP_INDEXING_ENABLED)) {
                                    ActivityLifecycleTracker.updateAppIndexing(Boolean.valueOf(jSONObject.getBoolean(Constants.APP_INDEXING_ENABLED)));
                                    return;
                                }
                                return;
                            }
                            Log.e(ViewIndexer.TAG, "Error sending UI component tree to Facebook: " + executeAndWait.getError());
                        } catch (JSONException e) {
                            Log.e(ViewIndexer.TAG, "Error decoding server response.", e);
                        }
                    }
                }
            }
        });
    }

    public static void sendToServerUnityInstance(String str) {
        if (instance != null) {
            instance.sendToServerUnity(str);
        }
    }

    public void schedule() {
        final Activity activity = (Activity) this.activityReference.get();
        if (activity != null) {
            final String simpleName = activity.getClass().getSimpleName();
            FacebookSdk.getApplicationId();
            final C05931 r2 = new TimerTask() {
                public void run() {
                    String str;
                    try {
                        View rootView = activity.getWindow().getDecorView().getRootView();
                        if (ActivityLifecycleTracker.getIsAppIndexingEnabled()) {
                            if (InternalSettings.isUnityApp()) {
                                UnityReflection.captureViewHierarchy();
                                return;
                            }
                            FutureTask futureTask = new FutureTask(new ScreenshotTaker(rootView));
                            ViewIndexer.this.uiThreadHandler.post(futureTask);
                            String str2 = "";
                            try {
                                str = (String) futureTask.get(1, TimeUnit.SECONDS);
                            } catch (Exception e) {
                                Log.e(ViewIndexer.TAG, "Failed to take screenshot.", e);
                                str = str2;
                            }
                            JSONObject jSONObject = new JSONObject();
                            try {
                                jSONObject.put("screenname", simpleName);
                                jSONObject.put("screenshot", str);
                                JSONArray jSONArray = new JSONArray();
                                jSONArray.put(ViewHierarchy.getDictionaryOfView(rootView));
                                jSONObject.put("view", jSONArray);
                            } catch (JSONException e2) {
                                Log.e(ViewIndexer.TAG, "Failed to create JSONObject");
                            }
                            ViewIndexer.this.sendToServer(jSONObject.toString(), simpleName);
                        }
                    } catch (Exception e3) {
                        Log.e(ViewIndexer.TAG, "UI Component tree indexing failure!", e3);
                    }
                }
            };
            FacebookSdk.getExecutor().execute(new Runnable() {
                public void run() {
                    try {
                        if (ViewIndexer.this.indexingTimer != null) {
                            ViewIndexer.this.indexingTimer.cancel();
                        }
                        ViewIndexer.this.previousDigest = null;
                        ViewIndexer.this.indexingTimer = new Timer();
                        ViewIndexer.this.indexingTimer.scheduleAtFixedRate(r2, 0, 1000);
                    } catch (Exception e) {
                        Log.e(ViewIndexer.TAG, "Error scheduling indexing job", e);
                    }
                }
            });
        }
    }

    @Deprecated
    public void sendToServerUnity(String str) {
        Activity activity = (Activity) this.activityReference.get();
        instance.sendToServer(str, activity != null ? activity.getClass().getSimpleName() : "");
    }

    public void unschedule() {
        if (((Activity) this.activityReference.get()) != null && this.indexingTimer != null) {
            try {
                this.indexingTimer.cancel();
                this.indexingTimer = null;
            } catch (Exception e) {
                Log.e(TAG, "Error unscheduling indexing job", e);
            }
        }
    }
}
