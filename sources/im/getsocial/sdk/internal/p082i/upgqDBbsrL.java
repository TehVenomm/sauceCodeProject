package im.getsocial.sdk.internal.p082i;

import im.getsocial.sdk.consts.LanguageCodes;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

/* renamed from: im.getsocial.sdk.internal.i.upgqDBbsrL */
public final class upgqDBbsrL {
    /* renamed from: a */
    static final String[] f1979a = new String[]{LanguageCodes.CHINESE_SIMPLIFIED, LanguageCodes.CHINESE_TRADITIONAL, LanguageCodes.DANISH, LanguageCodes.DUTCH, "en", LanguageCodes.FRENCH, LanguageCodes.GERMAN, LanguageCodes.ICELANDIC, "id", LanguageCodes.ITALIAN, LanguageCodes.JAPANESE, LanguageCodes.KOREAN, LanguageCodes.MALAY, LanguageCodes.NORWEGIAN, LanguageCodes.POLISH, LanguageCodes.PORTUGUESE, LanguageCodes.PORTUGUESE_BRAZILLIAN, LanguageCodes.RUSSIAN, LanguageCodes.SPANISH, LanguageCodes.SWEDISH, LanguageCodes.TAGALOG, LanguageCodes.TURKISH, LanguageCodes.UKRAINIAN, LanguageCodes.VIETNAMESE};
    /* renamed from: b */
    private static final Map<String, List<String>> f1980b;

    static {
        Map hashMap = new HashMap();
        f1980b = hashMap;
        hashMap.put(LanguageCodes.CHINESE_SIMPLIFIED, Arrays.asList(new String[]{"zh", "zh-CN", "zh-SG"}));
        f1980b.put(LanguageCodes.CHINESE_TRADITIONAL, Arrays.asList(new String[]{"zh-TW", "zh-HK"}));
    }

    private upgqDBbsrL() {
    }

    /* renamed from: a */
    public static boolean m1995a(String str) {
        return Arrays.asList(f1979a).contains(str);
    }

    /* renamed from: b */
    public static String m1996b(String str) {
        for (Entry entry : f1980b.entrySet()) {
            if (((List) entry.getValue()).contains(str)) {
                return (String) entry.getKey();
            }
        }
        return null;
    }
}
