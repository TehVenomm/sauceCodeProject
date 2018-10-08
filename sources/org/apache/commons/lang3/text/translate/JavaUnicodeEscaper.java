package org.apache.commons.lang3.text.translate;

import com.google.android.gms.nearby.messages.Strategy;

public class JavaUnicodeEscaper extends UnicodeEscaper {
    public static JavaUnicodeEscaper above(int i) {
        return outsideOf(0, i);
    }

    public static JavaUnicodeEscaper below(int i) {
        return outsideOf(i, Strategy.TTL_SECONDS_INFINITE);
    }

    public static JavaUnicodeEscaper between(int i, int i2) {
        return new JavaUnicodeEscaper(i, i2, true);
    }

    public static JavaUnicodeEscaper outsideOf(int i, int i2) {
        return new JavaUnicodeEscaper(i, i2, false);
    }

    public JavaUnicodeEscaper(int i, int i2, boolean z) {
        super(i, i2, z);
    }

    protected String toUtf16Escape(int i) {
        char[] toChars = Character.toChars(i);
        return "\\u" + CharSequenceTranslator.hex(toChars[0]) + "\\u" + CharSequenceTranslator.hex(toChars[1]);
    }
}
