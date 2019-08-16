package com.facebook;

import android.content.SharedPreferences;
import android.os.Bundle;
import com.facebook.internal.Validate;
import org.json.JSONException;

class AccessTokenCache {
    static final String CACHED_ACCESS_TOKEN_KEY = "com.facebook.AccessTokenManager.CachedAccessToken";
    private final SharedPreferences sharedPreferences;
    private LegacyTokenHelper tokenCachingStrategy;
    private final SharedPreferencesTokenCachingStrategyFactory tokenCachingStrategyFactory;

    static class SharedPreferencesTokenCachingStrategyFactory {
        SharedPreferencesTokenCachingStrategyFactory() {
        }

        public LegacyTokenHelper create() {
            return new LegacyTokenHelper(FacebookSdk.getApplicationContext());
        }
    }

    public AccessTokenCache() {
        this(FacebookSdk.getApplicationContext().getSharedPreferences(AccessTokenManager.SHARED_PREFERENCES_NAME, 0), new SharedPreferencesTokenCachingStrategyFactory());
    }

    AccessTokenCache(SharedPreferences sharedPreferences2, SharedPreferencesTokenCachingStrategyFactory sharedPreferencesTokenCachingStrategyFactory) {
        this.sharedPreferences = sharedPreferences2;
        this.tokenCachingStrategyFactory = sharedPreferencesTokenCachingStrategyFactory;
    }

    /* JADX WARNING: type inference failed for: r0v0, types: [com.facebook.AccessToken, java.lang.String] */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r0v0, types: [com.facebook.AccessToken, java.lang.String]
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY]]
      uses: [java.lang.String, com.facebook.AccessToken]
      mth insns count: 11
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 1 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private com.facebook.AccessToken getCachedAccessToken() {
        /*
            r3 = this;
            r0 = 0
            android.content.SharedPreferences r1 = r3.sharedPreferences
            java.lang.String r2 = "com.facebook.AccessTokenManager.CachedAccessToken"
            java.lang.String r1 = r1.getString(r2, r0)
            if (r1 == 0) goto L_0x0014
            org.json.JSONObject r2 = new org.json.JSONObject     // Catch:{ JSONException -> 0x0015 }
            r2.<init>(r1)     // Catch:{ JSONException -> 0x0015 }
            com.facebook.AccessToken r0 = com.facebook.AccessToken.createFromJSONObject(r2)     // Catch:{ JSONException -> 0x0015 }
        L_0x0014:
            return r0
        L_0x0015:
            r1 = move-exception
            goto L_0x0014
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.AccessTokenCache.getCachedAccessToken():com.facebook.AccessToken");
    }

    private AccessToken getLegacyAccessToken() {
        Bundle load = getTokenCachingStrategy().load();
        if (load == null || !LegacyTokenHelper.hasTokenInformation(load)) {
            return null;
        }
        return AccessToken.createFromLegacyCache(load);
    }

    private LegacyTokenHelper getTokenCachingStrategy() {
        if (this.tokenCachingStrategy == null) {
            synchronized (this) {
                if (this.tokenCachingStrategy == null) {
                    this.tokenCachingStrategy = this.tokenCachingStrategyFactory.create();
                }
            }
        }
        return this.tokenCachingStrategy;
    }

    private boolean hasCachedAccessToken() {
        return this.sharedPreferences.contains(CACHED_ACCESS_TOKEN_KEY);
    }

    private boolean shouldCheckLegacyToken() {
        return FacebookSdk.isLegacyTokenUpgradeSupported();
    }

    public void clear() {
        this.sharedPreferences.edit().remove(CACHED_ACCESS_TOKEN_KEY).apply();
        if (shouldCheckLegacyToken()) {
            getTokenCachingStrategy().clear();
        }
    }

    public AccessToken load() {
        if (hasCachedAccessToken()) {
            return getCachedAccessToken();
        }
        if (!shouldCheckLegacyToken()) {
            return null;
        }
        AccessToken legacyAccessToken = getLegacyAccessToken();
        if (legacyAccessToken == null) {
            return legacyAccessToken;
        }
        save(legacyAccessToken);
        getTokenCachingStrategy().clear();
        return legacyAccessToken;
    }

    public void save(AccessToken accessToken) {
        Validate.notNull(accessToken, "accessToken");
        try {
            this.sharedPreferences.edit().putString(CACHED_ACCESS_TOKEN_KEY, accessToken.toJSONObject().toString()).apply();
        } catch (JSONException e) {
        }
    }
}
