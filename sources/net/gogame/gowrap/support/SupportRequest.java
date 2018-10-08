package net.gogame.gowrap.support;

import android.net.Uri;

public class SupportRequest {
    private final Uri attachment;
    private final String body;
    private final SupportCategory category;
    private final String email;
    private final String mobileNumber;
    private final String name;

    public SupportRequest(String str, String str2, String str3, SupportCategory supportCategory, String str4, Uri uri) {
        this.name = str;
        this.email = str2;
        this.mobileNumber = str3;
        this.category = supportCategory;
        this.body = str4;
        this.attachment = uri;
    }

    public String getName() {
        return this.name;
    }

    public String getEmail() {
        return this.email;
    }

    public String getMobileNumber() {
        return this.mobileNumber;
    }

    public SupportCategory getCategory() {
        return this.category;
    }

    public String getBody() {
        return this.body;
    }

    public Uri getAttachment() {
        return this.attachment;
    }
}
