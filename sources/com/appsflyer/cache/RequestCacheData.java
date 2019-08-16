package com.appsflyer.cache;

import java.util.Scanner;

public class RequestCacheData {

    /* renamed from: ˊ */
    private String f240;

    /* renamed from: ˎ */
    private String f241;

    /* renamed from: ˏ */
    private String f242;

    /* renamed from: ॱ */
    private String f243;

    public RequestCacheData(String str, String str2, String str3) {
        this.f241 = str;
        this.f243 = str2;
        this.f240 = str3;
    }

    public RequestCacheData(char[] cArr) {
        Scanner scanner = new Scanner(new String(cArr));
        while (scanner.hasNextLine()) {
            String nextLine = scanner.nextLine();
            if (nextLine.startsWith("url=")) {
                this.f241 = nextLine.substring(4).trim();
            } else if (nextLine.startsWith("version=")) {
                this.f240 = nextLine.substring(8).trim();
            } else if (nextLine.startsWith("data=")) {
                this.f243 = nextLine.substring(5).trim();
            }
        }
        scanner.close();
    }

    public String getVersion() {
        return this.f240;
    }

    public void setVersion(String str) {
        this.f240 = str;
    }

    public String getPostData() {
        return this.f243;
    }

    public void setPostData(String str) {
        this.f243 = str;
    }

    public String getRequestURL() {
        return this.f241;
    }

    public void setRequestURL(String str) {
        this.f241 = str;
    }

    public String getCacheKey() {
        return this.f242;
    }

    public void setCacheKey(String str) {
        this.f242 = str;
    }
}
