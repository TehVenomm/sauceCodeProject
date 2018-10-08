package org.apache.commons.lang3.time;

import im.getsocial.sdk.consts.LanguageCodes;
import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.Serializable;
import java.text.DateFormatSymbols;
import java.text.ParseException;
import java.text.ParsePosition;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.Map.Entry;
import java.util.SortedMap;
import java.util.TimeZone;
import java.util.TreeMap;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentMap;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class FastDateParser implements DateParser, Serializable {
    private static final Strategy ABBREVIATED_YEAR_STRATEGY = new NumberStrategy(1) {
        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            int parseInt = Integer.parseInt(str);
            if (parseInt < 100) {
                parseInt = fastDateParser.adjustYear(parseInt);
            }
            calendar.set(1, parseInt);
        }
    };
    private static final Strategy DAY_OF_MONTH_STRATEGY = new NumberStrategy(5);
    private static final Strategy DAY_OF_WEEK_IN_MONTH_STRATEGY = new NumberStrategy(8);
    private static final Strategy DAY_OF_YEAR_STRATEGY = new NumberStrategy(6);
    private static final Strategy HOUR12_STRATEGY = new NumberStrategy(10) {
        int modify(int i) {
            return i == 12 ? 0 : i;
        }
    };
    private static final Strategy HOUR24_OF_DAY_STRATEGY = new NumberStrategy(11) {
        int modify(int i) {
            return i == 24 ? 0 : i;
        }
    };
    private static final Strategy HOUR_OF_DAY_STRATEGY = new NumberStrategy(11);
    private static final Strategy HOUR_STRATEGY = new NumberStrategy(10);
    private static final Strategy ISO_8601_STRATEGY = new ISO8601TimeZoneStrategy("(Z|(?:[+-]\\d{2}(?::?\\d{2})?))");
    static final Locale JAPANESE_IMPERIAL = new Locale(LanguageCodes.JAPANESE, "JP", "JP");
    private static final Strategy LITERAL_YEAR_STRATEGY = new NumberStrategy(1);
    private static final Strategy MILLISECOND_STRATEGY = new NumberStrategy(14);
    private static final Strategy MINUTE_STRATEGY = new NumberStrategy(12);
    private static final Strategy NUMBER_MONTH_STRATEGY = new NumberStrategy(2) {
        int modify(int i) {
            return i - 1;
        }
    };
    private static final Strategy SECOND_STRATEGY = new NumberStrategy(13);
    private static final Strategy WEEK_OF_MONTH_STRATEGY = new NumberStrategy(4);
    private static final Strategy WEEK_OF_YEAR_STRATEGY = new NumberStrategy(3);
    private static final ConcurrentMap<Locale, Strategy>[] caches = new ConcurrentMap[17];
    private static final Pattern formatPattern = Pattern.compile("D+|E+|F+|G+|H+|K+|M+|S+|W+|X+|Z+|a+|d+|h+|k+|m+|s+|w+|y+|z+|''|'[^']++(''[^']*+)*+'|[^'A-Za-z]++");
    private static final long serialVersionUID = 2;
    private final int century;
    private transient String currentFormatField;
    private final Locale locale;
    private transient Strategy nextStrategy;
    private transient Pattern parsePattern;
    private final String pattern;
    private final int startYear;
    private transient Strategy[] strategies;
    private final TimeZone timeZone;

    private static abstract class Strategy {
        abstract boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder);

        private Strategy() {
        }

        boolean isNumber() {
            return false;
        }

        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
        }
    }

    private static class NumberStrategy extends Strategy {
        private final int field;

        NumberStrategy(int i) {
            super();
            this.field = i;
        }

        boolean isNumber() {
            return true;
        }

        boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder) {
            if (fastDateParser.isNextNumber()) {
                stringBuilder.append("(\\p{Nd}{").append(fastDateParser.getFieldWidth()).append("}+)");
            } else {
                stringBuilder.append("(\\p{Nd}++)");
            }
            return true;
        }

        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            calendar.set(this.field, modify(Integer.parseInt(str)));
        }

        int modify(int i) {
            return i;
        }
    }

    private static class CaseInsensitiveTextStrategy extends Strategy {
        private final int field;
        private final Map<String, Integer> lKeyValues = new HashMap();
        private final Locale locale;

        CaseInsensitiveTextStrategy(int i, Calendar calendar, Locale locale) {
            super();
            this.field = i;
            this.locale = locale;
            Map access$200 = FastDateParser.getDisplayNames(i, calendar, locale);
            for (Entry entry : access$200.entrySet()) {
                this.lKeyValues.put(((String) entry.getKey()).toLowerCase(locale), entry.getValue());
            }
        }

        boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder) {
            stringBuilder.append("((?iu)");
            for (String access$100 : this.lKeyValues.keySet()) {
                FastDateParser.escapeRegex(stringBuilder, access$100, false).append('|');
            }
            stringBuilder.setCharAt(stringBuilder.length() - 1, ')');
            return true;
        }

        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            Integer num = (Integer) this.lKeyValues.get(str.toLowerCase(this.locale));
            if (num == null) {
                StringBuilder stringBuilder = new StringBuilder(str);
                stringBuilder.append(" not in (");
                for (String append : this.lKeyValues.keySet()) {
                    stringBuilder.append(append).append(' ');
                }
                stringBuilder.setCharAt(stringBuilder.length() - 1, ')');
                throw new IllegalArgumentException(stringBuilder.toString());
            }
            calendar.set(this.field, num.intValue());
        }
    }

    private static class CopyQuotedStrategy extends Strategy {
        private final String formatField;

        CopyQuotedStrategy(String str) {
            super();
            this.formatField = str;
        }

        boolean isNumber() {
            char charAt = this.formatField.charAt(0);
            if (charAt == '\'') {
                charAt = this.formatField.charAt(1);
            }
            return Character.isDigit(charAt);
        }

        boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder) {
            FastDateParser.escapeRegex(stringBuilder, this.formatField, true);
            return false;
        }
    }

    private static class ISO8601TimeZoneStrategy extends Strategy {
        private static final Strategy ISO_8601_1_STRATEGY = new ISO8601TimeZoneStrategy("(Z|(?:[+-]\\d{2}))");
        private static final Strategy ISO_8601_2_STRATEGY = new ISO8601TimeZoneStrategy("(Z|(?:[+-]\\d{2}\\d{2}))");
        private static final Strategy ISO_8601_3_STRATEGY = new ISO8601TimeZoneStrategy("(Z|(?:[+-]\\d{2}(?::)\\d{2}))");
        private final String pattern;

        ISO8601TimeZoneStrategy(String str) {
            super();
            this.pattern = str;
        }

        boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder) {
            stringBuilder.append(this.pattern);
            return true;
        }

        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            if (str.equals("Z")) {
                calendar.setTimeZone(TimeZone.getTimeZone("UTC"));
            } else {
                calendar.setTimeZone(TimeZone.getTimeZone("GMT" + str));
            }
        }

        static Strategy getStrategy(int i) {
            switch (i) {
                case 1:
                    return ISO_8601_1_STRATEGY;
                case 2:
                    return ISO_8601_2_STRATEGY;
                case 3:
                    return ISO_8601_3_STRATEGY;
                default:
                    throw new IllegalArgumentException("invalid number of X");
            }
        }
    }

    private static class TimeZoneStrategy extends Strategy {
        private static final int ID = 0;
        private static final int LONG_DST = 3;
        private static final int LONG_STD = 1;
        private static final int SHORT_DST = 4;
        private static final int SHORT_STD = 2;
        private final SortedMap<String, TimeZone> tzNames = new TreeMap(String.CASE_INSENSITIVE_ORDER);
        private final String validTimeZoneChars;

        TimeZoneStrategy(Locale locale) {
            super();
            for (Object[] objArr : DateFormatSymbols.getInstance(locale).getZoneStrings()) {
                if (!objArr[0].startsWith("GMT")) {
                    TimeZone timeZone = TimeZone.getTimeZone(objArr[0]);
                    if (!this.tzNames.containsKey(objArr[1])) {
                        this.tzNames.put(objArr[1], timeZone);
                    }
                    if (!this.tzNames.containsKey(objArr[2])) {
                        this.tzNames.put(objArr[2], timeZone);
                    }
                    if (timeZone.useDaylightTime()) {
                        if (!this.tzNames.containsKey(objArr[3])) {
                            this.tzNames.put(objArr[3], timeZone);
                        }
                        if (!this.tzNames.containsKey(objArr[4])) {
                            this.tzNames.put(objArr[4], timeZone);
                        }
                    }
                }
            }
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.append("(GMT[+-]\\d{1,2}:\\d{2}").append('|');
            stringBuilder.append("[+-]\\d{4}").append('|');
            for (String access$100 : this.tzNames.keySet()) {
                FastDateParser.escapeRegex(stringBuilder, access$100, false).append('|');
            }
            stringBuilder.setCharAt(stringBuilder.length() - 1, ')');
            this.validTimeZoneChars = stringBuilder.toString();
        }

        boolean addRegex(FastDateParser fastDateParser, StringBuilder stringBuilder) {
            stringBuilder.append(this.validTimeZoneChars);
            return true;
        }

        void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            TimeZone timeZone;
            if (str.charAt(0) == '+' || str.charAt(0) == '-') {
                timeZone = TimeZone.getTimeZone("GMT" + str);
            } else if (str.startsWith("GMT")) {
                timeZone = TimeZone.getTimeZone(str);
            } else {
                timeZone = (TimeZone) this.tzNames.get(str);
                if (timeZone == null) {
                    throw new IllegalArgumentException(str + " is not a supported timezone name");
                }
            }
            calendar.setTimeZone(timeZone);
        }
    }

    protected FastDateParser(String str, TimeZone timeZone, Locale locale) {
        this(str, timeZone, locale, null);
    }

    protected FastDateParser(String str, TimeZone timeZone, Locale locale, Date date) {
        int i;
        this.pattern = str;
        this.timeZone = timeZone;
        this.locale = locale;
        Calendar instance = Calendar.getInstance(timeZone, locale);
        if (date != null) {
            instance.setTime(date);
            i = instance.get(1);
        } else if (locale.equals(JAPANESE_IMPERIAL)) {
            i = 0;
        } else {
            instance.setTime(new Date());
            i = instance.get(1) - 80;
        }
        this.century = (i / 100) * 100;
        this.startYear = i - this.century;
        init(instance);
    }

    private void init(Calendar calendar) {
        StringBuilder stringBuilder = new StringBuilder();
        List arrayList = new ArrayList();
        Matcher matcher = formatPattern.matcher(this.pattern);
        if (matcher.lookingAt()) {
            this.currentFormatField = matcher.group();
            Strategy strategy = getStrategy(this.currentFormatField, calendar);
            while (true) {
                matcher.region(matcher.end(), matcher.regionEnd());
                if (!matcher.lookingAt()) {
                    break;
                }
                String group = matcher.group();
                this.nextStrategy = getStrategy(group, calendar);
                if (strategy.addRegex(this, stringBuilder)) {
                    arrayList.add(strategy);
                }
                this.currentFormatField = group;
                strategy = this.nextStrategy;
            }
            this.nextStrategy = null;
            if (matcher.regionStart() != matcher.regionEnd()) {
                throw new IllegalArgumentException("Failed to parse \"" + this.pattern + "\" ; gave up at index " + matcher.regionStart());
            }
            if (strategy.addRegex(this, stringBuilder)) {
                arrayList.add(strategy);
            }
            this.currentFormatField = null;
            this.strategies = (Strategy[]) arrayList.toArray(new Strategy[arrayList.size()]);
            this.parsePattern = Pattern.compile(stringBuilder.toString());
            return;
        }
        throw new IllegalArgumentException("Illegal pattern character '" + this.pattern.charAt(matcher.regionStart()) + "'");
    }

    public String getPattern() {
        return this.pattern;
    }

    public TimeZone getTimeZone() {
        return this.timeZone;
    }

    public Locale getLocale() {
        return this.locale;
    }

    Pattern getParsePattern() {
        return this.parsePattern;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof FastDateParser)) {
            return false;
        }
        FastDateParser fastDateParser = (FastDateParser) obj;
        if (this.pattern.equals(fastDateParser.pattern) && this.timeZone.equals(fastDateParser.timeZone) && this.locale.equals(fastDateParser.locale)) {
            return true;
        }
        return false;
    }

    public int hashCode() {
        return this.pattern.hashCode() + ((this.timeZone.hashCode() + (this.locale.hashCode() * 13)) * 13);
    }

    public String toString() {
        return "FastDateParser[" + this.pattern + "," + this.locale + "," + this.timeZone.getID() + "]";
    }

    private void readObject(ObjectInputStream objectInputStream) throws IOException, ClassNotFoundException {
        objectInputStream.defaultReadObject();
        init(Calendar.getInstance(this.timeZone, this.locale));
    }

    public Object parseObject(String str) throws ParseException {
        return parse(str);
    }

    public Date parse(String str) throws ParseException {
        Date parse = parse(str, new ParsePosition(0));
        if (parse != null) {
            return parse;
        }
        if (this.locale.equals(JAPANESE_IMPERIAL)) {
            throw new ParseException("(The " + this.locale + " locale does not support dates before 1868 AD)\n" + "Unparseable date: \"" + str + "\" does not match " + this.parsePattern.pattern(), 0);
        }
        throw new ParseException("Unparseable date: \"" + str + "\" does not match " + this.parsePattern.pattern(), 0);
    }

    public Object parseObject(String str, ParsePosition parsePosition) {
        return parse(str, parsePosition);
    }

    public Date parse(String str, ParsePosition parsePosition) {
        int index = parsePosition.getIndex();
        Matcher matcher = this.parsePattern.matcher(str.substring(index));
        if (!matcher.lookingAt()) {
            return null;
        }
        Calendar instance = Calendar.getInstance(this.timeZone, this.locale);
        instance.clear();
        int i = 0;
        while (i < this.strategies.length) {
            int i2 = i + 1;
            this.strategies[i].setCalendar(this, instance, matcher.group(i2));
            i = i2;
        }
        parsePosition.setIndex(matcher.end() + index);
        return instance.getTime();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.StringBuilder escapeRegex(java.lang.StringBuilder r4, java.lang.String r5, boolean r6) {
        /*
        r0 = "\\Q";
        r4.append(r0);
        r0 = 0;
    L_0x0006:
        r1 = r5.length();
        if (r0 >= r1) goto L_0x004b;
    L_0x000c:
        r1 = r5.charAt(r0);
        switch(r1) {
            case 39: goto L_0x001c;
            case 92: goto L_0x002c;
            default: goto L_0x0013;
        };
    L_0x0013:
        r3 = r1;
        r1 = r0;
        r0 = r3;
    L_0x0016:
        r4.append(r0);
        r0 = r1 + 1;
        goto L_0x0006;
    L_0x001c:
        if (r6 == 0) goto L_0x0013;
    L_0x001e:
        r1 = r0 + 1;
        r0 = r5.length();
        if (r1 != r0) goto L_0x0027;
    L_0x0026:
        return r4;
    L_0x0027:
        r0 = r5.charAt(r1);
        goto L_0x0016;
    L_0x002c:
        r2 = r0 + 1;
        r0 = r5.length();
        if (r2 != r0) goto L_0x0037;
    L_0x0034:
        r0 = r1;
        r1 = r2;
        goto L_0x0016;
    L_0x0037:
        r4.append(r1);
        r0 = r5.charAt(r2);
        r1 = 69;
        if (r0 != r1) goto L_0x0051;
    L_0x0042:
        r0 = "E\\\\E\\";
        r4.append(r0);
        r0 = 81;
        r1 = r2;
        goto L_0x0016;
    L_0x004b:
        r0 = "\\E";
        r4.append(r0);
        goto L_0x0026;
    L_0x0051:
        r1 = r2;
        goto L_0x0016;
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.time.FastDateParser.escapeRegex(java.lang.StringBuilder, java.lang.String, boolean):java.lang.StringBuilder");
    }

    private static Map<String, Integer> getDisplayNames(int i, Calendar calendar, Locale locale) {
        return calendar.getDisplayNames(i, 0, locale);
    }

    private int adjustYear(int i) {
        int i2 = this.century + i;
        return i >= this.startYear ? i2 : i2 + 100;
    }

    boolean isNextNumber() {
        return this.nextStrategy != null && this.nextStrategy.isNumber();
    }

    int getFieldWidth() {
        return this.currentFormatField.length();
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private org.apache.commons.lang3.time.FastDateParser.Strategy getStrategy(java.lang.String r4, java.util.Calendar r5) {
        /*
        r3 = this;
        r1 = 0;
        r2 = 2;
        r0 = r4.charAt(r1);
        switch(r0) {
            case 39: goto L_0x000f;
            case 68: goto L_0x0026;
            case 69: goto L_0x0029;
            case 70: goto L_0x002f;
            case 71: goto L_0x0032;
            case 72: goto L_0x0037;
            case 75: goto L_0x003a;
            case 77: goto L_0x003d;
            case 83: goto L_0x004c;
            case 87: goto L_0x004f;
            case 88: goto L_0x0077;
            case 90: goto L_0x0080;
            case 97: goto L_0x0052;
            case 100: goto L_0x0059;
            case 104: goto L_0x005c;
            case 107: goto L_0x005f;
            case 109: goto L_0x0062;
            case 115: goto L_0x0065;
            case 119: goto L_0x0068;
            case 121: goto L_0x006b;
            case 122: goto L_0x008b;
            default: goto L_0x0009;
        };
    L_0x0009:
        r0 = new org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy;
        r0.<init>(r4);
    L_0x000e:
        return r0;
    L_0x000f:
        r0 = r4.length();
        if (r0 <= r2) goto L_0x0009;
    L_0x0015:
        r0 = new org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy;
        r1 = 1;
        r2 = r4.length();
        r2 = r2 + -1;
        r1 = r4.substring(r1, r2);
        r0.<init>(r1);
        goto L_0x000e;
    L_0x0026:
        r0 = DAY_OF_YEAR_STRATEGY;
        goto L_0x000e;
    L_0x0029:
        r0 = 7;
        r0 = r3.getLocaleSpecificStrategy(r0, r5);
        goto L_0x000e;
    L_0x002f:
        r0 = DAY_OF_WEEK_IN_MONTH_STRATEGY;
        goto L_0x000e;
    L_0x0032:
        r0 = r3.getLocaleSpecificStrategy(r1, r5);
        goto L_0x000e;
    L_0x0037:
        r0 = HOUR_OF_DAY_STRATEGY;
        goto L_0x000e;
    L_0x003a:
        r0 = HOUR_STRATEGY;
        goto L_0x000e;
    L_0x003d:
        r0 = r4.length();
        r1 = 3;
        if (r0 < r1) goto L_0x0049;
    L_0x0044:
        r0 = r3.getLocaleSpecificStrategy(r2, r5);
        goto L_0x000e;
    L_0x0049:
        r0 = NUMBER_MONTH_STRATEGY;
        goto L_0x000e;
    L_0x004c:
        r0 = MILLISECOND_STRATEGY;
        goto L_0x000e;
    L_0x004f:
        r0 = WEEK_OF_MONTH_STRATEGY;
        goto L_0x000e;
    L_0x0052:
        r0 = 9;
        r0 = r3.getLocaleSpecificStrategy(r0, r5);
        goto L_0x000e;
    L_0x0059:
        r0 = DAY_OF_MONTH_STRATEGY;
        goto L_0x000e;
    L_0x005c:
        r0 = HOUR12_STRATEGY;
        goto L_0x000e;
    L_0x005f:
        r0 = HOUR24_OF_DAY_STRATEGY;
        goto L_0x000e;
    L_0x0062:
        r0 = MINUTE_STRATEGY;
        goto L_0x000e;
    L_0x0065:
        r0 = SECOND_STRATEGY;
        goto L_0x000e;
    L_0x0068:
        r0 = WEEK_OF_YEAR_STRATEGY;
        goto L_0x000e;
    L_0x006b:
        r0 = r4.length();
        if (r0 <= r2) goto L_0x0074;
    L_0x0071:
        r0 = LITERAL_YEAR_STRATEGY;
        goto L_0x000e;
    L_0x0074:
        r0 = ABBREVIATED_YEAR_STRATEGY;
        goto L_0x000e;
    L_0x0077:
        r0 = r4.length();
        r0 = org.apache.commons.lang3.time.FastDateParser.ISO8601TimeZoneStrategy.getStrategy(r0);
        goto L_0x000e;
    L_0x0080:
        r0 = "ZZ";
        r0 = r4.equals(r0);
        if (r0 == 0) goto L_0x008b;
    L_0x0088:
        r0 = ISO_8601_STRATEGY;
        goto L_0x000e;
    L_0x008b:
        r0 = 15;
        r0 = r3.getLocaleSpecificStrategy(r0, r5);
        goto L_0x000e;
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.time.FastDateParser.getStrategy(java.lang.String, java.util.Calendar):org.apache.commons.lang3.time.FastDateParser$Strategy");
    }

    private static ConcurrentMap<Locale, Strategy> getCache(int i) {
        ConcurrentMap<Locale, Strategy> concurrentMap;
        synchronized (caches) {
            if (caches[i] == null) {
                caches[i] = new ConcurrentHashMap(3);
            }
            concurrentMap = caches[i];
        }
        return concurrentMap;
    }

    private Strategy getLocaleSpecificStrategy(int i, Calendar calendar) {
        Strategy timeZoneStrategy;
        ConcurrentMap cache = getCache(i);
        Strategy strategy = (Strategy) cache.get(this.locale);
        if (strategy == null) {
            timeZoneStrategy = i == 15 ? new TimeZoneStrategy(this.locale) : new CaseInsensitiveTextStrategy(i, calendar, this.locale);
            strategy = (Strategy) cache.putIfAbsent(this.locale, timeZoneStrategy);
            if (strategy != null) {
                return strategy;
            }
        }
        timeZoneStrategy = strategy;
        return timeZoneStrategy;
    }
}
