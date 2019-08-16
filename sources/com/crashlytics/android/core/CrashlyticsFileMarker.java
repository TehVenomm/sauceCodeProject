package com.crashlytics.android.core;

import java.io.File;
import java.io.IOException;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.persistence.FileStore;

class CrashlyticsFileMarker {
    private final FileStore fileStore;
    private final String markerName;

    public CrashlyticsFileMarker(String str, FileStore fileStore2) {
        this.markerName = str;
        this.fileStore = fileStore2;
    }

    private File getMarkerFile() {
        return new File(this.fileStore.getFilesDir(), this.markerName);
    }

    public boolean create() {
        boolean z = false;
        try {
            return getMarkerFile().createNewFile();
        } catch (IOException e) {
            Fabric.getLogger().mo20972e(CrashlyticsCore.TAG, "Error creating marker: " + this.markerName, e);
            return z;
        }
    }

    public boolean isPresent() {
        return getMarkerFile().exists();
    }

    public boolean remove() {
        return getMarkerFile().delete();
    }
}
