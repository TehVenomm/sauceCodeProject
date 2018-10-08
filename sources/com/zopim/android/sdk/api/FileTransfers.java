package com.zopim.android.sdk.api;

import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import com.zopim.android.sdk.attachment.UriToFileUtil;
import java.io.File;
import java.util.Map;
import java.util.Map.Entry;

public enum FileTransfers {
    INSTANCE;
    
    private static final String LOG_TAG = null;
    Map<String, C0793a> mTransfers;

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$a */
    static class C0793a {
        /* renamed from: a */
        public File f614a;
        /* renamed from: b */
        public C0794b f615b;

        C0793a() {
            this.f615b = C0794b.f616a;
        }
    }

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$b */
    enum C0794b {
        /* renamed from: a */
        public static final C0794b f616a = null;
        /* renamed from: b */
        public static final C0794b f617b = null;
        /* renamed from: c */
        public static final C0794b f618c = null;
        /* renamed from: d */
        public static final C0794b f619d = null;
        /* renamed from: e */
        public static final C0794b f620e = null;
        /* renamed from: f */
        private static final /* synthetic */ C0794b[] f621f = null;

        static {
            f616a = new C0794b("UNKNOWN", 0);
            f617b = new C0794b("SCHEDULED", 1);
            f618c = new C0794b("STARTED", 2);
            f619d = new C0794b("COMPLETED", 3);
            f620e = new C0794b("FAILED", 4);
            f621f = new C0794b[]{f616a, f617b, f618c, f619d, f620e};
        }

        private C0794b(String str, int i) {
        }

        public static C0794b valueOf(String str) {
            return (C0794b) Enum.valueOf(C0794b.class, str);
        }

        public static C0794b[] values() {
            return (C0794b[]) f621f.clone();
        }
    }

    static {
        LOG_TAG = FileTransfers.class.getSimpleName();
    }

    private String createUniqueName(File file) {
        int i = 0;
        String replace = (file.getName() != null ? file.getName() : "").replace(" ", "-");
        do {
            try {
                if (!this.mTransfers.containsKey(replace)) {
                    return replace;
                }
                replace = UriToFileUtil.getExtension(file.getName());
                i = (short) (i + 1);
                replace = file.getName().split(replace)[0] + "-" + i + replace;
            } catch (IndexOutOfBoundsException e) {
                Log.w(LOG_TAG, "Error generating unique file name. Will use the actual file name.");
                return null;
            }
        } while (i < 32767);
        return replace;
    }

    @Nullable
    private Entry<String, C0793a> findTransfer(File file) {
        if (file == null) {
            return null;
        }
        for (Entry<String, C0793a> entry : this.mTransfers.entrySet()) {
            C0793a c0793a = (C0793a) entry.getValue();
            if (c0793a != null && file.equals(c0793a.f614a)) {
                return entry;
            }
        }
        return null;
    }

    @NonNull
    String add(File file) {
        if (file == null || file.getName() == null) {
            Log.w(LOG_TAG, "File validation failed. Can not add file to scheduled set.");
            return "";
        }
        Entry findTransfer = findTransfer(file);
        if (findTransfer != null) {
            ((C0793a) findTransfer.getValue()).f615b = C0794b.f617b;
            return (String) findTransfer.getKey();
        }
        String createUniqueName = createUniqueName(file);
        C0793a c0793a = new C0793a();
        c0793a.f614a = file;
        c0793a.f615b = C0794b.f617b;
        INSTANCE.mTransfers.put(createUniqueName, c0793a);
        return createUniqueName;
    }

    C0793a find(File file) {
        if (file == null) {
            return null;
        }
        for (C0793a c0793a : this.mTransfers.values()) {
            if (file.equals(c0793a.f614a)) {
                return c0793a;
            }
        }
        return null;
    }

    public File findFile(String str) {
        if (str == null) {
            Log.w(LOG_TAG, "File name must not be null. Can not find file.");
            return null;
        }
        C0793a c0793a = (C0793a) this.mTransfers.get(str);
        return c0793a != null ? c0793a.f614a : null;
    }
}
