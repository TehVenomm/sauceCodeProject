package com.zopim.android.sdk.attachment;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.content.ClipData;
import android.content.ClipData.Item;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build.VERSION;
import android.support.annotation.Nullable;
import android.support.p000v4.app.Fragment;
import android.util.Log;
import android.util.Pair;
import com.zopim.android.sdk.api.Logger;
import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Set;

public enum ImagePicker {
    INSTANCE;
    
    private static final int CAMERA_REQUEST_ID_END = 6568;
    private static final int CAMERA_REQUEST_ID_START = 4568;
    private static final String CONTENT_TYPE = "image/*";
    private static final int GALLERY_REQUEST_ID = 4567;
    /* access modifiers changed from: private */
    public static final String LOG_TAG = null;
    private final Map<Integer, File> mCameraImages;

    public interface Callback {
        void onSuccess(List<File> list);
    }

    /* renamed from: com.zopim.android.sdk.attachment.ImagePicker$a */
    class C1168a extends AsyncTask<Pair<Context, List<Uri>>, Void, List<C1169b>> {

        /* renamed from: a */
        final Callback f727a;

        C1168a(Callback callback) {
            this.f727a = callback;
        }

        /* renamed from: a */
        private List<C1169b> m651a(Context context, List<Uri> list) {
            ArrayList arrayList = new ArrayList();
            for (Uri uri : list) {
                arrayList.add(new C1169b(uri, UriToFileUtil.getFile(context, uri)));
            }
            return arrayList;
        }

