package io.fabric.sdk.android.services.settings;

import im.getsocial.sdk.ErrorCode;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.network.HttpMethod;
import io.fabric.sdk.android.services.network.HttpRequest;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import java.util.HashMap;
import java.util.Map;
import org.json.JSONObject;

class DefaultSettingsSpiCall extends AbstractSpiCall implements SettingsSpiCall {
    static final String BUILD_VERSION_PARAM = "build_version";
    static final String DISPLAY_VERSION_PARAM = "display_version";
    static final String ICON_HASH = "icon_hash";
    static final String INSTANCE_PARAM = "instance";
    static final String SOURCE_PARAM = "source";

    public DefaultSettingsSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory) {
        this(kit, str, str2, httpRequestFactory, HttpMethod.GET);
    }

    DefaultSettingsSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory, HttpMethod httpMethod) {
        super(kit, str, str2, httpRequestFactory, httpMethod);
    }

    private HttpRequest applyHeadersTo(HttpRequest httpRequest, SettingsRequest settingsRequest) {
        return httpRequest.header(AbstractSpiCall.HEADER_API_KEY, settingsRequest.apiKey).header(AbstractSpiCall.HEADER_CLIENT_TYPE, AbstractSpiCall.ANDROID_CLIENT_TYPE).header(AbstractSpiCall.HEADER_D, settingsRequest.deviceId).header(AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion()).header("Accept", "application/json");
    }

    private JSONObject getJsonObjectFrom(String str) {
        try {
            return new JSONObject(str);
        } catch (Throwable e) {
            Fabric.getLogger().mo4754d("Fabric", "Failed to parse settings JSON from " + getUrl(), e);
            Fabric.getLogger().mo4753d("Fabric", "Settings response " + str);
            return null;
        }
    }

    private Map<String, String> getQueryParamsFor(SettingsRequest settingsRequest) {
        Map<String, String> hashMap = new HashMap();
        hashMap.put(BUILD_VERSION_PARAM, settingsRequest.buildVersion);
        hashMap.put(DISPLAY_VERSION_PARAM, settingsRequest.displayVersion);
        hashMap.put("source", Integer.toString(settingsRequest.source));
        if (settingsRequest.iconHash != null) {
            hashMap.put(ICON_HASH, settingsRequest.iconHash);
        }
        String str = settingsRequest.instanceId;
        if (!CommonUtils.isNullOrEmpty(str)) {
            hashMap.put(INSTANCE_PARAM, str);
        }
        return hashMap;
    }

    JSONObject handleResponse(HttpRequest httpRequest) {
        int code = httpRequest.code();
        Fabric.getLogger().mo4753d("Fabric", "Settings result was: " + code);
        if (requestWasSuccessful(code)) {
            return getJsonObjectFrom(httpRequest.body());
        }
        Fabric.getLogger().mo4755e("Fabric", "Failed to retrieve settings from " + getUrl());
        return null;
    }

    public JSONObject invoke(SettingsRequest settingsRequest) {
        HttpRequest httpRequest = null;
        try {
            Map queryParamsFor = getQueryParamsFor(settingsRequest);
            httpRequest = applyHeadersTo(getHttpRequest(queryParamsFor), settingsRequest);
            Fabric.getLogger().mo4753d("Fabric", "Requesting settings from " + getUrl());
            Fabric.getLogger().mo4753d("Fabric", "Settings query params were: " + queryParamsFor);
            JSONObject handleResponse = handleResponse(httpRequest);
            return handleResponse;
        } finally {
            if (httpRequest != null) {
                Fabric.getLogger().mo4753d("Fabric", "Settings request ID: " + httpRequest.header(AbstractSpiCall.HEADER_REQUEST_ID));
            }
        }
    }

    boolean requestWasSuccessful(int i) {
        return i == 200 || i == ErrorCode.ACTION_DENIED || i == ErrorCode.SDK_NOT_INITIALIZED || i == ErrorCode.SDK_INITIALIZATION_FAILED;
    }
}
