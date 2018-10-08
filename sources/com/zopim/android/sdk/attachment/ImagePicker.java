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
import android.support.v4.app.Fragment;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.nearby.messages.Strategy;
import com.zopim.android.sdk.api.Logger;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
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
    private static final String LOG_TAG = null;
    private final Map<Integer, File> mCameraImages;

    public interface Callback {
        void onSuccess(List<File> list);
    }

    /* renamed from: com.zopim.android.sdk.attachment.ImagePicker$a */
    class C0823a extends AsyncTask<Pair<Context, List<Uri>>, Void, List<C0824b>> {
        /* renamed from: a */
        final Callback f683a;
        /* renamed from: b */
        final /* synthetic */ ImagePicker f684b;

        C0823a(ImagePicker imagePicker, Callback callback) {
            this.f684b = imagePicker;
            this.f683a = callback;
        }

        /* renamed from: a */
        private List<C0824b> m638a(Context context, List<Uri> list) {
            List<C0824b> arrayList = new ArrayList();
            for (Uri uri : list) {
                arrayList.add(new C0824b(this.f684b, uri, UriToFileUtil.getFile(context, uri)));
            }
            return arrayList;
        }

        /* renamed from: a */
        protected List<C0824b> m639a(Pair<Context, List<Uri>>... pairArr) {
            InputStream openInputStream;
            Throwable e;
            Throwable th;
            Context context = (Context) pairArr[0].first;
            List<C0824b> a = m638a(context, (List) pairArr[0].second);
            List<C0824b> arrayList = new ArrayList();
            List<C0824b> arrayList2 = new ArrayList();
            for (C0824b c0824b : a) {
                if (c0824b.m641a()) {
                    arrayList.add(c0824b);
                } else {
                    arrayList2.add(c0824b);
                }
            }
            for (C0824b c0824b2 : arrayList2) {
                FileOutputStream fileOutputStream;
                try {
                    File file = new File(SdkCache.INSTANCE.getSdkCacheDir(context) + File.separator + String.format(Locale.US, "attachment-%s.jpg", new Object[]{Long.valueOf(System.currentTimeMillis())}));
                    openInputStream = context.getContentResolver().openInputStream(c0824b2.m642b());
                    try {
                        fileOutputStream = new FileOutputStream(file);
                    } catch (FileNotFoundException e2) {
                        e = e2;
                        fileOutputStream = null;
                        try {
                            Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "File not found error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                            if (openInputStream != null) {
                                try {
                                    openInputStream.close();
                                } catch (Throwable e3) {
                                    Log.w(ImagePicker.LOG_TAG, "Failed to close file input stream.", e3);
                                }
                            }
                            if (fileOutputStream == null) {
                                try {
                                    fileOutputStream.close();
                                } catch (Throwable e32) {
                                    Log.w(ImagePicker.LOG_TAG, "Failed to close file output stream.", e32);
                                }
                            }
                        } catch (Throwable th2) {
                            th = th2;
                        }
                    } catch (IOException e4) {
                        e = e4;
                        fileOutputStream = null;
                        Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "IO Error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                        if (openInputStream != null) {
                            try {
                                openInputStream.close();
                            } catch (Throwable e322) {
                                Log.w(ImagePicker.LOG_TAG, "Failed to close file input stream.", e322);
                            }
                        }
                        if (fileOutputStream == null) {
                            try {
                                fileOutputStream.close();
                            } catch (Throwable e3222) {
                                Log.w(ImagePicker.LOG_TAG, "Failed to close file output stream.", e3222);
                            }
                        }
                    } catch (Throwable th3) {
                        th = th3;
                        fileOutputStream = null;
                    }
                    try {
                        byte[] bArr = new byte[1024];
                        while (true) {
                            int read = openInputStream.read(bArr);
                            if (read <= 0) {
                                break;
                            }
                            fileOutputStream.write(bArr, 0, read);
                        }
                        arrayList.add(new C0824b(this.f684b, c0824b2.m642b(), file));
                        file.deleteOnExit();
                        if (openInputStream != null) {
                            try {
                                openInputStream.close();
                            } catch (Throwable e32222) {
                                Log.w(ImagePicker.LOG_TAG, "Failed to close file input stream.", e32222);
                            }
                        }
                        if (fileOutputStream != null) {
                            try {
                                fileOutputStream.close();
                            } catch (Throwable e322222) {
                                Log.w(ImagePicker.LOG_TAG, "Failed to close file output stream.", e322222);
                            }
                        }
                    } catch (FileNotFoundException e5) {
                        e = e5;
                        Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "File not found error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                        if (openInputStream != null) {
                            openInputStream.close();
                        }
                        if (fileOutputStream == null) {
                            fileOutputStream.close();
                        }
                    } catch (IOException e6) {
                        e = e6;
                        Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "IO Error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                        if (openInputStream != null) {
                            openInputStream.close();
                        }
                        if (fileOutputStream == null) {
                            fileOutputStream.close();
                        }
                    }
                } catch (FileNotFoundException e7) {
                    e = e7;
                    fileOutputStream = null;
                    openInputStream = null;
                    Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "File not found error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                    if (openInputStream != null) {
                        openInputStream.close();
                    }
                    if (fileOutputStream == null) {
                        fileOutputStream.close();
                    }
                } catch (IOException e8) {
                    e = e8;
                    fileOutputStream = null;
                    openInputStream = null;
                    Logger.m561e(ImagePicker.LOG_TAG, String.format(Locale.US, "IO Error copying file, uri: %s", new Object[]{c0824b2.m642b()}), e);
                    if (openInputStream != null) {
                        openInputStream.close();
                    }
                    if (fileOutputStream == null) {
                        fileOutputStream.close();
                    }
                } catch (Throwable th4) {
                    th = th4;
                    fileOutputStream = null;
                    openInputStream = null;
                }
            }
            return arrayList;
            if (openInputStream != null) {
                try {
                    openInputStream.close();
                } catch (Throwable e3222222) {
                    Log.w(ImagePicker.LOG_TAG, "Failed to close file input stream.", e3222222);
                }
            }
            if (fileOutputStream != null) {
                try {
                    fileOutputStream.close();
                } catch (Throwable e32222222) {
                    Log.w(ImagePicker.LOG_TAG, "Failed to close file output stream.", e32222222);
                }
            }
            throw th;
            if (fileOutputStream != null) {
                fileOutputStream.close();
            }
            throw th;
            throw th;
        }

        /* renamed from: a */
        protected void m640a(List<C0824b> list) {
            super.onPostExecute(list);
            if (this.f683a != null) {
                List arrayList = new ArrayList();
                for (C0824b c0824b : list) {
                    if (c0824b.m641a()) {
                        arrayList.add(c0824b.m643c());
                    }
                }
                this.f683a.onSuccess(arrayList);
            }
        }

        protected /* synthetic */ Object doInBackground(Object[] objArr) {
            return m639a((Pair[]) objArr);
        }

        protected /* synthetic */ void onPostExecute(Object obj) {
            m640a((List) obj);
        }
    }

    /* renamed from: com.zopim.android.sdk.attachment.ImagePicker$b */
    class C0824b {
        /* renamed from: a */
        final /* synthetic */ ImagePicker f685a;
        /* renamed from: b */
        private final boolean f686b;
        /* renamed from: c */
        private final Uri f687c;
        /* renamed from: d */
        private final File f688d;

        C0824b(ImagePicker imagePicker, Uri uri, File file) {
            this.f685a = imagePicker;
            this.f687c = uri;
            this.f688d = file;
            this.f686b = file != null;
        }

        /* renamed from: a */
        public boolean m641a() {
            return this.f686b;
        }

        /* renamed from: b */
        public Uri m642b() {
            return this.f687c;
        }

        @Nullable
        /* renamed from: c */
        public File m643c() {
            return this.f688d;
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
        List<Uri> arrayList = new ArrayList();
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
        while (i < CAMERA_REQUEST_ID_END) {
            if (!keySet.contains(Integer.valueOf(i))) {
                break;
            }
            i++;
        }
        i = Strategy.TTL_SECONDS_INFINITE;
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
        intent = intent.setAction("android.intent.action.GET_CONTENT");
        intent.setType(CONTENT_TYPE);
        fragment.startActivityForResult(intent, GALLERY_REQUEST_ID);
    }

    public void getFilesFromActivityOnResult(Context context, int i, int i2, Intent intent, Callback callback) {
        if (i2 != -1) {
            answerCallback(new ArrayList(), callback);
        } else if (i == GALLERY_REQUEST_ID) {
            List extractUrisFromIntent = extractUrisFromIntent(intent);
            new C0823a(this, callback).execute(new Pair[]{new Pair(context, extractUrisFromIntent)});
        } else if (this.mCameraImages.containsKey(Integer.valueOf(i))) {
            File file = (File) this.mCameraImages.get(Integer.valueOf(i));
            this.mCameraImages.remove(Integer.valueOf(i));
            Logger.m558d(LOG_TAG, String.format(Locale.US, "Image from camera: %s\n", new Object[]{file.getAbsolutePath()}));
            answerCallback(new C0825a(this, file), callback);
        } else {
            answerCallback(new ArrayList(), callback);
        }
    }

    public boolean hasPermissionForCamera(Context context) {
        Intent intent = new Intent();
        intent.setAction("android.media.action.IMAGE_CAPTURE");
        boolean z = context.getPackageManager().hasSystemFeature("android.hardware.camera") || context.getPackageManager().hasSystemFeature("android.hardware.camera.front");
        Logger.m558d(LOG_TAG, String.format(Locale.US, "Camera permissions: camera present: %b, camera app present: %b, external storage read permission: %b", new Object[]{Boolean.valueOf(z), Boolean.valueOf(intent.resolveActivity(context.getPackageManager()) != null), Boolean.valueOf(hasExternalReadPermission(context))}));
        return z && (intent.resolveActivity(context.getPackageManager()) != null) && hasExternalReadPermission(context);
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
