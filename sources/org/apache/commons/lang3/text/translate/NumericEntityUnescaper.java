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
        if (this.options == null) {
            return false;
        }
        return this.options.contains(option);
    }

    public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
        boolean z;
        int parseInt;
        int i2;
        int i3 = 1;
        int length = charSequence.length();
        if (charSequence.charAt(i) != '&' || i >= length - 2 || charSequence.charAt(i + 1) != '#') {
            return 0;
        }
        int i4 = i + 2;
        char charAt = charSequence.charAt(i4);
        if (charAt == 'x' || charAt == 'X') {
            i4++;
            if (i4 == length) {
                return 0;
            }
            z = true;
        } else {
            z = false;
        }
        int i5 = i4;
        while (i5 < length && ((charSequence.charAt(i5) >= '0' && charSequence.charAt(i5) <= '9') || ((charSequence.charAt(i5) >= 'a' && charSequence.charAt(i5) <= 'f') || (charSequence.charAt(i5) >= 'A' && charSequence.charAt(i5) <= 'F')))) {
            i5++;
        }
        boolean z2 = i5 != length && charSequence.charAt(i5) == ';';
        if (!z2) {
            if (isSet(OPTION.semiColonRequired)) {
                return 0;
            }
            if (isSet(OPTION.errorIfNoSemiColon)) {
                throw new IllegalArgumentException("Semi-colon required at end of numeric entity");
            }
        }
        if (z) {
            try {
                parseInt = Integer.parseInt(charSequence.subSequence(i4, i5).toString(), 16);
            } catch (NumberFormatException e) {
                return 0;
            }
        } else {
            parseInt = Integer.parseInt(charSequence.subSequence(i4, i5).toString(), 10);
        }
        if (parseInt > 65535) {
            char[] chars = Character.toChars(parseInt);
            writer.write(chars[0]);
            writer.write(chars[1]);
        } else {
            writer.write(parseInt);
        }
        int i6 = (i5 + 2) - i4;
        if (z) {
            i2 = 1;
        } else {
            i2 = 0;
        }
        int i7 = i2 + i6;
        if (!z2) {
            i3 = 0;
        }
        return i7 + i3;
    }
}
