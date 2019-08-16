package com.fasterxml.jackson.databind.util;

import com.fasterxml.jackson.core.p015io.NumberInput;
import java.text.DateFormat;
import java.text.FieldPosition;
import java.text.ParseException;
import java.text.ParsePosition;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;

public class StdDateFormat extends DateFormat {
    protected static final String[] ALL_FORMATS = {DATE_FORMAT_STR_ISO8601, DATE_FORMAT_STR_ISO8601_Z, DATE_FORMAT_STR_RFC1123, DATE_FORMAT_STR_PLAIN};
    protected static final DateFormat DATE_FORMAT_ISO8601 = new SimpleDateFormat(DATE_FORMAT_STR_ISO8601, DEFAULT_LOCALE);
    protected static final DateFormat DATE_FORMAT_ISO8601_Z = new SimpleDateFormat(DATE_FORMAT_STR_ISO8601_Z, DEFAULT_LOCALE);
    protected static final DateFormat DATE_FORMAT_PLAIN = new SimpleDateFormat(DATE_FORMAT_STR_PLAIN, DEFAULT_LOCALE);
    protected static final DateFormat DATE_FORMAT_RFC1123 = new SimpleDateFormat(DATE_FORMAT_STR_RFC1123, DEFAULT_LOCALE);
    public static final String DATE_FORMAT_STR_ISO8601 = "yyyy-MM-dd'T'HH:mm:ss.SSSZ";
    protected static final String DATE_FORMAT_STR_ISO8601_Z = "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'";
    protected static final String DATE_FORMAT_STR_PLAIN = "yyyy-MM-dd";
    protected static final String DATE_FORMAT_STR_RFC1123 = "EEE, dd MMM yyyy HH:mm:ss zzz";
    private static final Locale DEFAULT_LOCALE = Locale.US;
    private static final TimeZone DEFAULT_TIMEZONE = TimeZone.getTimeZone("UTC");
    public static final StdDateFormat instance = new StdDateFormat();
    protected transient DateFormat _formatISO8601;
    protected transient DateFormat _formatISO8601_z;
    protected transient DateFormat _formatPlain;
    protected transient DateFormat _formatRFC1123;
    protected Boolean _lenient;
    protected final Locale _locale;
    protected transient TimeZone _timezone;

    static {
        DATE_FORMAT_RFC1123.setTimeZone(DEFAULT_TIMEZONE);
        DATE_FORMAT_ISO8601.setTimeZone(DEFAULT_TIMEZONE);
        DATE_FORMAT_ISO8601_Z.setTimeZone(DEFAULT_TIMEZONE);
        DATE_FORMAT_PLAIN.setTimeZone(DEFAULT_TIMEZONE);
    }

    public StdDateFormat() {
        this._locale = DEFAULT_LOCALE;
    }

    @Deprecated
    public StdDateFormat(TimeZone timeZone, Locale locale) {
        this._timezone = timeZone;
        this._locale = locale;
    }

    protected StdDateFormat(TimeZone timeZone, Locale locale, Boolean bool) {
        this._timezone = timeZone;
        this._locale = locale;
        this._lenient = bool;
    }

    public static TimeZone getDefaultTimeZone() {
        return DEFAULT_TIMEZONE;
    }

    public StdDateFormat withTimeZone(TimeZone timeZone) {
        if (timeZone == null) {
            timeZone = DEFAULT_TIMEZONE;
        }
        return (timeZone == this._timezone || timeZone.equals(this._timezone)) ? this : new StdDateFormat(timeZone, this._locale, this._lenient);
    }

    public StdDateFormat withLocale(Locale locale) {
        return locale.equals(this._locale) ? this : new StdDateFormat(this._timezone, locale, this._lenient);
    }

    public StdDateFormat clone() {
        return new StdDateFormat(this._timezone, this._locale, this._lenient);
    }

    @Deprecated
    public static DateFormat getISO8601Format(TimeZone timeZone) {
        return getISO8601Format(timeZone, DEFAULT_LOCALE);
    }

    public static DateFormat getISO8601Format(TimeZone timeZone, Locale locale) {
        return _cloneFormat(DATE_FORMAT_ISO8601, DATE_FORMAT_STR_ISO8601, timeZone, locale, null);
    }

    public static DateFormat getRFC1123Format(TimeZone timeZone, Locale locale) {
        return _cloneFormat(DATE_FORMAT_RFC1123, DATE_FORMAT_STR_RFC1123, timeZone, locale, null);
    }

    @Deprecated
    public static DateFormat getRFC1123Format(TimeZone timeZone) {
        return getRFC1123Format(timeZone, DEFAULT_LOCALE);
    }

