package com.zopim.android.sdk.model;

import android.support.annotation.Nullable;
import android.util.Log;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonProperty;
import java.net.URL;

@JsonIgnoreProperties(ignoreUnknown = true)
public class Attachment {
    @JsonIgnore
    private static final String LOG_TAG = ChatLog.class.getSimpleName();
    @JsonProperty("mime_type$string")
    private String mimeType;
    @JsonProperty("name$string")
    private String name;
    @JsonProperty("size$int")
    private Long size;
    @JsonProperty("thumb$string")
    private String thumbnail;
    @JsonProperty("type$string")
    private String type;
    @JsonProperty("url$string")
    private String url;

    @Nullable
    public String getMimeType() {
        return this.mimeType;
    }

    @Nullable
    public String getName() {
        return this.name;
    }

    @Nullable
    public Long getSize() {
        return this.size;
    }

    @Nullable
    public URL getThumbnail() {
        if (this.thumbnail == null) {
            return null;
        }
        try {
            return new URL(this.thumbnail);
        } catch (Throwable e) {
            Log.w(LOG_TAG, "Can not retrieve url. ", e);
            return null;
        }
    }

    @Nullable
    public String getType() {
        return this.type;
    }

    @Nullable
    public URL getUrl() {
        if (this.url == null) {
            return null;
        }
        try {
            return new URL(this.url);
        } catch (Throwable e) {
            Log.w(LOG_TAG, "Can not retrieve url. ", e);
            return null;
        }
    }
}
