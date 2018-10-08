package org.apache.commons.lang3.text.translate;

import java.io.IOException;
import java.io.Writer;
import java.util.Arrays;
import java.util.EnumSet;

public class NumericEntityUnescaper extends CharSequenceTranslator {
    private final EnumSet<OPTION> options;

    public enum OPTION {
        semiColonRequired,
        semiColonOptional,
        errorIfNoSemiColon
    }

    public NumericEntityUnescaper(OPTION... optionArr) {
        if (optionArr.length > 0) {
            this.options = EnumSet.copyOf(Arrays.asList(optionArr));
            return;
        }
        this.options = EnumSet.copyOf(Arrays.asList(new OPTION[]{OPTION.semiColonRequired}));
    }

    public boolean isSet(OPTION option) {
        return this.options == null ? false : this.options.contains(option);
    }

    public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
        int i2 = 1;
        int length = charSequence.length();
        if (charSequence.charAt(i) != '&' || i >= length - 2 || charSequence.charAt(i + 1) != '#') {
            return 0;
        }
        int i3;
        int i4 = i + 2;
        char charAt = charSequence.charAt(i4);
        if (charAt == 'x' || charAt == 'X') {
            i4++;
            if (i4 == length) {
                return 0;
            }
            i3 = i4;
            i4 = 1;
        } else {
            i3 = i4;
            i4 = 0;
        }
        int i5 = i3;
        while (i5 < length && ((charSequence.charAt(i5) >= '0' && charSequence.charAt(i5) <= '9') || ((charSequence.charAt(i5) >= 'a' && charSequence.charAt(i5) <= 'f') || (charSequence.charAt(i5) >= 'A' && charSequence.charAt(i5) <= 'F')))) {
            i5++;
        }
        length = (i5 == length || charSequence.charAt(i5) != ';') ? 0 : 1;
        if (length == 0) {
            if (isSet(OPTION.semiColonRequired)) {
                return 0;
            }
            if (isSet(OPTION.errorIfNoSemiColon)) {
                throw new IllegalArgumentException("Semi-colon required at end of numeric entity");
            }
        }
        if (i4 != 0) {
            try {
                int parseInt = Integer.parseInt(charSequence.subSequence(i3, i5).toString(), 16);
            } catch (NumberFormatException e) {
                return 0;
            }
        }
        parseInt = Integer.parseInt(charSequence.subSequence(i3, i5).toString(), 10);
        if (parseInt > 65535) {
            char[] toChars = Character.toChars(parseInt);
            writer.write(toChars[0]);
            writer.write(toChars[1]);
        } else {
            writer.write(parseInt);
        }
        i3 = (i5 + 2) - i3;
        if (i4 != 0) {
            i4 = 1;
        } else {
            i4 = 0;
        }
        i4 += i3;
        if (length == 0) {
            i2 = 0;
        }
        return i4 + i2;
    }
}