    public TimeZone getTimeZone() {
        return this._timezone;
    }

    public void setTimeZone(TimeZone timeZone) {
        if (!timeZone.equals(this._timezone)) {
            _clearFormats();
            this._timezone = timeZone;
        }
    }

    public void setLenient(boolean z) {
        Boolean valueOf = Boolean.valueOf(z);
        if (this._lenient != valueOf) {
            this._lenient = valueOf;
            _clearFormats();
        }
    }

    public boolean isLenient() {
        if (this._lenient == null) {
            return true;
        }
        return this._lenient.booleanValue();
    }

    public Date parse(String str) throws ParseException {
        Date parseAsRFC1123;
        String[] strArr;
        String trim = str.trim();
        ParsePosition parsePosition = new ParsePosition(0);
        if (looksLikeISO8601(trim)) {
            parseAsRFC1123 = parseAsISO8601(trim, parsePosition, true);
        } else {
            int length = trim.length();
            while (true) {
                length--;
                if (length < 0) {
                    break;
                }
                char charAt = trim.charAt(length);
                if ((charAt < '0' || charAt > '9') && (length > 0 || charAt != '-')) {
                    break;
                }
            }
            if (length >= 0 || (trim.charAt(0) != '-' && !NumberInput.inLongRange(trim, false))) {
                parseAsRFC1123 = parseAsRFC1123(trim, parsePosition);
            } else {
                parseAsRFC1123 = new Date(Long.parseLong(trim));
            }
        }
        if (parseAsRFC1123 != null) {
            return parseAsRFC1123;
        }
        StringBuilder sb = new StringBuilder();
        for (String str2 : ALL_FORMATS) {
            if (sb.length() > 0) {
                sb.append("\", \"");
            } else {
                sb.append('\"');
            }
            sb.append(str2);
        }
        sb.append('\"');
        throw new ParseException(String.format("Can not parse date \"%s\": not compatible with any of standard forms (%s)", new Object[]{trim, sb.toString()}), parsePosition.getErrorIndex());
    }

    public Date parse(String str, ParsePosition parsePosition) {
        if (looksLikeISO8601(str)) {
            try {
                return parseAsISO8601(str, parsePosition, false);
            } catch (ParseException e) {
                return null;
            }
        } else {
            int length = str.length();
            while (true) {
                length--;
                if (length < 0) {
                    break;
                }
                char charAt = str.charAt(length);
                if ((charAt < '0' || charAt > '9') && (length > 0 || charAt != '-')) {
                    break;
                }
            }
            if (length >= 0 || (str.charAt(0) != '-' && !NumberInput.inLongRange(str, false))) {
                return parseAsRFC1123(str, parsePosition);
            }
            return new Date(Long.parseLong(str));
        }
    }

    public StringBuffer format(Date date, StringBuffer stringBuffer, FieldPosition fieldPosition) {
        if (this._formatISO8601 == null) {
            this._formatISO8601 = _cloneFormat(DATE_FORMAT_ISO8601, DATE_FORMAT_STR_ISO8601, this._timezone, this._locale, this._lenient);
        }
        return this._formatISO8601.format(date, stringBuffer, fieldPosition);
    }

    public String toString() {
        String str = "DateFormat " + getClass().getName();
        TimeZone timeZone = this._timezone;
        if (timeZone != null) {
            str = str + " (timezone: " + timeZone + ")";
        }
        return str + "(locale: " + this._locale + ")";
    }

    public boolean equals(Object obj) {
        return obj == this;
    }

    public int hashCode() {
        return System.identityHashCode(this);
    }

    /* access modifiers changed from: protected */
    public boolean looksLikeISO8601(String str) {
        if (str.length() < 5 || !Character.isDigit(str.charAt(0)) || !Character.isDigit(str.charAt(3)) || str.charAt(4) != '-') {
            return false;
        }
        return true;
    }

