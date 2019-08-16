package com.crashlytics.android.core;

import java.io.File;
import java.util.Iterator;
import java.util.Map.Entry;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.Kit;
import p017io.fabric.sdk.android.services.common.AbstractSpiCall;
import p017io.fabric.sdk.android.services.common.ResponseParser;
import p017io.fabric.sdk.android.services.network.HttpMethod;
import p017io.fabric.sdk.android.services.network.HttpRequest;
import p017io.fabric.sdk.android.services.network.HttpRequestFactory;

class DefaultCreateReportSpiCall extends AbstractSpiCall implements CreateReportSpiCall {
    static final String FILE_CONTENT_TYPE = "application/octet-stream";
    static final String FILE_PARAM = "report[file]";
    static final String IDENTIFIER_PARAM = "report[identifier]";
    static final String MULTI_FILE_PARAM = "report[file";

    public DefaultCreateReportSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory) {
        super(kit, str, str2, httpRequestFactory, HttpMethod.POST);
    }

    DefaultCreateReportSpiCall(Kit kit, String str, String str2, HttpRequestFactory httpRequestFactory, HttpMethod httpMethod) {
        super(kit, str, str2, httpRequestFactory, httpMethod);
    }

    private HttpRequest applyHeadersTo(HttpRequest httpRequest, CreateReportRequest createReportRequest) {
        HttpRequest header = httpRequest.header(AbstractSpiCall.HEADER_API_KEY, createReportRequest.apiKey).header(AbstractSpiCall.HEADER_CLIENT_TYPE, "android").header(AbstractSpiCall.HEADER_CLIENT_VERSION, this.kit.getVersion());
        Iterator it = createReportRequest.report.getCustomHeaders().entrySet().iterator();
        while (true) {
            HttpRequest httpRequest2 = header;
            if (!it.hasNext()) {
                return httpRequest2;
            }
            header = httpRequest2.header((Entry) it.next());
        }
    }

    private HttpRequest applyMultipartDataTo(HttpRequest httpRequest, Report report) {
        File[] files;
        httpRequest.part(IDENTIFIER_PARAM, report.getIdentifier());
        if (report.getFiles().length == 1) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Adding single file " + report.getFileName() + " to report " + report.getIdentifier());
            return httpRequest.part(FILE_PARAM, report.getFileName(), FILE_CONTENT_TYPE, report.getFile());
        }
        int i = 0;
        for (File file : report.getFiles()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Adding file " + file.getName() + " to report " + report.getIdentifier());
            httpRequest.part(MULTI_FILE_PARAM + i + "]", file.getName(), FILE_CONTENT_TYPE, file);
            i++;
        }
        return httpRequest;
    }

    public boolean invoke(CreateReportRequest createReportRequest) {
        HttpRequest applyMultipartDataTo = applyMultipartDataTo(applyHeadersTo(getHttpRequest(), createReportRequest), createReportRequest.report);
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Sending report to: " + getUrl());
        int code = applyMultipartDataTo.code();
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Create report request ID: " + applyMultipartDataTo.header(AbstractSpiCall.HEADER_REQUEST_ID));
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Result was: " + code);
        return ResponseParser.parse(code) == 0;
    }
}
