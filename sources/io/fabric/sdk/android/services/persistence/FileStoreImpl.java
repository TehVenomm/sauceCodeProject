package io.fabric.sdk.android.services.persistence;

import android.annotation.TargetApi;
import android.content.Context;
import android.os.Build.VERSION;
import android.os.Environment;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.Kit;
import java.io.File;

public class FileStoreImpl implements FileStore {
    private final String contentPath;
    private final Context context;
    private final String legacySupport;

    public FileStoreImpl(Kit kit) {
        if (kit.getContext() == null) {
            throw new IllegalStateException("Cannot get directory before context has been set. Call Fabric.with() first");
        }
        this.context = kit.getContext();
        this.contentPath = kit.getPath();
        this.legacySupport = "Android/" + this.context.getPackageName();
    }

    public File getCacheDir() {
        return prepare(this.context.getCacheDir());
    }

    public File getExternalCacheDir() {
        File file = null;
        if (isExternalStorageAvailable()) {
            file = VERSION.SDK_INT >= 8 ? this.context.getExternalCacheDir() : new File(Environment.getExternalStorageDirectory(), this.legacySupport + "/cache/" + this.contentPath);
        }
        return prepare(file);
    }

    @TargetApi(8)
    public File getExternalFilesDir() {
        File file = null;
        if (isExternalStorageAvailable()) {
            file = VERSION.SDK_INT >= 8 ? this.context.getExternalFilesDir(null) : new File(Environment.getExternalStorageDirectory(), this.legacySupport + "/files/" + this.contentPath);
        }
        return prepare(file);
    }

    public File getFilesDir() {
        return prepare(this.context.getFilesDir());
    }

    boolean isExternalStorageAvailable() {
        if ("mounted".equals(Environment.getExternalStorageState())) {
            return true;
        }
        Fabric.getLogger().mo4766w("Fabric", "External Storage is not mounted and/or writable\nHave you declared android.permission.WRITE_EXTERNAL_STORAGE in the manifest?");
        return false;
    }

    File prepare(File file) {
        if (file == null) {
            Fabric.getLogger().mo4753d("Fabric", "Null File");
        } else if (file.exists() || file.mkdirs()) {
            return file;
        } else {
            Fabric.getLogger().mo4766w("Fabric", "Couldn't create file");
        }
        return null;
    }
}
