package com.facebook.share.internal;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.p000v4.content.LocalBroadcastManager;
import android.util.Log;
import com.facebook.AccessToken;
import com.facebook.AccessTokenTracker;
import com.facebook.FacebookException;
import com.facebook.FacebookOperationCanceledException;
import com.facebook.FacebookRequestError;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import com.facebook.GraphRequest.Callback;
import com.facebook.GraphRequestBatch;
import com.facebook.GraphResponse;
import com.facebook.HttpMethod;
import com.facebook.LoggingBehavior;
import com.facebook.appevents.AppEventsLogger;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.AppCall;
import com.facebook.internal.BundleJSONConverter;
import com.facebook.internal.CallbackManagerImpl;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.internal.FileLruCache;
import com.facebook.internal.FileLruCache.Limits;
import com.facebook.internal.FragmentWrapper;
import com.facebook.internal.Logger;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.PlatformServiceClient.CompletedListener;
import com.facebook.internal.Utility;
import com.facebook.internal.WorkQueue;
import com.facebook.places.model.PlaceFields;
import com.facebook.share.internal.LikeContent.Builder;
import com.facebook.share.widget.LikeView.ObjectType;
import java.io.IOException;
import java.io.OutputStream;
import java.util.ArrayList;
import java.util.Locale;
import java.util.concurrent.ConcurrentHashMap;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

@Deprecated
public class LikeActionController {
    @Deprecated
    public static final String ACTION_LIKE_ACTION_CONTROLLER_DID_ERROR = "com.facebook.sdk.LikeActionController.DID_ERROR";
    @Deprecated
    public static final String ACTION_LIKE_ACTION_CONTROLLER_DID_RESET = "com.facebook.sdk.LikeActionController.DID_RESET";
    @Deprecated
    public static final String ACTION_LIKE_ACTION_CONTROLLER_UPDATED = "com.facebook.sdk.LikeActionController.UPDATED";
    @Deprecated
    public static final String ACTION_OBJECT_ID_KEY = "com.facebook.sdk.LikeActionController.OBJECT_ID";
    private static final int ERROR_CODE_OBJECT_ALREADY_LIKED = 3501;
    @Deprecated
    public static final String ERROR_INVALID_OBJECT_ID = "Invalid Object Id";
    @Deprecated
    public static final String ERROR_PUBLISH_ERROR = "Unable to publish the like/unlike action";
    private static final String JSON_BOOL_IS_OBJECT_LIKED_KEY = "is_object_liked";
    private static final String JSON_BUNDLE_FACEBOOK_DIALOG_ANALYTICS_BUNDLE = "facebook_dialog_analytics_bundle";
    private static final String JSON_INT_OBJECT_TYPE_KEY = "object_type";
    private static final String JSON_INT_VERSION_KEY = "com.facebook.share.internal.LikeActionController.version";
    private static final String JSON_STRING_LIKE_COUNT_WITHOUT_LIKE_KEY = "like_count_string_without_like";
    private static final String JSON_STRING_LIKE_COUNT_WITH_LIKE_KEY = "like_count_string_with_like";
    private static final String JSON_STRING_OBJECT_ID_KEY = "object_id";
    private static final String JSON_STRING_SOCIAL_SENTENCE_WITHOUT_LIKE_KEY = "social_sentence_without_like";
    private static final String JSON_STRING_SOCIAL_SENTENCE_WITH_LIKE_KEY = "social_sentence_with_like";
    private static final String JSON_STRING_UNLIKE_TOKEN_KEY = "unlike_token";
    private static final String LIKE_ACTION_CONTROLLER_STORE = "com.facebook.LikeActionController.CONTROLLER_STORE_KEY";
    private static final String LIKE_ACTION_CONTROLLER_STORE_OBJECT_SUFFIX_KEY = "OBJECT_SUFFIX";
    private static final String LIKE_ACTION_CONTROLLER_STORE_PENDING_OBJECT_ID_KEY = "PENDING_CONTROLLER_KEY";
    private static final int LIKE_ACTION_CONTROLLER_VERSION = 3;
    private static final String LIKE_DIALOG_RESPONSE_LIKE_COUNT_STRING_KEY = "like_count_string";
    private static final String LIKE_DIALOG_RESPONSE_OBJECT_IS_LIKED_KEY = "object_is_liked";
    private static final String LIKE_DIALOG_RESPONSE_SOCIAL_SENTENCE_KEY = "social_sentence";
    private static final String LIKE_DIALOG_RESPONSE_UNLIKE_TOKEN_KEY = "unlike_token";
    private static final int MAX_CACHE_SIZE = 128;
    private static final int MAX_OBJECT_SUFFIX = 1000;
    /* access modifiers changed from: private */
    public static final String TAG = LikeActionController.class.getSimpleName();
    private static AccessTokenTracker accessTokenTracker;
    /* access modifiers changed from: private */
    public static final ConcurrentHashMap<String, LikeActionController> cache = new ConcurrentHashMap<>();
    /* access modifiers changed from: private */
    public static FileLruCache controllerDiskCache;
    private static WorkQueue diskIOWorkQueue = new WorkQueue(1);
    private static Handler handler;
    private static boolean isInitialized;
    private static WorkQueue mruCacheWorkQueue = new WorkQueue(1);
    private static String objectIdForPendingController;
    /* access modifiers changed from: private */
    public static volatile int objectSuffix;
    private AppEventsLogger appEventsLogger;
    private Bundle facebookDialogAnalyticsBundle;
    /* access modifiers changed from: private */
    public boolean isObjectLiked;
    /* access modifiers changed from: private */
    public boolean isObjectLikedOnServer;
    /* access modifiers changed from: private */
    public boolean isPendingLikeOrUnlike;
    /* access modifiers changed from: private */
    public String likeCountStringWithLike;
    /* access modifiers changed from: private */
    public String likeCountStringWithoutLike;
    /* access modifiers changed from: private */
    public String objectId;
    /* access modifiers changed from: private */
    public boolean objectIsPage;
    /* access modifiers changed from: private */
    public ObjectType objectType;
    /* access modifiers changed from: private */
    public String socialSentenceWithLike;
    /* access modifiers changed from: private */
    public String socialSentenceWithoutLike;
    /* access modifiers changed from: private */
    public String unlikeToken;
    /* access modifiers changed from: private */
    public String verifiedObjectId;

    private abstract class AbstractRequestWrapper implements RequestWrapper {
        protected FacebookRequestError error;
        protected String objectId;
        protected ObjectType objectType;
        private GraphRequest request;

        protected AbstractRequestWrapper(String str, ObjectType objectType2) {
            this.objectId = str;
            this.objectType = objectType2;
        }

