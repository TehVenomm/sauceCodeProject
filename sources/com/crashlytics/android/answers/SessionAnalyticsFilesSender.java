package com.crashlytics.android.answers;

import io.fabric.sdk.android.Kit;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.ResponseParser;
import io.fabric.sdk.android.services.events.FilesSender;
import io.fabric.sdk.android.services.network.HttpMethod;
import io.fabric.sdk.android.services.network.HttpRequest;
import io.fabric.sdk.android.services.network.HttpRequestFactory;
import java.io.File;
import java.util.List;

class SessionAnalyticsFilesSender extends AbstractSpiCall implements FilesSender {
    static final String FILE_CONTENT_TYPE = "application/vnd.crashlytics.android.events";
    static final String FILE_PARAM_NAME = "session_analytics_file_";
    private final String apiKey;

    public SessionAnalyticsFilesSender(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory, String str3) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.POST);
        this.apiKey = str3;
    }

    private HttpRequest applyHeadersTo(HttpRequest httpRequest, String str) {
        return httpRequest.header(AbstractSpiCall.HEADER_CLIENT_TYPE, AbstractSpiCall.ANDROID_CLIENT_TYPE).header(AbstractSpiCall.HEADER_CLIENT_VERSION, Answers.getInstance().getVersion()).header(AbstractSpiCall.HEADER_API_KEY, str);
    }

    private HttpRequest applyMultipartDataTo(HttpRequest httpRequest, List<File> list) {
        int i = 0;
        for (File file : list) {
            CommonUtils.logControlled(Answers.getInstance().getContext(), "Adding analytics session file " + file.getName() + " to multipart POST");
            httpRequest.part(FILE_PARAM_NAME + i, file.getName(), FILE_CONTENT_TYPE, file);
            i++;
        }
        return httpRequest;
    }

    public boolean send(List<File> list) {
        HttpRequest applyMultipartDataTo = applyMultipartDataTo(applyHeadersTo(getHttpRequest(), this.apiKey), list);
        CommonUtils.logControlled(Answers.getInstance().getContext(), "Sending " + list.size() + " analytics files to " + getUrl());
        int code = applyMultipartDataTo.code();
        CommonUtils.logControlled(Answers.getInstance().getContext(), "Response code for analytics file send is " + code);
        return ResponseParser.parse(code) == 0;
    }
}
