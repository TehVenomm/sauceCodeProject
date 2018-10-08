package io.fabric.sdk.android.services.settings;

public class SettingsRequest {
    public final String apiKey;
    public final String buildVersion;
    public final String deviceId;
    public final String displayVersion;
    public final String iconHash;
    public final String instanceId;
    public final int source;

    public SettingsRequest(String str, String str2, String str3, String str4, String str5, int i, String str6) {
        this.apiKey = str;
        this.deviceId = str2;
        this.instanceId = str3;
        this.displayVersion = str4;
        this.buildVersion = str5;
        this.source = i;
        this.iconHash = str6;
    }
}
