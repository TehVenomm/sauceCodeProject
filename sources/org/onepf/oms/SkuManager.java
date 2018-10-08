package org.onepf.oms;

import android.text.TextUtils;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.concurrent.ConcurrentHashMap;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.appstore.NokiaStore;
import org.onepf.oms.appstore.SamsungApps;
import org.onepf.oms.util.Logger;

public class SkuManager {
    private final Map<String, Map<String, String>> sku2storeSkuMappings;
    private final Map<String, Map<String, String>> storeSku2skuMappings;

    private static final class InstanceHolder {
        static final SkuManager SKU_MANAGER = new SkuManager();

        private InstanceHolder() {
        }
    }

    private SkuManager() {
        this.sku2storeSkuMappings = new ConcurrentHashMap();
        this.storeSku2skuMappings = new ConcurrentHashMap();
    }

    private static void checkSkuMappingParams(String str, @NotNull String str2) {
        if (TextUtils.isEmpty(str)) {
            throw SkuMappingException.newInstance(2);
        } else if (TextUtils.isEmpty(str2)) {
            throw SkuMappingException.newInstance(3);
        } else {
            if (OpenIabHelper.NAME_SAMSUNG.equals(str)) {
                SamsungApps.checkSku(str2);
            }
            if (OpenIabHelper.NAME_NOKIA.equals(str)) {
                NokiaStore.checkSku(str2);
            }
        }
    }

    private static void checkSkuMappingParams(String str, String str2, @NotNull String str3) {
        if (TextUtils.isEmpty(str)) {
            throw SkuMappingException.newInstance(1);
        }
        checkSkuMappingParams(str2, str3);
    }

    @NotNull
    public static SkuManager getInstance() {
        return InstanceHolder.SKU_MANAGER;
    }

    @Nullable
    public List<String> getAllStoreSkus(@NotNull String str) {
        if (TextUtils.isEmpty(str)) {
            throw SkuMappingException.newInstance(2);
        }
        Map map = (Map) this.sku2storeSkuMappings.get(str);
        return map == null ? null : Collections.unmodifiableList(new ArrayList(map.values()));
    }

    @NotNull
    public String getSku(@NotNull String str, @NotNull String str2) {
        checkSkuMappingParams(str, str2);
        Map map = (Map) this.storeSku2skuMappings.get(str);
        if (map == null || !map.containsKey(str2)) {
            return str2;
        }
        Logger.m4026d("getSku() restore sku from storeSku: ", str2, " -> ", (String) map.get(str2));
        return (String) map.get(str2);
    }

    @NotNull
    public String getStoreSku(@NotNull String str, @NotNull String str2) {
        if (TextUtils.isEmpty(str)) {
            throw SkuMappingException.newInstance(2);
        } else if (TextUtils.isEmpty(str2)) {
            throw SkuMappingException.newInstance(1);
        } else {
            Map map = (Map) this.sku2storeSkuMappings.get(str);
            if (map == null || !map.containsKey(str2)) {
                return str2;
            }
            Logger.m4026d("getStoreSku() using mapping for sku: ", str2, " -> ", (String) map.get(str2));
            return (String) map.get(str2);
        }
    }

    @NotNull
    public SkuManager mapSku(String str, String str2, @NotNull String str3) {
        Map map;
        checkSkuMappingParams(str, str2, str3);
        Map map2 = (Map) this.sku2storeSkuMappings.get(str2);
        if (map2 == null) {
            HashMap hashMap = new HashMap();
            this.sku2storeSkuMappings.put(str2, hashMap);
            map = hashMap;
        } else if (map2.containsKey(str)) {
            throw new SkuMappingException("Already specified SKU. sku: " + str + " -> storeSku: " + ((String) map2.get(str)));
        } else {
            map = map2;
        }
        map2 = (Map) this.storeSku2skuMappings.get(str2);
        if (map2 == null) {
            map2 = new HashMap();
            this.storeSku2skuMappings.put(str2, map2);
        } else if (map2.get(str3) != null) {
            throw new SkuMappingException("Ambiguous SKU mapping. You try to map sku: " + str + " -> storeSku: " + str3 + ", that is already mapped to sku: " + ((String) map2.get(str3)));
        }
        map.put(str, str3);
        map2.put(str3, str);
        return this;
    }

    @NotNull
    public SkuManager mapSku(String str, @Nullable Map<String, String> map) {
        if (map == null) {
            throw new SkuMappingException("Store skus map can't be null.");
        }
        for (Entry entry : map.entrySet()) {
            mapSku(str, (String) entry.getKey(), (String) entry.getValue());
        }
        return this;
    }
}
