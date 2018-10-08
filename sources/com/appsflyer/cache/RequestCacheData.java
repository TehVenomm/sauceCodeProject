package com.appsflyer.cache;

import java.util.Scanner;

public class RequestCacheData {
    /* renamed from: ˊ */
    private String f219;
    /* renamed from: ˎ */
    private String f220;
    /* renamed from: ˏ */
    private String f221;
    /* renamed from: ॱ */
    private String f222;

    public RequestCacheData(String str, String str2, String str3) {
        this.f220 = str;
        this.f222 = str2;
        this.f219 = str3;
    }

    public RequestCacheData(char[] cArr) {
        Scanner scanner = new Scanner(new String(cArr));
        while (scanner.hasNextLine()) {
            String nextLine = scanner.nextLine();
            if (nextLine.startsWith("url=")) {
                this.f220 = nextLine.substring(4).trim();
            } else if (nextLine.startsWith("version=")) {
                this.f219 = nextLine.substring(8).trim();
            } else if (nextLine.startsWith("data=")) {
                this.f222 = nextLine.substring(5).trim();
            }
        }
        scanner.close();
    }

    public String getVersion() {
        return this.f219;
    }

    public void setVersion(String str) {
        this.f219 = str;
    }

    public String getPostData() {
        return this.f222;
    }

    public void setPostData(String str) {
        this.f222 = str;
    }

    public String getRequestURL() {
        return this.f220;
    }

    public void setRequestURL(String str) {
        this.f220 = str;
    }

    public String getCacheKey() {
        return this.f221;
    }

    public void setCacheKey(String str) {
        this.f221 = str;
    }
}
