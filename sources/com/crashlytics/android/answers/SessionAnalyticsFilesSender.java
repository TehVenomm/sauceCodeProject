package com.crashlytics.android.answers;

import java.io.File;
import java.util.List;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.AbstractSpiCall;
import p017io.fabric.sdk.android.services.common.ResponseParser;
import p017io.fabric.sdk.android.services.events.FilesSender;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;

class SessionAnalyticsFilesSender extends AbstractSpiCall implements FilesSender {
    static final String FILE_CONTENT_TYPE = "application/vnd.crashlytics.android.events";
    static final String FILE_PARAM_NAME = "session_analytics_file_";
    private final String apiKey;

    public SessionAnalyticsFilesSender(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory, String str3) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.POST);
        this.apiKey = str3;
    }

    public boolean send(List<File> list) {
        HttpRequest header = getHttpRequest().header(AbstractSpiCall.HEADER_CLIENT_TYPE, "android").header(AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion()).header(AbstractSpiCall.HEADER_API_KEY, this.apiKey);
        int i = 0;
        for (File file : list) {
            header.part(FILE_PARAM_NAME + i, file.getName(), FILE_CONTENT_TYPE, file);
            i++;
        }
        Fabric.getLogger().mo20969d(Answers.TAG, "Sending " + list.size() + " analytics files to " + getUrl());
        int code = header.code();
        Fabric.getLogger().mo20969d(Answers.TAG, "Response code for analytics file send is " + code);
        return ResponseParser.parse(code) == 0;
    }
}
