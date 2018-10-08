package com.google.android.gms.internal;

import java.io.IOException;

public final class zzegr {
    public static final String[] EMPTY_STRING_ARRAY = new String[0];
    private static int zznde = 11;
    private static int zzndf = 12;
    private static int zzndg = 16;
    private static int zzndh = 26;
    public static final int[] zzndi = new int[0];
    public static final long[] zzndj = new long[0];
    public static final float[] zzndk = new float[0];
    private static double[] zzndl = new double[0];
    public static final boolean[] zzndm = new boolean[0];
    public static final byte[][] zzndn = new byte[0][];
    public static final byte[] zzndo = new byte[0];

    public static final int zzb(zzegf zzegf, int i) throws IOException {
        int i2 = 1;
        int position = zzegf.getPosition();
        zzegf.zzgl(i);
        while (zzegf.zzcbr() == i) {
            zzegf.zzgl(i);
            i2++;
        }
        zzegf.zzaa(position, i);
        return i2;
    }
}
