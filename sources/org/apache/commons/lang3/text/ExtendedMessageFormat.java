package org.apache.commons.lang3.text;

import java.text.Format;
import java.text.MessageFormat;
import java.text.ParsePosition;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Iterator;
import java.util.Locale;
import java.util.Map;
import org.apache.commons.lang3.ObjectUtils;
import org.apache.commons.lang3.Validate;

public class ExtendedMessageFormat extends MessageFormat {
    static final /* synthetic */ boolean $assertionsDisabled = (!ExtendedMessageFormat.class.desiredAssertionStatus());
    private static final String DUMMY_PATTERN = "";
    private static final char END_FE = '}';
    private static final int HASH_SEED = 31;
    private static final char QUOTE = '\'';
    private static final char START_FE = '{';
    private static final char START_FMT = ',';
    private static final long serialVersionUID = -2362048321261811743L;
    private final Map<String, ? extends FormatFactory> registry;
    private String toPattern;

    public ExtendedMessageFormat(String str) {
        this(str, Locale.getDefault());
    }

    public ExtendedMessageFormat(String str, Locale locale) {
        this(str, locale, null);
    }

    public ExtendedMessageFormat(String str, Map<String, ? extends FormatFactory> map) {
        this(str, Locale.getDefault(), map);
    }

    public ExtendedMessageFormat(String str, Locale locale, Map<String, ? extends FormatFactory> map) {
        super("");
        setLocale(locale);
        this.registry = map;
        applyPattern(str);
    }

    public String toPattern() {
        return this.toPattern;
    }

    public final void applyPattern(String str) {
        String str2;
        Format format;
        boolean z;
        boolean z2;
        int i = 0;
        if (this.registry == null) {
            super.applyPattern(str);
            this.toPattern = super.toPattern();
            return;
        }
        ArrayList arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        StringBuilder sb = new StringBuilder(str.length());
        ParsePosition parsePosition = new ParsePosition(0);
        char[] charArray = str.toCharArray();
        int i2 = 0;
        while (parsePosition.getIndex() < str.length()) {
            switch (charArray[parsePosition.getIndex()]) {
                case '\'':
                    appendQuotedString(str, parsePosition, sb);
                    continue;
                case '{':
                    int i3 = i2 + 1;
                    seekNonWs(str, parsePosition);
                    int index = parsePosition.getIndex();
                    sb.append(START_FE).append(readArgumentIndex(str, next(parsePosition)));
                    seekNonWs(str, parsePosition);
                    if (charArray[parsePosition.getIndex()] == ',') {
                        str2 = parseFormatDescription(str, next(parsePosition));
                        format = getFormat(str2);
                        if (format == null) {
                            sb.append(START_FMT).append(str2);
                        }
                    } else {
                        str2 = null;
                        format = null;
                    }
                    arrayList.add(format);
                    if (format == null) {
                        str2 = null;
                    }
                    arrayList2.add(str2);
                    if (arrayList.size() == i3) {
                        z = true;
                    } else {
                        z = false;
                    }
                    Validate.isTrue(z);
                    if (arrayList2.size() == i3) {
                        z2 = true;
                    } else {
                        z2 = false;
                    }
                    Validate.isTrue(z2);
                    if (charArray[parsePosition.getIndex()] == '}') {
                        i2 = i3;
                        break;
                    } else {
                        throw new IllegalArgumentException("Unreadable format element at position " + index);
                    }
            }
            sb.append(charArray[parsePosition.getIndex()]);
            next(parsePosition);
        }
        super.applyPattern(sb.toString());
        this.toPattern = insertFormats(super.toPattern(), arrayList2);
        if (containsElements(arrayList)) {
            Format[] formats = getFormats();
            Iterator it = arrayList.iterator();
            while (it.hasNext()) {
                Format format2 = (Format) it.next();
                if (format2 != null) {
                    formats[i] = format2;
                }
                i++;
            }
            super.setFormats(formats);
        }
    }

    public void setFormat(int i, Format format) {
        throw new UnsupportedOperationException();
    }

    public void setFormatByArgumentIndex(int i, Format format) {
        throw new UnsupportedOperationException();
    }

    public void setFormats(Format[] formatArr) {
        throw new UnsupportedOperationException();
    }

    public void setFormatsByArgumentIndex(Format[] formatArr) {
        throw new UnsupportedOperationException();
    }

