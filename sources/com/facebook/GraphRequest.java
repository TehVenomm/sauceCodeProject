package com.facebook;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;
import android.location.Location;
import android.net.Uri;
import android.net.Uri.Builder;
import android.os.Bundle;
import android.os.Handler;
import android.os.Parcel;
import android.os.ParcelFileDescriptor;
import android.os.ParcelFileDescriptor.AutoCloseInputStream;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.text.TextUtils;
import android.util.Log;
import android.util.Pair;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.internal.AttributionIdentifiers;
import com.facebook.internal.InternalSettings;
import com.facebook.internal.Logger;
import com.facebook.internal.NativeProtocol;
import com.facebook.internal.ServerProtocol;
import com.facebook.internal.Utility;
import com.facebook.internal.Validate;
import com.facebook.share.internal.OpenGraphJSONUtility;
import com.facebook.share.internal.OpenGraphJSONUtility.PhotoJSONProcessor;
import com.facebook.share.internal.ShareConstants;
import com.facebook.share.model.ShareOpenGraphObject;
import com.facebook.share.model.SharePhoto;
import com.google.android.gms.drive.DriveFile;
import com.google.android.gms.games.request.Requests;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.OutputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import java.net.URLEncoder;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.GZIPOutputStream;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class GraphRequest {
    private static final String ACCEPT_LANGUAGE_HEADER = "Accept-Language";
    public static final String ACCESS_TOKEN_PARAM = "access_token";
    private static final String ATTACHED_FILES_PARAM = "attached_files";
    private static final String ATTACHMENT_FILENAME_PREFIX = "file";
    private static final String BATCH_APP_ID_PARAM = "batch_app_id";
    private static final String BATCH_BODY_PARAM = "body";
    private static final String BATCH_ENTRY_DEPENDS_ON_PARAM = "depends_on";
    private static final String BATCH_ENTRY_NAME_PARAM = "name";
    private static final String BATCH_ENTRY_OMIT_RESPONSE_ON_SUCCESS_PARAM = "omit_response_on_success";
    private static final String BATCH_METHOD_PARAM = "method";
    private static final String BATCH_PARAM = "batch";
    private static final String BATCH_RELATIVE_URL_PARAM = "relative_url";
    private static final String CAPTION_PARAM = "caption";
    private static final String CONTENT_ENCODING_HEADER = "Content-Encoding";
    private static final String CONTENT_TYPE_HEADER = "Content-Type";
    private static final String DEBUG_KEY = "__debug__";
    private static final String DEBUG_MESSAGES_KEY = "messages";
    private static final String DEBUG_MESSAGE_KEY = "message";
    private static final String DEBUG_MESSAGE_LINK_KEY = "link";
    private static final String DEBUG_MESSAGE_TYPE_KEY = "type";
    private static final String DEBUG_PARAM = "debug";
    private static final String DEBUG_SEVERITY_INFO = "info";
    private static final String DEBUG_SEVERITY_WARNING = "warning";
    public static final String FIELDS_PARAM = "fields";
    private static final String FORMAT_JSON = "json";
    private static final String FORMAT_PARAM = "format";
    private static final String GRAPH_PATH_FORMAT = "%s/%s";
    private static final String ISO_8601_FORMAT_STRING = "yyyy-MM-dd'T'HH:mm:ssZ";
    public static final int MAXIMUM_BATCH_SIZE = 50;
    private static final String ME = "me";
    private static final String MIME_BOUNDARY = "3i2ndDfv2rTHiSisAbouNdArYfORhtTPEefj3q2f";
    private static final String MY_FRIENDS = "me/friends";
    private static final String MY_PHOTOS = "me/photos";
    private static final String PICTURE_PARAM = "picture";
    private static final String SDK_ANDROID = "android";
    private static final String SDK_PARAM = "sdk";
    private static final String SEARCH = "search";
    public static final String TAG = GraphRequest.class.getSimpleName();
    private static final String USER_AGENT_BASE = "FBAndroidSDK";
    private static final String USER_AGENT_HEADER = "User-Agent";
    private static final String VIDEOS_SUFFIX = "/videos";
    private static String defaultBatchApplicationId;
    private static volatile String userAgent;
    private static Pattern versionPattern = Pattern.compile("^/?v\\d+\\.\\d+/(.*)");
    private AccessToken accessToken;
    private String batchEntryDependsOn;
    private String batchEntryName;
    private boolean batchEntryOmitResultOnSuccess;
    private Callback callback;
    private JSONObject graphObject;
    private String graphPath;
    private HttpMethod httpMethod;
    private String overriddenURL;
    private Bundle parameters;
    private boolean skipClientToken;
    private Object tag;
    private String version;

    public interface Callback {
        void onCompleted(GraphResponse graphResponse);
    }

    private interface KeyValueSerializer {
        void writeString(String str, String str2) throws IOException;
    }

    /* renamed from: com.facebook.GraphRequest$7 */
    static final class C03597 implements PhotoJSONProcessor {
        C03597() {
        }

        public JSONObject toJSONObject(SharePhoto sharePhoto) {
            Uri imageUrl = sharePhoto.getImageUrl();
            JSONObject jSONObject = new JSONObject();
            try {
                jSONObject.put("url", imageUrl.toString());
                return jSONObject;
            } catch (Throwable e) {
                throw new FacebookException("Unable to attach images", e);
            }
        }
    }

    private static class Attachment {
        private final GraphRequest request;
        private final Object value;

        public Attachment(GraphRequest graphRequest, Object obj) {
            this.request = graphRequest;
            this.value = obj;
        }

        public GraphRequest getRequest() {
            return this.request;
        }

        public Object getValue() {
            return this.value;
        }
    }

    public interface GraphJSONArrayCallback {
        void onCompleted(JSONArray jSONArray, GraphResponse graphResponse);
    }

    public interface GraphJSONObjectCallback {
        void onCompleted(JSONObject jSONObject, GraphResponse graphResponse);
    }

    public interface OnProgressCallback extends Callback {
        void onProgress(long j, long j2);
    }

    public static class ParcelableResourceWithMimeType<RESOURCE extends Parcelable> implements Parcelable {
        public static final Creator<ParcelableResourceWithMimeType> CREATOR = new C03601();
        private final String mimeType;
        private final RESOURCE resource;

        /* renamed from: com.facebook.GraphRequest$ParcelableResourceWithMimeType$1 */
        static final class C03601 implements Creator<ParcelableResourceWithMimeType> {
            C03601() {
            }

            public ParcelableResourceWithMimeType createFromParcel(Parcel parcel) {
                return new ParcelableResourceWithMimeType(parcel);
            }

            public ParcelableResourceWithMimeType[] newArray(int i) {
                return new ParcelableResourceWithMimeType[i];
            }
        }

        private ParcelableResourceWithMimeType(Parcel parcel) {
            this.mimeType = parcel.readString();
            this.resource = parcel.readParcelable(FacebookSdk.getApplicationContext().getClassLoader());
        }

        public ParcelableResourceWithMimeType(RESOURCE resource, String str) {
            this.mimeType = str;
            this.resource = resource;
        }

        public int describeContents() {
            return 1;
        }

        public String getMimeType() {
            return this.mimeType;
        }

        public RESOURCE getResource() {
            return this.resource;
        }

        public void writeToParcel(Parcel parcel, int i) {
            parcel.writeString(this.mimeType);
            parcel.writeParcelable(this.resource, i);
        }
    }

    private static class Serializer implements KeyValueSerializer {
        private boolean firstWrite = true;
        private final Logger logger;
        private final OutputStream outputStream;
        private boolean useUrlEncode = false;

        public Serializer(OutputStream outputStream, Logger logger, boolean z) {
            this.outputStream = outputStream;
            this.logger = logger;
            this.useUrlEncode = z;
        }

        private RuntimeException getInvalidTypeError() {
            return new IllegalArgumentException("value is not a supported type.");
        }

        public void write(String str, Object... objArr) throws IOException {
            if (this.useUrlEncode) {
                this.outputStream.write(URLEncoder.encode(String.format(Locale.US, str, objArr), "UTF-8").getBytes());
                return;
            }
            if (this.firstWrite) {
                this.outputStream.write("--".getBytes());
                this.outputStream.write(GraphRequest.MIME_BOUNDARY.getBytes());
                this.outputStream.write("\r\n".getBytes());
                this.firstWrite = false;
            }
            this.outputStream.write(String.format(str, objArr).getBytes());
        }

        public void writeBitmap(String str, Bitmap bitmap) throws IOException {
            writeContentDisposition(str, str, "image/png");
            bitmap.compress(CompressFormat.PNG, 100, this.outputStream);
            writeLine("", new Object[0]);
            writeRecordBoundary();
            if (this.logger != null) {
                this.logger.appendKeyValue("    " + str, "<Image>");
            }
        }

        public void writeBytes(String str, byte[] bArr) throws IOException {
            writeContentDisposition(str, str, "content/unknown");
            this.outputStream.write(bArr);
            writeLine("", new Object[0]);
            writeRecordBoundary();
            if (this.logger != null) {
                this.logger.appendKeyValue("    " + str, String.format(Locale.ROOT, "<Data: %d>", new Object[]{Integer.valueOf(bArr.length)}));
            }
        }

        public void writeContentDisposition(String str, String str2, String str3) throws IOException {
            if (this.useUrlEncode) {
                this.outputStream.write(String.format("%s=", new Object[]{str}).getBytes());
                return;
            }
            write("Content-Disposition: form-data; name=\"%s\"", str);
            if (str2 != null) {
                write("; filename=\"%s\"", str2);
            }
            writeLine("", new Object[0]);
            if (str3 != null) {
                writeLine("%s: %s", "Content-Type", str3);
            }
            writeLine("", new Object[0]);
        }

        public void writeContentUri(String str, Uri uri, String str2) throws IOException {
            int i;
            if (str2 == null) {
                str2 = "content/unknown";
            }
            writeContentDisposition(str, str, str2);
            if (this.outputStream instanceof ProgressNoopOutputStream) {
                ((ProgressNoopOutputStream) this.outputStream).addProgress(Utility.getContentSize(uri));
                i = 0;
            } else {
                i = Utility.copyAndCloseInputStream(FacebookSdk.getApplicationContext().getContentResolver().openInputStream(uri), this.outputStream) + 0;
            }
            writeLine("", new Object[0]);
            writeRecordBoundary();
            if (this.logger != null) {
                this.logger.appendKeyValue("    " + str, String.format(Locale.ROOT, "<Data: %d>", new Object[]{Integer.valueOf(i)}));
            }
        }

        public void writeFile(String str, ParcelFileDescriptor parcelFileDescriptor, String str2) throws IOException {
            int i;
            if (str2 == null) {
                str2 = "content/unknown";
            }
            writeContentDisposition(str, str, str2);
            if (this.outputStream instanceof ProgressNoopOutputStream) {
                ((ProgressNoopOutputStream) this.outputStream).addProgress(parcelFileDescriptor.getStatSize());
                i = 0;
            } else {
                i = Utility.copyAndCloseInputStream(new AutoCloseInputStream(parcelFileDescriptor), this.outputStream) + 0;
            }
            writeLine("", new Object[0]);
            writeRecordBoundary();
            if (this.logger != null) {
                this.logger.appendKeyValue("    " + str, String.format(Locale.ROOT, "<Data: %d>", new Object[]{Integer.valueOf(i)}));
            }
        }

        public void writeLine(String str, Object... objArr) throws IOException {
            write(str, objArr);
            if (!this.useUrlEncode) {
                write("\r\n", new Object[0]);
            }
        }

        public void writeObject(String str, Object obj, GraphRequest graphRequest) throws IOException {
            if (this.outputStream instanceof RequestOutputStream) {
                ((RequestOutputStream) this.outputStream).setCurrentRequest(graphRequest);
            }
            if (GraphRequest.isSupportedParameterType(obj)) {
                writeString(str, GraphRequest.parameterToString(obj));
            } else if (obj instanceof Bitmap) {
                writeBitmap(str, (Bitmap) obj);
            } else if (obj instanceof byte[]) {
                writeBytes(str, (byte[]) obj);
            } else if (obj instanceof Uri) {
                writeContentUri(str, (Uri) obj, null);
            } else if (obj instanceof ParcelFileDescriptor) {
                writeFile(str, (ParcelFileDescriptor) obj, null);
            } else if (obj instanceof ParcelableResourceWithMimeType) {
                ParcelableResourceWithMimeType parcelableResourceWithMimeType = (ParcelableResourceWithMimeType) obj;
                Parcelable resource = parcelableResourceWithMimeType.getResource();
                String mimeType = parcelableResourceWithMimeType.getMimeType();
                if (resource instanceof ParcelFileDescriptor) {
                    writeFile(str, (ParcelFileDescriptor) resource, mimeType);
                } else if (resource instanceof Uri) {
                    writeContentUri(str, (Uri) resource, mimeType);
                } else {
                    throw getInvalidTypeError();
                }
            } else {
                throw getInvalidTypeError();
            }
        }

        public void writeRecordBoundary() throws IOException {
            if (this.useUrlEncode) {
                this.outputStream.write("&".getBytes());
                return;
            }
            writeLine("--%s", GraphRequest.MIME_BOUNDARY);
        }

        public void writeRequestsAsJson(String str, JSONArray jSONArray, Collection<GraphRequest> collection) throws IOException, JSONException {
            if (this.outputStream instanceof RequestOutputStream) {
                RequestOutputStream requestOutputStream = (RequestOutputStream) this.outputStream;
                writeContentDisposition(str, null, null);
                write("[", new Object[0]);
                int i = 0;
                for (GraphRequest graphRequest : collection) {
                    JSONObject jSONObject = jSONArray.getJSONObject(i);
                    requestOutputStream.setCurrentRequest(graphRequest);
                    if (i > 0) {
                        write(",%s", jSONObject.toString());
                    } else {
                        write("%s", jSONObject.toString());
                    }
                    i++;
                }
                write("]", new Object[0]);
                if (this.logger != null) {
                    this.logger.appendKeyValue("    " + str, jSONArray.toString());
                    return;
                }
                return;
            }
            writeString(str, jSONArray.toString());
        }

        public void writeString(String str, String str2) throws IOException {
            writeContentDisposition(str, null, null);
            writeLine("%s", str2);
            writeRecordBoundary();
            if (this.logger != null) {
                this.logger.appendKeyValue("    " + str, str2);
            }
        }
    }

    public GraphRequest() {
        this(null, null, null, null, null);
    }

    public GraphRequest(AccessToken accessToken, String str) {
        this(accessToken, str, null, null, null);
    }

    public GraphRequest(AccessToken accessToken, String str, Bundle bundle, HttpMethod httpMethod) {
        this(accessToken, str, bundle, httpMethod, null);
    }

    public GraphRequest(AccessToken accessToken, String str, Bundle bundle, HttpMethod httpMethod, Callback callback) {
        this(accessToken, str, bundle, httpMethod, callback, null);
    }

    public GraphRequest(AccessToken accessToken, String str, Bundle bundle, HttpMethod httpMethod, Callback callback, String str2) {
        this.batchEntryOmitResultOnSuccess = true;
        this.skipClientToken = false;
        this.accessToken = accessToken;
        this.graphPath = str;
        this.version = str2;
        setCallback(callback);
        setHttpMethod(httpMethod);
        if (bundle != null) {
            this.parameters = new Bundle(bundle);
        } else {
            this.parameters = new Bundle();
        }
        if (this.version == null) {
            this.version = ServerProtocol.getAPIVersion();
        }
    }

    GraphRequest(AccessToken accessToken, URL url) {
        this.batchEntryOmitResultOnSuccess = true;
        this.skipClientToken = false;
        this.accessToken = accessToken;
        this.overriddenURL = url.toString();
        setHttpMethod(HttpMethod.GET);
        this.parameters = new Bundle();
    }

    private void addCommonParameters() {
        String token;
        if (this.accessToken != null) {
            if (!this.parameters.containsKey("access_token")) {
                token = this.accessToken.getToken();
                Logger.registerAccessToken(token);
                this.parameters.putString("access_token", token);
            }
        } else if (!(this.skipClientToken || this.parameters.containsKey("access_token"))) {
            token = FacebookSdk.getApplicationId();
            String clientToken = FacebookSdk.getClientToken();
            if (Utility.isNullOrEmpty(token) || Utility.isNullOrEmpty(clientToken)) {
                Log.d(TAG, "Warning: Request without access token missing application ID or client token.");
            } else {
                this.parameters.putString("access_token", token + "|" + clientToken);
            }
        }
        this.parameters.putString("sdk", "android");
        this.parameters.putString(FORMAT_PARAM, FORMAT_JSON);
        if (FacebookSdk.isLoggingBehaviorEnabled(LoggingBehavior.GRAPH_API_DEBUG_INFO)) {
            this.parameters.putString("debug", DEBUG_SEVERITY_INFO);
        } else if (FacebookSdk.isLoggingBehaviorEnabled(LoggingBehavior.GRAPH_API_DEBUG_WARNING)) {
            this.parameters.putString("debug", DEBUG_SEVERITY_WARNING);
        }
    }

    private String appendParametersToBaseUrl(String str) {
        Builder encodedPath = new Builder().encodedPath(str);
        for (String str2 : this.parameters.keySet()) {
            Object obj = this.parameters.get(str2);
            if (obj == null) {
                obj = "";
            }
            if (isSupportedParameterType(obj)) {
                encodedPath.appendQueryParameter(str2, parameterToString(obj).toString());
            } else if (this.httpMethod == HttpMethod.GET) {
                throw new IllegalArgumentException(String.format(Locale.US, "Unsupported parameter type for GET request: %s", new Object[]{obj.getClass().getSimpleName()}));
            }
        }
        return encodedPath.toString();
    }

    private static HttpURLConnection createConnection(URL url) throws IOException {
        HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();
        httpURLConnection.setRequestProperty("User-Agent", getUserAgent());
        httpURLConnection.setRequestProperty(ACCEPT_LANGUAGE_HEADER, Locale.getDefault().toString());
        httpURLConnection.setChunkedStreamingMode(0);
        return httpURLConnection;
    }

    public static GraphRequest createOpenGraphObject(ShareOpenGraphObject shareOpenGraphObject) throws FacebookException {
        String string = shareOpenGraphObject.getString("type");
        if ((string == null ? shareOpenGraphObject.getString("og:type") : string) == null) {
            throw new FacebookException("Open graph object type cannot be null");
        }
        try {
            JSONObject jSONObject = (JSONObject) OpenGraphJSONUtility.toJSONValue(shareOpenGraphObject, new C03597());
            Bundle bundle = new Bundle();
            bundle.putString("object", jSONObject.toString());
            Locale locale = Locale.ROOT;
            StringBuilder stringBuilder = new StringBuilder();
            return new GraphRequest(AccessToken.getCurrentAccessToken(), String.format(locale, GRAPH_PATH_FORMAT, new Object[]{ME, stringBuilder.append("objects/").append(r1).toString()}), bundle, HttpMethod.POST);
        } catch (JSONException e) {
            throw new FacebookException(e.getMessage());
        }
    }

    public static GraphResponse executeAndWait(GraphRequest graphRequest) {
        List executeBatchAndWait = executeBatchAndWait(graphRequest);
        if (executeBatchAndWait != null && executeBatchAndWait.size() == 1) {
            return (GraphResponse) executeBatchAndWait.get(0);
        }
        throw new FacebookException("invalid state: expected a single response");
    }

    public static List<GraphResponse> executeBatchAndWait(GraphRequestBatch graphRequestBatch) {
        List<GraphResponse> executeConnectionAndWait;
        URLConnection uRLConnection = null;
        Validate.notEmptyAndContainsNoNulls(graphRequestBatch, Requests.EXTRA_REQUESTS);
        try {
            uRLConnection = toHttpConnection(graphRequestBatch);
            executeConnectionAndWait = executeConnectionAndWait((HttpURLConnection) uRLConnection, graphRequestBatch);
        } catch (Throwable e) {
            executeConnectionAndWait = GraphResponse.constructErrorResponses(graphRequestBatch.getRequests(), null, new FacebookException(e));
            runCallbacks(graphRequestBatch, executeConnectionAndWait);
        } finally {
            Utility.disconnectQuietly(uRLConnection);
        }
        return executeConnectionAndWait;
    }

    public static List<GraphResponse> executeBatchAndWait(Collection<GraphRequest> collection) {
        return executeBatchAndWait(new GraphRequestBatch((Collection) collection));
    }

    public static List<GraphResponse> executeBatchAndWait(GraphRequest... graphRequestArr) {
        Validate.notNull(graphRequestArr, Requests.EXTRA_REQUESTS);
        return executeBatchAndWait(Arrays.asList(graphRequestArr));
    }

    public static GraphRequestAsyncTask executeBatchAsync(GraphRequestBatch graphRequestBatch) {
        Validate.notEmptyAndContainsNoNulls(graphRequestBatch, Requests.EXTRA_REQUESTS);
        GraphRequestAsyncTask graphRequestAsyncTask = new GraphRequestAsyncTask(graphRequestBatch);
        graphRequestAsyncTask.executeOnExecutor(FacebookSdk.getExecutor(), new Void[0]);
        return graphRequestAsyncTask;
    }

    public static GraphRequestAsyncTask executeBatchAsync(Collection<GraphRequest> collection) {
        return executeBatchAsync(new GraphRequestBatch((Collection) collection));
    }

    public static GraphRequestAsyncTask executeBatchAsync(GraphRequest... graphRequestArr) {
        Validate.notNull(graphRequestArr, Requests.EXTRA_REQUESTS);
        return executeBatchAsync(Arrays.asList(graphRequestArr));
    }

    public static List<GraphResponse> executeConnectionAndWait(HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) {
        List<GraphResponse> fromHttpConnection = GraphResponse.fromHttpConnection(httpURLConnection, graphRequestBatch);
        Utility.disconnectQuietly(httpURLConnection);
        if (graphRequestBatch.size() != fromHttpConnection.size()) {
            throw new FacebookException(String.format(Locale.US, "Received %d responses while expecting %d", new Object[]{Integer.valueOf(fromHttpConnection.size()), Integer.valueOf(graphRequestBatch.size())}));
        }
        runCallbacks(graphRequestBatch, fromHttpConnection);
        AccessTokenManager.getInstance().extendAccessTokenIfNeeded();
        return fromHttpConnection;
    }

    public static List<GraphResponse> executeConnectionAndWait(HttpURLConnection httpURLConnection, Collection<GraphRequest> collection) {
        return executeConnectionAndWait(httpURLConnection, new GraphRequestBatch((Collection) collection));
    }

    public static GraphRequestAsyncTask executeConnectionAsync(Handler handler, HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) {
        Validate.notNull(httpURLConnection, "connection");
        GraphRequestAsyncTask graphRequestAsyncTask = new GraphRequestAsyncTask(httpURLConnection, graphRequestBatch);
        graphRequestBatch.setCallbackHandler(handler);
        graphRequestAsyncTask.executeOnExecutor(FacebookSdk.getExecutor(), new Void[0]);
        return graphRequestAsyncTask;
    }

    public static GraphRequestAsyncTask executeConnectionAsync(HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) {
        return executeConnectionAsync(null, httpURLConnection, graphRequestBatch);
    }

    private static String getBatchAppId(GraphRequestBatch graphRequestBatch) {
        if (!Utility.isNullOrEmpty(graphRequestBatch.getBatchApplicationId())) {
            return graphRequestBatch.getBatchApplicationId();
        }
        Iterator it = graphRequestBatch.iterator();
        while (it.hasNext()) {
            AccessToken accessToken = ((GraphRequest) it.next()).accessToken;
            if (accessToken != null) {
                String applicationId = accessToken.getApplicationId();
                if (applicationId != null) {
                    return applicationId;
                }
            }
        }
        return !Utility.isNullOrEmpty(defaultBatchApplicationId) ? defaultBatchApplicationId : FacebookSdk.getApplicationId();
    }

    public static final String getDefaultBatchApplicationId() {
        return defaultBatchApplicationId;
    }

    private static String getDefaultPhotoPathIfNull(String str) {
        return str == null ? "me/photos" : str;
    }

    private String getGraphPathWithVersion() {
        if (versionPattern.matcher(this.graphPath).matches()) {
            return this.graphPath;
        }
        return String.format(GRAPH_PATH_FORMAT, new Object[]{this.version, this.graphPath});
    }

    private static String getMimeContentType() {
        return String.format("multipart/form-data; boundary=%s", new Object[]{MIME_BOUNDARY});
    }

    private static String getUserAgent() {
        if (userAgent == null) {
            userAgent = String.format("%s.%s", new Object[]{USER_AGENT_BASE, FacebookSdkVersion.BUILD});
            if (!Utility.isNullOrEmpty(InternalSettings.getCustomUserAgent())) {
                userAgent = String.format(Locale.ROOT, GRAPH_PATH_FORMAT, new Object[]{userAgent, r0});
            }
        }
        return userAgent;
    }

    private static boolean hasOnProgressCallbacks(GraphRequestBatch graphRequestBatch) {
        for (com.facebook.GraphRequestBatch.Callback callback : graphRequestBatch.getCallbacks()) {
            if (callback instanceof com.facebook.GraphRequestBatch.OnProgressCallback) {
                return true;
            }
        }
        Iterator it = graphRequestBatch.iterator();
        while (it.hasNext()) {
            if (((GraphRequest) it.next()).getCallback() instanceof OnProgressCallback) {
                return true;
            }
        }
        return false;
    }

    private static boolean isGzipCompressible(GraphRequestBatch graphRequestBatch) {
        Iterator it = graphRequestBatch.iterator();
        while (it.hasNext()) {
            GraphRequest graphRequest = (GraphRequest) it.next();
            for (String str : graphRequest.parameters.keySet()) {
                if (isSupportedAttachmentType(graphRequest.parameters.get(str))) {
                    return false;
                }
            }
        }
        return true;
    }

    private static boolean isMeRequest(String str) {
        Matcher matcher = versionPattern.matcher(str);
        if (matcher.matches()) {
            str = matcher.group(1);
        }
        return str.startsWith("me/") || str.startsWith("/me/");
    }

    private static boolean isSupportedAttachmentType(Object obj) {
        return (obj instanceof Bitmap) || (obj instanceof byte[]) || (obj instanceof Uri) || (obj instanceof ParcelFileDescriptor) || (obj instanceof ParcelableResourceWithMimeType);
    }

    private static boolean isSupportedParameterType(Object obj) {
        return (obj instanceof String) || (obj instanceof Boolean) || (obj instanceof Number) || (obj instanceof Date);
    }

    public static GraphRequest newCustomAudienceThirdPartyIdRequest(AccessToken accessToken, Context context, Callback callback) {
        return newCustomAudienceThirdPartyIdRequest(accessToken, context, null, callback);
    }

    public static GraphRequest newCustomAudienceThirdPartyIdRequest(AccessToken accessToken, Context context, String str, Callback callback) {
        String applicationId = (str != null || accessToken == null) ? str : accessToken.getApplicationId();
        if (applicationId == null) {
            applicationId = Utility.getMetadataApplicationId(context);
        }
        if (applicationId == null) {
            throw new FacebookException("Facebook App ID cannot be determined");
        }
        String str2 = applicationId + "/custom_audience_third_party_id";
        AttributionIdentifiers attributionIdentifiers = AttributionIdentifiers.getAttributionIdentifiers(context);
        Bundle bundle = new Bundle();
        if (accessToken == null) {
            if (attributionIdentifiers == null) {
                throw new FacebookException("There is no access token and attribution identifiers could not be retrieved");
            }
            applicationId = attributionIdentifiers.getAttributionId() != null ? attributionIdentifiers.getAttributionId() : attributionIdentifiers.getAndroidAdvertiserId();
            if (attributionIdentifiers.getAttributionId() != null) {
                bundle.putString("udid", applicationId);
            }
        }
        if (FacebookSdk.getLimitEventAndDataUsage(context) || (attributionIdentifiers != null && attributionIdentifiers.isTrackingLimited())) {
            bundle.putString("limit_event_usage", AppEventsConstants.EVENT_PARAM_VALUE_YES);
        }
        return new GraphRequest(accessToken, str2, bundle, HttpMethod.GET, callback);
    }

    public static GraphRequest newDeleteObjectRequest(AccessToken accessToken, String str, Callback callback) {
        return new GraphRequest(accessToken, str, null, HttpMethod.DELETE, callback);
    }

    public static GraphRequest newGraphPathRequest(AccessToken accessToken, String str, Callback callback) {
        return new GraphRequest(accessToken, str, null, null, callback);
    }

    public static GraphRequest newMeRequest(AccessToken accessToken, final GraphJSONObjectCallback graphJSONObjectCallback) {
        return new GraphRequest(accessToken, ME, null, null, new Callback() {
            public void onCompleted(GraphResponse graphResponse) {
                if (graphJSONObjectCallback != null) {
                    graphJSONObjectCallback.onCompleted(graphResponse.getJSONObject(), graphResponse);
                }
            }
        });
    }

    public static GraphRequest newMyFriendsRequest(AccessToken accessToken, final GraphJSONArrayCallback graphJSONArrayCallback) {
        return new GraphRequest(accessToken, MY_FRIENDS, null, null, new Callback() {
            public void onCompleted(GraphResponse graphResponse) {
                if (graphJSONArrayCallback != null) {
                    JSONObject jSONObject = graphResponse.getJSONObject();
                    graphJSONArrayCallback.onCompleted(jSONObject != null ? jSONObject.optJSONArray(ShareConstants.WEB_DIALOG_PARAM_DATA) : null, graphResponse);
                }
            }
        });
    }

    public static GraphRequest newPlacesSearchRequest(AccessToken accessToken, Location location, int i, int i2, String str, final GraphJSONArrayCallback graphJSONArrayCallback) {
        if (location == null && Utility.isNullOrEmpty(str)) {
            throw new FacebookException("Either location or searchText must be specified.");
        }
        Bundle bundle = new Bundle(5);
        bundle.putString("type", "place");
        bundle.putInt("limit", i2);
        if (location != null) {
            bundle.putString("center", String.format(Locale.US, "%f,%f", new Object[]{Double.valueOf(location.getLatitude()), Double.valueOf(location.getLongitude())}));
            bundle.putInt("distance", i);
        }
        if (!Utility.isNullOrEmpty(str)) {
            bundle.putString("q", str);
        }
        return new GraphRequest(accessToken, "search", bundle, HttpMethod.GET, new Callback() {
            public void onCompleted(GraphResponse graphResponse) {
                if (graphJSONArrayCallback != null) {
                    JSONObject jSONObject = graphResponse.getJSONObject();
                    graphJSONArrayCallback.onCompleted(jSONObject != null ? jSONObject.optJSONArray(ShareConstants.WEB_DIALOG_PARAM_DATA) : null, graphResponse);
                }
            }
        });
    }

    public static GraphRequest newPostRequest(AccessToken accessToken, String str, JSONObject jSONObject, Callback callback) {
        GraphRequest graphRequest = new GraphRequest(accessToken, str, null, HttpMethod.POST, callback);
        graphRequest.setGraphObject(jSONObject);
        return graphRequest;
    }

    public static GraphRequest newUploadPhotoRequest(AccessToken accessToken, String str, Bitmap bitmap, String str2, Bundle bundle, Callback callback) {
        String defaultPhotoPathIfNull = getDefaultPhotoPathIfNull(str);
        Bundle bundle2 = new Bundle();
        if (bundle != null) {
            bundle2.putAll(bundle);
        }
        bundle2.putParcelable("picture", bitmap);
        if (!(str2 == null || str2.isEmpty())) {
            bundle2.putString("caption", str2);
        }
        return new GraphRequest(accessToken, defaultPhotoPathIfNull, bundle2, HttpMethod.POST, callback);
    }

    public static GraphRequest newUploadPhotoRequest(AccessToken accessToken, String str, Uri uri, String str2, Bundle bundle, Callback callback) throws FileNotFoundException {
        String defaultPhotoPathIfNull = getDefaultPhotoPathIfNull(str);
        if (Utility.isFileUri(uri)) {
            return newUploadPhotoRequest(accessToken, defaultPhotoPathIfNull, new File(uri.getPath()), str2, bundle, callback);
        } else if (Utility.isContentUri(uri)) {
            Bundle bundle2 = new Bundle();
            if (bundle != null) {
                bundle2.putAll(bundle);
            }
            bundle2.putParcelable("picture", uri);
            return new GraphRequest(accessToken, defaultPhotoPathIfNull, bundle2, HttpMethod.POST, callback);
        } else {
            throw new FacebookException("The photo Uri must be either a file:// or content:// Uri");
        }
    }

    public static GraphRequest newUploadPhotoRequest(AccessToken accessToken, String str, File file, String str2, Bundle bundle, Callback callback) throws FileNotFoundException {
        String defaultPhotoPathIfNull = getDefaultPhotoPathIfNull(str);
        Parcelable open = ParcelFileDescriptor.open(file, DriveFile.MODE_READ_ONLY);
        Bundle bundle2 = new Bundle();
        if (bundle != null) {
            bundle2.putAll(bundle);
        }
        bundle2.putParcelable("picture", open);
        if (!(str2 == null || str2.isEmpty())) {
            bundle2.putString("caption", str2);
        }
        return new GraphRequest(accessToken, defaultPhotoPathIfNull, bundle2, HttpMethod.POST, callback);
    }

    private static String parameterToString(Object obj) {
        if (obj instanceof String) {
            return (String) obj;
        }
        if ((obj instanceof Boolean) || (obj instanceof Number)) {
            return obj.toString();
        }
        if (obj instanceof Date) {
            return new SimpleDateFormat(ISO_8601_FORMAT_STRING, Locale.US).format(obj);
        }
        throw new IllegalArgumentException("Unsupported parameter type.");
    }

    private static void processGraphObject(JSONObject jSONObject, String str, KeyValueSerializer keyValueSerializer) throws IOException {
        Object obj;
        if (isMeRequest(str)) {
            int indexOf = str.indexOf(":");
            int indexOf2 = str.indexOf("?");
            obj = (indexOf <= 3 || (indexOf2 != -1 && indexOf >= indexOf2)) ? null : 1;
        } else {
            obj = null;
        }
        Iterator keys = jSONObject.keys();
        while (keys.hasNext()) {
            String str2 = (String) keys.next();
            Object opt = jSONObject.opt(str2);
            boolean z = obj != null && str2.equalsIgnoreCase("image");
            processGraphObjectProperty(str2, opt, keyValueSerializer, z);
        }
    }

    private static void processGraphObjectProperty(String str, Object obj, KeyValueSerializer keyValueSerializer, boolean z) throws IOException {
        Class cls = obj.getClass();
        if (JSONObject.class.isAssignableFrom(cls)) {
            JSONObject jSONObject = (JSONObject) obj;
            if (z) {
                Iterator keys = jSONObject.keys();
                while (keys.hasNext()) {
                    Object[] objArr = new Object[]{str, (String) keys.next()};
                    processGraphObjectProperty(String.format("%s[%s]", objArr), jSONObject.opt((String) keys.next()), keyValueSerializer, z);
                }
            } else if (jSONObject.has("id")) {
                processGraphObjectProperty(str, jSONObject.optString("id"), keyValueSerializer, z);
            } else if (jSONObject.has("url")) {
                processGraphObjectProperty(str, jSONObject.optString("url"), keyValueSerializer, z);
            } else if (jSONObject.has(NativeProtocol.OPEN_GRAPH_CREATE_OBJECT_KEY)) {
                processGraphObjectProperty(str, jSONObject.toString(), keyValueSerializer, z);
            }
        } else if (JSONArray.class.isAssignableFrom(cls)) {
            JSONArray jSONArray = (JSONArray) obj;
            int length = jSONArray.length();
            for (int i = 0; i < length; i++) {
                processGraphObjectProperty(String.format(Locale.ROOT, "%s[%d]", new Object[]{str, Integer.valueOf(i)}), jSONArray.opt(i), keyValueSerializer, z);
            }
        } else if (String.class.isAssignableFrom(cls) || Number.class.isAssignableFrom(cls) || Boolean.class.isAssignableFrom(cls)) {
            keyValueSerializer.writeString(str, obj.toString());
        } else if (Date.class.isAssignableFrom(cls)) {
            keyValueSerializer.writeString(str, new SimpleDateFormat(ISO_8601_FORMAT_STRING, Locale.US).format((Date) obj));
        }
    }

    private static void processRequest(GraphRequestBatch graphRequestBatch, Logger logger, int i, URL url, OutputStream outputStream, boolean z) throws IOException, JSONException {
        Serializer serializer = new Serializer(outputStream, logger, z);
        String batchAppId;
        if (i == 1) {
            GraphRequest graphRequest = graphRequestBatch.get(0);
            Map hashMap = new HashMap();
            for (String batchAppId2 : graphRequest.parameters.keySet()) {
                Object obj = graphRequest.parameters.get(batchAppId2);
                if (isSupportedAttachmentType(obj)) {
                    hashMap.put(batchAppId2, new Attachment(graphRequest, obj));
                }
            }
            if (logger != null) {
                logger.append("  Parameters:\n");
            }
            serializeParameters(graphRequest.parameters, serializer, graphRequest);
            if (logger != null) {
                logger.append("  Attachments:\n");
            }
            serializeAttachments(hashMap, serializer);
            if (graphRequest.graphObject != null) {
                processGraphObject(graphRequest.graphObject, url.getPath(), serializer);
                return;
            }
            return;
        }
        batchAppId2 = getBatchAppId(graphRequestBatch);
        if (Utility.isNullOrEmpty(batchAppId2)) {
            throw new FacebookException("App ID was not specified at the request or Settings.");
        }
        serializer.writeString(BATCH_APP_ID_PARAM, batchAppId2);
        Map hashMap2 = new HashMap();
        serializeRequestsAsJSON(serializer, graphRequestBatch, hashMap2);
        if (logger != null) {
            logger.append("  Attachments:\n");
        }
        serializeAttachments(hashMap2, serializer);
    }

    static void runCallbacks(final GraphRequestBatch graphRequestBatch, List<GraphResponse> list) {
        int size = graphRequestBatch.size();
        final ArrayList arrayList = new ArrayList();
        for (int i = 0; i < size; i++) {
            GraphRequest graphRequest = graphRequestBatch.get(i);
            if (graphRequest.callback != null) {
                arrayList.add(new Pair(graphRequest.callback, list.get(i)));
            }
        }
        if (arrayList.size() > 0) {
            Runnable c03575 = new Runnable() {
                public void run() {
                    Iterator it = arrayList.iterator();
                    while (it.hasNext()) {
                        Pair pair = (Pair) it.next();
                        ((Callback) pair.first).onCompleted((GraphResponse) pair.second);
                    }
                    for (com.facebook.GraphRequestBatch.Callback onBatchCompleted : graphRequestBatch.getCallbacks()) {
                        onBatchCompleted.onBatchCompleted(graphRequestBatch);
                    }
                }
            };
            Handler callbackHandler = graphRequestBatch.getCallbackHandler();
            if (callbackHandler == null) {
                c03575.run();
            } else {
                callbackHandler.post(c03575);
            }
        }
    }

    private static void serializeAttachments(Map<String, Attachment> map, Serializer serializer) throws IOException {
        for (String str : map.keySet()) {
            Attachment attachment = (Attachment) map.get(str);
            if (isSupportedAttachmentType(attachment.getValue())) {
                serializer.writeObject(str, attachment.getValue(), attachment.getRequest());
            }
        }
    }

    private static void serializeParameters(Bundle bundle, Serializer serializer, GraphRequest graphRequest) throws IOException {
        for (String str : bundle.keySet()) {
            Object obj = bundle.get(str);
            if (isSupportedParameterType(obj)) {
                serializer.writeObject(str, obj, graphRequest);
            }
        }
    }

    private static void serializeRequestsAsJSON(Serializer serializer, Collection<GraphRequest> collection, Map<String, Attachment> map) throws JSONException, IOException {
        JSONArray jSONArray = new JSONArray();
        for (GraphRequest serializeToBatch : collection) {
            serializeToBatch.serializeToBatch(jSONArray, map);
        }
        serializer.writeRequestsAsJson(BATCH_PARAM, jSONArray, collection);
    }

    private void serializeToBatch(JSONArray jSONArray, Map<String, Attachment> map) throws JSONException, IOException {
        JSONObject jSONObject = new JSONObject();
        if (this.batchEntryName != null) {
            jSONObject.put("name", this.batchEntryName);
            jSONObject.put(BATCH_ENTRY_OMIT_RESPONSE_ON_SUCCESS_PARAM, this.batchEntryOmitResultOnSuccess);
        }
        if (this.batchEntryDependsOn != null) {
            jSONObject.put(BATCH_ENTRY_DEPENDS_ON_PARAM, this.batchEntryDependsOn);
        }
        String urlForBatchedRequest = getUrlForBatchedRequest();
        jSONObject.put(BATCH_RELATIVE_URL_PARAM, urlForBatchedRequest);
        jSONObject.put(BATCH_METHOD_PARAM, this.httpMethod);
        if (this.accessToken != null) {
            Logger.registerAccessToken(this.accessToken.getToken());
        }
        Iterable arrayList = new ArrayList();
        for (String str : this.parameters.keySet()) {
            Object obj = this.parameters.get(str);
            if (isSupportedAttachmentType(obj)) {
                String format = String.format(Locale.ROOT, "%s%d", new Object[]{ATTACHMENT_FILENAME_PREFIX, Integer.valueOf(map.size())});
                arrayList.add(format);
                map.put(format, new Attachment(this, obj));
            }
        }
        if (!arrayList.isEmpty()) {
            jSONObject.put(ATTACHED_FILES_PARAM, TextUtils.join(",", arrayList));
        }
        if (this.graphObject != null) {
            final Iterable arrayList2 = new ArrayList();
            processGraphObject(this.graphObject, urlForBatchedRequest, new KeyValueSerializer() {
                public void writeString(String str, String str2) throws IOException {
                    arrayList2.add(String.format(Locale.US, "%s=%s", new Object[]{str, URLEncoder.encode(str2, "UTF-8")}));
                }
            });
            jSONObject.put("body", TextUtils.join("&", arrayList2));
        }
        jSONArray.put(jSONObject);
    }

    static final void serializeToUrlConnection(GraphRequestBatch graphRequestBatch, HttpURLConnection httpURLConnection) throws IOException, JSONException {
        Throwable th;
        OutputStream outputStream = null;
        Logger logger = new Logger(LoggingBehavior.REQUESTS, "Request");
        int size = graphRequestBatch.size();
        boolean isGzipCompressible = isGzipCompressible(graphRequestBatch);
        HttpMethod httpMethod = size == 1 ? graphRequestBatch.get(0).httpMethod : HttpMethod.POST;
        httpURLConnection.setRequestMethod(httpMethod.name());
        setConnectionContentType(httpURLConnection, isGzipCompressible);
        URL url = httpURLConnection.getURL();
        logger.append("Request:\n");
        logger.appendKeyValue("Id", graphRequestBatch.getId());
        logger.appendKeyValue("URL", url);
        logger.appendKeyValue("Method", httpURLConnection.getRequestMethod());
        logger.appendKeyValue("User-Agent", httpURLConnection.getRequestProperty("User-Agent"));
        logger.appendKeyValue("Content-Type", httpURLConnection.getRequestProperty("Content-Type"));
        httpURLConnection.setConnectTimeout(graphRequestBatch.getTimeout());
        httpURLConnection.setReadTimeout(graphRequestBatch.getTimeout());
        if (httpMethod == HttpMethod.POST) {
            httpURLConnection.setDoOutput(true);
            OutputStream gZIPOutputStream;
            try {
                OutputStream bufferedOutputStream = new BufferedOutputStream(httpURLConnection.getOutputStream());
                if (isGzipCompressible) {
                    try {
                        gZIPOutputStream = new GZIPOutputStream(bufferedOutputStream);
                    } catch (Throwable th2) {
                        gZIPOutputStream = bufferedOutputStream;
                        th = th2;
                        if (gZIPOutputStream != null) {
                            gZIPOutputStream.close();
                        }
                        throw th;
                    }
                }
                gZIPOutputStream = bufferedOutputStream;
                try {
                    if (hasOnProgressCallbacks(graphRequestBatch)) {
                        outputStream = new ProgressNoopOutputStream(graphRequestBatch.getCallbackHandler());
                        processRequest(graphRequestBatch, null, size, url, outputStream, isGzipCompressible);
                        outputStream = new ProgressOutputStream(gZIPOutputStream, graphRequestBatch, outputStream.getProgressMap(), (long) outputStream.getMaxProgress());
                    } else {
                        outputStream = gZIPOutputStream;
                    }
                    processRequest(graphRequestBatch, logger, size, url, outputStream, isGzipCompressible);
                    if (outputStream != null) {
                        outputStream.close();
                    }
                    logger.log();
                    return;
                } catch (Throwable th3) {
                    th = th3;
                    if (gZIPOutputStream != null) {
                        gZIPOutputStream.close();
                    }
                    throw th;
                }
            } catch (Throwable th4) {
                th = th4;
                gZIPOutputStream = outputStream;
                if (gZIPOutputStream != null) {
                    gZIPOutputStream.close();
                }
                throw th;
            }
        }
        logger.log();
    }

    private static void setConnectionContentType(HttpURLConnection httpURLConnection, boolean z) {
        if (z) {
            httpURLConnection.setRequestProperty("Content-Type", HttpRequest.CONTENT_TYPE_FORM);
            httpURLConnection.setRequestProperty("Content-Encoding", HttpRequest.ENCODING_GZIP);
            return;
        }
        httpURLConnection.setRequestProperty("Content-Type", getMimeContentType());
    }

    public static final void setDefaultBatchApplicationId(String str) {
        defaultBatchApplicationId = str;
    }

    static final boolean shouldWarnOnMissingFieldsParam(GraphRequest graphRequest) {
        String version = graphRequest.getVersion();
        if (Utility.isNullOrEmpty(version)) {
            return true;
        }
        if (version.startsWith("v")) {
            version = version.substring(1);
        }
        String[] split = version.split("\\.");
        return (split.length >= 2 && Integer.parseInt(split[0]) > 2) || (Integer.parseInt(split[0]) >= 2 && Integer.parseInt(split[1]) >= 4);
    }

    public static HttpURLConnection toHttpConnection(GraphRequestBatch graphRequestBatch) {
        Throwable e;
        validateFieldsParamForGetRequests(graphRequestBatch);
        try {
            URLConnection createConnection;
            try {
                createConnection = createConnection(graphRequestBatch.size() == 1 ? new URL(graphRequestBatch.get(0).getUrlForSingleRequest()) : new URL(ServerProtocol.getGraphUrlBase()));
                try {
                    serializeToUrlConnection(graphRequestBatch, createConnection);
                    return createConnection;
                } catch (IOException e2) {
                    e = e2;
                    Utility.disconnectQuietly(createConnection);
                    throw new FacebookException("could not construct request body", e);
                } catch (JSONException e3) {
                    e = e3;
                    Utility.disconnectQuietly(createConnection);
                    throw new FacebookException("could not construct request body", e);
                }
            } catch (IOException e4) {
                e = e4;
                createConnection = null;
                Utility.disconnectQuietly(createConnection);
                throw new FacebookException("could not construct request body", e);
            } catch (JSONException e5) {
                e = e5;
                createConnection = null;
                Utility.disconnectQuietly(createConnection);
                throw new FacebookException("could not construct request body", e);
            }
        } catch (Throwable e6) {
            throw new FacebookException("could not construct URL for request", e6);
        }
    }

    public static HttpURLConnection toHttpConnection(Collection<GraphRequest> collection) {
        Validate.notEmptyAndContainsNoNulls(collection, Requests.EXTRA_REQUESTS);
        return toHttpConnection(new GraphRequestBatch((Collection) collection));
    }

    public static HttpURLConnection toHttpConnection(GraphRequest... graphRequestArr) {
        return toHttpConnection(Arrays.asList(graphRequestArr));
    }

    static final void validateFieldsParamForGetRequests(GraphRequestBatch graphRequestBatch) {
        Iterator it = graphRequestBatch.iterator();
        while (it.hasNext()) {
            GraphRequest graphRequest = (GraphRequest) it.next();
            if (HttpMethod.GET.equals(graphRequest.getHttpMethod()) && shouldWarnOnMissingFieldsParam(graphRequest)) {
                Bundle parameters = graphRequest.getParameters();
                if (!parameters.containsKey(FIELDS_PARAM) || Utility.isNullOrEmpty(parameters.getString(FIELDS_PARAM))) {
                    Logger.log(LoggingBehavior.DEVELOPER_ERRORS, 5, "Request", "starting with Graph API v2.4, GET requests for /%s should contain an explicit \"fields\" parameter.", graphRequest.getGraphPath());
                }
            }
        }
    }

    public final GraphResponse executeAndWait() {
        return executeAndWait(this);
    }

    public final GraphRequestAsyncTask executeAsync() {
        return executeBatchAsync(this);
    }

    public final AccessToken getAccessToken() {
        return this.accessToken;
    }

    public final String getBatchEntryDependsOn() {
        return this.batchEntryDependsOn;
    }

    public final String getBatchEntryName() {
        return this.batchEntryName;
    }

    public final boolean getBatchEntryOmitResultOnSuccess() {
        return this.batchEntryOmitResultOnSuccess;
    }

    public final Callback getCallback() {
        return this.callback;
    }

    public final JSONObject getGraphObject() {
        return this.graphObject;
    }

    public final String getGraphPath() {
        return this.graphPath;
    }

    public final HttpMethod getHttpMethod() {
        return this.httpMethod;
    }

    public final Bundle getParameters() {
        return this.parameters;
    }

    public final Object getTag() {
        return this.tag;
    }

    final String getUrlForBatchedRequest() {
        if (this.overriddenURL != null) {
            throw new FacebookException("Can't override URL for a batch request");
        }
        String graphPathWithVersion = getGraphPathWithVersion();
        addCommonParameters();
        return appendParametersToBaseUrl(graphPathWithVersion);
    }

    final String getUrlForSingleRequest() {
        if (this.overriddenURL != null) {
            return this.overriddenURL.toString();
        }
        String graphVideoUrlBase = (getHttpMethod() == HttpMethod.POST && this.graphPath != null && this.graphPath.endsWith(VIDEOS_SUFFIX)) ? ServerProtocol.getGraphVideoUrlBase() : ServerProtocol.getGraphUrlBase();
        graphVideoUrlBase = String.format(GRAPH_PATH_FORMAT, new Object[]{graphVideoUrlBase, getGraphPathWithVersion()});
        addCommonParameters();
        return appendParametersToBaseUrl(graphVideoUrlBase);
    }

    public final String getVersion() {
        return this.version;
    }

    public final void setAccessToken(AccessToken accessToken) {
        this.accessToken = accessToken;
    }

    public final void setBatchEntryDependsOn(String str) {
        this.batchEntryDependsOn = str;
    }

    public final void setBatchEntryName(String str) {
        this.batchEntryName = str;
    }

    public final void setBatchEntryOmitResultOnSuccess(boolean z) {
        this.batchEntryOmitResultOnSuccess = z;
    }

    public final void setCallback(final Callback callback) {
        if (FacebookSdk.isLoggingBehaviorEnabled(LoggingBehavior.GRAPH_API_DEBUG_INFO) || FacebookSdk.isLoggingBehaviorEnabled(LoggingBehavior.GRAPH_API_DEBUG_WARNING)) {
            this.callback = new Callback() {
                public void onCompleted(GraphResponse graphResponse) {
                    JSONObject jSONObject = graphResponse.getJSONObject();
                    jSONObject = jSONObject != null ? jSONObject.optJSONObject(GraphRequest.DEBUG_KEY) : null;
                    JSONArray optJSONArray = jSONObject != null ? jSONObject.optJSONArray(GraphRequest.DEBUG_MESSAGES_KEY) : null;
                    if (optJSONArray != null) {
                        for (int i = 0; i < optJSONArray.length(); i++) {
                            JSONObject optJSONObject = optJSONArray.optJSONObject(i);
                            String optString = optJSONObject != null ? optJSONObject.optString("message") : null;
                            String optString2 = optJSONObject != null ? optJSONObject.optString("type") : null;
                            String optString3 = optJSONObject != null ? optJSONObject.optString("link") : null;
                            if (!(optString == null || optString2 == null)) {
                                LoggingBehavior loggingBehavior = LoggingBehavior.GRAPH_API_DEBUG_INFO;
                                if (optString2.equals(GraphRequest.DEBUG_SEVERITY_WARNING)) {
                                    loggingBehavior = LoggingBehavior.GRAPH_API_DEBUG_WARNING;
                                }
                                if (!Utility.isNullOrEmpty(optString3)) {
                                    optString = optString + " Link: " + optString3;
                                }
                                Logger.log(loggingBehavior, GraphRequest.TAG, optString);
                            }
                        }
                    }
                    if (callback != null) {
                        callback.onCompleted(graphResponse);
                    }
                }
            };
        } else {
            this.callback = callback;
        }
    }

    public final void setGraphObject(JSONObject jSONObject) {
        this.graphObject = jSONObject;
    }

    public final void setGraphPath(String str) {
        this.graphPath = str;
    }

    public final void setHttpMethod(HttpMethod httpMethod) {
        if (this.overriddenURL == null || httpMethod == HttpMethod.GET) {
            if (httpMethod == null) {
                httpMethod = HttpMethod.GET;
            }
            this.httpMethod = httpMethod;
            return;
        }
        throw new FacebookException("Can't change HTTP method on request with overridden URL.");
    }

    public final void setParameters(Bundle bundle) {
        this.parameters = bundle;
    }

    public final void setSkipClientToken(boolean z) {
        this.skipClientToken = z;
    }

    public final void setTag(Object obj) {
        this.tag = obj;
    }

    public final void setVersion(String str) {
        this.version = str;
    }

    public String toString() {
        return "{Request: " + " accessToken: " + (this.accessToken == null ? "null" : this.accessToken) + ", graphPath: " + this.graphPath + ", graphObject: " + this.graphObject + ", httpMethod: " + this.httpMethod + ", parameters: " + this.parameters + "}";
    }
}