        /* access modifiers changed from: protected */
        /* JADX WARNING: Removed duplicated region for block: B:25:0x00be A[SYNTHETIC, Splitter:B:25:0x00be] */
        /* JADX WARNING: Removed duplicated region for block: B:28:0x00c3 A[SYNTHETIC, Splitter:B:28:0x00c3] */
        /* JADX WARNING: Removed duplicated region for block: B:51:0x0131 A[SYNTHETIC, Splitter:B:51:0x0131] */
        /* JADX WARNING: Removed duplicated region for block: B:54:0x0136 A[SYNTHETIC, Splitter:B:54:0x0136] */
        /* JADX WARNING: Removed duplicated region for block: B:63:0x0157 A[SYNTHETIC, Splitter:B:63:0x0157] */
        /* JADX WARNING: Removed duplicated region for block: B:66:0x015c A[SYNTHETIC, Splitter:B:66:0x015c] */
        /* JADX WARNING: Removed duplicated region for block: B:90:0x003e A[SYNTHETIC] */
        /* JADX WARNING: Removed duplicated region for block: B:96:0x003e A[SYNTHETIC] */
        /* renamed from: a */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        public java.util.List<com.zopim.android.sdk.attachment.ImagePicker.C1169b> doInBackground(android.util.Pair<android.content.Context, java.util.List<android.net.Uri>>... r15) {
            /*
                r14 = this;
                r4 = 0
                r1 = 0
                r0 = r15[r1]
                java.lang.Object r0 = r0.first
                android.content.Context r0 = (android.content.Context) r0
                r1 = r15[r1]
                java.lang.Object r1 = r1.second
                java.util.List r1 = (java.util.List) r1
                java.util.List r1 = r14.m651a(r0, r1)
                java.util.ArrayList r6 = new java.util.ArrayList
                r6.<init>()
                java.util.ArrayList r2 = new java.util.ArrayList
                r2.<init>()
                java.util.Iterator r3 = r1.iterator()
            L_0x0020:
                boolean r1 = r3.hasNext()
                if (r1 == 0) goto L_0x003a
                java.lang.Object r1 = r3.next()
                com.zopim.android.sdk.attachment.ImagePicker$b r1 = (com.zopim.android.sdk.attachment.ImagePicker.C1169b) r1
                boolean r5 = r1.mo20695a()
                if (r5 == 0) goto L_0x0036
                r6.add(r1)
                goto L_0x0020
            L_0x0036:
                r2.add(r1)
                goto L_0x0020
            L_0x003a:
                java.util.Iterator r7 = r2.iterator()
            L_0x003e:
                boolean r1 = r7.hasNext()
                if (r1 == 0) goto L_0x0176
                java.lang.Object r1 = r7.next()
                com.zopim.android.sdk.attachment.ImagePicker$b r1 = (com.zopim.android.sdk.attachment.ImagePicker.C1169b) r1
                com.zopim.android.sdk.attachment.SdkCache r2 = com.zopim.android.sdk.attachment.SdkCache.INSTANCE     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.io.File r2 = r2.getSdkCacheDir(r0)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.io.File r8 = new java.io.File     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.StringBuilder r3 = new java.lang.StringBuilder     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                r3.<init>()     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.StringBuilder r2 = r3.append(r2)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.String r3 = java.io.File.separator     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.util.Locale r3 = java.util.Locale.US     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.String r5 = "attachment-%s.jpg"
                r9 = 1
                java.lang.Object[] r9 = new java.lang.Object[r9]     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                r10 = 0
                long r12 = java.lang.System.currentTimeMillis()     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.Long r11 = java.lang.Long.valueOf(r12)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                r9[r10] = r11     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.String r3 = java.lang.String.format(r3, r5, r9)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.StringBuilder r2 = r2.append(r3)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.lang.String r2 = r2.toString()     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                r8.<init>(r2)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                android.content.ContentResolver r2 = r0.getContentResolver()     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                android.net.Uri r3 = r1.mo20696b()     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.io.InputStream r5 = r2.openInputStream(r3)     // Catch:{ FileNotFoundException -> 0x0181, IOException -> 0x0113, all -> 0x0152 }
                java.io.FileOutputStream r3 = new java.io.FileOutputStream     // Catch:{ FileNotFoundException -> 0x0186, IOException -> 0x017c, all -> 0x0177 }
                r3.<init>(r8)     // Catch:{ FileNotFoundException -> 0x0186, IOException -> 0x017c, all -> 0x0177 }
                r2 = 1024(0x400, float:1.435E-42)
                byte[] r2 = new byte[r2]     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
            L_0x0097:
                int r9 = r5.read(r2)     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                if (r9 <= 0) goto L_0x00d4
                r10 = 0
                r3.write(r2, r10, r9)     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                goto L_0x0097
            L_0x00a2:
                r2 = move-exception
            L_0x00a3:
                java.lang.String r8 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG     // Catch:{ all -> 0x017a }
                java.util.Locale r9 = java.util.Locale.US     // Catch:{ all -> 0x017a }
                java.lang.String r10 = "File not found error copying file, uri: %s"
                r11 = 1
                java.lang.Object[] r11 = new java.lang.Object[r11]     // Catch:{ all -> 0x017a }
                r12 = 0
                android.net.Uri r1 = r1.mo20696b()     // Catch:{ all -> 0x017a }
                r11[r12] = r1     // Catch:{ all -> 0x017a }
                java.lang.String r1 = java.lang.String.format(r9, r10, r11)     // Catch:{ all -> 0x017a }
                com.zopim.android.sdk.api.Logger.m574e(r8, r1, r2)     // Catch:{ all -> 0x017a }
                if (r5 == 0) goto L_0x00c1
                r5.close()     // Catch:{ IOException -> 0x0108 }
            L_0x00c1:
                if (r3 == 0) goto L_0x003e
                r3.close()     // Catch:{ IOException -> 0x00c8 }
                goto L_0x003e
            L_0x00c8:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r3 = "Failed to close file output stream."
                android.util.Log.w(r2, r3, r1)
                goto L_0x003e
            L_0x00d4:
                com.zopim.android.sdk.attachment.ImagePicker$b r2 = new com.zopim.android.sdk.attachment.ImagePicker$b     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                com.zopim.android.sdk.attachment.ImagePicker r9 = com.zopim.android.sdk.attachment.ImagePicker.this     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                android.net.Uri r10 = r1.mo20696b()     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                r2.<init>(r10, r8)     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                r6.add(r2)     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                r8.deleteOnExit()     // Catch:{ FileNotFoundException -> 0x00a2, IOException -> 0x017f }
                if (r5 == 0) goto L_0x00ea
                r5.close()     // Catch:{ IOException -> 0x00fd }
            L_0x00ea:
                if (r3 == 0) goto L_0x003e
                r3.close()     // Catch:{ IOException -> 0x00f1 }
                goto L_0x003e
            L_0x00f1:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r3 = "Failed to close file output stream."
                android.util.Log.w(r2, r3, r1)
                goto L_0x003e
            L_0x00fd:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r5 = "Failed to close file input stream."
                android.util.Log.w(r2, r5, r1)
                goto L_0x00ea
            L_0x0108:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r5 = "Failed to close file input stream."
                android.util.Log.w(r2, r5, r1)
                goto L_0x00c1
            L_0x0113:
                r2 = move-exception
                r3 = r4
                r5 = r4
            L_0x0116:
                java.lang.String r8 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG     // Catch:{ all -> 0x017a }
                java.util.Locale r9 = java.util.Locale.US     // Catch:{ all -> 0x017a }
                java.lang.String r10 = "IO Error copying file, uri: %s"
                r11 = 1
                java.lang.Object[] r11 = new java.lang.Object[r11]     // Catch:{ all -> 0x017a }
                r12 = 0
                android.net.Uri r1 = r1.mo20696b()     // Catch:{ all -> 0x017a }
                r11[r12] = r1     // Catch:{ all -> 0x017a }
                java.lang.String r1 = java.lang.String.format(r9, r10, r11)     // Catch:{ all -> 0x017a }
                com.zopim.android.sdk.api.Logger.m574e(r8, r1, r2)     // Catch:{ all -> 0x017a }
                if (r5 == 0) goto L_0x0134
                r5.close()     // Catch:{ IOException -> 0x0147 }
            L_0x0134:
                if (r3 == 0) goto L_0x003e
                r3.close()     // Catch:{ IOException -> 0x013b }
                goto L_0x003e
            L_0x013b:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r3 = "Failed to close file output stream."
                android.util.Log.w(r2, r3, r1)
                goto L_0x003e
            L_0x0147:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r5 = "Failed to close file input stream."
                android.util.Log.w(r2, r5, r1)
                goto L_0x0134
            L_0x0152:
                r0 = move-exception
                r3 = r4
                r5 = r4
            L_0x0155:
                if (r5 == 0) goto L_0x015a
                r5.close()     // Catch:{ IOException -> 0x0160 }
            L_0x015a:
                if (r3 == 0) goto L_0x015f
                r3.close()     // Catch:{ IOException -> 0x016b }
            L_0x015f:
                throw r0
            L_0x0160:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r4 = "Failed to close file input stream."
                android.util.Log.w(r2, r4, r1)
                goto L_0x015a
            L_0x016b:
                r1 = move-exception
                java.lang.String r2 = com.zopim.android.sdk.attachment.ImagePicker.LOG_TAG
                java.lang.String r3 = "Failed to close file output stream."
                android.util.Log.w(r2, r3, r1)
                goto L_0x015f
            L_0x0176:
                return r6
            L_0x0177:
                r0 = move-exception
                r3 = r4
                goto L_0x0155
            L_0x017a:
                r0 = move-exception
                goto L_0x0155
            L_0x017c:
                r2 = move-exception
                r3 = r4
                goto L_0x0116
            L_0x017f:
                r2 = move-exception
                goto L_0x0116
            L_0x0181:
                r2 = move-exception
                r3 = r4
                r5 = r4
                goto L_0x00a3
            L_0x0186:
                r2 = move-exception
                r3 = r4
                goto L_0x00a3
            */
            throw new UnsupportedOperationException("Method not decompiled: com.zopim.android.sdk.attachment.ImagePicker.C1168a.doInBackground(android.util.Pair[]):java.util.List");
        }