    public boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (obj == null) {
            return false;
        }
        if (!super.equals(obj)) {
            return false;
        }
        if (ObjectUtils.notEqual(getClass(), obj.getClass())) {
            return false;
        }
        ExtendedMessageFormat extendedMessageFormat = (ExtendedMessageFormat) obj;
        if (ObjectUtils.notEqual(this.toPattern, extendedMessageFormat.toPattern)) {
            return false;
        }
        if (ObjectUtils.notEqual(this.registry, extendedMessageFormat.registry)) {
            return false;
        }
        return true;
    }

    public int hashCode() {
        return (((super.hashCode() * 31) + ObjectUtils.hashCode(this.registry)) * 31) + ObjectUtils.hashCode(this.toPattern);
    }

    private Format getFormat(String str) {
        String str2;
        if (this.registry == null) {
            return null;
        }
        int indexOf = str.indexOf(44);
        if (indexOf > 0) {
            String trim = str.substring(0, indexOf).trim();
            str2 = str.substring(indexOf + 1).trim();
            str = trim;
        } else {
            str2 = null;
        }
        FormatFactory formatFactory = (FormatFactory) this.registry.get(str);
        if (formatFactory != null) {
            return formatFactory.getFormat(str, str2, getLocale());
        }
        return null;
    }

    private int readArgumentIndex(String str, ParsePosition parsePosition) {
        int index = parsePosition.getIndex();
        seekNonWs(str, parsePosition);
        StringBuilder sb = new StringBuilder();
        boolean z = false;
        while (!z && parsePosition.getIndex() < str.length()) {
            char charAt = str.charAt(parsePosition.getIndex());
            if (Character.isWhitespace(charAt)) {
                seekNonWs(str, parsePosition);
                charAt = str.charAt(parsePosition.getIndex());
                if (!(charAt == ',' || charAt == '}')) {
                    z = true;
                    next(parsePosition);
                }
            }
            char c = charAt;
            if ((c == ',' || c == '}') && sb.length() > 0) {
                try {
                    return Integer.parseInt(sb.toString());
                } catch (NumberFormatException e) {
                }
            }
            if (!Character.isDigit(c)) {
                z = true;
            } else {
                z = false;
            }
            sb.append(c);
            next(parsePosition);
        }
        if (z) {
            throw new IllegalArgumentException("Invalid format argument index at position " + index + ": " + str.substring(index, parsePosition.getIndex()));
        }
        throw new IllegalArgumentException("Unterminated format element at position " + index);
    }

    private String parseFormatDescription(String str, ParsePosition parsePosition) {
        int index = parsePosition.getIndex();
        seekNonWs(str, parsePosition);
        int index2 = parsePosition.getIndex();
        int i = 1;
        while (parsePosition.getIndex() < str.length()) {
            switch (str.charAt(parsePosition.getIndex())) {
                case '\'':
                    getQuotedString(str, parsePosition);
                    break;
                case '{':
                    i++;
                    break;
                case '}':
                    i--;
                    if (i != 0) {
                        break;
                    } else {
                        return str.substring(index2, parsePosition.getIndex());
                    }
            }
            next(parsePosition);
        }
        throw new IllegalArgumentException("Unterminated format element at position " + index);
    }

    private String insertFormats(String str, ArrayList<String> arrayList) {
        int i = 0;
        if (!containsElements(arrayList)) {
            return str;
        }
        StringBuilder sb = new StringBuilder(str.length() * 2);
        ParsePosition parsePosition = new ParsePosition(0);
        int i2 = -1;
        while (parsePosition.getIndex() < str.length()) {
            char charAt = str.charAt(parsePosition.getIndex());
            switch (charAt) {
                case '\'':
                    appendQuotedString(str, parsePosition, sb);
                    continue;
                case '{':
                    int i3 = i + 1;
                    sb.append(START_FE).append(readArgumentIndex(str, next(parsePosition)));
                    if (i3 != 1) {
                        i = i3;
                        break;
                    } else {
                        i2++;
                        String str2 = (String) arrayList.get(i2);
                        if (str2 != null) {
                            sb.append(START_FMT).append(str2);
                        }
                        i = i3;
                        continue;
                    }
                case '}':
                    i--;
                    break;
            }
            sb.append(charAt);
            next(parsePosition);
        }
        return sb.toString();
    }

    private void seekNonWs(String str, ParsePosition parsePosition) {
        char[] charArray = str.toCharArray();
        do {
            int isMatch = StrMatcher.splitMatcher().isMatch(charArray, parsePosition.getIndex());
            parsePosition.setIndex(parsePosition.getIndex() + isMatch);
            if (isMatch <= 0) {
                return;
            }
        } while (parsePosition.getIndex() < str.length());
    }

    private ParsePosition next(ParsePosition parsePosition) {
        parsePosition.setIndex(parsePosition.getIndex() + 1);
        return parsePosition;
    }

    private StringBuilder appendQuotedString(String str, ParsePosition parsePosition, StringBuilder sb) {
        if ($assertionsDisabled || str.toCharArray()[parsePosition.getIndex()] == '\'') {
            if (sb != null) {
                sb.append(QUOTE);
            }
            next(parsePosition);
            int index = parsePosition.getIndex();
            char[] charArray = str.toCharArray();
            int index2 = parsePosition.getIndex();
            while (index2 < str.length()) {
                switch (charArray[parsePosition.getIndex()]) {
                    case '\'':
                        next(parsePosition);
                        if (sb == null) {
                            return null;
                        }
                        return sb.append(charArray, index, parsePosition.getIndex() - index);
                    default:
                        next(parsePosition);
                        index2++;
                }
            }
            throw new IllegalArgumentException("Unterminated quoted string at position " + index);
        }
        throw new AssertionError("Quoted string must start with quote character");
    }

    private void getQuotedString(String str, ParsePosition parsePosition) {
        appendQuotedString(str, parsePosition, null);
    }

    private boolean containsElements(Collection<?> collection) {
        if (collection == null || collection.isEmpty()) {
            return false;
        }
        for (Object obj : collection) {
            if (obj != null) {
                return true;
            }
        }
        return false;
    }
}