        public void addToBatch(GraphRequestBatch graphRequestBatch) {
            graphRequestBatch.add(this.request);
        }

        public FacebookRequestError getError() {
            return this.error;
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error running request for object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public abstract void processSuccess(GraphResponse graphResponse);

        /* access modifiers changed from: protected */
        public void setRequest(GraphRequest graphRequest) {
            this.request = graphRequest;
            graphRequest.setVersion(FacebookSdk.getGraphApiVersion());
            graphRequest.setCallback(new Callback() {
                public void onCompleted(GraphResponse graphResponse) {
                    AbstractRequestWrapper.this.error = graphResponse.getError();
                    if (AbstractRequestWrapper.this.error != null) {
                        AbstractRequestWrapper.this.processError(AbstractRequestWrapper.this.error);
                    } else {
                        AbstractRequestWrapper.this.processSuccess(graphResponse);
                    }
                }
            });
        }
    }

    private static class CreateLikeActionControllerWorkItem implements Runnable {
        private CreationCallback callback;
        private String objectId;
        private ObjectType objectType;

        CreateLikeActionControllerWorkItem(String str, ObjectType objectType2, CreationCallback creationCallback) {
            this.objectId = str;
            this.objectType = objectType2;
            this.callback = creationCallback;
        }

        public void run() {
            LikeActionController.createControllerForObjectIdAndType(this.objectId, this.objectType, this.callback);
        }
    }

    @Deprecated
    public interface CreationCallback {
        void onComplete(LikeActionController likeActionController, FacebookException facebookException);
    }

    private class GetEngagementRequestWrapper extends AbstractRequestWrapper {
        String likeCountStringWithLike = LikeActionController.this.likeCountStringWithLike;
        String likeCountStringWithoutLike = LikeActionController.this.likeCountStringWithoutLike;
        String socialSentenceStringWithLike = LikeActionController.this.socialSentenceWithLike;
        String socialSentenceStringWithoutLike = LikeActionController.this.socialSentenceWithoutLike;

