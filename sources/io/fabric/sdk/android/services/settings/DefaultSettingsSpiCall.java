package p017io.fabric.sdk.android.services.settings;

import java.util.HashMap;
import java.util.Map;
import org.json.JSONObject;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.AbstractSpiCall;
import p017io.fabric.sdk.android.services.common.CommonUtils;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;

/* renamed from: io.fabric.sdk.android.services.settings.DefaultSettingsSpiCall */
class DefaultSettingsSpiCall extends AbstractSpiCall implements SettingsSpiCall {
    static final String BUILD_VERSION_PARAM = "build_version";
    static final String DISPLAY_VERSION_PARAM = "display_version";
    static final String HEADER_DEVICE_MODEL = "X-CRASHLYTICS-DEVICE-MODEL";
    static final String HEADER_INSTALLATION_ID = "X-CRASHLYTICS-INSTALLATION-ID";
    static final String HEADER_OS_BUILD_VERSION = "X-CRASHLYTICS-OS-BUILD-VERSION";
    static final String HEADER_OS_DISPLAY_VERSION = "X-CRASHLYTICS-OS-DISPLAY-VERSION";
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
        applyNonNullHeader(httpRequest, AbstractSpiCall.HEADER_API_KEY, settingsRequest.apiKey);
        applyNonNullHeader(httpRequest, AbstractSpiCall.HEADER_CLIENT_TYPE, "android");
        applyNonNullHeader(httpRequest, AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion());
        applyNonNullHeader(httpRequest, "Accept", "application/json");
        applyNonNullHeader(httpRequest, HEADER_DEVICE_MODEL, settingsRequest.deviceModel);
        applyNonNullHeader(httpRequest, HEADER_OS_BUILD_VERSION, settingsRequest.osBuildVersion);
        applyNonNullHeader(httpRequest, HEADER_OS_DISPLAY_VERSION, settingsRequest.osDisplayVersion);
        applyNonNullHeader(httpRequest, HEADER_INSTALLATION_ID, settingsRequest.installationId);
        return httpRequest;
    }

    private void applyNonNullHeader(HttpRequest httpRequest, String str, String str2) {
        if (str2 != null) {
            httpRequest.header(str, str2);
        }
    }

    private JSONObject getJsonObjectFrom(String str) {
        try {
            return new JSONObject(str);
        } catch (Exception e) {
            Fabric.getLogger().mo20970d(Fabric.TAG, "Failed to parse settings JSON from " + getUrl(), e);
            Fabric.getLogger().mo20969d(Fabric.TAG, "Settings response " + str);
            return null;
        }
    }

    private Map<String, String> getQueryParamsFor(SettingsRequest settingsRequest) {
        HashMap hashMap = new HashMap();
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

    /* access modifiers changed from: 0000 */
    public JSONObject handleResponse(HttpRequest httpRequest) {
        int code = httpRequest.code();
        Fabric.getLogger().mo20969d(Fabric.TAG, "Settings result was: " + code);
        if (requestWasSuccessful(code)) {
            return getJsonObjectFrom(httpRequest.body());
        }
        Fabric.getLogger().mo20971e(Fabric.TAG, "Failed to retrieve settings from " + getUrl());
        return null;
    }

    /* JADX WARNING: Removed duplicated region for block: B:17:0x00a8  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public org.json.JSONObject invoke(p017io.fabric.sdk.android.services.settings.SettingsRequest r8) {
        /*
            r7 = this;
            r0 = 0
            java.util.Map r1 = r7.getQueryParamsFor(r8)     // Catch:{ HttpRequestException -> 0x0072, all -> 0x00a4 }
            io.fabric.sdk.android.services.network.HttpRequest r2 = r7.getHttpRequest(r1)     // Catch:{ HttpRequestException -> 0x0072, all -> 0x00a4 }
            io.fabric.sdk.android.services.network.HttpRequest r2 = r7.applyHeadersTo(r2, r8)     // Catch:{ HttpRequestException -> 0x00ce }
            io.fabric.sdk.android.Logger r3 = p017io.fabric.sdk.android.Fabric.getLogger()     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ HttpRequestException -> 0x00ce }
            r4.<init>()     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.String r5 = "Fabric"
            java.lang.String r6 = "Requesting settings from "
            java.lang.StringBuilder r4 = r4.append(r6)     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.String r6 = r7.getUrl()     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.StringBuilder r4 = r4.append(r6)     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.String r4 = r4.toString()     // Catch:{ HttpRequestException -> 0x00ce }
            r3.mo20969d(r5, r4)     // Catch:{ HttpRequestException -> 0x00ce }
            io.fabric.sdk.android.Logger r3 = p017io.fabric.sdk.android.Fabric.getLogger()     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ HttpRequestException -> 0x00ce }
            r4.<init>()     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.String r5 = "Fabric"
            java.lang.String r6 = "Settings query params were: "
            java.lang.StringBuilder r4 = r4.append(r6)     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.StringBuilder r1 = r4.append(r1)     // Catch:{ HttpRequestException -> 0x00ce }
            java.lang.String r1 = r1.toString()     // Catch:{ HttpRequestException -> 0x00ce }
            r3.mo20969d(r5, r1)     // Catch:{ HttpRequestException -> 0x00ce }
            org.json.JSONObject r0 = r7.handleResponse(r2)     // Catch:{ HttpRequestException -> 0x00ce }
            if (r2 == 0) goto L_0x0071
            io.fabric.sdk.android.Logger r1 = p017io.fabric.sdk.android.Fabric.getLogger()
            java.lang.String r3 = "Fabric"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.lang.String r5 = "Settings request ID: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = "X-REQUEST-ID"
            java.lang.String r2 = r2.header(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r2 = r2.toString()
            r1.mo20969d(r3, r2)
        L_0x0071:
            return r0
        L_0x0072:
            r1 = move-exception
            r2 = r0
        L_0x0074:
            io.fabric.sdk.android.Logger r3 = p017io.fabric.sdk.android.Fabric.getLogger()     // Catch:{ all -> 0x00cb }
            java.lang.String r4 = "Fabric"
            java.lang.String r5 = "Settings request failed."
            r3.mo20972e(r4, r5, r1)     // Catch:{ all -> 0x00cb }
            if (r2 == 0) goto L_0x0071
            io.fabric.sdk.android.Logger r1 = p017io.fabric.sdk.android.Fabric.getLogger()
            java.lang.String r3 = "Fabric"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.lang.String r5 = "Settings request ID: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = "X-REQUEST-ID"
            java.lang.String r2 = r2.header(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r2 = r2.toString()
            r1.mo20969d(r3, r2)
            goto L_0x0071
        L_0x00a4:
            r1 = move-exception
            r2 = r0
        L_0x00a6:
            if (r2 == 0) goto L_0x00ca
            io.fabric.sdk.android.Logger r0 = p017io.fabric.sdk.android.Fabric.getLogger()
            java.lang.String r3 = "Fabric"
            java.lang.StringBuilder r4 = new java.lang.StringBuilder
            r4.<init>()
            java.lang.String r5 = "Settings request ID: "
            java.lang.StringBuilder r4 = r4.append(r5)
            java.lang.String r5 = "X-REQUEST-ID"
            java.lang.String r2 = r2.header(r5)
            java.lang.StringBuilder r2 = r4.append(r2)
            java.lang.String r2 = r2.toString()
            r0.mo20969d(r3, r2)
        L_0x00ca:
            throw r1
        L_0x00cb:
            r0 = move-exception
            r1 = r0
            goto L_0x00a6
        L_0x00ce:
            r1 = move-exception
            goto L_0x0074
        */
        throw new UnsupportedOperationException("Method not decompiled: p017io.fabric.sdk.android.services.settings.DefaultSettingsSpiCall.invoke(io.fabric.sdk.android.services.settings.SettingsRequest):org.json.JSONObject");
    }

    /* access modifiers changed from: 0000 */
    public boolean requestWasSuccessful(int i) {
        return i == 200 || i == 201 || i == 202 || i == 203;
    }
}
