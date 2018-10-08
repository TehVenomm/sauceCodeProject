package org.apache.commons.lang3;

import android.support.v4.media.TransportMediator;
import com.appsflyer.share.Constants;
import java.io.IOException;
import java.io.Writer;
import org.apache.commons.lang3.text.translate.AggregateTranslator;
import org.apache.commons.lang3.text.translate.CharSequenceTranslator;
import org.apache.commons.lang3.text.translate.EntityArrays;
import org.apache.commons.lang3.text.translate.JavaUnicodeEscaper;
import org.apache.commons.lang3.text.translate.LookupTranslator;
import org.apache.commons.lang3.text.translate.NumericEntityEscaper;
import org.apache.commons.lang3.text.translate.NumericEntityUnescaper;
import org.apache.commons.lang3.text.translate.NumericEntityUnescaper.OPTION;
import org.apache.commons.lang3.text.translate.OctalUnescaper;
import org.apache.commons.lang3.text.translate.UnicodeUnescaper;
import org.apache.commons.lang3.text.translate.UnicodeUnpairedSurrogateRemover;

public class StringEscapeUtils {
    public static final CharSequenceTranslator ESCAPE_CSV = new CsvEscaper();
    public static final CharSequenceTranslator ESCAPE_ECMASCRIPT;
    public static final CharSequenceTranslator ESCAPE_HTML3 = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_ESCAPE()), new LookupTranslator(EntityArrays.ISO8859_1_ESCAPE()));
    public static final CharSequenceTranslator ESCAPE_HTML4 = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_ESCAPE()), new LookupTranslator(EntityArrays.ISO8859_1_ESCAPE()), new LookupTranslator(EntityArrays.HTML40_EXTENDED_ESCAPE()));
    public static final CharSequenceTranslator ESCAPE_JAVA;
    public static final CharSequenceTranslator ESCAPE_JSON;
    @Deprecated
    public static final CharSequenceTranslator ESCAPE_XML = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_ESCAPE()), new LookupTranslator(EntityArrays.APOS_ESCAPE()));
    public static final CharSequenceTranslator ESCAPE_XML10;
    public static final CharSequenceTranslator ESCAPE_XML11;
    public static final CharSequenceTranslator UNESCAPE_CSV = new CsvUnescaper();
    public static final CharSequenceTranslator UNESCAPE_ECMASCRIPT = UNESCAPE_JAVA;
    public static final CharSequenceTranslator UNESCAPE_HTML3 = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_UNESCAPE()), new LookupTranslator(EntityArrays.ISO8859_1_UNESCAPE()), new NumericEntityUnescaper(new OPTION[0]));
    public static final CharSequenceTranslator UNESCAPE_HTML4 = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_UNESCAPE()), new LookupTranslator(EntityArrays.ISO8859_1_UNESCAPE()), new LookupTranslator(EntityArrays.HTML40_EXTENDED_UNESCAPE()), new NumericEntityUnescaper(new OPTION[0]));
    public static final CharSequenceTranslator UNESCAPE_JAVA;
    public static final CharSequenceTranslator UNESCAPE_JSON = UNESCAPE_JAVA;
    public static final CharSequenceTranslator UNESCAPE_XML = new AggregateTranslator(new LookupTranslator(EntityArrays.BASIC_UNESCAPE()), new LookupTranslator(EntityArrays.APOS_UNESCAPE()), new NumericEntityUnescaper(new OPTION[0]));

    static class CsvEscaper extends CharSequenceTranslator {
        private static final char CSV_DELIMITER = ',';
        private static final char CSV_QUOTE = '\"';
        private static final String CSV_QUOTE_STR = String.valueOf(CSV_QUOTE);
        private static final char[] CSV_SEARCH_CHARS = new char[]{CSV_DELIMITER, CSV_QUOTE, CharUtils.CR, '\n'};

        CsvEscaper() {
        }

        public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
            if (i != 0) {
                throw new IllegalStateException("CsvEscaper should never reach the [1] index");
            }
            if (StringUtils.containsNone(charSequence.toString(), CSV_SEARCH_CHARS)) {
                writer.write(charSequence.toString());
            } else {
                writer.write(34);
                writer.write(StringUtils.replace(charSequence.toString(), CSV_QUOTE_STR, CSV_QUOTE_STR + CSV_QUOTE_STR));
                writer.write(34);
            }
            return Character.codePointCount(charSequence, 0, charSequence.length());
        }
    }

    static class CsvUnescaper extends CharSequenceTranslator {
        private static final char CSV_DELIMITER = ',';
        private static final char CSV_QUOTE = '\"';
        private static final String CSV_QUOTE_STR = String.valueOf(CSV_QUOTE);
        private static final char[] CSV_SEARCH_CHARS = new char[]{CSV_DELIMITER, CSV_QUOTE, CharUtils.CR, '\n'};

        CsvUnescaper() {
        }

        public int translate(CharSequence charSequence, int i, Writer writer) throws IOException {
            if (i != 0) {
                throw new IllegalStateException("CsvUnescaper should never reach the [1] index");
            } else if (charSequence.charAt(0) == CSV_QUOTE && charSequence.charAt(charSequence.length() - 1) == CSV_QUOTE) {
                CharSequence charSequence2 = charSequence.subSequence(1, charSequence.length() - 1).toString();
                if (StringUtils.containsAny(charSequence2, CSV_SEARCH_CHARS)) {
                    writer.write(StringUtils.replace(charSequence2, CSV_QUOTE_STR + CSV_QUOTE_STR, CSV_QUOTE_STR));
                } else {
                    writer.write(charSequence.toString());
                }
                return Character.codePointCount(charSequence, 0, charSequence.length());
            } else {
                writer.write(charSequence.toString());
                return Character.codePointCount(charSequence, 0, charSequence.length());
            }
        }
    }

    static {
        r1 = new String[2][];
        r1[0] = new String[]{"\"", "\\\""};
        r1[1] = new String[]{"\\", "\\\\"};
        ESCAPE_JAVA = new LookupTranslator(r1).with(new LookupTranslator(EntityArrays.JAVA_CTRL_CHARS_ESCAPE())).with(JavaUnicodeEscaper.outsideOf(32, TransportMediator.KEYCODE_MEDIA_PAUSE));
        r1 = new CharSequenceTranslator[3];
        r3 = new String[4][];
        r3[0] = new String[]{"'", "\\'"};
        r3[1] = new String[]{"\"", "\\\""};
        r3[2] = new String[]{"\\", "\\\\"};
        r3[3] = new String[]{Constants.URL_PATH_DELIMITER, "\\/"};
        r1[0] = new LookupTranslator(r3);
        r1[1] = new LookupTranslator(EntityArrays.JAVA_CTRL_CHARS_ESCAPE());
        r1[2] = JavaUnicodeEscaper.outsideOf(32, TransportMediator.KEYCODE_MEDIA_PAUSE);
        ESCAPE_ECMASCRIPT = new AggregateTranslator(r1);
        r1 = new CharSequenceTranslator[3];
        r3 = new String[3][];
        r3[0] = new String[]{"\"", "\\\""};
        r3[1] = new String[]{"\\", "\\\\"};
        r3[2] = new String[]{Constants.URL_PATH_DELIMITER, "\\/"};
        r1[0] = new LookupTranslator(r3);
        r1[1] = new LookupTranslator(EntityArrays.JAVA_CTRL_CHARS_ESCAPE());
        r1[2] = JavaUnicodeEscaper.outsideOf(32, TransportMediator.KEYCODE_MEDIA_PAUSE);
        ESCAPE_JSON = new AggregateTranslator(r1);
        r1 = new CharSequenceTranslator[6];
        r3 = new String[31][];
        r3[0] = new String[]{"\u0000", ""};
        r3[1] = new String[]{"\u0001", ""};
        r3[2] = new String[]{"\u0002", ""};
        r3[3] = new String[]{"\u0003", ""};
        r3[4] = new String[]{"\u0004", ""};
        r3[5] = new String[]{"\u0005", ""};
        r3[6] = new String[]{"\u0006", ""};
        r3[7] = new String[]{"\u0007", ""};
        r3[8] = new String[]{"\b", ""};
        r3[9] = new String[]{"\u000b", ""};
        r3[10] = new String[]{"\f", ""};
        r3[11] = new String[]{"\u000e", ""};
        r3[12] = new String[]{"\u000f", ""};
        r3[13] = new String[]{"\u0010", ""};
        r3[14] = new String[]{"\u0011", ""};
        r3[15] = new String[]{"\u0012", ""};
        r3[16] = new String[]{"\u0013", ""};
        r3[17] = new String[]{"\u0014", ""};
        r3[18] = new String[]{"\u0015", ""};
        r3[19] = new String[]{"\u0016", ""};
        r3[20] = new String[]{"\u0017", ""};
        r3[21] = new String[]{"\u0018", ""};
        r3[22] = new String[]{"\u0019", ""};
        r3[23] = new String[]{"\u001a", ""};
        r3[24] = new String[]{"\u001b", ""};
        r3[25] = new String[]{"\u001c", ""};
        r3[26] = new String[]{"\u001d", ""};
        r3[27] = new String[]{"\u001e", ""};
        r3[28] = new String[]{"\u001f", ""};
        r3[29] = new String[]{"￾", ""};
        r3[30] = new String[]{"￿", ""};
        r1[2] = new LookupTranslator(r3);
        r1[3] = NumericEntityEscaper.between(TransportMediator.KEYCODE_MEDIA_PAUSE, 132);
        r1[4] = NumericEntityEscaper.between(134, 159);
        r1[5] = new UnicodeUnpairedSurrogateRemover();
        ESCAPE_XML10 = new AggregateTranslator(r1);
        r1 = new CharSequenceTranslator[8];
        r1[0] = new LookupTranslator(EntityArrays.BASIC_ESCAPE());
        r1[1] = new LookupTranslator(EntityArrays.APOS_ESCAPE());
        r3 = new String[5][];
        r3[0] = new String[]{"\u0000", ""};
        r3[1] = new String[]{"\u000b", "&#11;"};
        r3[2] = new String[]{"\f", "&#12;"};
        r3[3] = new String[]{"￾", ""};
        r3[4] = new String[]{"￿", ""};
        r1[2] = new LookupTranslator(r3);
        r1[3] = NumericEntityEscaper.between(1, 8);
        r1[4] = NumericEntityEscaper.between(14, 31);
        r1[5] = NumericEntityEscaper.between(TransportMediator.KEYCODE_MEDIA_PAUSE, 132);
        r1[6] = NumericEntityEscaper.between(134, 159);
        r1[7] = new UnicodeUnpairedSurrogateRemover();
        ESCAPE_XML11 = new AggregateTranslator(r1);
        r1 = new CharSequenceTranslator[4];
        r1[0] = new OctalUnescaper();
        r1[1] = new UnicodeUnescaper();
        r1[2] = new LookupTranslator(EntityArrays.JAVA_CTRL_CHARS_UNESCAPE());
        r3 = new String[4][];
        r3[0] = new String[]{"\\\\", "\\"};
        r3[1] = new String[]{"\\\"", "\""};
        r3[2] = new String[]{"\\'", "'"};
        r3[3] = new String[]{"\\", ""};
        r1[3] = new LookupTranslator(r3);
        UNESCAPE_JAVA = new AggregateTranslator(r1);
    }

    public static final String escapeJava(String str) {
        return ESCAPE_JAVA.translate(str);
    }

    public static final String escapeEcmaScript(String str) {
        return ESCAPE_ECMASCRIPT.translate(str);
    }

    public static final String escapeJson(String str) {
        return ESCAPE_JSON.translate(str);
    }

    public static final String unescapeJava(String str) {
        return UNESCAPE_JAVA.translate(str);
    }

    public static final String unescapeEcmaScript(String str) {
        return UNESCAPE_ECMASCRIPT.translate(str);
    }

    public static final String unescapeJson(String str) {
        return UNESCAPE_JSON.translate(str);
    }

    public static final String escapeHtml4(String str) {
        return ESCAPE_HTML4.translate(str);
    }

    public static final String escapeHtml3(String str) {
        return ESCAPE_HTML3.translate(str);
    }

    public static final String unescapeHtml4(String str) {
        return UNESCAPE_HTML4.translate(str);
    }

    public static final String unescapeHtml3(String str) {
        return UNESCAPE_HTML3.translate(str);
    }

    @Deprecated
    public static final String escapeXml(String str) {
        return ESCAPE_XML.translate(str);
    }

    public static String escapeXml10(String str) {
        return ESCAPE_XML10.translate(str);
    }

    public static String escapeXml11(String str) {
        return ESCAPE_XML11.translate(str);
    }

    public static final String unescapeXml(String str) {
        return UNESCAPE_XML.translate(str);
    }

    public static final String escapeCsv(String str) {
        return ESCAPE_CSV.translate(str);
    }

    public static final String unescapeCsv(String str) {
        return UNESCAPE_CSV.translate(str);
    }
}