        GetEngagementRequestWrapper(String str, ObjectType objectType) {
            super(str, objectType);
            Bundle bundle = new Bundle();
            bundle.putString(GraphRequest.FIELDS_PARAM, "engagement.fields(count_string_with_like,count_string_without_like,social_sentence_with_like,social_sentence_without_like)");
            bundle.putString("locale", Locale.getDefault().toString());
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), str, bundle, HttpMethod.GET));
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error fetching engagement for object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
            LikeActionController.this.logAppEventForError("get_engagement", facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            JSONObject tryGetJSONObjectFromResponse = Utility.tryGetJSONObjectFromResponse(graphResponse.getJSONObject(), PlaceFields.ENGAGEMENT);
            if (tryGetJSONObjectFromResponse != null) {
                this.likeCountStringWithLike = tryGetJSONObjectFromResponse.optString("count_string_with_like", this.likeCountStringWithLike);
                this.likeCountStringWithoutLike = tryGetJSONObjectFromResponse.optString("count_string_without_like", this.likeCountStringWithoutLike);
                this.socialSentenceStringWithLike = tryGetJSONObjectFromResponse.optString(LikeActionController.JSON_STRING_SOCIAL_SENTENCE_WITH_LIKE_KEY, this.socialSentenceStringWithLike);
                this.socialSentenceStringWithoutLike = tryGetJSONObjectFromResponse.optString(LikeActionController.JSON_STRING_SOCIAL_SENTENCE_WITHOUT_LIKE_KEY, this.socialSentenceStringWithoutLike);
            }
        }
    }

    private class GetOGObjectIdRequestWrapper extends AbstractRequestWrapper {
        String verifiedObjectId;

        GetOGObjectIdRequestWrapper(String str, ObjectType objectType) {
            super(str, objectType);
            Bundle bundle = new Bundle();
            bundle.putString(GraphRequest.FIELDS_PARAM, "og_object.fields(id)");
            bundle.putString("ids", str);
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), "", bundle, HttpMethod.GET));
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            if (facebookRequestError.getErrorMessage().contains("og_object")) {
                this.error = null;
                return;
            }
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error getting the FB id for object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            JSONObject tryGetJSONObjectFromResponse = Utility.tryGetJSONObjectFromResponse(graphResponse.getJSONObject(), this.objectId);
            if (tryGetJSONObjectFromResponse != null) {
                JSONObject optJSONObject = tryGetJSONObjectFromResponse.optJSONObject("og_object");
                if (optJSONObject != null) {
                    this.verifiedObjectId = optJSONObject.optString("id");
                }
            }
        }
    }

    private class GetOGObjectLikesRequestWrapper extends AbstractRequestWrapper implements LikeRequestWrapper {
        private final String objectId;
        private boolean objectIsLiked = LikeActionController.this.isObjectLiked;
        private final ObjectType objectType;
        private String unlikeToken;

        GetOGObjectLikesRequestWrapper(String str, ObjectType objectType2) {
            super(str, objectType2);
            this.objectId = str;
            this.objectType = objectType2;
            Bundle bundle = new Bundle();
            bundle.putString(GraphRequest.FIELDS_PARAM, "id,application");
            bundle.putString("object", this.objectId);
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), "me/og.likes", bundle, HttpMethod.GET));
        }

        public String getUnlikeToken() {
            return this.unlikeToken;
        }

        public boolean isObjectLiked() {
            return this.objectIsLiked;
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error fetching like status for object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
            LikeActionController.this.logAppEventForError("get_og_object_like", facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            JSONArray tryGetJSONArrayFromResponse = Utility.tryGetJSONArrayFromResponse(graphResponse.getJSONObject(), ShareConstants.WEB_DIALOG_PARAM_DATA);
            if (tryGetJSONArrayFromResponse != null) {
                for (int i = 0; i < tryGetJSONArrayFromResponse.length(); i++) {
                    JSONObject optJSONObject = tryGetJSONArrayFromResponse.optJSONObject(i);
                    if (optJSONObject != null) {
                        this.objectIsLiked = true;
                        JSONObject optJSONObject2 = optJSONObject.optJSONObject("application");
                        AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
                        if (optJSONObject2 != null && AccessToken.isCurrentAccessTokenActive() && Utility.areObjectsEqual(currentAccessToken.getApplicationId(), optJSONObject2.optString("id"))) {
                            this.unlikeToken = optJSONObject.optString("id");
                        }
                    }
                }
            }
        }
    }

    private class GetPageIdRequestWrapper extends AbstractRequestWrapper {
        boolean objectIsPage;
        String verifiedObjectId;

        GetPageIdRequestWrapper(String str, ObjectType objectType) {
            super(str, objectType);
            Bundle bundle = new Bundle();
            bundle.putString(GraphRequest.FIELDS_PARAM, "id");
            bundle.putString("ids", str);
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), "", bundle, HttpMethod.GET));
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error getting the FB id for object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            JSONObject tryGetJSONObjectFromResponse = Utility.tryGetJSONObjectFromResponse(graphResponse.getJSONObject(), this.objectId);
            if (tryGetJSONObjectFromResponse != null) {
                this.verifiedObjectId = tryGetJSONObjectFromResponse.optString("id");
                this.objectIsPage = !Utility.isNullOrEmpty(this.verifiedObjectId);
            }
        }
    }

    private class GetPageLikesRequestWrapper extends AbstractRequestWrapper implements LikeRequestWrapper {
        private boolean objectIsLiked = LikeActionController.this.isObjectLiked;
        private String pageId;

        GetPageLikesRequestWrapper(String str) {
            super(str, ObjectType.PAGE);
            this.pageId = str;
            Bundle bundle = new Bundle();
            bundle.putString(GraphRequest.FIELDS_PARAM, "id");
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), "me/likes/" + str, bundle, HttpMethod.GET));
        }

        public String getUnlikeToken() {
            return null;
        }

        public boolean isObjectLiked() {
            return this.objectIsLiked;
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error fetching like status for page id '%s': %s", this.pageId, facebookRequestError);
            LikeActionController.this.logAppEventForError("get_page_like", facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            JSONArray tryGetJSONArrayFromResponse = Utility.tryGetJSONArrayFromResponse(graphResponse.getJSONObject(), ShareConstants.WEB_DIALOG_PARAM_DATA);
            if (tryGetJSONArrayFromResponse != null && tryGetJSONArrayFromResponse.length() > 0) {
                this.objectIsLiked = true;
            }
        }
    }

    private interface LikeRequestWrapper extends RequestWrapper {
        String getUnlikeToken();

        boolean isObjectLiked();
    }

    private static class MRUCacheWorkItem implements Runnable {
        private static ArrayList<String> mruCachedItems = new ArrayList<>();
        private String cacheItem;
        private boolean shouldTrim;

        MRUCacheWorkItem(String str, boolean z) {
            this.cacheItem = str;
            this.shouldTrim = z;
        }

        public void run() {
            if (this.cacheItem != null) {
                mruCachedItems.remove(this.cacheItem);
                mruCachedItems.add(0, this.cacheItem);
            }
            if (this.shouldTrim && mruCachedItems.size() >= 128) {
                while (64 < mruCachedItems.size()) {
                    LikeActionController.cache.remove((String) mruCachedItems.remove(mruCachedItems.size() - 1));
                }
            }
        }
    }

    private class PublishLikeRequestWrapper extends AbstractRequestWrapper {
        String unlikeToken;

        PublishLikeRequestWrapper(String str, ObjectType objectType) {
            super(str, objectType);
            Bundle bundle = new Bundle();
            bundle.putString("object", str);
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), "me/og.likes", bundle, HttpMethod.POST));
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            if (facebookRequestError.getErrorCode() == LikeActionController.ERROR_CODE_OBJECT_ALREADY_LIKED) {
                this.error = null;
                return;
            }
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error liking object '%s' with type '%s' : %s", this.objectId, this.objectType, facebookRequestError);
            LikeActionController.this.logAppEventForError("publish_like", facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
            this.unlikeToken = Utility.safeGetStringFromResponse(graphResponse.getJSONObject(), "id");
        }
    }

    private class PublishUnlikeRequestWrapper extends AbstractRequestWrapper {
        private String unlikeToken;

        PublishUnlikeRequestWrapper(String str) {
            super(null, null);
            this.unlikeToken = str;
            setRequest(new GraphRequest(AccessToken.getCurrentAccessToken(), str, null, HttpMethod.DELETE));
        }

        /* access modifiers changed from: protected */
        public void processError(FacebookRequestError facebookRequestError) {
            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Error unliking object with unlike token '%s' : %s", this.unlikeToken, facebookRequestError);
            LikeActionController.this.logAppEventForError("publish_unlike", facebookRequestError);
        }

        /* access modifiers changed from: protected */
        public void processSuccess(GraphResponse graphResponse) {
        }
    }

    private interface RequestCompletionCallback {
        void onComplete();
    }

    private interface RequestWrapper {
        void addToBatch(GraphRequestBatch graphRequestBatch);

        FacebookRequestError getError();
    }

    private static class SerializeToDiskWorkItem implements Runnable {
        private String cacheKey;
        private String controllerJson;

        SerializeToDiskWorkItem(String str, String str2) {
            this.cacheKey = str;
            this.controllerJson = str2;
        }

        public void run() {
            LikeActionController.serializeToDiskSynchronously(this.cacheKey, this.controllerJson);
        }
    }

    private LikeActionController(String str, ObjectType objectType2) {
        this.objectId = str;
        this.objectType = objectType2;
    }

    /* access modifiers changed from: private */
    public static void broadcastAction(LikeActionController likeActionController, String str) {
        broadcastAction(likeActionController, str, null);
    }

    /* access modifiers changed from: private */
    public static void broadcastAction(LikeActionController likeActionController, String str, Bundle bundle) {
        Intent intent = new Intent(str);
        if (likeActionController != null) {
            if (bundle == null) {
                bundle = new Bundle();
            }
            bundle.putString(ACTION_OBJECT_ID_KEY, likeActionController.getObjectId());
        }
        if (bundle != null) {
            intent.putExtras(bundle);
        }
        LocalBroadcastManager.getInstance(FacebookSdk.getApplicationContext()).sendBroadcast(intent);
    }

    private boolean canUseOGPublish() {
        AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
        return !this.objectIsPage && this.verifiedObjectId != null && AccessToken.isCurrentAccessTokenActive() && currentAccessToken.getPermissions() != null && currentAccessToken.getPermissions().contains("publish_actions");
    }

    private void clearState() {
        this.facebookDialogAnalyticsBundle = null;
        storeObjectIdForPendingController(null);
    }

    /* access modifiers changed from: private */
    public static void createControllerForObjectIdAndType(String str, ObjectType objectType2, CreationCallback creationCallback) {
        LikeActionController controllerFromInMemoryCache = getControllerFromInMemoryCache(str);
        if (controllerFromInMemoryCache != null) {
            verifyControllerAndInvokeCallback(controllerFromInMemoryCache, objectType2, creationCallback);
            return;
        }
        LikeActionController deserializeFromDiskSynchronously = deserializeFromDiskSynchronously(str);
        if (deserializeFromDiskSynchronously == null) {
            deserializeFromDiskSynchronously = new LikeActionController(str, objectType2);
            serializeToDiskAsync(deserializeFromDiskSynchronously);
        }
        putControllerInMemoryCache(str, deserializeFromDiskSynchronously);
        handler.post(new Runnable(deserializeFromDiskSynchronously) {
            final /* synthetic */ LikeActionController val$controllerToRefresh;

            {
                this.val$controllerToRefresh = r1;
            }

            public void run() {
                this.val$controllerToRefresh.refreshStatusAsync();
            }
        });
        invokeCallbackWithController(creationCallback, deserializeFromDiskSynchronously, null);
    }

    /* JADX WARNING: Removed duplicated region for block: B:20:0x0034  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static com.facebook.share.internal.LikeActionController deserializeFromDiskSynchronously(java.lang.String r5) {
        /*
            r0 = 0
            java.lang.String r1 = getCacheKeyForObjectId(r5)     // Catch:{ IOException -> 0x0021, all -> 0x0030 }
            com.facebook.internal.FileLruCache r2 = controllerDiskCache     // Catch:{ IOException -> 0x0021, all -> 0x0030 }
            java.io.InputStream r2 = r2.get(r1)     // Catch:{ IOException -> 0x0021, all -> 0x0030 }
            if (r2 == 0) goto L_0x001b
            java.lang.String r1 = com.facebook.internal.Utility.readStreamToString(r2)     // Catch:{ IOException -> 0x003b }
            boolean r3 = com.facebook.internal.Utility.isNullOrEmpty(r1)     // Catch:{ IOException -> 0x003b }
            if (r3 != 0) goto L_0x001b
            com.facebook.share.internal.LikeActionController r0 = deserializeFromJson(r1)     // Catch:{ IOException -> 0x003b }
        L_0x001b:
            if (r2 == 0) goto L_0x0020
            com.facebook.internal.Utility.closeQuietly(r2)
        L_0x0020:
            return r0
        L_0x0021:
            r1 = move-exception
            r2 = r0
        L_0x0023:
            java.lang.String r3 = TAG     // Catch:{ all -> 0x0038 }
            java.lang.String r4 = "Unable to deserialize controller from disk"
            android.util.Log.e(r3, r4, r1)     // Catch:{ all -> 0x0038 }
            if (r2 == 0) goto L_0x0020
            com.facebook.internal.Utility.closeQuietly(r2)
            goto L_0x0020
        L_0x0030:
            r1 = move-exception
            r2 = r0
        L_0x0032:
            if (r2 == 0) goto L_0x0037
            com.facebook.internal.Utility.closeQuietly(r2)
        L_0x0037:
            throw r1
        L_0x0038:
            r0 = move-exception
            r1 = r0
            goto L_0x0032
        L_0x003b:
            r1 = move-exception
            goto L_0x0023
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.share.internal.LikeActionController.deserializeFromDiskSynchronously(java.lang.String):com.facebook.share.internal.LikeActionController");
    }

    private static LikeActionController deserializeFromJson(String str) {
        try {
            JSONObject jSONObject = new JSONObject(str);
            if (jSONObject.optInt(JSON_INT_VERSION_KEY, -1) != 3) {
                return null;
            }
            LikeActionController likeActionController = new LikeActionController(jSONObject.getString("object_id"), ObjectType.fromInt(jSONObject.optInt("object_type", ObjectType.UNKNOWN.getValue())));
            likeActionController.likeCountStringWithLike = jSONObject.optString(JSON_STRING_LIKE_COUNT_WITH_LIKE_KEY, null);
            likeActionController.likeCountStringWithoutLike = jSONObject.optString(JSON_STRING_LIKE_COUNT_WITHOUT_LIKE_KEY, null);
            likeActionController.socialSentenceWithLike = jSONObject.optString(JSON_STRING_SOCIAL_SENTENCE_WITH_LIKE_KEY, null);
            likeActionController.socialSentenceWithoutLike = jSONObject.optString(JSON_STRING_SOCIAL_SENTENCE_WITHOUT_LIKE_KEY, null);
            likeActionController.isObjectLiked = jSONObject.optBoolean(JSON_BOOL_IS_OBJECT_LIKED_KEY);
            likeActionController.unlikeToken = jSONObject.optString("unlike_token", null);
            JSONObject optJSONObject = jSONObject.optJSONObject(JSON_BUNDLE_FACEBOOK_DIALOG_ANALYTICS_BUNDLE);
            if (optJSONObject == null) {
                return likeActionController;
            }
            likeActionController.facebookDialogAnalyticsBundle = BundleJSONConverter.convertToBundle(optJSONObject);
            return likeActionController;
        } catch (JSONException e) {
            Log.e(TAG, "Unable to deserialize controller from JSON", e);
            return null;
        }
    }

    private void fetchVerifiedObjectId(final RequestCompletionCallback requestCompletionCallback) {
        if (Utility.isNullOrEmpty(this.verifiedObjectId)) {
            final GetOGObjectIdRequestWrapper getOGObjectIdRequestWrapper = new GetOGObjectIdRequestWrapper(this.objectId, this.objectType);
            final GetPageIdRequestWrapper getPageIdRequestWrapper = new GetPageIdRequestWrapper(this.objectId, this.objectType);
            GraphRequestBatch graphRequestBatch = new GraphRequestBatch();
            getOGObjectIdRequestWrapper.addToBatch(graphRequestBatch);
            getPageIdRequestWrapper.addToBatch(graphRequestBatch);
            graphRequestBatch.addCallback(new GraphRequestBatch.Callback() {
                public void onBatchCompleted(GraphRequestBatch graphRequestBatch) {
                    LikeActionController.this.verifiedObjectId = getOGObjectIdRequestWrapper.verifiedObjectId;
                    if (Utility.isNullOrEmpty(LikeActionController.this.verifiedObjectId)) {
                        LikeActionController.this.verifiedObjectId = getPageIdRequestWrapper.verifiedObjectId;
                        LikeActionController.this.objectIsPage = getPageIdRequestWrapper.objectIsPage;
                    }
                    if (Utility.isNullOrEmpty(LikeActionController.this.verifiedObjectId)) {
                        Logger.log(LoggingBehavior.DEVELOPER_ERRORS, LikeActionController.TAG, "Unable to verify the FB id for '%s'. Verify that it is a valid FB object or page", LikeActionController.this.objectId);
                        LikeActionController.this.logAppEventForError("get_verified_id", getPageIdRequestWrapper.getError() != null ? getPageIdRequestWrapper.getError() : getOGObjectIdRequestWrapper.getError());
                    }
                    if (requestCompletionCallback != null) {
                        requestCompletionCallback.onComplete();
                    }
                }
            });
            graphRequestBatch.executeAsync();
        } else if (requestCompletionCallback != null) {
            requestCompletionCallback.onComplete();
        }
    }

    /* access modifiers changed from: private */
    public AppEventsLogger getAppEventsLogger() {
        if (this.appEventsLogger == null) {
            this.appEventsLogger = AppEventsLogger.newLogger(FacebookSdk.getApplicationContext());
        }
        return this.appEventsLogger;
    }

    private static String getCacheKeyForObjectId(String str) {
        String str2 = null;
        AccessToken currentAccessToken = AccessToken.getCurrentAccessToken();
        if (AccessToken.isCurrentAccessTokenActive()) {
            str2 = currentAccessToken.getToken();
        }
        if (str2 != null) {
            str2 = Utility.md5hash(str2);
        }
        return String.format(Locale.ROOT, "%s|%s|com.fb.sdk.like|%d", new Object[]{str, Utility.coerceValueIfNullOrEmpty(str2, ""), Integer.valueOf(objectSuffix)});
    }

    @Deprecated
    public static void getControllerForObjectId(String str, ObjectType objectType2, CreationCallback creationCallback) {
        if (!isInitialized) {
            performFirstInitialize();
        }
        LikeActionController controllerFromInMemoryCache = getControllerFromInMemoryCache(str);
        if (controllerFromInMemoryCache != null) {
            verifyControllerAndInvokeCallback(controllerFromInMemoryCache, objectType2, creationCallback);
        } else {
            diskIOWorkQueue.addActiveWorkItem(new CreateLikeActionControllerWorkItem(str, objectType2, creationCallback));
        }
    }

    private static LikeActionController getControllerFromInMemoryCache(String str) {
        String cacheKeyForObjectId = getCacheKeyForObjectId(str);
        LikeActionController likeActionController = (LikeActionController) cache.get(cacheKeyForObjectId);
        if (likeActionController != null) {
            mruCacheWorkQueue.addActiveWorkItem(new MRUCacheWorkItem(cacheKeyForObjectId, false));
        }
        return likeActionController;
    }

    private ResultProcessor getResultProcessor(final Bundle bundle) {
        return new ResultProcessor(null) {
            public void onCancel(AppCall appCall) {
                onError(appCall, new FacebookOperationCanceledException());
            }

            public void onError(AppCall appCall, FacebookException facebookException) {
                Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Like Dialog failed with error : %s", facebookException);
                Bundle bundle = bundle == null ? new Bundle() : bundle;
                bundle.putString(AnalyticsEvents.PARAMETER_CALL_ID, appCall.getCallId().toString());
                LikeActionController.this.logAppEventForError("present_dialog", bundle);
                LikeActionController.broadcastAction(LikeActionController.this, LikeActionController.ACTION_LIKE_ACTION_CONTROLLER_DID_ERROR, NativeProtocol.createBundleForException(facebookException));
            }

            public void onSuccess(AppCall appCall, Bundle bundle) {
                if (bundle != null && bundle.containsKey(LikeActionController.LIKE_DIALOG_RESPONSE_OBJECT_IS_LIKED_KEY)) {
                    boolean z = bundle.getBoolean(LikeActionController.LIKE_DIALOG_RESPONSE_OBJECT_IS_LIKED_KEY);
                    String access$700 = LikeActionController.this.likeCountStringWithLike;
                    String access$800 = LikeActionController.this.likeCountStringWithoutLike;
                    if (bundle.containsKey(LikeActionController.LIKE_DIALOG_RESPONSE_LIKE_COUNT_STRING_KEY)) {
                        String string = bundle.getString(LikeActionController.LIKE_DIALOG_RESPONSE_LIKE_COUNT_STRING_KEY);
                        access$700 = string;
                        access$800 = string;
                    }
                    String access$900 = LikeActionController.this.socialSentenceWithLike;
                    String access$1000 = LikeActionController.this.socialSentenceWithoutLike;
                    if (bundle.containsKey(LikeActionController.LIKE_DIALOG_RESPONSE_SOCIAL_SENTENCE_KEY)) {
                        String string2 = bundle.getString(LikeActionController.LIKE_DIALOG_RESPONSE_SOCIAL_SENTENCE_KEY);
                        access$1000 = string2;
                        access$900 = string2;
                    }
                    String access$1100 = bundle.containsKey(LikeActionController.LIKE_DIALOG_RESPONSE_OBJECT_IS_LIKED_KEY) ? bundle.getString("unlike_token") : LikeActionController.this.unlikeToken;
                    Bundle bundle2 = bundle == null ? new Bundle() : bundle;
                    bundle2.putString(AnalyticsEvents.PARAMETER_CALL_ID, appCall.getCallId().toString());
                    LikeActionController.this.getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_DIALOG_DID_SUCCEED, null, bundle2);
                    LikeActionController.this.updateState(z, access$700, access$800, access$900, access$1000, access$1100);
                }
            }
        };
    }

    @Deprecated
    public static boolean handleOnActivityResult(final int i, final int i2, final Intent intent) {
        if (Utility.isNullOrEmpty(objectIdForPendingController)) {
            objectIdForPendingController = FacebookSdk.getApplicationContext().getSharedPreferences(LIKE_ACTION_CONTROLLER_STORE, 0).getString(LIKE_ACTION_CONTROLLER_STORE_PENDING_OBJECT_ID_KEY, null);
        }
        if (Utility.isNullOrEmpty(objectIdForPendingController)) {
            return false;
        }
        getControllerForObjectId(objectIdForPendingController, ObjectType.UNKNOWN, new CreationCallback() {
            public void onComplete(LikeActionController likeActionController, FacebookException facebookException) {
                if (facebookException == null) {
                    likeActionController.onActivityResult(i, i2, intent);
                } else {
                    Utility.logd(LikeActionController.TAG, (Exception) facebookException);
                }
            }
        });
        return true;
    }

    private static void invokeCallbackWithController(final CreationCallback creationCallback, final LikeActionController likeActionController, final FacebookException facebookException) {
        if (creationCallback != null) {
            handler.post(new Runnable() {
                public void run() {
                    creationCallback.onComplete(likeActionController, facebookException);
                }
            });
        }
    }

    /* access modifiers changed from: private */
    public void logAppEventForError(String str, Bundle bundle) {
        Bundle bundle2 = new Bundle(bundle);
        bundle2.putString("object_id", this.objectId);
        bundle2.putString("object_type", this.objectType.toString());
        bundle2.putString(AnalyticsEvents.PARAMETER_LIKE_VIEW_CURRENT_ACTION, str);
        getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_ERROR, null, bundle2);
    }

    /* access modifiers changed from: private */
    public void logAppEventForError(String str, FacebookRequestError facebookRequestError) {
        Bundle bundle = new Bundle();
        if (facebookRequestError != null) {
            JSONObject requestResult = facebookRequestError.getRequestResult();
            if (requestResult != null) {
                bundle.putString("error", requestResult.toString());
            }
        }
        logAppEventForError(str, bundle);
    }

    /* access modifiers changed from: private */
    public void onActivityResult(int i, int i2, Intent intent) {
        ShareInternalUtility.handleActivityResult(i, i2, intent, getResultProcessor(this.facebookDialogAnalyticsBundle));
        clearState();
    }

    private static void performFirstInitialize() {
        synchronized (LikeActionController.class) {
            try {
                if (!isInitialized) {
                    handler = new Handler(Looper.getMainLooper());
                    objectSuffix = FacebookSdk.getApplicationContext().getSharedPreferences(LIKE_ACTION_CONTROLLER_STORE, 0).getInt(LIKE_ACTION_CONTROLLER_STORE_OBJECT_SUFFIX_KEY, 1);
                    controllerDiskCache = new FileLruCache(TAG, new Limits());
                    registerAccessTokenTracker();
                    CallbackManagerImpl.registerStaticCallback(RequestCodeOffset.Like.toRequestCode(), new CallbackManagerImpl.Callback() {
                        public boolean onActivityResult(int i, Intent intent) {
                            return LikeActionController.handleOnActivityResult(RequestCodeOffset.Like.toRequestCode(), i, intent);
                        }
                    });
                    isInitialized = true;
                }
            } finally {
                Class<LikeActionController> cls = LikeActionController.class;
            }
        }
    }

    private void presentLikeDialog(Activity activity, FragmentWrapper fragmentWrapper, Bundle bundle) {
        String str;
        if (LikeDialog.canShowNativeDialog()) {
            str = AnalyticsEvents.EVENT_LIKE_VIEW_DID_PRESENT_DIALOG;
        } else if (LikeDialog.canShowWebFallback()) {
            str = AnalyticsEvents.EVENT_LIKE_VIEW_DID_PRESENT_FALLBACK;
        } else {
            logAppEventForError("present_dialog", bundle);
            Utility.logd(TAG, "Cannot show the Like Dialog on this device.");
            broadcastAction(null, ACTION_LIKE_ACTION_CONTROLLER_UPDATED);
            str = null;
        }
        if (str != null) {
            LikeContent build = new Builder().setObjectId(this.objectId).setObjectType(this.objectType != null ? this.objectType.toString() : ObjectType.UNKNOWN.toString()).build();
            if (fragmentWrapper != null) {
                new LikeDialog(fragmentWrapper).show(build);
            } else {
                new LikeDialog(activity).show(build);
            }
            saveState(bundle);
            getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_DID_PRESENT_DIALOG, null, bundle);
        }
    }

    /* access modifiers changed from: private */
    public void publishAgainIfNeeded(Bundle bundle) {
        if (this.isObjectLiked != this.isObjectLikedOnServer && !publishLikeOrUnlikeAsync(this.isObjectLiked, bundle)) {
            publishDidError(!this.isObjectLiked);
        }
    }

    /* access modifiers changed from: private */
    public void publishDidError(boolean z) {
        updateLikeState(z);
        Bundle bundle = new Bundle();
        bundle.putString(NativeProtocol.STATUS_ERROR_DESCRIPTION, ERROR_PUBLISH_ERROR);
        broadcastAction(this, ACTION_LIKE_ACTION_CONTROLLER_DID_ERROR, bundle);
    }

    private void publishLikeAsync(final Bundle bundle) {
        this.isPendingLikeOrUnlike = true;
        fetchVerifiedObjectId(new RequestCompletionCallback() {
            public void onComplete() {
                if (Utility.isNullOrEmpty(LikeActionController.this.verifiedObjectId)) {
                    Bundle bundle = new Bundle();
                    bundle.putString(NativeProtocol.STATUS_ERROR_DESCRIPTION, LikeActionController.ERROR_INVALID_OBJECT_ID);
                    LikeActionController.broadcastAction(LikeActionController.this, LikeActionController.ACTION_LIKE_ACTION_CONTROLLER_DID_ERROR, bundle);
                    return;
                }
                GraphRequestBatch graphRequestBatch = new GraphRequestBatch();
                final PublishLikeRequestWrapper publishLikeRequestWrapper = new PublishLikeRequestWrapper(LikeActionController.this.verifiedObjectId, LikeActionController.this.objectType);
                publishLikeRequestWrapper.addToBatch(graphRequestBatch);
                graphRequestBatch.addCallback(new GraphRequestBatch.Callback() {
                    public void onBatchCompleted(GraphRequestBatch graphRequestBatch) {
                        LikeActionController.this.isPendingLikeOrUnlike = false;
                        if (publishLikeRequestWrapper.getError() != null) {
                            LikeActionController.this.publishDidError(false);
                            return;
                        }
                        LikeActionController.this.unlikeToken = Utility.coerceValueIfNullOrEmpty(publishLikeRequestWrapper.unlikeToken, null);
                        LikeActionController.this.isObjectLikedOnServer = true;
                        LikeActionController.this.getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_DID_LIKE, null, bundle);
                        LikeActionController.this.publishAgainIfNeeded(bundle);
                    }
                });
                graphRequestBatch.executeAsync();
            }
        });
    }

    private boolean publishLikeOrUnlikeAsync(boolean z, Bundle bundle) {
        if (canUseOGPublish()) {
            if (z) {
                publishLikeAsync(bundle);
                return true;
            } else if (!Utility.isNullOrEmpty(this.unlikeToken)) {
                publishUnlikeAsync(bundle);
                return true;
            }
        }
        return false;
    }

    private void publishUnlikeAsync(final Bundle bundle) {
        this.isPendingLikeOrUnlike = true;
        GraphRequestBatch graphRequestBatch = new GraphRequestBatch();
        final PublishUnlikeRequestWrapper publishUnlikeRequestWrapper = new PublishUnlikeRequestWrapper(this.unlikeToken);
        publishUnlikeRequestWrapper.addToBatch(graphRequestBatch);
        graphRequestBatch.addCallback(new GraphRequestBatch.Callback() {
            public void onBatchCompleted(GraphRequestBatch graphRequestBatch) {
                LikeActionController.this.isPendingLikeOrUnlike = false;
                if (publishUnlikeRequestWrapper.getError() != null) {
                    LikeActionController.this.publishDidError(true);
                    return;
                }
                LikeActionController.this.unlikeToken = null;
                LikeActionController.this.isObjectLikedOnServer = false;
                LikeActionController.this.getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_DID_UNLIKE, null, bundle);
                LikeActionController.this.publishAgainIfNeeded(bundle);
            }
        });
        graphRequestBatch.executeAsync();
    }

    private static void putControllerInMemoryCache(String str, LikeActionController likeActionController) {
        String cacheKeyForObjectId = getCacheKeyForObjectId(str);
        mruCacheWorkQueue.addActiveWorkItem(new MRUCacheWorkItem(cacheKeyForObjectId, true));
        cache.put(cacheKeyForObjectId, likeActionController);
    }

    /* access modifiers changed from: private */
    public void refreshStatusAsync() {
        if (!AccessToken.isCurrentAccessTokenActive()) {
            refreshStatusViaService();
        } else {
            fetchVerifiedObjectId(new RequestCompletionCallback() {
                public void onComplete() {
                    final LikeRequestWrapper getPageLikesRequestWrapper;
                    switch (LikeActionController.this.objectType) {
                        case PAGE:
                            getPageLikesRequestWrapper = new GetPageLikesRequestWrapper(LikeActionController.this.verifiedObjectId);
                            break;
                        default:
                            getPageLikesRequestWrapper = new GetOGObjectLikesRequestWrapper(LikeActionController.this.verifiedObjectId, LikeActionController.this.objectType);
                            break;
                    }
                    final GetEngagementRequestWrapper getEngagementRequestWrapper = new GetEngagementRequestWrapper(LikeActionController.this.verifiedObjectId, LikeActionController.this.objectType);
                    GraphRequestBatch graphRequestBatch = new GraphRequestBatch();
                    getPageLikesRequestWrapper.addToBatch(graphRequestBatch);
                    getEngagementRequestWrapper.addToBatch(graphRequestBatch);
                    graphRequestBatch.addCallback(new GraphRequestBatch.Callback() {
                        public void onBatchCompleted(GraphRequestBatch graphRequestBatch) {
                            if (getPageLikesRequestWrapper.getError() == null && getEngagementRequestWrapper.getError() == null) {
                                LikeActionController.this.updateState(getPageLikesRequestWrapper.isObjectLiked(), getEngagementRequestWrapper.likeCountStringWithLike, getEngagementRequestWrapper.likeCountStringWithoutLike, getEngagementRequestWrapper.socialSentenceStringWithLike, getEngagementRequestWrapper.socialSentenceStringWithoutLike, getPageLikesRequestWrapper.getUnlikeToken());
                                return;
                            }
                            Logger.log(LoggingBehavior.REQUESTS, LikeActionController.TAG, "Unable to refresh like state for id: '%s'", LikeActionController.this.objectId);
                        }
                    });
                    graphRequestBatch.executeAsync();
                }
            });
        }
    }

    private void refreshStatusViaService() {
        LikeStatusClient likeStatusClient = new LikeStatusClient(FacebookSdk.getApplicationContext(), FacebookSdk.getApplicationId(), this.objectId);
        if (likeStatusClient.start()) {
            likeStatusClient.setCompletedListener(new CompletedListener() {
                public void completed(Bundle bundle) {
                    if (bundle != null && bundle.containsKey(ShareConstants.EXTRA_OBJECT_IS_LIKED)) {
                        LikeActionController.this.updateState(bundle.getBoolean(ShareConstants.EXTRA_OBJECT_IS_LIKED), bundle.containsKey(ShareConstants.EXTRA_LIKE_COUNT_STRING_WITH_LIKE) ? bundle.getString(ShareConstants.EXTRA_LIKE_COUNT_STRING_WITH_LIKE) : LikeActionController.this.likeCountStringWithLike, bundle.containsKey(ShareConstants.EXTRA_LIKE_COUNT_STRING_WITHOUT_LIKE) ? bundle.getString(ShareConstants.EXTRA_LIKE_COUNT_STRING_WITHOUT_LIKE) : LikeActionController.this.likeCountStringWithoutLike, bundle.containsKey(ShareConstants.EXTRA_SOCIAL_SENTENCE_WITH_LIKE) ? bundle.getString(ShareConstants.EXTRA_SOCIAL_SENTENCE_WITH_LIKE) : LikeActionController.this.socialSentenceWithLike, bundle.containsKey(ShareConstants.EXTRA_SOCIAL_SENTENCE_WITHOUT_LIKE) ? bundle.getString(ShareConstants.EXTRA_SOCIAL_SENTENCE_WITHOUT_LIKE) : LikeActionController.this.socialSentenceWithoutLike, bundle.containsKey(ShareConstants.EXTRA_UNLIKE_TOKEN) ? bundle.getString(ShareConstants.EXTRA_UNLIKE_TOKEN) : LikeActionController.this.unlikeToken);
                    }
                }
            });
        }
    }

    private static void registerAccessTokenTracker() {
        accessTokenTracker = new AccessTokenTracker() {
            /* access modifiers changed from: protected */
            public void onCurrentAccessTokenChanged(AccessToken accessToken, AccessToken accessToken2) {
                Context applicationContext = FacebookSdk.getApplicationContext();
                if (accessToken2 == null) {
                    LikeActionController.objectSuffix = (LikeActionController.objectSuffix + 1) % 1000;
                    applicationContext.getSharedPreferences(LikeActionController.LIKE_ACTION_CONTROLLER_STORE, 0).edit().putInt(LikeActionController.LIKE_ACTION_CONTROLLER_STORE_OBJECT_SUFFIX_KEY, LikeActionController.objectSuffix).apply();
                    LikeActionController.cache.clear();
                    LikeActionController.controllerDiskCache.clearCache();
                }
                LikeActionController.broadcastAction(null, LikeActionController.ACTION_LIKE_ACTION_CONTROLLER_DID_RESET);
            }
        };
    }

    private void saveState(Bundle bundle) {
        storeObjectIdForPendingController(this.objectId);
        this.facebookDialogAnalyticsBundle = bundle;
        serializeToDiskAsync(this);
    }

    private static void serializeToDiskAsync(LikeActionController likeActionController) {
        String serializeToJson = serializeToJson(likeActionController);
        String cacheKeyForObjectId = getCacheKeyForObjectId(likeActionController.objectId);
        if (!Utility.isNullOrEmpty(serializeToJson) && !Utility.isNullOrEmpty(cacheKeyForObjectId)) {
            diskIOWorkQueue.addActiveWorkItem(new SerializeToDiskWorkItem(cacheKeyForObjectId, serializeToJson));
        }
    }

    /* access modifiers changed from: private */
    public static void serializeToDiskSynchronously(String str, String str2) {
        OutputStream outputStream = null;
        try {
            outputStream = controllerDiskCache.openPutStream(str);
            outputStream.write(str2.getBytes());
            if (outputStream != null) {
                Utility.closeQuietly(outputStream);
            }
        } catch (IOException e) {
            Log.e(TAG, "Unable to serialize controller to disk", e);
            if (outputStream != null) {
                Utility.closeQuietly(outputStream);
            }
        } catch (Throwable th) {
            if (outputStream != null) {
                Utility.closeQuietly(outputStream);
            }
            throw th;
        }
    }

    private static String serializeToJson(LikeActionController likeActionController) {
        JSONObject jSONObject = new JSONObject();
        try {
            jSONObject.put(JSON_INT_VERSION_KEY, 3);
            jSONObject.put("object_id", likeActionController.objectId);
            jSONObject.put("object_type", likeActionController.objectType.getValue());
            jSONObject.put(JSON_STRING_LIKE_COUNT_WITH_LIKE_KEY, likeActionController.likeCountStringWithLike);
            jSONObject.put(JSON_STRING_LIKE_COUNT_WITHOUT_LIKE_KEY, likeActionController.likeCountStringWithoutLike);
            jSONObject.put(JSON_STRING_SOCIAL_SENTENCE_WITH_LIKE_KEY, likeActionController.socialSentenceWithLike);
            jSONObject.put(JSON_STRING_SOCIAL_SENTENCE_WITHOUT_LIKE_KEY, likeActionController.socialSentenceWithoutLike);
            jSONObject.put(JSON_BOOL_IS_OBJECT_LIKED_KEY, likeActionController.isObjectLiked);
            jSONObject.put("unlike_token", likeActionController.unlikeToken);
            if (likeActionController.facebookDialogAnalyticsBundle != null) {
                JSONObject convertToJSON = BundleJSONConverter.convertToJSON(likeActionController.facebookDialogAnalyticsBundle);
                if (convertToJSON != null) {
                    jSONObject.put(JSON_BUNDLE_FACEBOOK_DIALOG_ANALYTICS_BUNDLE, convertToJSON);
                }
            }
            return jSONObject.toString();
        } catch (JSONException e) {
            Log.e(TAG, "Unable to serialize controller to JSON", e);
            return null;
        }
    }

    private static void storeObjectIdForPendingController(String str) {
        objectIdForPendingController = str;
        FacebookSdk.getApplicationContext().getSharedPreferences(LIKE_ACTION_CONTROLLER_STORE, 0).edit().putString(LIKE_ACTION_CONTROLLER_STORE_PENDING_OBJECT_ID_KEY, objectIdForPendingController).apply();
    }

    private void updateLikeState(boolean z) {
        updateState(z, this.likeCountStringWithLike, this.likeCountStringWithoutLike, this.socialSentenceWithLike, this.socialSentenceWithoutLike, this.unlikeToken);
    }

    /* access modifiers changed from: private */
    public void updateState(boolean z, String str, String str2, String str3, String str4, String str5) {
        String coerceValueIfNullOrEmpty = Utility.coerceValueIfNullOrEmpty(str, null);
        String coerceValueIfNullOrEmpty2 = Utility.coerceValueIfNullOrEmpty(str2, null);
        String coerceValueIfNullOrEmpty3 = Utility.coerceValueIfNullOrEmpty(str3, null);
        String coerceValueIfNullOrEmpty4 = Utility.coerceValueIfNullOrEmpty(str4, null);
        String coerceValueIfNullOrEmpty5 = Utility.coerceValueIfNullOrEmpty(str5, null);
        if (z != this.isObjectLiked || !Utility.areObjectsEqual(coerceValueIfNullOrEmpty, this.likeCountStringWithLike) || !Utility.areObjectsEqual(coerceValueIfNullOrEmpty2, this.likeCountStringWithoutLike) || !Utility.areObjectsEqual(coerceValueIfNullOrEmpty3, this.socialSentenceWithLike) || !Utility.areObjectsEqual(coerceValueIfNullOrEmpty4, this.socialSentenceWithoutLike) || !Utility.areObjectsEqual(coerceValueIfNullOrEmpty5, this.unlikeToken)) {
            this.isObjectLiked = z;
            this.likeCountStringWithLike = coerceValueIfNullOrEmpty;
            this.likeCountStringWithoutLike = coerceValueIfNullOrEmpty2;
            this.socialSentenceWithLike = coerceValueIfNullOrEmpty3;
            this.socialSentenceWithoutLike = coerceValueIfNullOrEmpty4;
            this.unlikeToken = coerceValueIfNullOrEmpty5;
            serializeToDiskAsync(this);
            broadcastAction(this, ACTION_LIKE_ACTION_CONTROLLER_UPDATED);
        }
    }

    private static void verifyControllerAndInvokeCallback(LikeActionController likeActionController, ObjectType objectType2, CreationCallback creationCallback) {
        FacebookException facebookException;
        ObjectType mostSpecificObjectType = ShareInternalUtility.getMostSpecificObjectType(objectType2, likeActionController.objectType);
        if (mostSpecificObjectType == null) {
            facebookException = new FacebookException("Object with id:\"%s\" is already marked as type:\"%s\". Cannot change the type to:\"%s\"", likeActionController.objectId, likeActionController.objectType.toString(), objectType2.toString());
            likeActionController = null;
        } else {
            likeActionController.objectType = mostSpecificObjectType;
            facebookException = null;
        }
        invokeCallbackWithController(creationCallback, likeActionController, facebookException);
    }

    @Deprecated
    public String getLikeCountString() {
        return this.isObjectLiked ? this.likeCountStringWithLike : this.likeCountStringWithoutLike;
    }

    @Deprecated
    public String getObjectId() {
        return this.objectId;
    }

    @Deprecated
    public String getSocialSentence() {
        return this.isObjectLiked ? this.socialSentenceWithLike : this.socialSentenceWithoutLike;
    }

    @Deprecated
    public boolean isObjectLiked() {
        return this.isObjectLiked;
    }

    @Deprecated
    public boolean shouldEnableView() {
        return false;
    }

    @Deprecated
    public void toggleLike(Activity activity, FragmentWrapper fragmentWrapper, Bundle bundle) {
        boolean z = true;
        boolean z2 = !this.isObjectLiked;
        if (canUseOGPublish()) {
            updateLikeState(z2);
            if (this.isPendingLikeOrUnlike) {
                getAppEventsLogger().logSdkEvent(AnalyticsEvents.EVENT_LIKE_VIEW_DID_UNDO_QUICKLY, null, bundle);
            } else if (!publishLikeOrUnlikeAsync(z2, bundle)) {
                if (z2) {
                    z = false;
                }
                updateLikeState(z);
                presentLikeDialog(activity, fragmentWrapper, bundle);
            }
        } else {
            presentLikeDialog(activity, fragmentWrapper, bundle);
        }
    }
}
