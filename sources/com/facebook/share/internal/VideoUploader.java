package com.facebook.share.internal;

import android.net.Uri;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.ParcelFileDescriptor;
import android.os.ParcelFileDescriptor.AutoCloseInputStream;
import android.text.TextUtils;
import android.util.Log;
import com.facebook.AccessToken;
import com.facebook.AccessTokenTracker;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.FacebookGraphResponseException;
import com.facebook.FacebookRequestError;
import com.facebook.FacebookSdk;
import com.facebook.GraphRequest;
import com.facebook.GraphResponse;
import com.facebook.HttpMethod;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import com.facebook.internal.WorkQueue;
import com.facebook.internal.WorkQueue.WorkItem;
import com.facebook.share.Sharer.Result;
import com.facebook.share.model.ShareVideo;
import com.facebook.share.model.ShareVideoContent;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.games.GamesStatusCodes;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Locale;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;

public class VideoUploader {
    private static final String ERROR_BAD_SERVER_RESPONSE = "Unexpected error in server response";
    private static final String ERROR_UPLOAD = "Video upload failed";
    private static final int MAX_RETRIES_PER_PHASE = 2;
    private static final String PARAM_DESCRIPTION = "description";
    private static final String PARAM_END_OFFSET = "end_offset";
    private static final String PARAM_FILE_SIZE = "file_size";
    private static final String PARAM_REF = "ref";
    private static final String PARAM_SESSION_ID = "upload_session_id";
    private static final String PARAM_START_OFFSET = "start_offset";
    private static final String PARAM_TITLE = "title";
    private static final String PARAM_UPLOAD_PHASE = "upload_phase";
    private static final String PARAM_VALUE_UPLOAD_FINISH_PHASE = "finish";
    private static final String PARAM_VALUE_UPLOAD_START_PHASE = "start";
    private static final String PARAM_VALUE_UPLOAD_TRANSFER_PHASE = "transfer";
    private static final String PARAM_VIDEO_FILE_CHUNK = "video_file_chunk";
    private static final String PARAM_VIDEO_ID = "video_id";
    private static final int RETRY_DELAY_BACK_OFF_FACTOR = 3;
    private static final int RETRY_DELAY_UNIT_MS = 5000;
    private static final String TAG = "VideoUploader";
    private static final int UPLOAD_QUEUE_MAX_CONCURRENT = 8;
    private static AccessTokenTracker accessTokenTracker;
    private static Handler handler;
    private static boolean initialized;
    private static Set<UploadContext> pendingUploads = new HashSet();
    private static WorkQueue uploadQueue = new WorkQueue(8);

    /* renamed from: com.facebook.share.internal.VideoUploader$1 */
    static final class C05021 extends AccessTokenTracker {
        C05021() {
        }

        protected void onCurrentAccessTokenChanged(AccessToken accessToken, AccessToken accessToken2) {
            if (accessToken != null) {
                if (accessToken2 == null || !Utility.areObjectsEqual(accessToken2.getUserId(), accessToken.getUserId())) {
                    VideoUploader.cancelAllRequests();
                }
            }
        }
    }

    private static abstract class UploadWorkItemBase implements Runnable {
        protected int completedRetries;
        protected UploadContext uploadContext;

        /* renamed from: com.facebook.share.internal.VideoUploader$UploadWorkItemBase$1 */
        class C05061 implements Runnable {
            C05061() {
            }

            public void run() {
                UploadWorkItemBase.this.enqueueRetry(UploadWorkItemBase.this.completedRetries + 1);
            }
        }

        protected UploadWorkItemBase(UploadContext uploadContext, int i) {
            this.uploadContext = uploadContext;
            this.completedRetries = i;
        }

        private boolean attemptRetry(int i) {
            if (this.completedRetries >= 2 || !getTransientErrorCodes().contains(Integer.valueOf(i))) {
                return false;
            }
            VideoUploader.getHandler().postDelayed(new C05061(), (long) (((int) Math.pow(3.0d, (double) this.completedRetries)) * VideoUploader.RETRY_DELAY_UNIT_MS));
            return true;
        }