        /* access modifiers changed from: protected */
        /* renamed from: a */
        public void onPostExecute(List<C1169b> list) {
            super.onPostExecute(list);
            if (this.f727a != null) {
                ArrayList arrayList = new ArrayList();
                for (C1169b bVar : list) {
                    if (bVar.mo20695a()) {
                        arrayList.add(bVar.mo20697c());
                    }
                }
                this.f727a.onSuccess(arrayList);
            }
        }
    }

    /* renamed from: com.zopim.android.sdk.attachment.ImagePicker$b */
    class C1169b {

        /* renamed from: b */
        private final boolean f730b;

        /* renamed from: c */
        private final Uri f731c;

        /* renamed from: d */
        private final File f732d;

        C1169b(Uri uri, File file) {
            this.f731c = uri;
            this.f732d = file;
            this.f730b = file != null;
        }

        /* renamed from: a */
        public boolean mo20695a() {
            return this.f730b;
        }

        /* renamed from: b */
        public Uri mo20696b() {
            return this.f731c;
        }

        @Nullable
        /* renamed from: c */
        public File mo20697c() {
            return this.f732d;
        }
    }

    static {
        LOG_TAG = ImagePicker.class.getSimpleName();
    }

    private void answerCallback(List<File> list, Callback callback) {
        if (callback != null) {
            callback.onSuccess(list);
        }
    }

