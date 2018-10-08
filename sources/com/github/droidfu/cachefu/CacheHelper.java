package com.github.droidfu.cachefu;

public class CacheHelper {
    public static String getFileNameFromUrl(String str) {
        return str.replaceAll("[.:/,%?&=]", "+").replaceAll("[+]+", "+");
    }
}
