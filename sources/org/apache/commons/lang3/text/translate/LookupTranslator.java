package org.apache.commons.lang3.text.translate;

import java.io.IOException;
import java.io.Writer;
import java.util.HashMap;
import java.util.HashSet;

public class LookupTranslator extends CharSequenceTranslator {
    private final int longest;
    private final HashMap<String, String> lookupMap = new HashMap<>();
    private final HashSet<Character> prefixSet = new HashSet<>();
    private final int shortest;

    public LookupTranslator(CharSequence[]... charSequenceArr) {
        int i;
        int i2 = Integer.MAX_VALUE;
        if (charSequenceArr != null) {
            int length = charSequenceArr.length;
            int i3 = 0;
            i = 0;
            int i4 = Integer.MAX_VALUE;
            while (i3 < length) {
                CharSequence[] charSequenceArr2 = charSequenceArr[i3];
                this.lookupMap.put(charSequenceArr2[0].toString(), charSequenceArr2[1].toString());
                this.prefixSet.add(Character.valueOf(charSequenceArr2[0].charAt(0)));
                int length2 = charSequenceArr2[0].length();
                if (length2 < i4) {
                    i4 = length2;
                }
                if (length2 <= i) {
                    length2 = i;
                }
                i3++;
                i = length2;
            }
            i2 = i4;
        } else {
            i = 0;
        }
        this.shortest = i2;
        this.longest = i;
    }

    public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
        if (this.prefixSet.contains(Character.valueOf(charSequence.charAt(i)))) {
            int i2 = this.longest;
            if (this.longest + i > charSequence.length()) {
                i2 = charSequence.length() - i;
            }
            while (true) {
                int i3 = i2;
                if (i3 < this.shortest) {
                    break;
                }
                String str = (String) this.lookupMap.get(charSequence.subSequence(i, i + i3).toString());
                if (str != null) {
                    writer.write(str);
                    return i3;
                }
                i2 = i3 - 1;
            }
        }
        return 0;
    }
}
