package net.gogame.gopay.sdk.support;

import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.zip.ZipEntry;
import java.util.zip.ZipInputStream;

/* renamed from: net.gogame.gopay.sdk.support.v */
public final class C1656v {
    /* renamed from: a */
    public static void m965a(String str, String str2) {
        File file = new File(str2);
        if (!file.exists()) {
            file.mkdir();
        }
        ZipInputStream zipInputStream = new ZipInputStream(new FileInputStream(str));
        ZipEntry nextEntry = zipInputStream.getNextEntry();
        if (!nextEntry.getName().contains("assets")) {
            str2 = str2 + "/assets";
        }
        while (nextEntry != null) {
            String str3 = str2 + File.separator + nextEntry.getName();
            File file2 = new File(str3);
            if (!nextEntry.isDirectory()) {
                if (!file2.getParentFile().exists()) {
                    file2.getParentFile().mkdir();
                }
                m966a(zipInputStream, str3);
            } else {
                file2.mkdir();
            }
            zipInputStream.closeEntry();
            nextEntry = zipInputStream.getNextEntry();
        }
        zipInputStream.close();
    }

    /* renamed from: a */
    private static void m966a(ZipInputStream zipInputStream, String str) {
        BufferedOutputStream bufferedOutputStream = new BufferedOutputStream(new FileOutputStream(str));
        byte[] bArr = new byte[4096];
        while (true) {
            int read = zipInputStream.read(bArr);
            if (read != -1) {
                bufferedOutputStream.write(bArr, 0, read);
            } else {
                bufferedOutputStream.close();
                return;
            }
        }
    }
}
