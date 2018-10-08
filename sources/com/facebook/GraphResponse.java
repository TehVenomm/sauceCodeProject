package com.facebook;

import com.facebook.internal.FacebookRequestErrorClassification;
import com.facebook.internal.Logger;
import com.facebook.internal.Utility;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.json.JSONTokener;

public class GraphResponse {
    private static final String BODY_KEY = "body";
    private static final String CODE_KEY = "code";
    public static final String NON_JSON_RESPONSE_PROPERTY = "FACEBOOK_NON_JSON_RESULT";
    private static final String RESPONSE_LOG_TAG = "Response";
    public static final String SUCCESS_KEY = "success";
    private final HttpURLConnection connection;
    private final FacebookRequestError error;
    private final JSONObject graphObject;
    private final JSONArray graphObjectArray;
    private final String rawResponse;
    private final GraphRequest request;

    public enum PagingDirection {
        NEXT,
        PREVIOUS
    }

    GraphResponse(GraphRequest graphRequest, HttpURLConnection httpURLConnection, FacebookRequestError facebookRequestError) {
        this(graphRequest, httpURLConnection, null, null, null, facebookRequestError);
    }

    GraphResponse(GraphRequest graphRequest, HttpURLConnection httpURLConnection, String str, JSONArray jSONArray) {
        this(graphRequest, httpURLConnection, str, null, jSONArray, null);
    }

    GraphResponse(GraphRequest graphRequest, HttpURLConnection httpURLConnection, String str, JSONObject jSONObject) {
        this(graphRequest, httpURLConnection, str, jSONObject, null, null);
    }

    GraphResponse(GraphRequest graphRequest, HttpURLConnection httpURLConnection, String str, JSONObject jSONObject, JSONArray jSONArray, FacebookRequestError facebookRequestError) {
        this.request = graphRequest;
        this.connection = httpURLConnection;
        this.rawResponse = str;
        this.graphObject = jSONObject;
        this.graphObjectArray = jSONArray;
        this.error = facebookRequestError;
    }

    static List<GraphResponse> constructErrorResponses(List<GraphRequest> list, HttpURLConnection httpURLConnection, FacebookException facebookException) {
        int size = list.size();
        List<GraphResponse> arrayList = new ArrayList(size);
        for (int i = 0; i < size; i++) {
            arrayList.add(new GraphResponse((GraphRequest) list.get(i), httpURLConnection, new FacebookRequestError(httpURLConnection, (Exception) facebookException)));
        }
        return arrayList;
    }

    private static GraphResponse createResponseFromObject(GraphRequest graphRequest, HttpURLConnection httpURLConnection, Object obj, Object obj2) throws JSONException {
        if (obj instanceof JSONObject) {
            JSONObject jSONObject = (JSONObject) obj;
            FacebookRequestError checkResponseAndCreateError = FacebookRequestError.checkResponseAndCreateError(jSONObject, obj2, httpURLConnection);
            if (checkResponseAndCreateError != null) {
                if (checkResponseAndCreateError.getErrorCode() == FacebookRequestErrorClassification.EC_INVALID_TOKEN && Utility.isCurrentAccessToken(graphRequest.getAccessToken())) {
                    AccessToken.setCurrentAccessToken(null);
                }
                return new GraphResponse(graphRequest, httpURLConnection, checkResponseAndCreateError);
            }
            Object stringPropertyAsJSON = Utility.getStringPropertyAsJSON(jSONObject, "body", NON_JSON_RESPONSE_PROPERTY);
            if (stringPropertyAsJSON instanceof JSONObject) {
                return new GraphResponse(graphRequest, httpURLConnection, stringPropertyAsJSON.toString(), (JSONObject) stringPropertyAsJSON);
            }
            if (stringPropertyAsJSON instanceof JSONArray) {
                return new GraphResponse(graphRequest, httpURLConnection, stringPropertyAsJSON.toString(), (JSONArray) stringPropertyAsJSON);
            }
            obj = JSONObject.NULL;
        }
        if (obj == JSONObject.NULL) {
            return new GraphResponse(graphRequest, httpURLConnection, obj.toString(), (JSONObject) null);
        }
        throw new FacebookException("Got unexpected object type in response, class: " + obj.getClass().getSimpleName());
    }

