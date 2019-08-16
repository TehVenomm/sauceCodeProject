package p018jp.colopl.util;

import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import java.nio.ByteBuffer;
import java.text.SimpleDateFormat;
import java.util.Date;
import p018jp.colopl.drapro.BuildConfig;

/* renamed from: jp.colopl.util.Util */
public class Util {
    public static byte[] asByteArray(String str) {
        byte[] bArr = new byte[(str.length() / 2)];
        for (int i = 0; i < bArr.length; i++) {
            bArr[i] = (byte) ((byte) Integer.parseInt(str.substring(i * 2, (i + 1) * 2), 16));
        }
        return bArr;
    }

    public static String asHex(byte[] bArr) {
        StringBuffer stringBuffer = new StringBuffer(bArr.length * 2);
        for (byte ubyte2int : bArr) {
            int ubyte2int2 = ubyte2int(ubyte2int);
            if (ubyte2int2 < 16) {
                stringBuffer.append(AppEventsConstants.EVENT_PARAM_VALUE_NO);
            }
            stringBuffer.append(Integer.toHexString(ubyte2int2));
        }
        return stringBuffer.toString().toUpperCase();
    }

    public static void dLog(String str, String str2) {
        if (BuildConfig.DEBUG) {
            Log.d(str, str2);
        }
    }

    public static void eLog(String str, String str2) {
        if (BuildConfig.DEBUG) {
            Log.e(str, str2);
        }
    }

    public static final String getDateTimeFormat(Date date, String str) {
        return new SimpleDateFormat(str).format(date);
    }

    public static String getDump(ByteBuffer byteBuffer) {
        StringBuffer stringBuffer = new StringBuffer();
        byteBuffer.position(0);
        while (byteBuffer.hasRemaining()) {
            StringBuffer stringBuffer2 = new StringBuffer(Integer.toHexString(ubyte2int(byteBuffer.get())));
            if (stringBuffer2.length() <= 1) {
                stringBuffer2.insert(0, AppEventsConstants.EVENT_PARAM_VALUE_NO);
            }
            stringBuffer.append(stringBuffer2);
        }
        return stringBuffer.toString().toUpperCase();
    }

    public static String getDump(byte[] bArr) {
        return getDump(ByteBuffer.wrap(bArr));
    }

    public static final int hexStr2int(String str) {
        if (str.length() < 2) {
            throw new NumberFormatException("Number of digits is less than 2.");
        } else if (str.length() % 2 != 1) {
            return Integer.valueOf(Integer.parseInt(str, 16)).intValue();
        } else {
            throw new NumberFormatException("Odd number of hex digits");
        }
    }

    public static int ubyte2int(byte b) {
        return b & 255;
    }
}
