package com.zopim.android.sdk.attachment;

import java.io.File;

public enum FileExtension {
    JPG("jpg"),
    JPEG("jpeg"),
    PNG("png"),
    PDF("pdf"),
    TXT("txt"),
    UNKNOWN("unknown");
    
    final String extension;

    private FileExtension(String str) {
        this.extension = str;
    }

    public static FileExtension getExtension(File file) {
        return (file == null || file.getPath() == null) ? UNKNOWN : valueOfExtension(UriToFileUtil.getExtension(file.getName()));
    }

    public static FileExtension valueOfExtension(String str) {
        return JPEG.getValue().equalsIgnoreCase(str) ? JPEG : JPG.getValue().equalsIgnoreCase(str) ? JPG : PNG.getValue().equalsIgnoreCase(str) ? PNG : PDF.getValue().equalsIgnoreCase(str) ? PDF : TXT.getValue().equalsIgnoreCase(str) ? TXT : UNKNOWN;
    }

    public String getValue() {
        return this.extension;
    }
}