    private static List<GraphResponse> createResponsesFromObject(HttpURLConnection httpURLConnection, List<GraphRequest> list, Object obj) throws FacebookException, JSONException {
        GraphRequest graphRequest;
        JSONArray jSONArray;
        int i = 0;
        int size = list.size();
        List<GraphResponse> arrayList = new ArrayList(size);
        if (size == 1) {
            graphRequest = (GraphRequest) list.get(0);
            try {
                JSONObject jSONObject = new JSONObject();
                jSONObject.put("body", obj);
                jSONObject.put(CODE_KEY, httpURLConnection != null ? httpURLConnection.getResponseCode() : 200);
                jSONArray = new JSONArray();
                jSONArray.put(jSONObject);
            } catch (Exception e) {
                arrayList.add(new GraphResponse(graphRequest, httpURLConnection, new FacebookRequestError(httpURLConnection, e)));
                jSONArray = obj;
            } catch (Exception e2) {
                arrayList.add(new GraphResponse(graphRequest, httpURLConnection, new FacebookRequestError(httpURLConnection, e2)));
                jSONArray = obj;
            }
        } else {
            jSONArray = obj;
        }
        if ((jSONArray instanceof JSONArray) && jSONArray.length() == size) {
            jSONArray = jSONArray;
            while (i < jSONArray.length()) {
                graphRequest = (GraphRequest) list.get(i);
                try {
                    arrayList.add(createResponseFromObject(graphRequest, httpURLConnection, jSONArray.get(i), obj));
                } catch (Exception e3) {
                    arrayList.add(new GraphResponse(graphRequest, httpURLConnection, new FacebookRequestError(httpURLConnection, e3)));
                } catch (Exception e32) {
                    arrayList.add(new GraphResponse(graphRequest, httpURLConnection, new FacebookRequestError(httpURLConnection, e32)));
                }
                i++;
            }
            return arrayList;
        }
        throw new FacebookException("Unexpected number of results");
    }

    static List<GraphResponse> createResponsesFromStream(InputStream inputStream, HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) throws FacebookException, JSONException, IOException {
        Logger.log(LoggingBehavior.INCLUDE_RAW_RESPONSES, RESPONSE_LOG_TAG, "Response (raw)\n  Size: %d\n  Response:\n%s\n", Integer.valueOf(Utility.readStreamToString(inputStream).length()), r0);
        return createResponsesFromString(Utility.readStreamToString(inputStream), httpURLConnection, graphRequestBatch);
    }

    static List<GraphResponse> createResponsesFromString(String str, HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) throws FacebookException, JSONException, IOException {
        Logger.log(LoggingBehavior.REQUESTS, RESPONSE_LOG_TAG, "Response\n  Id: %s\n  Size: %d\n  Responses:\n%s\n", graphRequestBatch.getId(), Integer.valueOf(str.length()), createResponsesFromObject(httpURLConnection, graphRequestBatch, new JSONTokener(str).nextValue()));
        return createResponsesFromObject(httpURLConnection, graphRequestBatch, new JSONTokener(str).nextValue());
    }