        protected void endUploadWithFailure(FacebookException facebookException) {
            issueResponseOnMainThread(facebookException, null);
        }

        protected abstract void enqueueRetry(int i);

        protected void executeGraphRequestSynchronously(Bundle bundle) {
            Bundle bundle2 = bundle;
            GraphResponse executeAndWait = new GraphRequest(this.uploadContext.accessToken, String.format(Locale.ROOT, "%s/videos", new Object[]{this.uploadContext.graphNode}), bundle2, HttpMethod.POST, null).executeAndWait();
            if (executeAndWait != null) {
                FacebookRequestError error = executeAndWait.getError();
                JSONObject jSONObject = executeAndWait.getJSONObject();
                if (error != null) {
                    if (!attemptRetry(error.getSubErrorCode())) {
                        handleError(new FacebookGraphResponseException(executeAndWait, VideoUploader.ERROR_UPLOAD));
                        return;
                    }
                    return;
                } else if (jSONObject != null) {
                    try {
                        handleSuccess(jSONObject);
                        return;
                    } catch (Throwable e) {
                        endUploadWithFailure(new FacebookException(VideoUploader.ERROR_BAD_SERVER_RESPONSE, e));
                        return;
                    }
                } else {
                    handleError(new FacebookException(VideoUploader.ERROR_BAD_SERVER_RESPONSE));
                    return;
                }
            }
            handleError(new FacebookException(VideoUploader.ERROR_BAD_SERVER_RESPONSE));
        }

        protected abstract Bundle getParameters() throws Exception;

        protected abstract Set<Integer> getTransientErrorCodes();

        protected abstract void handleError(FacebookException facebookException);

        protected abstract void handleSuccess(JSONObject jSONObject) throws JSONException;

        protected void issueResponseOnMainThread(final FacebookException facebookException, final String str) {
            VideoUploader.getHandler().post(new Runnable() {
                public void run() {
                    VideoUploader.issueResponse(UploadWorkItemBase.this.uploadContext, facebookException, str);
                }
            });
        }

        public void run() {
            if (this.uploadContext.isCanceled) {
                endUploadWithFailure(null);
                return;
            }
            try {
                executeGraphRequestSynchronously(getParameters());
            } catch (FacebookException e) {
                endUploadWithFailure(e);
            } catch (Throwable e2) {
                endUploadWithFailure(new FacebookException(VideoUploader.ERROR_UPLOAD, e2));
            }
        }
    }

    private static class FinishUploadWorkItem extends UploadWorkItemBase {
        static final Set<Integer> transientErrorCodes = new C05031();

        /* renamed from: com.facebook.share.internal.VideoUploader$FinishUploadWorkItem$1 */
        static final class C05031 extends HashSet<Integer> {
            C05031() {
                add(Integer.valueOf(1363011));
            }
        }

        public FinishUploadWorkItem(UploadContext uploadContext, int i) {
            super(uploadContext, i);
        }

        protected void enqueueRetry(int i) {
            VideoUploader.enqueueUploadFinish(this.uploadContext, i);
        }

        public Bundle getParameters() {
            Bundle bundle = new Bundle();
            if (this.uploadContext.params != null) {
                bundle.putAll(this.uploadContext.params);
            }
            bundle.putString(VideoUploader.PARAM_UPLOAD_PHASE, VideoUploader.PARAM_VALUE_UPLOAD_FINISH_PHASE);
            bundle.putString(VideoUploader.PARAM_SESSION_ID, this.uploadContext.sessionId);
            Utility.putNonEmptyString(bundle, "title", this.uploadContext.title);
            Utility.putNonEmptyString(bundle, "description", this.uploadContext.description);
            Utility.putNonEmptyString(bundle, VideoUploader.PARAM_REF, this.uploadContext.ref);
            return bundle;
        }

        protected Set<Integer> getTransientErrorCodes() {
            return transientErrorCodes;
        }

        protected void handleError(FacebookException facebookException) {
            VideoUploader.logError(facebookException, "Video '%s' failed to finish uploading", this.uploadContext.videoId);
            endUploadWithFailure(facebookException);
        }