    /* access modifiers changed from: protected */
    /* JADX WARNING: Code restructure failed: missing block: B:51:0x014f, code lost:
        r0.append('0');
     */
    /* JADX WARNING: Code restructure failed: missing block: B:52:0x0152, code lost:
        r0.append('0');
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.util.Date parseAsISO8601(java.lang.String r9, java.text.ParsePosition r10, boolean r11) throws java.text.ParseException {
        /*
            r8 = this;
            r7 = 90
            r5 = 84
            r6 = 58
            r4 = 12
            r3 = 48
            int r2 = r9.length()
            int r0 = r2 + -1
            char r0 = r9.charAt(r0)
            r1 = 10
            if (r2 > r1) goto L_0x0057
            boolean r1 = java.lang.Character.isDigit(r0)
            if (r1 == 0) goto L_0x0057
            java.text.DateFormat r2 = r8._formatPlain
            java.lang.String r0 = "yyyy-MM-dd"
            if (r2 != 0) goto L_0x0157
            java.text.DateFormat r1 = DATE_FORMAT_PLAIN
            java.util.TimeZone r2 = r8._timezone
            java.util.Locale r3 = r8._locale
            java.lang.Boolean r4 = r8._lenient
            java.text.DateFormat r2 = _cloneFormat(r1, r0, r2, r3, r4)
            r8._formatPlain = r2
            r1 = r0
        L_0x0033:
            java.util.Date r0 = r2.parse(r9, r10)
            if (r0 != 0) goto L_0x0156
            java.text.ParseException r0 = new java.text.ParseException
            java.lang.String r2 = "Can not parse date \"%s\": while it seems to fit format '%s', parsing fails (leniency? %s)"
            r3 = 3
            java.lang.Object[] r3 = new java.lang.Object[r3]
            r4 = 0
            r3[r4] = r9
            r4 = 1
            r3[r4] = r1
            r1 = 2
            java.lang.Boolean r4 = r8._lenient
            r3[r1] = r4
            java.lang.String r1 = java.lang.String.format(r2, r3)
            int r2 = r10.getErrorIndex()
            r0.<init>(r1, r2)
            throw r0
        L_0x0057:
            if (r0 != r7) goto L_0x0087
            java.text.DateFormat r0 = r8._formatISO8601_z
            java.lang.String r1 = "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"
            if (r0 != 0) goto L_0x006d
            java.text.DateFormat r0 = DATE_FORMAT_ISO8601_Z
            java.util.TimeZone r3 = r8._timezone
            java.util.Locale r4 = r8._locale
            java.lang.Boolean r5 = r8._lenient
            java.text.DateFormat r0 = _cloneFormat(r0, r1, r3, r4, r5)
            r8._formatISO8601_z = r0
        L_0x006d:
            int r3 = r2 + -4
            char r3 = r9.charAt(r3)
            if (r3 != r6) goto L_0x015a
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r3.<init>(r9)
            int r2 = r2 + -1
            java.lang.String r4 = ".000"
            r3.insert(r2, r4)
            java.lang.String r9 = r3.toString()
            r2 = r0
            goto L_0x0033
        L_0x0087:
            boolean r0 = hasTimeZone(r9)
            if (r0 == 0) goto L_0x0117
            int r0 = r2 + -3
            char r0 = r9.charAt(r0)
            if (r0 != r6) goto L_0x00da
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>(r9)
            int r1 = r2 + -3
            int r2 = r2 + -2
            r0.delete(r1, r2)
            java.lang.String r9 = r0.toString()
        L_0x00a5:
            int r0 = r9.length()
            int r1 = r9.lastIndexOf(r5)
            int r1 = r0 - r1
            int r1 = r1 + -6
            if (r1 >= r4) goto L_0x00c1
            int r0 = r0 + -5
            java.lang.StringBuilder r2 = new java.lang.StringBuilder
            r2.<init>(r9)
            switch(r1) {
                case 5: goto L_0x0111;
                case 6: goto L_0x010c;
                case 7: goto L_0x00bd;
                case 8: goto L_0x0106;
                case 9: goto L_0x0100;
                case 10: goto L_0x00fa;
                case 11: goto L_0x00f6;
                default: goto L_0x00bd;
            }
        L_0x00bd:
            java.lang.String r9 = r2.toString()
        L_0x00c1:
            java.text.DateFormat r0 = r8._formatISO8601
            java.lang.String r1 = "yyyy-MM-dd'T'HH:mm:ss.SSSZ"
            java.text.DateFormat r2 = r8._formatISO8601
            if (r2 != 0) goto L_0x00d7
            java.text.DateFormat r0 = DATE_FORMAT_ISO8601
            java.util.TimeZone r2 = r8._timezone
            java.util.Locale r3 = r8._locale
            java.lang.Boolean r4 = r8._lenient
            java.text.DateFormat r0 = _cloneFormat(r0, r1, r2, r3, r4)
            r8._formatISO8601 = r0
        L_0x00d7:
            r2 = r0
            goto L_0x0033
        L_0x00da:
            r1 = 43
            if (r0 == r1) goto L_0x00e2
            r1 = 45
            if (r0 != r1) goto L_0x00a5
        L_0x00e2:
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>()
            java.lang.StringBuilder r0 = r0.append(r9)
            java.lang.String r1 = "00"
            java.lang.StringBuilder r0 = r0.append(r1)
            java.lang.String r9 = r0.toString()
            goto L_0x00a5
        L_0x00f6:
            r2.insert(r0, r3)
            goto L_0x00bd
        L_0x00fa:
            java.lang.String r1 = "00"
            r2.insert(r0, r1)
            goto L_0x00bd
        L_0x0100:
            java.lang.String r1 = "000"
            r2.insert(r0, r1)
            goto L_0x00bd
        L_0x0106:
            java.lang.String r1 = ".000"
            r2.insert(r0, r1)
            goto L_0x00bd
        L_0x010c:
            java.lang.String r1 = "00.000"
            r2.insert(r0, r1)
        L_0x0111:
            java.lang.String r1 = ":00.000"
            r2.insert(r0, r1)
            goto L_0x00bd
        L_0x0117:
            java.lang.StringBuilder r0 = new java.lang.StringBuilder
            r0.<init>(r9)
            int r1 = r9.lastIndexOf(r5)
            int r1 = r2 - r1
            int r1 = r1 + -1
            if (r1 >= r4) goto L_0x012e
            switch(r1) {
                case 9: goto L_0x0152;
                case 10: goto L_0x014f;
                case 11: goto L_0x014c;
                default: goto L_0x0129;
            }
        L_0x0129:
            java.lang.String r1 = ".000"
            r0.append(r1)
        L_0x012e:
            r0.append(r7)
            java.lang.String r9 = r0.toString()
            java.text.DateFormat r2 = r8._formatISO8601_z
            java.lang.String r0 = "yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"
            if (r2 != 0) goto L_0x0157
            java.text.DateFormat r1 = DATE_FORMAT_ISO8601_Z
            java.util.TimeZone r2 = r8._timezone
            java.util.Locale r3 = r8._locale
            java.lang.Boolean r4 = r8._lenient
            java.text.DateFormat r2 = _cloneFormat(r1, r0, r2, r3, r4)
            r8._formatISO8601_z = r2
            r1 = r0
            goto L_0x0033
        L_0x014c:
            r0.append(r3)
        L_0x014f:
            r0.append(r3)
        L_0x0152:
            r0.append(r3)
            goto L_0x012e
        L_0x0156:
            return r0
        L_0x0157:
            r1 = r0
            goto L_0x0033
        L_0x015a:
            r2 = r0
            goto L_0x0033
        */
        throw new UnsupportedOperationException("Method not decompiled: com.fasterxml.jackson.databind.util.StdDateFormat.parseAsISO8601(java.lang.String, java.text.ParsePosition, boolean):java.util.Date");
    }

    /* access modifiers changed from: protected */
    public Date parseAsRFC1123(String str, ParsePosition parsePosition) {
        if (this._formatRFC1123 == null) {
            this._formatRFC1123 = _cloneFormat(DATE_FORMAT_RFC1123, DATE_FORMAT_STR_RFC1123, this._timezone, this._locale, this._lenient);
        }
        return this._formatRFC1123.parse(str, parsePosition);
    }

    private static final boolean hasTimeZone(String str) {
        int length = str.length();
        if (length >= 6) {
            char charAt = str.charAt(length - 6);
            if (charAt == '+' || charAt == '-') {
                return true;
            }
            char charAt2 = str.charAt(length - 5);
            if (charAt2 == '+' || charAt2 == '-') {
                return true;
            }
            char charAt3 = str.charAt(length - 3);
            if (charAt3 == '+' || charAt3 == '-') {
                return true;
            }
        }
        return false;
    }

    private static final DateFormat _cloneFormat(DateFormat dateFormat, String str, TimeZone timeZone, Locale locale, Boolean bool) {
        DateFormat dateFormat2;
        if (!locale.equals(DEFAULT_LOCALE)) {
            dateFormat2 = new SimpleDateFormat(str, locale);
            if (timeZone == null) {
                timeZone = DEFAULT_TIMEZONE;
            }
            dateFormat2.setTimeZone(timeZone);
        } else {
            dateFormat2 = (DateFormat) dateFormat.clone();
            if (timeZone != null) {
                dateFormat2.setTimeZone(timeZone);
            }
        }
        if (bool != null) {
            dateFormat2.setLenient(bool.booleanValue());
        }
        return dateFormat2;
    }

    /* access modifiers changed from: protected */
    public void _clearFormats() {
        this._formatRFC1123 = null;
        this._formatISO8601 = null;
        this._formatISO8601_z = null;
        this._formatPlain = null;
    }
}
