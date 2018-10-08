package org.onepf.oms.util;

import java.util.Collection;
import java.util.Map;
import org.jetbrains.annotations.Nullable;

public final class CollectionUtils {
    private CollectionUtils() {
    }

    public static boolean isEmpty(@Nullable Collection<?> collection) {
        return collection == null || collection.isEmpty();
    }

    public static boolean isEmpty(@Nullable Map<?, ?> map) {
        return map == null || map.isEmpty();
    }

    public static boolean isEmpty(@Nullable Object[] objArr) {
        return objArr == null || objArr.length == 0;
    }
}