        protected void handleSuccess(JSONObject jSONObject) throws JSONException {
            if (jSONObject.getBoolean(GraphResponse.SUCCESS_KEY)) {
                issueResponseOnMainThread(null, this.uploadContext.videoId);
            } else {
                handleError(new FacebookException(VideoUploader.ERROR_BAD_SERVER_RESPONSE));
            }
        }
    }

    private static class StartUploadWorkItem extends UploadWorkItemBase {
        static final Set<Integer> transientErrorCodes = new C05041();

        /* renamed from: com.facebook.share.internal.VideoUploader$StartUploadWorkItem$1 */
        static final class C05041 extends HashSet<Integer> {
            C05041() {
                add(Integer.valueOf(GamesStatusCodes.STATUS_MULTIPLAYER_ERROR_CREATION_NOT_ALLOWED));
            }
        }

        public StartUploadWorkItem(UploadContext uploadContext, int i) {
            super(uploadContext, i);
        }

        protected void enqueueRetry(int i) {
            VideoUploader.enqueueUploadStart(this.uploadContext, i);
        }

        public Bundle getParameters() {
            Bundle bundle = new Bundle();
            bundle.putString(VideoUploader.PARAM_UPLOAD_PHASE, VideoUploader.PARAM_VALUE_UPLOAD_START_PHASE);
            bundle.putLong(VideoUploader.PARAM_FILE_SIZE, this.uploadContext.videoSize);
            return bundle;
        }

        protected Set<Integer> getTransientErrorCodes() {
            return transientErrorCodes;
        }

        protected void handleError(FacebookException facebookException) {
            VideoUploader.logError(facebookException, "Error starting video upload", new Object[0]);
            endUploadWithFailure(facebookException);
        }

        protected void handleSuccess(JSONObject jSONObject) throws JSONException {
            this.uploadContext.sessionId = jSONObject.getString(VideoUploader.PARAM_SESSION_ID);
            this.uploadContext.videoId = jSONObject.getString(VideoUploader.PARAM_VIDEO_ID);
            VideoUploader.enqueueUploadChunk(this.uploadContext, jSONObject.getString(VideoUploader.PARAM_START_OFFSET), jSONObject.getString(VideoUploader.PARAM_END_OFFSET), 0);
        }
    }

    private static class TransferChunkWorkItem extends UploadWorkItemBase {
        static final Set<Integer> transientErrorCodes = new C05051();
        private String chunkEnd;
        private String chunkStart;

        /* renamed from: com.facebook.share.internal.VideoUploader$TransferChunkWorkItem$1 */
        static final class C05051 extends HashSet<Integer> {
            C05051() {
                add(Integer.valueOf(1363019));
                add(Integer.valueOf(1363021));
                add(Integer.valueOf(1363030));
                add(Integer.valueOf(1363033));
                add(Integer.valueOf(1363041));
            }
        }

        public TransferChunkWorkItem(UploadContext uploadContext, String str, String str2, int i) {
            super(uploadContext, i);
            this.chunkStart = str;
            this.chunkEnd = str2;
        }

        protected void enqueueRetry(int i) {
            VideoUploader.enqueueUploadChunk(this.uploadContext, this.chunkStart, this.chunkEnd, i);
        }

        public Bundle getParameters() throws IOException {
            Bundle bundle = new Bundle();
            bundle.putString(VideoUploader.PARAM_UPLOAD_PHASE, VideoUploader.PARAM_VALUE_UPLOAD_TRANSFER_PHASE);
            bundle.putString(VideoUploader.PARAM_SESSION_ID, this.uploadContext.sessionId);
            bundle.putString(VideoUploader.PARAM_START_OFFSET, this.chunkStart);
            byte[] access$600 = VideoUploader.getChunk(this.uploadContext, this.chunkStart, this.chunkEnd);
            if (access$600 != null) {
                bundle.putByteArray(VideoUploader.PARAM_VIDEO_FILE_CHUNK, access$600);
                return bundle;
            }
            throw new FacebookException("Error reading video");
        }

        protected Set<Integer> getTransientErrorCodes() {
            return transientErrorCodes;
        }

