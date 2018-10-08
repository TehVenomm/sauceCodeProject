package im.getsocial.sdk.sharedl10n;

import im.getsocial.sdk.consts.LanguageCodes;
import im.getsocial.sdk.sharedl10n.generated.LanguageStrings;
import java.util.HashSet;
import java.util.Set;

public final class LocalizationAdapter {
    /* renamed from: a */
    private static final Set<String> f2518a;

    static class jjbQypPegg {
        /* renamed from: a */
        final String f2515a;
        /* renamed from: b */
        final String f2516b;
        /* renamed from: c */
        final String f2517c;

        jjbQypPegg(String str, String str2, String str3) {
            this.f2515a = str;
            this.f2516b = str2;
            this.f2517c = str3;
        }
    }

    static {
        Set hashSet = new HashSet();
        f2518a = hashSet;
        hashSet.add(LanguageCodes.UKRAINIAN);
        f2518a.add(LanguageCodes.RUSSIAN);
    }

    private LocalizationAdapter() {
    }

    /* renamed from: a */
    private static String m2472a(jjbQypPegg jjbqyppegg, int i, boolean z) {
        if (!z) {
            return i == 1 ? jjbqyppegg.f2515a : jjbqyppegg.f2516b;
        } else {
            if ((i / 10) % 10 == 1) {
                return jjbqyppegg.f2517c;
            }
            int i2 = i % 10;
            return i2 >= 5 ? jjbqyppegg.f2517c : i2 == 0 ? jjbqyppegg.f2517c : i2 == 1 ? jjbqyppegg.f2515a : jjbqyppegg.f2516b;
        }
    }

    /* renamed from: a */
    private static boolean m2473a(String str) {
        return f2518a.contains(str);
    }

    public static String comments(Localization localization, int i) {
        LanguageStrings strings = localization.strings();
        return m2472a(new jjbQypPegg(strings.ViewCommentLink, strings.ViewCommentsLink, strings.ViewComments2Link), i, m2473a(localization.m2471a()));
    }

    public static String likes(Localization localization, int i) {
        LanguageStrings strings = localization.strings();
        return m2472a(new jjbQypPegg(strings.ViewLikeLink, strings.ViewLikesLink, strings.ViewLikes2Link), i, m2473a(localization.m2471a()));
    }

    public static String noResults(Localization localization, String str) {
        return localization.strings().ActivityNoSearchResultsPlaceholderTitle.replace("**[SEARCH_TERM]**", str);
    }
}
