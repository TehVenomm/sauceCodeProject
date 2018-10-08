package im.getsocial.sdk.sharedl10n;

import im.getsocial.sdk.consts.LanguageCodes;
import im.getsocial.sdk.sharedl10n.generated.LanguageStrings;
import im.getsocial.sdk.sharedl10n.generated.da;
import im.getsocial.sdk.sharedl10n.generated.de;
import im.getsocial.sdk.sharedl10n.generated.en;
import im.getsocial.sdk.sharedl10n.generated.es;
import im.getsocial.sdk.sharedl10n.generated.fr;
import im.getsocial.sdk.sharedl10n.generated.idLang;
import im.getsocial.sdk.sharedl10n.generated.is;
import im.getsocial.sdk.sharedl10n.generated.it;
import im.getsocial.sdk.sharedl10n.generated.ja;
import im.getsocial.sdk.sharedl10n.generated.ko;
import im.getsocial.sdk.sharedl10n.generated.ms;
import im.getsocial.sdk.sharedl10n.generated.nb;
import im.getsocial.sdk.sharedl10n.generated.nl;
import im.getsocial.sdk.sharedl10n.generated.pl;
import im.getsocial.sdk.sharedl10n.generated.pt;
import im.getsocial.sdk.sharedl10n.generated.ptbr;
import im.getsocial.sdk.sharedl10n.generated.ru;
import im.getsocial.sdk.sharedl10n.generated.sv;
import im.getsocial.sdk.sharedl10n.generated.tl;
import im.getsocial.sdk.sharedl10n.generated.tr;
import im.getsocial.sdk.sharedl10n.generated.uk;
import im.getsocial.sdk.sharedl10n.generated.vi;
import im.getsocial.sdk.sharedl10n.generated.zhHans;
import im.getsocial.sdk.sharedl10n.generated.zhHant;
import java.util.HashMap;
import java.util.Map;

public final class Localization {
    /* renamed from: a */
    private static final LanguageStrings f2511a = new en();
    /* renamed from: b */
    private static Map<String, Class<? extends LanguageStrings>> f2512b;
    /* renamed from: c */
    private static Map<String, LanguageStrings> f2513c;
    /* renamed from: d */
    private final UserLanguageCodeProvider f2514d;

    public interface UserLanguageCodeProvider {
        String getCurrentLanguageCode();
    }

    public Localization(UserLanguageCodeProvider userLanguageCodeProvider) {
        if (userLanguageCodeProvider == null) {
            throw new NullPointerException("languageProvider can't be null");
        }
        Map hashMap = new HashMap();
        f2512b = hashMap;
        hashMap.put(LanguageCodes.CHINESE_SIMPLIFIED, zhHans.class);
        f2512b.put(LanguageCodes.CHINESE_TRADITIONAL, zhHant.class);
        f2512b.put(LanguageCodes.DANISH, da.class);
        f2512b.put(LanguageCodes.DUTCH, nl.class);
        f2512b.put("en", en.class);
        f2512b.put(LanguageCodes.FRENCH, fr.class);
        f2512b.put(LanguageCodes.GERMAN, de.class);
        f2512b.put(LanguageCodes.ICELANDIC, is.class);
        f2512b.put("id", idLang.class);
        f2512b.put(LanguageCodes.ITALIAN, it.class);
        f2512b.put(LanguageCodes.JAPANESE, ja.class);
        f2512b.put(LanguageCodes.KOREAN, ko.class);
        f2512b.put(LanguageCodes.MALAY, ms.class);
        f2512b.put(LanguageCodes.NORWEGIAN, nb.class);
        f2512b.put(LanguageCodes.POLISH, pl.class);
        f2512b.put(LanguageCodes.PORTUGUESE, pt.class);
        f2512b.put(LanguageCodes.PORTUGUESE_BRAZILLIAN, ptbr.class);
        f2512b.put(LanguageCodes.RUSSIAN, ru.class);
        f2512b.put(LanguageCodes.SPANISH, es.class);
        f2512b.put(LanguageCodes.SWEDISH, sv.class);
        f2512b.put(LanguageCodes.TAGALOG, tl.class);
        f2512b.put(LanguageCodes.TURKISH, tr.class);
        f2512b.put(LanguageCodes.UKRAINIAN, uk.class);
        f2512b.put(LanguageCodes.VIETNAMESE, vi.class);
        f2513c = new HashMap();
        this.f2514d = userLanguageCodeProvider;
    }

    /* renamed from: a */
    final String m2471a() {
        return this.f2514d.getCurrentLanguageCode();
    }

    public final LanguageStrings strings() {
        String a = m2471a();
        if (f2512b.containsKey(a)) {
            if (!f2513c.containsKey(a)) {
                try {
                    f2513c.put(a, ((Class) f2512b.get(a)).newInstance());
                } catch (Exception e) {
                    e.printStackTrace();
                    return f2511a;
                }
            }
            return (LanguageStrings) f2513c.get(a);
        }
        throw new IllegalStateException("No translation strings for " + a);
    }
}
