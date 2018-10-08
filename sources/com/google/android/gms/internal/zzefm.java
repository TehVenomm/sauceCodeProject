package com.google.android.gms.internal;

import android.support.v4.view.MotionEventCompat;

final class zzefm {
    static String zzai(zzedk zzedk) {
        zzefo zzefn = new zzefn(zzedk);
        StringBuilder stringBuilder = new StringBuilder(zzefn.size());
        for (int i = 0; i < zzefn.size(); i++) {
            byte zzgi = zzefn.zzgi(i);
            switch (zzgi) {
                case (byte) 7:
                    stringBuilder.append("\\a");
                    break;
                case (byte) 8:
                    stringBuilder.append("\\b");
                    break;
                case (byte) 9:
                    stringBuilder.append("\\t");
                    break;
                case (byte) 10:
                    stringBuilder.append("\\n");
                    break;
                case (byte) 11:
                    stringBuilder.append("\\v");
                    break;
                case (byte) 12:
                    stringBuilder.append("\\f");
                    break;
                case (byte) 13:
                    stringBuilder.append("\\r");
                    break;
                case MotionEventCompat.AXIS_GENERIC_3 /*34*/:
                    stringBuilder.append("\\\"");
                    break;
                case MotionEventCompat.AXIS_GENERIC_8 /*39*/:
                    stringBuilder.append("\\'");
                    break;
                case (byte) 92:
                    stringBuilder.append("\\\\");
                    break;
                default:
                    if (zzgi >= (byte) 32 && zzgi <= (byte) 126) {
                        stringBuilder.append((char) zzgi);
                        break;
                    }
                    stringBuilder.append('\\');
                    stringBuilder.append((char) (((zzgi >>> 6) & 3) + 48));
                    stringBuilder.append((char) (((zzgi >>> 3) & 7) + 48));
                    stringBuilder.append((char) ((zzgi & 7) + 48));
                    break;
            }
        }
        return stringBuilder.toString();
    }
}
