package im.getsocial.p026c.p027a;

import java.util.Locale;

/* renamed from: im.getsocial.c.a.upgqDBbsrL */
public class upgqDBbsrL extends Exception {
    public upgqDBbsrL(String str) {
        super(String.format(Locale.ENGLISH, "File %s not ready after all attempts", new Object[]{str}));
    }

    public upgqDBbsrL(String str, int i) {
        super(String.format(Locale.ENGLISH, "Unexpected HTTP response %d for %s", new Object[]{Integer.valueOf(i), str}));
    }

    public upgqDBbsrL(Throwable th) {
        super(String.format(Locale.ENGLISH, "Unexpected error: %s", new Object[]{th.getMessage()}));
    }
}
