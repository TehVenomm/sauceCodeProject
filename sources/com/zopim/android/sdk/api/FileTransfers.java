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
    Map<String, C0792a> mTransfers;

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$a */
    static class C0792a {
        /* renamed from: a */
        public File f614a;
        /* renamed from: b */
        public C0793b f615b;

        C0792a() {
            this.f615b = C0793b.f616a;
        }
    }

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$b */
    enum C0793b {
        /* renamed from: a */
        public static final C0793b f616a = null;
        /* renamed from: b */
        public static final C0793b f617b = null;
        /* renamed from: c */
        public static final C0793b f618c = null;
        /* renamed from: d */
        public static final C0793b f619d = null;
        /* renamed from: e */
        public static final C0793b f620e = null;
        /* renamed from: f */
        private static final /* synthetic */ C0793b[] f621f = null;

        static {
            f616a = new C0793b("UNKNOWN", 0);
            f617b = new C0793b("SCHEDULED", 1);
            f618c = new C0793b("STARTED", 2);
            f619d = new C0793b("COMPLETED", 3);
            f620e = new C0793b("FAILED", 4);
            f621f = new C0793b[]{f616a, f617b, f618c, f619d, f620e};
        }

        private C0793b(String str, int i) {
        }

        public static C0793b valueOf(String str) {
            return (C0793b) Enum.valueOf(C0793b.class, str);
        }

        public static C0793b[] values() {
            return (C0793b[]) f621f.clone();
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
    private Entry<String, C0792a> findTransfer(File file) {
        if (file == null) {
            return null;
        }
        for (Entry<String, C0792a> entry : this.mTransfers.entrySet()) {
            C0792a c0792a = (C0792a) entry.getValue();
            if (c0792a != null && file.equals(c0792a.f614a)) {
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
            ((C0792a) findTransfer.getValue()).f615b = C0793b.f617b;
            return (String) findTransfer.getKey();
        }
        String createUniqueName = createUniqueName(file);
        C0792a c0792a = new C0792a();
        c0792a.f614a = file;
        c0792a.f615b = C0793b.f617b;
        INSTANCE.mTransfers.put(createUniqueName, c0792a);
        return createUniqueName;
    }

    C0792a find(File file) {
        if (file == null) {
            return null;
        }
        for (C0792a c0792a : this.mTransfers.values()) {
            if (file.equals(c0792a.f614a)) {
                return c0792a;
            }
        }
        return null;
    }

    public File findFile(String str) {
        if (str == null) {
            Log.w(LOG_TAG, "File name must not be null. Can not find file.");
            return null;
        }
        C0792a c0792a = (C0792a) this.mTransfers.get(str);
        return c0792a != null ? c0792a.f614a : null;
    }
}
