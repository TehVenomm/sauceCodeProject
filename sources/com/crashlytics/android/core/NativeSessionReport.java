package com.crashlytics.android.core;

import com.crashlytics.android.core.Report.Type;
import java.io.File;
import java.util.Map;
import p017io.fabric.sdk.android.Fabric;

class NativeSessionReport implements Report {
    private final File reportDirectory;

    public NativeSessionReport(File file) {
        this.reportDirectory = file;
    }

    public Map<String, String> getCustomHeaders() {
        return null;
    }

    public File getFile() {
        return null;
    }

    public String getFileName() {
        return null;
    }

    public File[] getFiles() {
        return this.reportDirectory.listFiles();
    }

    public String getIdentifier() {
        return this.reportDirectory.getName();
    }

    public Type getType() {
        return Type.NATIVE;
    }

    public void remove() {
        File[] files;
        for (File file : getFiles()) {
            Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Removing native report file at " + file.getPath());
            file.delete();
        }
        Fabric.getLogger().mo20969d(CrashlyticsCore.TAG, "Removing native report directory at " + this.reportDirectory);
        this.reportDirectory.delete();
    }
}
