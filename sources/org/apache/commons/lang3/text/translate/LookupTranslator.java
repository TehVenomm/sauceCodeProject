package org.apache.commons.lang3.text.translate;

import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;
import java.io.Writer;
import java.util.HashMap;
import java.util.HashSet;

public class LookupTranslator extends CharSequenceTranslator {
    private final int longest;
    private final HashMap<String, String> lookupMap = new HashMap();
    private final HashSet<Character> prefixSet = new HashSet();
    private final int shortest;

    public LookupTranslator(CharSequence[]... charSequenceArr) {
        int i;
        int i2 = Strategy.TTL_SECONDS_INFINITE;
        if (charSequenceArr != null) {
            int length = charSequenceArr.length;
            int i3 = 0;
            i = 0;
            int i4 = Strategy.TTL_SECONDS_INFINITE;
            while (i3 < length) {
                CharSequence[] charSequenceArr2 = charSequenceArr[i3];
                this.lookupMap.put(charSequenceArr2[0].toString(), charSequenceArr2[1].toString());
                this.prefixSet.add(Character.valueOf(charSequenceArr2[0].charAt(0)));
                i2 = charSequenceArr2[0].length();
                if (i2 < i4) {
                    i4 = i2;
                }
                if (i2 <= i) {
                    i2 = i;
                }
                i3++;
                i = i2;
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
            for (int i3 = i2; i3 >= this.shortest; i3--) {
                String str = (String) this.lookupMap.get(charSequence.subSequence(i, i + i3).toString());
                if (str != null) {
                    writer.write(str);
                    return i3;
                }
            }
        }
        return 0;
    }
}
