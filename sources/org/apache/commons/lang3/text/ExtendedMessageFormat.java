package org.apache.commons.lang3.text;

import android.support.v4.view.MotionEventCompat;
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
        int i = 0;
        if (this.registry == null) {
            super.applyPattern(str);
            this.toPattern = super.toPattern();
            return;
        }
        Object arrayList = new ArrayList();
        ArrayList arrayList2 = new ArrayList();
        StringBuilder stringBuilder = new StringBuilder(str.length());
        ParsePosition parsePosition = new ParsePosition(0);
        char[] toCharArray = str.toCharArray();
        int i2 = 0;
        while (parsePosition.getIndex() < str.length()) {
            switch (toCharArray[parsePosition.getIndex()]) {
                case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    appendQuotedString(str, parsePosition, stringBuilder);
                    continue;
                case '{':
                    Object parseFormatDescription;
                    Object format;
                    boolean z;
                    int i3 = i2 + 1;
                    seekNonWs(str, parsePosition);
                    int index = parsePosition.getIndex();
                    stringBuilder.append(START_FE).append(readArgumentIndex(str, next(parsePosition)));
                    seekNonWs(str, parsePosition);
                    if (toCharArray[parsePosition.getIndex()] == START_FMT) {
                        parseFormatDescription = parseFormatDescription(str, next(parsePosition));
                        format = getFormat(parseFormatDescription);
                        if (format == null) {
                            stringBuilder.append(START_FMT).append(parseFormatDescription);
                        }
                    } else {
                        parseFormatDescription = null;
                        format = null;
                    }
                    arrayList.add(format);
                    if (format == null) {
                        parseFormatDescription = null;
                    }
                    arrayList2.add(parseFormatDescription);
                    if (arrayList.size() == i3) {
                        z = true;
                    } else {
                        z = false;
                    }
                    Validate.isTrue(z);
                    if (arrayList2.size() == i3) {
                        z = true;
                    } else {
                        z = false;
                    }
                    Validate.isTrue(z);
                    if (toCharArray[parsePosition.getIndex()] == END_FE) {
                        i2 = i3;
                        break;
                    }
                    throw new IllegalArgumentException("Unreadable format element at position " + index);
            }
            stringBuilder.append(toCharArray[parsePosition.getIndex()]);
            next(parsePosition);
        }
        super.applyPattern(stringBuilder.toString());
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
        return (((super.hashCode() * HASH_SEED) + ObjectUtils.hashCode(this.registry)) * HASH_SEED) + ObjectUtils.hashCode(this.toPattern);
    }

    private Format getFormat(String str) {
        if (this.registry == null) {
            return null;
        }
        String trim;
        int indexOf = str.indexOf(44);
        if (indexOf > 0) {
            str = str.substring(0, indexOf).trim();
            trim = str.substring(indexOf + 1).trim();
        } else {
            trim = null;
        }
        FormatFactory formatFactory = (FormatFactory) this.registry.get(str);
        if (formatFactory != null) {
            return formatFactory.getFormat(str, trim, getLocale());
        }
        return null;
    }

    private int readArgumentIndex(String str, ParsePosition parsePosition) {
        int index = parsePosition.getIndex();
        seekNonWs(str, parsePosition);
        StringBuilder stringBuilder = new StringBuilder();
        Object obj = null;
        while (obj == null && parsePosition.getIndex() < str.length()) {
            char charAt = str.charAt(parsePosition.getIndex());
            if (Character.isWhitespace(charAt)) {
                seekNonWs(str, parsePosition);
                charAt = str.charAt(parsePosition.getIndex());
                if (!(charAt == START_FMT || charAt == END_FE)) {
                    obj = 1;
                    next(parsePosition);
                }
            }
            char c = charAt;
            if ((c == START_FMT || c == END_FE) && stringBuilder.length() > 0) {
                try {
                    return Integer.parseInt(stringBuilder.toString());
                } catch (NumberFormatException e) {
                }
            }
            if (Character.isDigit(c)) {
                obj = null;
            } else {
                obj = 1;
            }
            stringBuilder.append(c);
            next(parsePosition);
        }
        if (obj != null) {
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
                case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    getQuotedString(str, parsePosition);
                    break;
                case '{':
                    i++;
                    break;
                case '}':
                    i--;
                    if (i != 0) {
                        break;
                    }
                    return str.substring(index2, parsePosition.getIndex());
                default:
                    break;
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
        StringBuilder stringBuilder = new StringBuilder(str.length() * 2);
        ParsePosition parsePosition = new ParsePosition(0);
        int i2 = -1;
        while (parsePosition.getIndex() < str.length()) {
            char charAt = str.charAt(parsePosition.getIndex());
            switch (charAt) {
                case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    appendQuotedString(str, parsePosition, stringBuilder);
                    continue;
                case '{':
                    int i3 = i + 1;
                    stringBuilder.append(START_FE).append(readArgumentIndex(str, next(parsePosition)));
                    if (i3 != 1) {
                        i = i3;
                        break;
                    }
                    i2++;
                    String str2 = (String) arrayList.get(i2);
                    if (str2 != null) {
                        stringBuilder.append(START_FMT).append(str2);
                    }
                    i = i3;
                    continue;
                case '}':
                    i--;
                    break;
            }
            stringBuilder.append(charAt);
            next(parsePosition);
        }
        return stringBuilder.toString();
    }

    private void seekNonWs(String str, ParsePosition parsePosition) {
        char[] toCharArray = str.toCharArray();
        do {
            int isMatch = StrMatcher.splitMatcher().isMatch(toCharArray, parsePosition.getIndex());
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

    private StringBuilder appendQuotedString(String str, ParsePosition parsePosition, StringBuilder stringBuilder) {
        if ($assertionsDisabled || str.toCharArray()[parsePosition.getIndex()] == QUOTE) {
            if (stringBuilder != null) {
                stringBuilder.append(QUOTE);
            }
            next(parsePosition);
            int index = parsePosition.getIndex();
            char[] toCharArray = str.toCharArray();
            int index2 = parsePosition.getIndex();
            while (index2 < str.length()) {
                switch (toCharArray[parsePosition.getIndex()]) {
                    case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                        next(parsePosition);
                        return stringBuilder == null ? null : stringBuilder.append(toCharArray, index, parsePosition.getIndex() - index);
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
