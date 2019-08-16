package org.apache.commons.lang3.text;

import java.util.Enumeration;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import org.apache.commons.lang3.StringUtils;

public class StrSubstitutor {
    public static final char DEFAULT_ESCAPE = '$';
    public static final StrMatcher DEFAULT_PREFIX = StrMatcher.stringMatcher("${");
    public static final StrMatcher DEFAULT_SUFFIX = StrMatcher.stringMatcher("}");
    public static final StrMatcher DEFAULT_VALUE_DELIMITER = StrMatcher.stringMatcher(":-");
    private boolean enableSubstitutionInVariables;
    private char escapeChar;
    private StrMatcher prefixMatcher;
    private StrMatcher suffixMatcher;
    private StrMatcher valueDelimiterMatcher;
    private StrLookup<?> variableResolver;

    public static <V> String replace(Object obj, Map<String, V> map) {
        return new StrSubstitutor(map).replace(obj);
    }

    public static <V> String replace(Object obj, Map<String, V> map, String str, String str2) {
        return new StrSubstitutor(map, str, str2).replace(obj);
    }

    public static String replace(Object obj, Properties properties) {
        if (properties == null) {
            return obj.toString();
        }
        HashMap hashMap = new HashMap();
        Enumeration propertyNames = properties.propertyNames();
        while (propertyNames.hasMoreElements()) {
            String str = (String) propertyNames.nextElement();
            hashMap.put(str, properties.getProperty(str));
        }
        return replace(obj, (Map<String, V>) hashMap);
    }

    public static String replaceSystemProperties(Object obj) {
        return new StrSubstitutor(StrLookup.systemPropertiesLookup()).replace(obj);
    }

    public StrSubstitutor() {
        this(null, DEFAULT_PREFIX, DEFAULT_SUFFIX, '$');
    }

    public <V> StrSubstitutor(Map<String, V> map) {
        this(StrLookup.mapLookup(map), DEFAULT_PREFIX, DEFAULT_SUFFIX, '$');
    }

    public <V> StrSubstitutor(Map<String, V> map, String str, String str2) {
        this(StrLookup.mapLookup(map), str, str2, '$');
    }

    public <V> StrSubstitutor(Map<String, V> map, String str, String str2, char c) {
        this(StrLookup.mapLookup(map), str, str2, c);
    }

    public <V> StrSubstitutor(Map<String, V> map, String str, String str2, char c, String str3) {
        this(StrLookup.mapLookup(map), str, str2, c, str3);
    }

    public StrSubstitutor(StrLookup<?> strLookup) {
        this(strLookup, DEFAULT_PREFIX, DEFAULT_SUFFIX, '$');
    }

    public StrSubstitutor(StrLookup<?> strLookup, String str, String str2, char c) {
        setVariableResolver(strLookup);
        setVariablePrefix(str);
        setVariableSuffix(str2);
        setEscapeChar(c);
        setValueDelimiterMatcher(DEFAULT_VALUE_DELIMITER);
    }

    public StrSubstitutor(StrLookup<?> strLookup, String str, String str2, char c, String str3) {
        setVariableResolver(strLookup);
        setVariablePrefix(str);
        setVariableSuffix(str2);
        setEscapeChar(c);
        setValueDelimiter(str3);
    }

    public StrSubstitutor(StrLookup<?> strLookup, StrMatcher strMatcher, StrMatcher strMatcher2, char c) {
        this(strLookup, strMatcher, strMatcher2, c, DEFAULT_VALUE_DELIMITER);
    }

    public StrSubstitutor(StrLookup<?> strLookup, StrMatcher strMatcher, StrMatcher strMatcher2, char c, StrMatcher strMatcher3) {
        setVariableResolver(strLookup);
        setVariablePrefixMatcher(strMatcher);
        setVariableSuffixMatcher(strMatcher2);
        setEscapeChar(c);
        setValueDelimiterMatcher(strMatcher3);
    }

    public String replace(String str) {
        if (str == null) {
            return null;
        }
        StrBuilder strBuilder = new StrBuilder(str);
        return substitute(strBuilder, 0, str.length()) ? strBuilder.toString() : str;
    }

