package im.getsocial.sdk.internal.p033c.p066m;

import java.util.ArrayList;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: im.getsocial.sdk.internal.c.m.upgqDBbsrL */
public final class upgqDBbsrL {
    private upgqDBbsrL() {
    }

    /* renamed from: a */
    public static <T> List<T> m1518a(List<T> list) {
        return list == null ? Collections.emptyList() : new ArrayList(list);
    }

    /* renamed from: a */
    public static <K, V> Map<K, V> m1519a(Map<K, V> map) {
        return map == null ? Collections.emptyMap() : new HashMap(map);
    }

    /* renamed from: a */
    public static <T> boolean m1520a(List<T> list, List<T> list2) {
        return (list == null && list2 == null) ? true : (list == null || list2 == null) ? false : list.size() == list2.size() && list.containsAll(list2);
    }
}
