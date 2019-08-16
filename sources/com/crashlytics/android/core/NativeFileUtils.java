package com.crashlytics.android.core;

import android.content.Context;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;

final class NativeFileUtils {
    private NativeFileUtils() {
    }

    private static byte[] binaryImagesJsonFromBinaryLibsFile(File file, Context context) throws IOException {
        byte[] readFile = readFile(file);
        if (readFile == null || readFile.length == 0) {
            return null;
        }
        return processBinaryImages(context, new String(readFile));
    }

    static byte[] binaryImagesJsonFromDirectory(File file, Context context) throws IOException {
        File filter = filter(file, ".maps");
        if (filter != null) {
            return binaryImagesJsonFromMapsFile(filter, context);
        }
        File filter2 = filter(file, ".binary_libs");
        if (filter2 != null) {
            return binaryImagesJsonFromBinaryLibsFile(filter2, context);
        }
        return null;
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r0v1, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v2, types: [java.io.Closeable, java.io.BufferedReader] */
    /* JADX WARNING: type inference failed for: r0v2 */
    /* JADX WARNING: type inference failed for: r0v4, types: [byte[]] */
    /* JADX WARNING: type inference failed for: r0v5, types: [byte[]] */
    /* JADX WARNING: type inference failed for: r0v6 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r0v0
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY], ?[OBJECT, ARRAY], byte[]]
      uses: [byte[], java.io.Closeable]
      mth insns count: 17
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static byte[] binaryImagesJsonFromMapsFile(java.io.File r3, android.content.Context r4) throws java.io.IOException {
        /*
            r0 = 0
            boolean r1 = r3.exists()
            if (r1 != 0) goto L_0x0008
        L_0x0007:
            return r0
        L_0x0008:
            java.io.BufferedReader r1 = new java.io.BufferedReader     // Catch:{ all -> 0x0024 }
            java.io.FileReader r2 = new java.io.FileReader     // Catch:{ all -> 0x0024 }
            r2.<init>(r3)     // Catch:{ all -> 0x0024 }
            r1.<init>(r2)     // Catch:{ all -> 0x0024 }
            com.crashlytics.android.core.BinaryImagesConverter r0 = new com.crashlytics.android.core.BinaryImagesConverter     // Catch:{ all -> 0x002a }
            com.crashlytics.android.core.Sha1FileIdStrategy r2 = new com.crashlytics.android.core.Sha1FileIdStrategy     // Catch:{ all -> 0x002a }
            r2.<init>()     // Catch:{ all -> 0x002a }
            r0.<init>(r4, r2)     // Catch:{ all -> 0x002a }
            byte[] r0 = r0.convert(r1)     // Catch:{ all -> 0x002a }
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r1)
            goto L_0x0007
        L_0x0024:
            r1 = move-exception
            r2 = r1
        L_0x0026:
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r0)
            throw r2
        L_0x002a:
            r2 = move-exception
            r0 = r1
            goto L_0x0026
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.NativeFileUtils.binaryImagesJsonFromMapsFile(java.io.File, android.content.Context):byte[]");
    }

    private static File filter(File file, String str) {
        File[] listFiles;
        for (File file2 : file.listFiles()) {
            if (file2.getName().endsWith(str)) {
                return file2;
            }
        }
        return null;
    }

    static byte[] metadataJsonFromDirectory(File file) {
        File filter = filter(file, ".device_info");
        if (filter == null) {
            return null;
        }
        return readFile(filter);
    }

    static byte[] minidumpFromDirectory(File file) {
        File filter = filter(file, ".dmp");
        return filter == null ? new byte[0] : minidumpFromFile(filter);
    }

    private static byte[] minidumpFromFile(File file) {
        return readFile(file);
    }

    private static byte[] processBinaryImages(Context context, String str) throws IOException {
        return new BinaryImagesConverter(context, new Sha1FileIdStrategy()).convert(str);
    }

    private static byte[] readBytes(InputStream inputStream) throws IOException {
        byte[] bArr = new byte[1024];
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        while (true) {
            int read = inputStream.read(bArr);
            if (read == -1) {
                return byteArrayOutputStream.toByteArray();
            }
            byteArrayOutputStream.write(bArr, 0, read);
        }
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r0v1, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v1, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v3 */
    /* JADX WARNING: type inference failed for: r1v4, types: [java.io.Closeable] */
    /* JADX WARNING: type inference failed for: r1v6 */
    /* JADX WARNING: type inference failed for: r1v7, types: [java.io.Closeable, java.io.FileInputStream, java.io.InputStream] */
    /* JADX WARNING: type inference failed for: r0v7 */
    /* JADX WARNING: type inference failed for: r0v9 */
    /* JADX WARNING: type inference failed for: r1v8 */
    /* JADX WARNING: type inference failed for: r1v9 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Unknown variable types count: 5 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static byte[] readFile(java.io.File r3) {
        /*
            r0 = 0
            java.io.FileInputStream r1 = new java.io.FileInputStream     // Catch:{ FileNotFoundException -> 0x000e, IOException -> 0x0014, all -> 0x001a }
            r1.<init>(r3)     // Catch:{ FileNotFoundException -> 0x000e, IOException -> 0x0014, all -> 0x001a }
            byte[] r0 = readBytes(r1)     // Catch:{ FileNotFoundException -> 0x0023, IOException -> 0x0025, all -> 0x0020 }
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r1)
        L_0x000d:
            return r0
        L_0x000e:
            r1 = move-exception
            r1 = r0
        L_0x0010:
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r1)
            goto L_0x000d
        L_0x0014:
            r1 = move-exception
            r1 = r0
        L_0x0016:
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r1)
            goto L_0x000d
        L_0x001a:
            r1 = move-exception
            r2 = r1
        L_0x001c:
            p017io.fabric.sdk.android.services.common.CommonUtils.closeQuietly(r0)
            throw r2
        L_0x0020:
            r2 = move-exception
            r0 = r1
            goto L_0x001c
        L_0x0023:
            r2 = move-exception
            goto L_0x0010
        L_0x0025:
            r2 = move-exception
            goto L_0x0016
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.core.NativeFileUtils.readFile(java.io.File):byte[]");
    }
}