    static List<GraphResponse> fromHttpConnection(HttpURLConnection httpURLConnection, GraphRequestBatch graphRequestBatch) {
        FacebookException facebookException;
        List<GraphResponse> constructErrorResponses;
        Throwable th;
        Throwable th2;
        Closeable closeable = null;
        try {
            Closeable errorStream = httpURLConnection.getResponseCode() >= 400 ? httpURLConnection.getErrorStream() : httpURLConnection.getInputStream();
            try {
                List<GraphResponse> createResponsesFromStream = createResponsesFromStream(errorStream, httpURLConnection, graphRequestBatch);
                Utility.closeQuietly(errorStream);
                return createResponsesFromStream;
            } catch (FacebookException e) {
                FacebookException facebookException2 = e;
                closeable = errorStream;
                facebookException = facebookException2;
                try {
                    Logger.log(LoggingBehavior.REQUESTS, RESPONSE_LOG_TAG, "Response <Error>: %s", facebookException);
                    constructErrorResponses = constructErrorResponses(graphRequestBatch, httpURLConnection, facebookException);
                    Utility.closeQuietly(closeable);
                    return constructErrorResponses;
                } catch (Throwable th3) {
                    th = th3;
                    Utility.closeQuietly(closeable);
                    throw th;
                }
            } catch (Throwable e2) {
                th2 = e2;
                closeable = errorStream;
                th = th2;
                Logger.log(LoggingBehavior.REQUESTS, RESPONSE_LOG_TAG, "Response <Error>: %s", th);
                constructErrorResponses = constructErrorResponses(graphRequestBatch, httpURLConnection, new FacebookException(th));
                Utility.closeQuietly(closeable);
                return constructErrorResponses;
            } catch (Throwable e22) {
                th2 = e22;
                closeable = errorStream;
                th = th2;
                Utility.closeQuietly(closeable);
                throw th;
            }
        } catch (FacebookException e3) {
            facebookException = e3;
            Logger.log(LoggingBehavior.REQUESTS, RESPONSE_LOG_TAG, "Response <Error>: %s", facebookException);
            constructErrorResponses = constructErrorResponses(graphRequestBatch, httpURLConnection, facebookException);
            Utility.closeQuietly(closeable);
            return constructErrorResponses;
        } catch (Exception e4) {
            th = e4;
            Logger.log(LoggingBehavior.REQUESTS, RESPONSE_LOG_TAG, "Response <Error>: %s", th);
            constructErrorResponses = constructErrorResponses(graphRequestBatch, httpURLConnection, new FacebookException(th));
            Utility.closeQuietly(closeable);
            return constructErrorResponses;
        }
    }

    public final HttpURLConnection getConnection() {
        return this.connection;
    }

    public final FacebookRequestError getError() {
        return this.error;
    }

    public final JSONArray getJSONArray() {
        return this.graphObjectArray;
    }

    public final JSONObject getJSONObject() {
        return this.graphObject;
    }

    public String getRawResponse() {
        return this.rawResponse;
    }

    public GraphRequest getRequest() {
        return this.request;
    }

    public GraphRequest getRequestForPagedResults(PagingDirection pagingDirection) {
        String optString;
        if (this.graphObject != null) {
            JSONObject optJSONObject = this.graphObject.optJSONObject("paging");
            if (optJSONObject != null) {
                optString = pagingDirection == PagingDirection.NEXT ? optJSONObject.optString("next") : optJSONObject.optString("previous");
                if (Utility.isNullOrEmpty(optString)) {
                    return null;
                }
                if (optString == null && optString.equals(this.request.getUrlForSingleRequest())) {
                    return null;
                }
                try {
                    return new GraphRequest(this.request.getAccessToken(), new URL(optString));
                } catch (MalformedURLException e) {
                    return null;
                }
            }
        }
        optString = null;
        if (Utility.isNullOrEmpty(optString)) {
            return null;
        }
        if (optString == null) {
        }
        return new GraphRequest(this.request.getAccessToken(), new URL(optString));
    }

    public String toString() {
        String format;
        try {
            Locale locale = Locale.US;
            int responseCode = this.connection != null ? this.connection.getResponseCode() : 200;
            format = String.format(locale, "%d", new Object[]{Integer.valueOf(responseCode)});
        } catch (IOException e) {
            format = "unknown";
        }
        return "{Response: " + " responseCode: " + format + ", graphObject: " + this.graphObject + ", error: " + this.error + "}";
    }
}
