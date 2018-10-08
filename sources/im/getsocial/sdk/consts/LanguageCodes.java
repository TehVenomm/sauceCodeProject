package im.getsocial.sdk.consts;

import java.lang.reflect.Field;
import java.util.Collections;
import java.util.Map;
import java.util.SortedMap;
import java.util.TreeMap;

public final class LanguageCodes {
    public static final String CHINESE_SIMPLIFIED = "zh-Hans";
    public static final String CHINESE_TRADITIONAL = "zh-Hant";
    public static final String DANISH = "da";
    public static final String DEFAULT_LANGUAGE = "en";
    public static final String DUTCH = "nl";
    public static final String ENGLISH = "en";
    public static final String FRENCH = "fr";
    public static final String GERMAN = "de";
    public static final String ICELANDIC = "is";
    public static final String INDONESIAN = "id";
    public static final String ITALIAN = "it";
    public static final String JAPANESE = "ja";
    public static final String KOREAN = "ko";
    public static final String MALAY = "ms";
    public static final String NORWEGIAN = "nb";
    public static final String POLISH = "pl";
    public static final String PORTUGUESE = "pt";
    public static final String PORTUGUESE_BRAZILLIAN = "pt-br";
    public static final String RUSSIAN = "ru";
    public static final String SPANISH = "es";
    public static final String SWEDISH = "sv";
    public static final String TAGALOG = "tl";
    public static final String TURKISH = "tr";
    public static final String UKRAINIAN = "uk";
    public static final String VIETNAMESE = "vi";

    private LanguageCodes() {
    }

    public static Map<String, String> all() {
        Field[] fields = LanguageCodes.class.getFields();
        SortedMap treeMap = new TreeMap();
        for (Field field : fields) {
            try {
                String name = field.getName();
                String str = (String) field.get(null);
                if (!"DEFAULT_LANGUAGE".equals(name)) {
                    treeMap.put(str, name.substring(0, 1).toUpperCase() + name.substring(1).toLowerCase());
                }
            } catch (IllegalAccessException e) {
                e.printStackTrace();
            }
        }
        return Collections.unmodifiableSortedMap(treeMap);
    }
}
