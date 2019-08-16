package com.crashlytics.android.core;

import java.io.File;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.AbstractSpiCall;
import p017io.fabric.sdk.android.services.common.ResponseParser;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

class NativeCreateReportSpiCall extends AbstractSpiCall implements CreateReportSpiCall {
    private static final String APP_META_FILE_MULTIPART_PARAM = "app_meta_file";
    private static final String BINARY_IMAGES_FILE_MULTIPART_PARAM = "binary_images_file";
    private static final String DEVICE_META_FILE_MULTIPART_PARAM = "device_meta_file";
    private static final String GZIP_FILE_CONTENT_TYPE = "application/octet-stream";
    private static final String KEYS_FILE_MULTIPART_PARAM = "keys_file";
    private static final String LOGS_FILE_MULTIPART_PARAM = "logs_file";
    private static final String METADATA_FILE_MULTIPART_PARAM = "crash_meta_file";
    private static final String MINIDUMP_FILE_MULTIPART_PARAM = "minidump_file";
    private static final String OS_META_FILE_MULTIPART_PARAM = "os_meta_file";
    private static final String REPORT_IDENTIFIER_PARAM = "report_id";
    private static final String SESSION_META_FILE_MULTIPART_PARAM = "session_meta_file";
    private static final String USER_META_FILE_MULTIPART_PARAM = "user_meta_file";

    public NativeCreateReportSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.POST);
    }

    private HttpRequest applyHeadersTo(HttpRequest httpRequest, String str) {
        httpRequest.header("User-Agent", AbstractSpiCall.CRASHLYTICS_USER_AGENT + this.kit.getVersion()).header(AbstractSpiCall.HEADER_CLIENT_TYPE, "android").header(AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion()).header(AbstractSpiCall.HEADER_API_KEY, str);
        return httpRequest;
    }

    private HttpRequest applyMultipartDataTo(HttpRequest httpRequest, Report report) {
        File[] files;
        httpRequest.part(REPORT_IDENTIFIER_PARAM, report.getIdentifier());
        for (File file : report.getFiles()) {
            if (file.getName().equals("minidump")) {
                httpRequest.part(MINIDUMP_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("metadata")) {
                httpRequest.part(METADATA_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("binaryImages")) {
                httpRequest.part(BINARY_IMAGES_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals(SettingsJsonConstants.SESSION_KEY)) {
                httpRequest.part(SESSION_META_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals(SettingsJsonConstants.APP_KEY)) {
                httpRequest.part(APP_META_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("device")) {
                httpRequest.part(DEVICE_META_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("os")) {
                httpRequest.part(OS_META_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("user")) {
                httpRequest.part(USER_META_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("logs")) {
                httpRequest.part(LOGS_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            } else if (file.getName().equals("keys")) {
                httpRequest.part(KEYS_FILE_MULTIPART_PARAM, file.getName(), GZIP_FILE_CONTENT_TYPE, file);
            }
        }
        return httpRequest;
    }

    public boolean invoke(CreateReportRequest createReportRequest) {
        HttpRequest applyMultipartDataTo = applyMultipartDataTo(applyHeadersTo(getHttpRequest(), createReportRequest.apiKey), createReportRequest.report);
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Sending report to: " + getUrl());
        int code = applyMultipartDataTo.code();
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Result was: " + code);
        return ResponseParser.parse(code) == 0;
    }
}
