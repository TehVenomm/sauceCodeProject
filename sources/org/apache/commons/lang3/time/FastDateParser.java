package org.apache.commons.lang3.time;

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
        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
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
        /* access modifiers changed from: 0000 */
        public int modify(int i) {
            if (i == 12) {
                return 0;
            }
            return i;
        }
    };
    private static final Strategy HOUR24_OF_DAY_STRATEGY = new NumberStrategy(11) {
        /* access modifiers changed from: 0000 */
        public int modify(int i) {
            if (i == 24) {
                return 0;
            }
            return i;
        }
    };
    private static final Strategy HOUR_OF_DAY_STRATEGY = new NumberStrategy(11);
    private static final Strategy HOUR_STRATEGY = new NumberStrategy(10);
    private static final Strategy ISO_8601_STRATEGY = new ISO8601TimeZoneStrategy("(Z|(?:[+-]\\d{2}(?::?\\d{2})?))");
    static final Locale JAPANESE_IMPERIAL = new Locale("ja", "JP", "JP");
    private static final Strategy LITERAL_YEAR_STRATEGY = new NumberStrategy(1);
    private static final Strategy MILLISECOND_STRATEGY = new NumberStrategy(14);
    private static final Strategy MINUTE_STRATEGY = new NumberStrategy(12);
    private static final Strategy NUMBER_MONTH_STRATEGY = new NumberStrategy(2) {
        /* access modifiers changed from: 0000 */
        public int modify(int i) {
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

    private static class CaseInsensitiveTextStrategy extends Strategy {
        private final int field;
        private final Map<String, Integer> lKeyValues = new HashMap();
        private final Locale locale;

        CaseInsensitiveTextStrategy(int i, Calendar calendar, Locale locale2) {
            super();
            this.field = i;
            this.locale = locale2;
            Map access$200 = FastDateParser.getDisplayNames(i, calendar, locale2);
            for (Entry entry : access$200.entrySet()) {
                this.lKeyValues.put(((String) entry.getKey()).toLowerCase(locale2), entry.getValue());
            }
        }

        /* access modifiers changed from: 0000 */
        public boolean addRegex(FastDateParser fastDateParser, StringBuilder sb) {
            sb.append("((?iu)");
            for (String access$100 : this.lKeyValues.keySet()) {
                FastDateParser.escapeRegex(sb, access$100, false).append('|');
            }
            sb.setCharAt(sb.length() - 1, ')');
            return true;
        }

        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            Integer num = (Integer) this.lKeyValues.get(str.toLowerCase(this.locale));
            if (num == null) {
                StringBuilder sb = new StringBuilder(str);
                sb.append(" not in (");
                for (String append : this.lKeyValues.keySet()) {
                    sb.append(append).append(' ');
                }
                sb.setCharAt(sb.length() - 1, ')');
                throw new IllegalArgumentException(sb.toString());
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

        /* access modifiers changed from: 0000 */
        public boolean isNumber() {
            char charAt = this.formatField.charAt(0);
            if (charAt == '\'') {
                charAt = this.formatField.charAt(1);
            }
            return Character.isDigit(charAt);
        }

        /* access modifiers changed from: 0000 */
        public boolean addRegex(FastDateParser fastDateParser, StringBuilder sb) {
            FastDateParser.escapeRegex(sb, this.formatField, true);
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

        /* access modifiers changed from: 0000 */
        public boolean addRegex(FastDateParser fastDateParser, StringBuilder sb) {
            sb.append(this.pattern);
            return true;
        }

        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
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

    private static class NumberStrategy extends Strategy {
        private final int field;

        NumberStrategy(int i) {
            super();
            this.field = i;
        }

        /* access modifiers changed from: 0000 */
        public boolean isNumber() {
            return true;
        }

        /* access modifiers changed from: 0000 */
        public boolean addRegex(FastDateParser fastDateParser, StringBuilder sb) {
            if (fastDateParser.isNextNumber()) {
                sb.append("(\\p{Nd}{").append(fastDateParser.getFieldWidth()).append("}+)");
            } else {
                sb.append("(\\p{Nd}++)");
            }
            return true;
        }

        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
            calendar.set(this.field, modify(Integer.parseInt(str)));
        }

        /* access modifiers changed from: 0000 */
        public int modify(int i) {
            return i;
        }
    }

    private static abstract class Strategy {
        /* access modifiers changed from: 0000 */
        public abstract boolean addRegex(FastDateParser fastDateParser, StringBuilder sb);

        private Strategy() {
        }

        /* access modifiers changed from: 0000 */
        public boolean isNumber() {
            return false;
        }

        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
        }
    }

    private static class TimeZoneStrategy extends Strategy {

        /* renamed from: ID */
        private static final int f1433ID = 0;
        private static final int LONG_DST = 3;
        private static final int LONG_STD = 1;
        private static final int SHORT_DST = 4;
        private static final int SHORT_STD = 2;
        private final SortedMap<String, TimeZone> tzNames = new TreeMap(String.CASE_INSENSITIVE_ORDER);
        private final String validTimeZoneChars;

        TimeZoneStrategy(Locale locale) {
            String[][] zoneStrings;
            super();
            for (String[] strArr : DateFormatSymbols.getInstance(locale).getZoneStrings()) {
                if (!strArr[0].startsWith("GMT")) {
                    TimeZone timeZone = TimeZone.getTimeZone(strArr[0]);
                    if (!this.tzNames.containsKey(strArr[1])) {
                        this.tzNames.put(strArr[1], timeZone);
                    }
                    if (!this.tzNames.containsKey(strArr[2])) {
                        this.tzNames.put(strArr[2], timeZone);
                    }
                    if (timeZone.useDaylightTime()) {
                        if (!this.tzNames.containsKey(strArr[3])) {
                            this.tzNames.put(strArr[3], timeZone);
                        }
                        if (!this.tzNames.containsKey(strArr[4])) {
                            this.tzNames.put(strArr[4], timeZone);
                        }
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.append("(GMT[+-]\\d{1,2}:\\d{2}").append('|');
            sb.append("[+-]\\d{4}").append('|');
            for (String access$100 : this.tzNames.keySet()) {
                FastDateParser.escapeRegex(sb, access$100, false).append('|');
            }
            sb.setCharAt(sb.length() - 1, ')');
            this.validTimeZoneChars = sb.toString();
        }

        /* access modifiers changed from: 0000 */
        public boolean addRegex(FastDateParser fastDateParser, StringBuilder sb) {
            sb.append(this.validTimeZoneChars);
            return true;
        }

        /* access modifiers changed from: 0000 */
        public void setCalendar(FastDateParser fastDateParser, Calendar calendar, String str) {
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

    protected FastDateParser(String str, TimeZone timeZone2, Locale locale2) {
        this(str, timeZone2, locale2, null);
    }

    protected FastDateParser(String str, TimeZone timeZone2, Locale locale2, Date date) {
        int i;
        this.pattern = str;
        this.timeZone = timeZone2;
        this.locale = locale2;
        Calendar instance = Calendar.getInstance(timeZone2, locale2);
        if (date != null) {
            instance.setTime(date);
            i = instance.get(1);
        } else if (locale2.equals(JAPANESE_IMPERIAL)) {
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
        StringBuilder sb = new StringBuilder();
        ArrayList arrayList = new ArrayList();
        Matcher matcher = formatPattern.matcher(this.pattern);
        if (!matcher.lookingAt()) {
            throw new IllegalArgumentException("Illegal pattern character '" + this.pattern.charAt(matcher.regionStart()) + "'");
        }
        this.currentFormatField = matcher.group();
        Strategy strategy = getStrategy(this.currentFormatField, calendar);
        while (true) {
            matcher.region(matcher.end(), matcher.regionEnd());
            if (!matcher.lookingAt()) {
                break;
            }
            String group = matcher.group();
            this.nextStrategy = getStrategy(group, calendar);
            if (strategy.addRegex(this, sb)) {
                arrayList.add(strategy);
            }
            this.currentFormatField = group;
            strategy = this.nextStrategy;
        }
        this.nextStrategy = null;
        if (matcher.regionStart() != matcher.regionEnd()) {
            throw new IllegalArgumentException("Failed to parse \"" + this.pattern + "\" ; gave up at index " + matcher.regionStart());
        }
        if (strategy.addRegex(this, sb)) {
            arrayList.add(strategy);
        }
        this.currentFormatField = null;
        this.strategies = (Strategy[]) arrayList.toArray(new Strategy[arrayList.size()]);
        this.parsePattern = Pattern.compile(sb.toString());
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

    /* access modifiers changed from: 0000 */
    public Pattern getParsePattern() {
        return this.parsePattern;
    }

    public boolean equals(Object obj) {
        if (!(obj instanceof FastDateParser)) {
            return false;
        }
        FastDateParser fastDateParser = (FastDateParser) obj;
        if (!this.pattern.equals(fastDateParser.pattern) || !this.timeZone.equals(fastDateParser.timeZone) || !this.locale.equals(fastDateParser.locale)) {
            return false;
        }
        return true;
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

    /* access modifiers changed from: private */
    /* JADX WARNING: Code restructure failed: missing block: B:5:0x0013, code lost:
        r2 = r0;
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static java.lang.StringBuilder escapeRegex(java.lang.StringBuilder r3, java.lang.String r4, boolean r5) {
        /*
            java.lang.String r0 = "\\Q"
            r3.append(r0)
            r0 = 0
        L_0x0006:
            int r1 = r4.length()
            if (r0 >= r1) goto L_0x0047
            char r1 = r4.charAt(r0)
            switch(r1) {
                case 39: goto L_0x001a;
                case 92: goto L_0x002b;
                default: goto L_0x0013;
            }
        L_0x0013:
            r2 = r0
        L_0x0014:
            r3.append(r1)
            int r0 = r2 + 1
            goto L_0x0006
        L_0x001a:
            if (r5 == 0) goto L_0x0013
            int r2 = r0 + 1
            int r0 = r4.length()
            if (r2 != r0) goto L_0x0025
        L_0x0024:
            return r3
        L_0x0025:
            char r0 = r4.charAt(r2)
            r1 = r0
            goto L_0x0014
        L_0x002b:
            int r2 = r0 + 1
            int r0 = r4.length()
            if (r2 == r0) goto L_0x0014
            r3.append(r1)
            char r0 = r4.charAt(r2)
            r1 = 69
            if (r0 != r1) goto L_0x004d
            java.lang.String r0 = "E\\\\E\\"
            r3.append(r0)
            r0 = 81
            r1 = r0
            goto L_0x0014
        L_0x0047:
            java.lang.String r0 = "\\E"
            r3.append(r0)
            goto L_0x0024
        L_0x004d:
            r1 = r0
            goto L_0x0014
        */
        throw new UnsupportedOperationException("Method not decompiled: org.apache.commons.lang3.time.FastDateParser.escapeRegex(java.lang.StringBuilder, java.lang.String, boolean):java.lang.StringBuilder");
    }

    /* access modifiers changed from: private */
    public static Map<String, Integer> getDisplayNames(int i, Calendar calendar, Locale locale2) {
        return calendar.getDisplayNames(i, 0, locale2);
    }

    /* access modifiers changed from: private */
    public int adjustYear(int i) {
        int i2 = this.century + i;
        return i >= this.startYear ? i2 : i2 + 100;
    }

    /* access modifiers changed from: 0000 */
    public boolean isNextNumber() {
        return this.nextStrategy != null && this.nextStrategy.isNumber();
    }

    /* access modifiers changed from: 0000 */
    public int getFieldWidth() {
        return this.currentFormatField.length();
    }

    /* JADX WARNING: Code restructure failed: missing block: B:34:?, code lost:
        return new org.apache.commons.lang3.time.FastDateParser.CopyQuotedStrategy(r4);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:57:?, code lost:
        return getLocaleSpecificStrategy(15, r5);
     */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private org.apache.commons.lang3.time.FastDateParser.Strategy getStrategy(java.lang.String r4, java.util.Calendar r5) {
        /*
            r3 = this;
            r1 = 0
            r2 = 2
            char r0 = r4.charAt(r1)
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
            }
        L_0x0009:
            org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy r0 = new org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy
            r0.<init>(r4)
        L_0x000e:
            return r0
        L_0x000f:
            int r0 = r4.length()
            if (r0 <= r2) goto L_0x0009
            org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy r0 = new org.apache.commons.lang3.time.FastDateParser$CopyQuotedStrategy
            r1 = 1
            int r2 = r4.length()
            int r2 = r2 + -1
            java.lang.String r1 = r4.substring(r1, r2)
            r0.<init>(r1)
            goto L_0x000e
        L_0x0026:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = DAY_OF_YEAR_STRATEGY
            goto L_0x000e
        L_0x0029:
            r0 = 7
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = r3.getLocaleSpecificStrategy(r0, r5)
            goto L_0x000e
        L_0x002f:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = DAY_OF_WEEK_IN_MONTH_STRATEGY
            goto L_0x000e
        L_0x0032:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = r3.getLocaleSpecificStrategy(r1, r5)
            goto L_0x000e
        L_0x0037:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = HOUR_OF_DAY_STRATEGY
            goto L_0x000e
        L_0x003a:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = HOUR_STRATEGY
            goto L_0x000e
        L_0x003d:
            int r0 = r4.length()
            r1 = 3
            if (r0 < r1) goto L_0x0049
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = r3.getLocaleSpecificStrategy(r2, r5)
            goto L_0x000e
        L_0x0049:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = NUMBER_MONTH_STRATEGY
            goto L_0x000e
        L_0x004c:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = MILLISECOND_STRATEGY
            goto L_0x000e
        L_0x004f:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = WEEK_OF_MONTH_STRATEGY
            goto L_0x000e
        L_0x0052:
            r0 = 9
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = r3.getLocaleSpecificStrategy(r0, r5)
            goto L_0x000e
        L_0x0059:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = DAY_OF_MONTH_STRATEGY
            goto L_0x000e
        L_0x005c:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = HOUR12_STRATEGY
            goto L_0x000e
        L_0x005f:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = HOUR24_OF_DAY_STRATEGY
            goto L_0x000e
        L_0x0062:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = MINUTE_STRATEGY
            goto L_0x000e
        L_0x0065:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = SECOND_STRATEGY
            goto L_0x000e
        L_0x0068:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = WEEK_OF_YEAR_STRATEGY
            goto L_0x000e
        L_0x006b:
            int r0 = r4.length()
            if (r0 <= r2) goto L_0x0074
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = LITERAL_YEAR_STRATEGY
            goto L_0x000e
        L_0x0074:
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = ABBREVIATED_YEAR_STRATEGY
            goto L_0x000e
        L_0x0077:
            int r0 = r4.length()
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = org.apache.commons.lang3.time.FastDateParser.ISO8601TimeZoneStrategy.getStrategy(r0)
            goto L_0x000e
        L_0x0080:
            java.lang.String r0 = "ZZ"
            boolean r0 = r4.equals(r0)
            if (r0 == 0) goto L_0x008b
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = ISO_8601_STRATEGY
            goto L_0x000e
        L_0x008b:
            r0 = 15
            org.apache.commons.lang3.time.FastDateParser$Strategy r0 = r3.getLocaleSpecificStrategy(r0, r5)
            goto L_0x000e
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
        Strategy strategy;
        ConcurrentMap cache = getCache(i);
        Strategy strategy2 = (Strategy) cache.get(this.locale);
        if (strategy2 == null) {
            strategy = i == 15 ? new TimeZoneStrategy(this.locale) : new CaseInsensitiveTextStrategy(i, calendar, this.locale);
            Strategy strategy3 = (Strategy) cache.putIfAbsent(this.locale, strategy);
            if (strategy3 != null) {
                return strategy3;
            }
        } else {
            strategy = strategy2;
        }
        return strategy;
    }
}