    public String replace(String str, int i, int i2) {
        if (str == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(i2).append(str, i, i2);
        if (!substitute(append, 0, i2)) {
            return str.substring(i, i + i2);
        }
        return append.toString();
    }

    public String replace(char[] cArr) {
        if (cArr == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(cArr.length).append(cArr);
        substitute(append, 0, cArr.length);
        return append.toString();
    }

    public String replace(char[] cArr, int i, int i2) {
        if (cArr == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(i2).append(cArr, i, i2);
        substitute(append, 0, i2);
        return append.toString();
    }

    public String replace(StringBuffer stringBuffer) {
        if (stringBuffer == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(stringBuffer.length()).append(stringBuffer);
        substitute(append, 0, append.length());
        return append.toString();
    }

    public String replace(StringBuffer stringBuffer, int i, int i2) {
        if (stringBuffer == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(i2).append(stringBuffer, i, i2);
        substitute(append, 0, i2);
        return append.toString();
    }

    public String replace(CharSequence charSequence) {
        if (charSequence == null) {
            return null;
        }
        return replace(charSequence, 0, charSequence.length());
    }

    public String replace(CharSequence charSequence, int i, int i2) {
        if (charSequence == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(i2).append(charSequence, i, i2);
        substitute(append, 0, i2);
        return append.toString();
    }

    public String replace(StrBuilder strBuilder) {
        if (strBuilder == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(strBuilder.length()).append(strBuilder);
        substitute(append, 0, append.length());
        return append.toString();
    }

    public String replace(StrBuilder strBuilder, int i, int i2) {
        if (strBuilder == null) {
            return null;
        }
        StrBuilder append = new StrBuilder(i2).append(strBuilder, i, i2);
        substitute(append, 0, i2);
        return append.toString();
    }

    public String replace(Object obj) {
        if (obj == null) {
            return null;
        }
        StrBuilder append = new StrBuilder().append(obj);
        substitute(append, 0, append.length());
        return append.toString();
    }

    public boolean replaceIn(StringBuffer stringBuffer) {
        if (stringBuffer == null) {
            return false;
        }
        return replaceIn(stringBuffer, 0, stringBuffer.length());
    }

    public boolean replaceIn(StringBuffer stringBuffer, int i, int i2) {
        if (stringBuffer == null) {
            return false;
        }
        StrBuilder append = new StrBuilder(i2).append(stringBuffer, i, i2);
        if (!substitute(append, 0, i2)) {
            return false;
        }
        stringBuffer.replace(i, i + i2, append.toString());
        return true;
    }

    public boolean replaceIn(StringBuilder sb) {
        if (sb == null) {
            return false;
        }
        return replaceIn(sb, 0, sb.length());
    }

    public boolean replaceIn(StringBuilder sb, int i, int i2) {
        if (sb == null) {
            return false;
        }
        StrBuilder append = new StrBuilder(i2).append(sb, i, i2);
        if (!substitute(append, 0, i2)) {
            return false;
        }
        sb.replace(i, i + i2, append.toString());
        return true;
    }

    public boolean replaceIn(StrBuilder strBuilder) {
        if (strBuilder == null) {
            return false;
        }
        return substitute(strBuilder, 0, strBuilder.length());
    }

    public boolean replaceIn(StrBuilder strBuilder, int i, int i2) {
        if (strBuilder == null) {
            return false;
        }
        return substitute(strBuilder, i, i2);
    }

    /* access modifiers changed from: protected */
    public boolean substitute(StrBuilder strBuilder, int i, int i2) {
        return substitute(strBuilder, i, i2, null) > 0;
    }

    /* JADX WARNING: Removed duplicated region for block: B:38:0x00b6  */
    /* JADX WARNING: Removed duplicated region for block: B:42:0x00db  */
    /* JADX WARNING: Removed duplicated region for block: B:55:0x0130  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private int substitute(org.apache.commons.lang3.text.StrBuilder r21, int r22, int r23, java.util.List<java.lang.String> r24) {
        /*
            r20 = this;
            org.apache.commons.lang3.text.StrMatcher r13 = r20.getVariablePrefixMatcher()
            org.apache.commons.lang3.text.StrMatcher r14 = r20.getVariableSuffixMatcher()
            char r15 = r20.getEscapeChar()
            org.apache.commons.lang3.text.StrMatcher r16 = r20.getValueDelimiterMatcher()
            boolean r17 = r20.isEnableSubstitutionInVariables()
            if (r24 != 0) goto L_0x0031
            r2 = 1
        L_0x0017:
            r8 = 0
            r7 = 0
            r0 = r21
            char[] r6 = r0.buffer
            int r5 = r22 + r23
            r9 = r22
            r3 = r24
        L_0x0023:
            if (r9 >= r5) goto L_0x0124
            r0 = r22
            int r11 = r13.isMatch(r6, r9, r0, r5)
            if (r11 != 0) goto L_0x0033
            int r4 = r9 + 1
        L_0x002f:
            r9 = r4
            goto L_0x0023
        L_0x0031:
            r2 = 0
            goto L_0x0017
        L_0x0033:
            r0 = r22
            if (r9 <= r0) goto L_0x004f
            int r4 = r9 + -1
            char r4 = r6[r4]
            if (r4 != r15) goto L_0x004f
            int r4 = r9 + -1
            r0 = r21
            r0.deleteCharAt(r4)
            r0 = r21
            char[] r6 = r0.buffer
            int r7 = r7 + -1
            r8 = 1
            int r5 = r5 + -1
            r4 = r9
            goto L_0x002f
        L_0x004f:
            int r4 = r9 + r11
            r10 = 0
        L_0x0052:
            if (r4 >= r5) goto L_0x002f
            if (r17 == 0) goto L_0x0062
            r0 = r22
            int r12 = r13.isMatch(r6, r4, r0, r5)
            if (r12 == 0) goto L_0x0062
            int r10 = r10 + 1
            int r4 = r4 + r12
            goto L_0x0052
        L_0x0062:
            r0 = r22
            int r18 = r14.isMatch(r6, r4, r0, r5)
            if (r18 != 0) goto L_0x006d
            int r4 = r4 + 1
            goto L_0x0052
        L_0x006d:
            if (r10 != 0) goto L_0x011e
            java.lang.String r12 = new java.lang.String
            int r10 = r9 + r11
            int r19 = r4 - r9
            int r11 = r19 - r11
            r12.<init>(r6, r10, r11)
            if (r17 == 0) goto L_0x008f
            org.apache.commons.lang3.text.StrBuilder r10 = new org.apache.commons.lang3.text.StrBuilder
            r10.<init>(r12)
            r11 = 0
            int r12 = r10.length()
            r0 = r20
            r0.substitute(r10, r11, r12)
            java.lang.String r12 = r10.toString()
        L_0x008f:
            int r4 = r4 + r18
            r11 = 0
            if (r16 == 0) goto L_0x012e
            char[] r18 = r12.toCharArray()
            r10 = 0
        L_0x0099:
            r0 = r18
            int r0 = r0.length
            r19 = r0
            r0 = r19
            if (r10 >= r0) goto L_0x012e
            if (r17 != 0) goto L_0x0103
            r0 = r18
            int r0 = r0.length
            r19 = r0
            r0 = r18
            r1 = r19
            int r19 = r13.isMatch(r0, r10, r10, r1)
            if (r19 == 0) goto L_0x0103
            r10 = r11
        L_0x00b4:
            if (r3 != 0) goto L_0x00c7
            java.util.ArrayList r3 = new java.util.ArrayList
            r3.<init>()
            java.lang.String r11 = new java.lang.String
            r0 = r22
            r1 = r23
            r11.<init>(r6, r0, r1)
            r3.add(r11)
        L_0x00c7:
            r0 = r20
            r0.checkCyclicSubstitution(r12, r3)
            r3.add(r12)
            r0 = r20
            r1 = r21
            java.lang.String r11 = r0.resolveVariable(r12, r1, r9, r4)
            if (r11 != 0) goto L_0x0130
        L_0x00d9:
            if (r10 == 0) goto L_0x00f8
            int r6 = r10.length()
            r0 = r21
            r0.replace(r9, r4, r10)
            r8 = 1
            r0 = r20
            r1 = r21
            int r10 = r0.substitute(r1, r9, r6, r3)
            int r6 = r6 + r10
            int r9 = r4 - r9
            int r6 = r6 - r9
            int r4 = r4 + r6
            int r5 = r5 + r6
            int r7 = r7 + r6
            r0 = r21
            char[] r6 = r0.buffer
        L_0x00f8:
            int r9 = r3.size()
            int r9 = r9 + -1
            r3.remove(r9)
            goto L_0x002f
        L_0x0103:
            r0 = r16
            r1 = r18
            int r19 = r0.isMatch(r1, r10)
            if (r19 == 0) goto L_0x011a
            r11 = 0
            java.lang.String r11 = r12.substring(r11, r10)
            int r10 = r10 + r19
            java.lang.String r10 = r12.substring(r10)
            r12 = r11
            goto L_0x00b4
        L_0x011a:
            int r10 = r10 + 1
            goto L_0x0099
        L_0x011e:
            int r10 = r10 + -1
            int r4 = r4 + r18
            goto L_0x0052
        L_0x0124:
            if (r2 == 0) goto L_0x012c
            if (r8 == 0) goto L_0x012a
            r2 = 1
        L_0x0129:
            return r2
        L_0x012a:
            r2 = 0
            goto L_0x0129
        L_0x012c:
            r2 = r7
            goto L_0x0129
        L_0x012e:
            r10 = r11
            goto L_0x00b4
        L_0x0130:
            r10 = r11
            goto L_0x00d9
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.text.StrSubstitutor.substitute(org.apache.commons.lang3.text.StrBuilder, int, int, java.util.List):int");
    }

    private void checkCyclicSubstitution(String str, List<String> list) {
        if (list.contains(str)) {
            StrBuilder strBuilder = new StrBuilder(256);
            strBuilder.append("Infinite loop in property interpolation of ");
            strBuilder.append((String) list.remove(0));
            strBuilder.append(": ");
            strBuilder.appendWithSeparators((Iterable<?>) list, "->");
            throw new IllegalStateException(strBuilder.toString());
        }
    }

    /* access modifiers changed from: protected */
    public String resolveVariable(String str, StrBuilder strBuilder, int i, int i2) {
        StrLookup variableResolver2 = getVariableResolver();
        if (variableResolver2 == null) {
            return null;
        }
        return variableResolver2.lookup(str);
    }

    public char getEscapeChar() {
        return this.escapeChar;
    }

    public void setEscapeChar(char c) {
        this.escapeChar = c;
    }

    public StrMatcher getVariablePrefixMatcher() {
        return this.prefixMatcher;
    }

    public StrSubstitutor setVariablePrefixMatcher(StrMatcher strMatcher) {
        if (strMatcher == null) {
            throw new IllegalArgumentException("Variable prefix matcher must not be null!");
        }
        this.prefixMatcher = strMatcher;
        return this;
    }

    public StrSubstitutor setVariablePrefix(char c) {
        return setVariablePrefixMatcher(StrMatcher.charMatcher(c));
    }

    public StrSubstitutor setVariablePrefix(String str) {
        if (str != null) {
            return setVariablePrefixMatcher(StrMatcher.stringMatcher(str));
        }
        throw new IllegalArgumentException("Variable prefix must not be null!");
    }

    public StrMatcher getVariableSuffixMatcher() {
        return this.suffixMatcher;
    }

    public StrSubstitutor setVariableSuffixMatcher(StrMatcher strMatcher) {
        if (strMatcher == null) {
            throw new IllegalArgumentException("Variable suffix matcher must not be null!");
        }
        this.suffixMatcher = strMatcher;
        return this;
    }

    public StrSubstitutor setVariableSuffix(char c) {
        return setVariableSuffixMatcher(StrMatcher.charMatcher(c));
    }

    public StrSubstitutor setVariableSuffix(String str) {
        if (str != null) {
            return setVariableSuffixMatcher(StrMatcher.stringMatcher(str));
        }
        throw new IllegalArgumentException("Variable suffix must not be null!");
    }

    public StrMatcher getValueDelimiterMatcher() {
        return this.valueDelimiterMatcher;
    }

    public StrSubstitutor setValueDelimiterMatcher(StrMatcher strMatcher) {
        this.valueDelimiterMatcher = strMatcher;
        return this;
    }

    public StrSubstitutor setValueDelimiter(char c) {
        return setValueDelimiterMatcher(StrMatcher.charMatcher(c));
    }

    public StrSubstitutor setValueDelimiter(String str) {
        if (!StringUtils.isEmpty(str)) {
            return setValueDelimiterMatcher(StrMatcher.stringMatcher(str));
        }
        setValueDelimiterMatcher(null);
        return this;
    }

    public StrLookup<?> getVariableResolver() {
        return this.variableResolver;
    }

    public void setVariableResolver(StrLookup<?> strLookup) {
        this.variableResolver = strLookup;
    }

    public boolean isEnableSubstitutionInVariables() {
        return this.enableSubstitutionInVariables;
    }

    public void setEnableSubstitutionInVariables(boolean z) {
        this.enableSubstitutionInVariables = z;
    }
}
