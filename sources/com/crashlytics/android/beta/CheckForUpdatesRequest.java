package com.crashlytics.android.beta;

import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import io.fabric.sdk.android.services.network.HttpMethod;
import io.fabric.sdk.android.services.network.HttpRequest;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import java.util.HashMap;
import java.util.Map;
import org.json.JSONObject;

class CheckForUpdatesRequest extends AbstractSpiCall {
    static final String BETA_SOURCE = "3";
    static final String BUILD_VERSION = "build_version";
    static final String DISPLAY_VERSION = "display_version";
    static final String INSTANCE = "instance";
    static final String SOURCE = "source";
    private final CheckForUpdatesResponseTransform responseTransform;

    public CheckForUpdatesRequest(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory, CheckForUpdatesResponseTransform checkForUpdatesResponseTransform) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.GET);
        this.responseTransform = checkForUpdatesResponseTransform;
    }

    private HttpRequest applyHeadersTo(HttpRequest httpRequest, String str, String str2) {
        return httpRequest.header("Accept", "application/json").header("User-Agent", AbstractSpiCall.CRASHLYTICS_USER_AGENT + this.kit.getVersion()).header(AbstractSpiCall.HEADER_DEVELOPER_TOKEN, "470fa2b4ae81cd56ecbcda9735803434cec591fa").header(AbstractSpiCall.HEADER_CLIENT_TYPE, AbstractSpiCall.ANDROID_CLIENT_TYPE).header(AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion()).header(AbstractSpiCall.HEADER_API_KEY, str).header(AbstractSpiCall.HEADER_D, str2);
    }

    private Map<String, String> getQueryParamsFor(BuildProperties buildProperties) {
        Map<String, String> hashMap = new HashMap();
        hashMap.put(BUILD_VERSION, buildProperties.versionCode);
        hashMap.put(DISPLAY_VERSION, buildProperties.versionName);
        hashMap.put(INSTANCE, buildProperties.buildId);
        hashMap.put("source", BETA_SOURCE);
        return hashMap;
    }

    public CheckForUpdatesResponse invoke(String str, String str2, BuildProperties buildProperties) {
        Throwable e;
        Throwable th;
        CheckForUpdatesResponse checkForUpdatesResponse = null;
        HttpRequest applyHeadersTo;
        try {
            Map queryParamsFor = getQueryParamsFor(buildProperties);
            try {
                applyHeadersTo = applyHeadersTo(getHttpRequest(queryParamsFor), str, str2);
                Fabric.getLogger().mo4289d(Beta.TAG, "Checking for updates from " + getUrl());
                Fabric.getLogger().mo4289d(Beta.TAG, "Checking for updates query params are: " + queryParamsFor);
                if (applyHeadersTo.ok()) {
                    Fabric.getLogger().mo4289d(Beta.TAG, "Checking for updates was successful");
                    checkForUpdatesResponse = this.responseTransform.fromJson(new JSONObject(applyHeadersTo.body()));
                    if (applyHeadersTo != null) {
                        Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
                    }
                } else {
                    Fabric.getLogger().mo4291e(Beta.TAG, "Checking for updates failed. Response code: " + applyHeadersTo.code());
                    if (applyHeadersTo != null) {
                        Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
                    }
                }
            } catch (Exception e2) {
                e = e2;
                try {
                    Fabric.getLogger().mo4292e(Beta.TAG, "Error while checking for updates from " + getUrl(), e);
                    if (applyHeadersTo != null) {
                        Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
                    }
                    return checkForUpdatesResponse;
                } catch (Throwable th2) {
                    th = th2;
                    if (applyHeadersTo != null) {
                        Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
                    }
                    throw th;
                }
            }
        } catch (Exception e3) {
            e = e3;
            applyHeadersTo = null;
            Fabric.getLogger().mo4292e(Beta.TAG, "Error while checking for updates from " + getUrl(), e);
            if (applyHeadersTo != null) {
                Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
            }
            return checkForUpdatesResponse;
        } catch (Throwable e4) {
            applyHeadersTo = null;
            th = e4;
            if (applyHeadersTo != null) {
                Fabric.getLogger().mo4289d("Fabric", "Checking for updates request ID: " + applyHeadersTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
            }
            throw th;
        }
        return checkForUpdatesResponse;
    }
}