        protected void handleError(FacebookException facebookException) {
            VideoUploader.logError(facebookException, "Error uploading video '%s'", this.uploadContext.videoId);
            endUploadWithFailure(facebookException);
        }

        protected void handleSuccess(JSONObject jSONObject) throws JSONException {
            String string = jSONObject.getString(VideoUploader.PARAM_START_OFFSET);
            String string2 = jSONObject.getString(VideoUploader.PARAM_END_OFFSET);
            if (Utility.areObjectsEqual(string, string2)) {
                VideoUploader.enqueueUploadFinish(this.uploadContext, 0);
            } else {
                VideoUploader.enqueueUploadChunk(this.uploadContext, string, string2, 0);
            }
        }
    }

    private static class UploadContext {
        public final AccessToken accessToken;
        public final FacebookCallback<Result> callback;
        public String chunkStart;
        public final String description;
        public final String graphNode;
        public boolean isCanceled;
        public Bundle params;
        public final String ref;
        public String sessionId;
        public final String title;
        public String videoId;
        public long videoSize;
        public InputStream videoStream;
        public final Uri videoUri;
        public WorkItem workItem;

        private UploadContext(ShareVideoContent shareVideoContent, String str, FacebookCallback<Result> facebookCallback) {
            this.chunkStart = AppEventsConstants.EVENT_PARAM_VALUE_NO;
            this.accessToken = AccessToken.getCurrentAccessToken();
            this.videoUri = shareVideoContent.getVideo().getLocalUrl();
            this.title = shareVideoContent.getContentTitle();
            this.description = shareVideoContent.getContentDescription();
            this.ref = shareVideoContent.getRef();
            this.graphNode = str;
            this.callback = facebookCallback;
            this.params = shareVideoContent.getVideo().getParameters();
            if (!Utility.isNullOrEmpty(shareVideoContent.getPeopleIds())) {
                this.params.putString("tags", TextUtils.join(", ", shareVideoContent.getPeopleIds()));
            }
            if (!Utility.isNullOrEmpty(shareVideoContent.getPlaceId())) {
                this.params.putString("place", shareVideoContent.getPlaceId());
            }
            if (!Utility.isNullOrEmpty(shareVideoContent.getRef())) {
                this.params.putString(VideoUploader.PARAM_REF, shareVideoContent.getRef());
            }
        }

        private void initialize() throws FileNotFoundException {
            try {
                if (Utility.isFileUri(this.videoUri)) {
                    ParcelFileDescriptor open = ParcelFileDescriptor.open(new File(this.videoUri.getPath()), DriveFile.MODE_READ_ONLY);
                    this.videoSize = open.getStatSize();
                    this.videoStream = new AutoCloseInputStream(open);
                } else if (Utility.isContentUri(this.videoUri)) {
                    this.videoSize = Utility.getContentSize(this.videoUri);
                    this.videoStream = FacebookSdk.getApplicationContext().getContentResolver().openInputStream(this.videoUri);
                } else {
                    throw new FacebookException("Uri must be a content:// or file:// uri");
                }
            } catch (FileNotFoundException e) {
                Utility.closeQuietly(this.videoStream);
                throw e;
            }
        }
    }

    private static void cancelAllRequests() {
        synchronized (VideoUploader.class) {
            try {
                Iterator it = pendingUploads.iterator();
                while (true) {
                    Object hasNext = it.hasNext();
                    if (hasNext == null) {
                        break;
                    }
                    ((UploadContext) it.next()).isCanceled = true;
                }
            } finally {
                Class cls = VideoUploader.class;
            }
        }
    }

    private static void enqueueRequest(UploadContext uploadContext, Runnable runnable) {
        synchronized (VideoUploader.class) {
            try {
                Object addActiveWorkItem = uploadQueue.addActiveWorkItem(runnable);
                uploadContext.workItem = addActiveWorkItem;
            } finally {
                Class cls = VideoUploader.class;
            }
        }
    }

    private static void enqueueUploadChunk(UploadContext uploadContext, String str, String str2, int i) {
        enqueueRequest(uploadContext, new TransferChunkWorkItem(uploadContext, str, str2, i));
    }

