package com.crashlytics.android.core;

import com.crashlytics.android.core.Report.Type;
import java.io.File;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import p017io.fabric.sdk.android.Fabric;

class InvalidSessionReport implements Report {
    private final Map<String, String> customHeaders = new HashMap(ReportUploader.HEADER_INVALID_CLS_FILE);
    private final File[] files;
    private final String identifier;

    public InvalidSessionReport(String str, File[] fileArr) {
        this.files = fileArr;
        this.identifier = str;
    }

    public Map<String, String> getCustomHeaders() {
        return Collections.unmodifiableMap(this.customHeaders);
    }

    public File getFile() {
        return this.files[0];
    }

    public String getFileName() {
        return this.files[0].getName();
    }

    public File[] getFiles() {
        return this.files;
    }

    public String getIdentifier() {
        return this.identifier;
    }

    public Type getType() {
        return Type.JAVA;
    }

    public void remove() {
        File[] fileArr;
        for (File file : this.files) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Removing invalid report file at " + file.getPath());
            file.delete();
        }
    }
}
