package com.crashlytics.android.core;

import java.io.BufferedInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import p017io.fabric.sdk.android.services.common.CommonUtils;

class Sha1FileIdStrategy implements FileIdStrategy {
    Sha1FileIdStrategy() {
    }

    private static String getFileSHA(String str) throws IOException {
        BufferedInputStream bufferedInputStream;
        try {
            bufferedInputStream = new BufferedInputStream(new FileInputStream(str));
            try {
                String sha1 = CommonUtils.sha1((InputStream) bufferedInputStream);
                CommonUtils.closeQuietly(bufferedInputStream);
                return sha1;
            } catch (Throwable th) {
                th = th;
                CommonUtils.closeQuietly(bufferedInputStream);
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            bufferedInputStream = null;
            CommonUtils.closeQuietly(bufferedInputStream);
            throw th;
        }
    }

    public String createId(File file) throws IOException {
        return getFileSHA(file.getPath());
    }
}