    private static void enqueueUploadFinish(UploadContext uploadContext, int i) {
        enqueueRequest(uploadContext, new FinishUploadWorkItem(uploadContext, i));
    }

    private static void enqueueUploadStart(UploadContext uploadContext, int i) {
        enqueueRequest(uploadContext, new StartUploadWorkItem(uploadContext, i));
    }

    private static byte[] getChunk(UploadContext uploadContext, String str, String str2) throws IOException {
        if (Utility.areObjectsEqual(str, uploadContext.chunkStart)) {
            int read;
            int parseLong = (int) (Long.parseLong(str2) - Long.parseLong(str));
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            byte[] bArr = new byte[Math.min(8192, parseLong)];
            do {
                read = uploadContext.videoStream.read(bArr);
                if (read != -1) {
                    byteArrayOutputStream.write(bArr, 0, read);
                    parseLong -= read;
                    if (parseLong == 0) {
                    }
                }
                uploadContext.chunkStart = str2;
                return byteArrayOutputStream.toByteArray();
            } while (parseLong >= 0);
            logError(null, "Error reading video chunk. Expected buffer length - '%d'. Actual - '%d'.", Integer.valueOf(parseLong + read), Integer.valueOf(read));
            return null;
        }
        logError(null, "Error reading video chunk. Expected chunk '%s'. Requested chunk '%s'.", uploadContext.chunkStart, str);
        return null;
    }

    private static Handler getHandler() {
        synchronized (VideoUploader.class) {
            Class mainLooper;
            try {
                if (handler == null) {
                    mainLooper = Looper.getMainLooper();
                    handler = new Handler(mainLooper);
                }
                Handler handler = handler;
                return handler;
            } finally {
                mainLooper = VideoUploader.class;
            }
        }
    }

    private static void issueResponse(UploadContext uploadContext, FacebookException facebookException, String str) {
        removePendingUpload(uploadContext);
        Utility.closeQuietly(uploadContext.videoStream);
        if (uploadContext.callback == null) {
            return;
        }
        if (facebookException != null) {
            ShareInternalUtility.invokeOnErrorCallback(uploadContext.callback, facebookException);
        } else if (uploadContext.isCanceled) {
            ShareInternalUtility.invokeOnCancelCallback(uploadContext.callback);
        } else {
            ShareInternalUtility.invokeOnSuccessCallback(uploadContext.callback, str);
        }
    }

    private static void logError(Exception exception, String str, Object... objArr) {
        Log.e(TAG, String.format(Locale.ROOT, str, objArr), exception);
    }

    private static void registerAccessTokenTracker() {
        accessTokenTracker = new C05021();
    }

    private static void removePendingUpload(UploadContext uploadContext) {
        synchronized (VideoUploader.class) {
            try {
                Object obj = pendingUploads;
                obj.remove(uploadContext);
            } finally {
                Class cls = VideoUploader.class;
            }
        }
    }

    public static void uploadAsync(ShareVideoContent shareVideoContent, FacebookCallback<Result> facebookCallback) throws FileNotFoundException {
        synchronized (VideoUploader.class) {
            try {
                Object obj = "me";
                uploadAsync(shareVideoContent, obj, facebookCallback);
            } finally {
                Class cls = VideoUploader.class;
            }
        }
    }

    public static void uploadAsync(ShareVideoContent shareVideoContent, String str, FacebookCallback<Result> facebookCallback) throws FileNotFoundException {
        synchronized (VideoUploader.class) {
            try {
                if (!initialized) {
                    registerAccessTokenTracker();
                    initialized = true;
                }
                Validate.notNull(shareVideoContent, "videoContent");
                Validate.notNull(str, "graphNode");
                ShareVideo video = shareVideoContent.getVideo();
                Validate.notNull(video, "videoContent.video");
                Validate.notNull(video.getLocalUrl(), "videoContent.video.localUrl");
                VideoUploader uploadContext = new UploadContext(shareVideoContent, str, facebookCallback);
                uploadContext.initialize();
                pendingUploads.add(uploadContext);
                enqueueUploadStart(uploadContext, 0);
            } finally {
                Class cls = VideoUploader.class;
            }
        }
    }
}
