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
    Map<String, C1134a> mTransfers;

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$a */
    static class C1134a {

        /* renamed from: a */
        public File f658a;

        /* renamed from: b */
        public C1135b f659b;

        C1134a() {
            this.f659b = C1135b.f660a;
        }
    }

    /* renamed from: com.zopim.android.sdk.api.FileTransfers$b */
    enum C1135b {

        /* renamed from: a */
        public static final C1135b f660a = null;

        /* renamed from: b */
        public static final C1135b f661b = null;

        /* renamed from: c */
        public static final C1135b f662c = null;

        /* renamed from: d */
        public static final C1135b f663d = null;

        /* renamed from: e */
        public static final C1135b f664e = null;

        /* renamed from: f */
        private static final /* synthetic */ C1135b[] f665f = null;

        static {
            f660a = new C1135b("UNKNOWN", 0);
            f661b = new C1135b("SCHEDULED", 1);
            f662c = new C1135b("STARTED", 2);
            f663d = new C1135b("COMPLETED", 3);
            f664e = new C1135b("FAILED", 4);
            f665f = new C1135b[]{f660a, f661b, f662c, f663d, f664e};
        }

        private C1135b(String str, int i) {
        }

        public static C1135b valueOf(String str) {
            return (C1135b) Enum.valueOf(C1135b.class, str);
        }

        public static C1135b[] values() {
            return (C1135b[]) f665f.clone();
        }
    }

    static {
        LOG_TAG = FileTransfers.class.getSimpleName();
    }

    private String createUniqueName(File file) {
        short s = 0;
        String replace = (file.getName() != null ? file.getName() : "").replace(" ", "-");
        do {
            try {
                if (!this.mTransfers.containsKey(replace)) {
                    return replace;
                }
                String extension = UriToFileUtil.getExtension(file.getName());
                s = (short) (s + 1);
                replace = file.getName().split(extension)[0] + "-" + s + extension;
            } catch (IndexOutOfBoundsException e) {
                Log.w(LOG_TAG, "Error generating unique file name. Will use the actual file name.");
                return null;
            }
        } while (s < Short.MAX_VALUE);
        return replace;
    }

    @Nullable
    private Entry<String, C1134a> findTransfer(File file) {
        if (file == null) {
            return null;
        }
        for (Entry<String, C1134a> entry : this.mTransfers.entrySet()) {
            C1134a aVar = (C1134a) entry.getValue();
            if (aVar != null && file.equals(aVar.f658a)) {
                return entry;
            }
        }
        return null;
    }

    /* access modifiers changed from: 0000 */
    @NonNull
    public String add(File file) {
        if (file == null || file.getName() == null) {
            Log.w(LOG_TAG, "File validation failed. Can not add file to scheduled set.");
            return "";
        }
        Entry findTransfer = findTransfer(file);
        if (findTransfer != null) {
            ((C1134a) findTransfer.getValue()).f659b = C1135b.f661b;
            return (String) findTransfer.getKey();
        }
        String createUniqueName = createUniqueName(file);
        C1134a aVar = new C1134a();
        aVar.f658a = file;
        aVar.f659b = C1135b.f661b;
        INSTANCE.mTransfers.put(createUniqueName, aVar);
        return createUniqueName;
    }

    /* access modifiers changed from: 0000 */
    public C1134a find(File file) {
        if (file == null) {
            return null;
        }
        for (C1134a aVar : this.mTransfers.values()) {
            if (file.equals(aVar.f658a)) {
                return aVar;
            }
        }
        return null;
    }

    public File findFile(String str) {
        if (str == null) {
            Log.w(LOG_TAG, "File name must not be null. Can not find file.");
            return null;
        }
        C1134a aVar = (C1134a) this.mTransfers.get(str);
        return aVar != null ? aVar.f658a : null;
    }
}
