package org.apache.commons.lang3;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashSet;
import java.util.List;
import java.util.Locale;
import java.util.Set;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class LocaleUtils {
    private static final ConcurrentMap<String, List<Locale>> cCountriesByLanguage = new ConcurrentHashMap();
    private static final ConcurrentMap<String, List<Locale>> cLanguagesByCountry = new ConcurrentHashMap();

    static class SyncAvoid {
        /* access modifiers changed from: private */
        public static final List<Locale> AVAILABLE_LOCALE_LIST;
        /* access modifiers changed from: private */
        public static final Set<Locale> AVAILABLE_LOCALE_SET;

        SyncAvoid() {
        }

        static {
            ArrayList arrayList = new ArrayList(Arrays.asList(Locale.getAvailableLocales()));
            AVAILABLE_LOCALE_LIST = Collections.unmodifiableList(arrayList);
            AVAILABLE_LOCALE_SET = Collections.unmodifiableSet(new HashSet(arrayList));
        }
    }

    public static Locale toLocale(String str) {
        if (str == null) {
            return null;
        }
        if (str.isEmpty()) {
            return new Locale("", "");
        }
        if (str.contains("#")) {
            throw new IllegalArgumentException("Invalid locale format: " + str);
        }
        int length = str.length();
        if (length < 2) {
            throw new IllegalArgumentException("Invalid locale format: " + str);
        } else if (str.charAt(0) != '_') {
            String[] split = str.split(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR, -1);
            switch (split.length - 1) {
                case 0:
                    if (StringUtils.isAllLowerCase(str) && (length == 2 || length == 3)) {
                        return new Locale(str);
                    }
                    throw new IllegalArgumentException("Invalid locale format: " + str);
                case 1:
                    if (StringUtils.isAllLowerCase(split[0]) && ((split[0].length() == 2 || split[0].length() == 3) && split[1].length() == 2 && StringUtils.isAllUpperCase(split[1]))) {
                        return new Locale(split[0], split[1]);
                    }
                    throw new IllegalArgumentException("Invalid locale format: " + str);
                case 2:
                    if (StringUtils.isAllLowerCase(split[0]) && ((split[0].length() == 2 || split[0].length() == 3) && ((split[1].length() == 0 || (split[1].length() == 2 && StringUtils.isAllUpperCase(split[1]))) && split[2].length() > 0))) {
                        return new Locale(split[0], split[1], split[2]);
                    }
            }
            throw new IllegalArgumentException("Invalid locale format: " + str);
        } else if (length < 3) {
            throw new IllegalArgumentException("Invalid locale format: " + str);
        } else {
            char charAt = str.charAt(1);
            char charAt2 = str.charAt(2);
            if (!Character.isUpperCase(charAt) || !Character.isUpperCase(charAt2)) {
                throw new IllegalArgumentException("Invalid locale format: " + str);
            } else if (length == 3) {
                return new Locale("", str.substring(1, 3));
            } else {
                if (length < 5) {
                    throw new IllegalArgumentException("Invalid locale format: " + str);
                } else if (str.charAt(3) == '_') {
                    return new Locale("", str.substring(1, 3), str.substring(4));
                } else {
                    throw new IllegalArgumentException("Invalid locale format: " + str);
                }
            }
        }
    }

    public static List<Locale> localeLookupList(Locale locale) {
        return localeLookupList(locale, locale);
    }

    public static List<Locale> localeLookupList(Locale locale, Locale locale2) {
        ArrayList arrayList = new ArrayList(4);
        if (locale != null) {
            arrayList.add(locale);
            if (locale.getVariant().length() > 0) {
                arrayList.add(new Locale(locale.getLanguage(), locale.getCountry()));
            }
            if (locale.getCountry().length() > 0) {
                arrayList.add(new Locale(locale.getLanguage(), ""));
            }
            if (!arrayList.contains(locale2)) {
                arrayList.add(locale2);
            }
        }
        return Collections.unmodifiableList(arrayList);
    }

    public static List<Locale> availableLocaleList() {
        return SyncAvoid.AVAILABLE_LOCALE_LIST;
    }

    public static Set<Locale> availableLocaleSet() {
        return SyncAvoid.AVAILABLE_LOCALE_SET;
    }

    public static boolean isAvailableLocale(Locale locale) {
        return availableLocaleList().contains(locale);
    }

    public static List<Locale> languagesByCountry(String str) {
        if (str == null) {
            return Collections.emptyList();
        }
        List<Locale> list = (List) cLanguagesByCountry.get(str);
        if (list != null) {
            return list;
        }
        ArrayList arrayList = new ArrayList();
        List availableLocaleList = availableLocaleList();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < availableLocaleList.size()) {
                Locale locale = (Locale) availableLocaleList.get(i2);
                if (str.equals(locale.getCountry()) && locale.getVariant().isEmpty()) {
                    arrayList.add(locale);
                }
                i = i2 + 1;
            } else {
                cLanguagesByCountry.putIfAbsent(str, Collections.unmodifiableList(arrayList));
                return (List) cLanguagesByCountry.get(str);
            }
        }
    }

    public static List<Locale> countriesByLanguage(String str) {
        if (str == null) {
            return Collections.emptyList();
        }
        List<Locale> list = (List) cCountriesByLanguage.get(str);
        if (list != null) {
            return list;
        }
        ArrayList arrayList = new ArrayList();
        List availableLocaleList = availableLocaleList();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 < availableLocaleList.size()) {
                Locale locale = (Locale) availableLocaleList.get(i2);
                if (str.equals(locale.getLanguage()) && locale.getCountry().length() != 0 && locale.getVariant().isEmpty()) {
                    arrayList.add(locale);
                }
                i = i2 + 1;
            } else {
                cCountriesByLanguage.putIfAbsent(str, Collections.unmodifiableList(arrayList));
                return (List) cCountriesByLanguage.get(str);
            }
        }
    }
}
