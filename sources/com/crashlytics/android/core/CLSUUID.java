package com.crashlytics.android.core;

import android.os.Process;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.common.IdManager;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.util.Date;
import java.util.Locale;
import java.util.concurrent.atomic.AtomicLong;

class CLSUUID {
    private static String _clsId;
    private static final AtomicLong _sequenceNumber = new AtomicLong(0);

    public CLSUUID(IdManager idManager) {
        r0 = new byte[10];
        populateTime(r0);
        populateSequenceNumber(r0);
        populatePID(r0);
        String sha1 = CommonUtils.sha1(idManager.getAppInstallIdentifier());
        String hexify = CommonUtils.hexify(r0);
        _clsId = String.format(Locale.US, "%s-%s-%s-%s", new Object[]{hexify.substring(0, 12), hexify.substring(12, 16), hexify.subSequence(16, 20), sha1.substring(0, 12)}).toUpperCase(Locale.US);
    }

    private static byte[] convertLongToFourByteBuffer(long j) {
        ByteBuffer allocate = ByteBuffer.allocate(4);
        allocate.putInt((int) j);
        allocate.order(ByteOrder.BIG_ENDIAN);
        allocate.position(0);
        return allocate.array();
    }

    private static byte[] convertLongToTwoByteBuffer(long j) {
        ByteBuffer allocate = ByteBuffer.allocate(2);
        allocate.putShort((short) ((int) j));
        allocate.order(ByteOrder.BIG_ENDIAN);
        allocate.position(0);
        return allocate.array();
    }

    private void populatePID(byte[] bArr) {
        byte[] convertLongToTwoByteBuffer = convertLongToTwoByteBuffer((long) Integer.valueOf(Process.myPid()).shortValue());
        bArr[8] = (byte) convertLongToTwoByteBuffer[0];
        bArr[9] = (byte) convertLongToTwoByteBuffer[1];
    }

    private void populateSequenceNumber(byte[] bArr) {
        byte[] convertLongToTwoByteBuffer = convertLongToTwoByteBuffer(_sequenceNumber.incrementAndGet());
        bArr[6] = (byte) convertLongToTwoByteBuffer[0];
        bArr[7] = (byte) convertLongToTwoByteBuffer[1];
    }

    private void populateTime(byte[] bArr) {
        long time = new Date().getTime();
        byte[] convertLongToFourByteBuffer = convertLongToFourByteBuffer(time / 1000);
        bArr[0] = (byte) convertLongToFourByteBuffer[0];
        bArr[1] = (byte) convertLongToFourByteBuffer[1];
        bArr[2] = (byte) convertLongToFourByteBuffer[2];
        bArr[3] = (byte) convertLongToFourByteBuffer[3];
        byte[] convertLongToTwoByteBuffer = convertLongToTwoByteBuffer(time % 1000);
        bArr[4] = (byte) convertLongToTwoByteBuffer[0];
        bArr[5] = (byte) convertLongToTwoByteBuffer[1];
    }

    public String toString() {
        return _clsId;
    }
}