    @SuppressLint({"NewApi"})
    private List<Uri> extractUrisFromIntent(Intent intent) {
        ArrayList arrayList = new ArrayList();
        if (VERSION.SDK_INT >= 16 && intent.getClipData() != null) {
            ClipData clipData = intent.getClipData();
            int itemCount = clipData.getItemCount();
            for (int i = 0; i < itemCount; i++) {
                Item itemAt = clipData.getItemAt(i);
                if (itemAt.getUri() != null) {
                    arrayList.add(itemAt.getUri());
                }
            }
        } else if (intent.getData() != null) {
            arrayList.add(intent.getData());
        }
        return arrayList;
    }

    @TargetApi(19)
    private boolean hasExternalReadPermission(Context context) {
        return VERSION.SDK_INT >= 19 ? context.checkCallingOrSelfPermission("android.permission.READ_EXTERNAL_STORAGE") == 0 : context.checkCallingOrSelfPermission("android.permission.WRITE_EXTERNAL_STORAGE") == 0;
    }

    private void pickImageFromCameraInternal(Fragment fragment) {
        Set keySet = this.mCameraImages.keySet();
        int i = CAMERA_REQUEST_ID_START;
        while (true) {
            if (i >= CAMERA_REQUEST_ID_END) {
                i = Integer.MAX_VALUE;
                break;
            } else if (!keySet.contains(Integer.valueOf(i))) {
                break;
            } else {
                i++;
            }
        }
        Intent intent = new Intent();
        intent.setAction("android.media.action.IMAGE_CAPTURE");
        File file = new File(fragment.getActivity().getExternalCacheDir() + File.separator + String.format(Locale.US, "image-%s.jpg", new Object[]{Long.valueOf(System.currentTimeMillis())}));
        this.mCameraImages.put(Integer.valueOf(i), file);
        intent.putExtra("output", Uri.fromFile(file));
        fragment.startActivityForResult(intent, i);
    }

    @TargetApi(18)
    private void pickImageFromGalleryInternal(Fragment fragment) {
        Intent intent = new Intent("android.intent.action.GET_CONTENT");
        if (VERSION.SDK_INT >= 18) {
            intent.putExtra("android.intent.extra.ALLOW_MULTIPLE", false);
        }
        Intent action = intent.setAction("android.intent.action.GET_CONTENT");
        action.setType(CONTENT_TYPE);
        fragment.startActivityForResult(action, GALLERY_REQUEST_ID);
    }

    public void getFilesFromActivityOnResult(Context context, int i, int i2, Intent intent, Callback callback) {
        if (i2 != -1) {
            answerCallback(new ArrayList(), callback);
        } else if (i == GALLERY_REQUEST_ID) {
            List extractUrisFromIntent = extractUrisFromIntent(intent);
            new C1168a(callback).execute(new Pair[]{new Pair(context, extractUrisFromIntent)});
        } else if (this.mCameraImages.containsKey(Integer.valueOf(i))) {
            File file = (File) this.mCameraImages.get(Integer.valueOf(i));
            this.mCameraImages.remove(Integer.valueOf(i));
            Logger.m571d(LOG_TAG, String.format(Locale.US, "Image from camera: %s\n", new Object[]{file.getAbsolutePath()}));
            answerCallback(new C1170a(this, file), callback);
        } else {
            answerCallback(new ArrayList(), callback);
        }
    }

    public boolean hasPermissionForCamera(Context context) {
        Intent intent = new Intent();
        intent.setAction("android.media.action.IMAGE_CAPTURE");
        boolean z = context.getPackageManager().hasSystemFeature("android.hardware.camera") || context.getPackageManager().hasSystemFeature("android.hardware.camera.front");
        boolean z2 = intent.resolveActivity(context.getPackageManager()) != null;
        boolean hasExternalReadPermission = hasExternalReadPermission(context);
        Logger.m571d(LOG_TAG, String.format(Locale.US, "Camera permissions: camera present: %b, camera app present: %b, external storage read permission: %b", new Object[]{Boolean.valueOf(z), Boolean.valueOf(z2), Boolean.valueOf(hasExternalReadPermission)}));
        return z && z2 && hasExternalReadPermission;
    }

    public boolean hasPermissionForGallery(Context context) {
        return hasExternalReadPermission(context);
    }

    public void pickImageFromCamera(Fragment fragment) {
        if (hasPermissionForCamera(fragment.getActivity())) {
            pickImageFromCameraInternal(fragment);
        }
    }

    public void pickImagesFromGallery(Fragment fragment) {
        if (hasPermissionForGallery(fragment.getActivity())) {
            pickImageFromGalleryInternal(fragment);
        } else {
            Log.w(LOG_TAG, "Image picker needs READ_EXTERNAL_STORAGE permission. Have you declared that permission in your manifest file?");
        }
    }
}
