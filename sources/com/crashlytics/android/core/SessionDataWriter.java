package com.crashlytics.android.core;

import android.app.ActivityManager.RunningAppProcessInfo;
import android.content.Context;
import android.os.Build.VERSION;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.common.ApiKey;
import io.fabric.sdk.android.services.common.IdManager.DeviceIdentifierType;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

class SessionDataWriter {
    private static final String SIGNAL_DEFAULT = "0";
    private static final ByteString SIGNAL_DEFAULT_BYTE_STRING = ByteString.copyFromUtf8("0");
    private final Context context;
    private StackTraceElement[] exceptionStack;
    private final int maxChainedExceptionsDepth = 8;
    private final ByteString optionalBuildIdBytes;
    private final ByteString packageNameBytes;
    private RunningAppProcessInfo runningAppProcessInfo;
    private List<StackTraceElement[]> stacks;
    private Thread[] threads;

    public SessionDataWriter(Context context, String str, String str2) {
        this.context = context;
        this.packageNameBytes = ByteString.copyFromUtf8(str2);
        this.optionalBuildIdBytes = str == null ? null : ByteString.copyFromUtf8(str.replace("-", ""));
    }

    private int getBinaryImageSize() {
        int computeUInt64Size = ((CodedOutputStream.computeUInt64Size(1, 0) + 0) + CodedOutputStream.computeUInt64Size(2, 0)) + CodedOutputStream.computeBytesSize(3, this.packageNameBytes);
        return this.optionalBuildIdBytes != null ? computeUInt64Size + CodedOutputStream.computeBytesSize(4, this.optionalBuildIdBytes) : computeUInt64Size;
    }

    private int getDeviceIdentifierSize(DeviceIdentifierType deviceIdentifierType, String str) {
        return CodedOutputStream.computeEnumSize(1, deviceIdentifierType.protobufIndex) + CodedOutputStream.computeBytesSize(2, ByteString.copyFromUtf8(str));
    }

    private int getEventAppCustomAttributeSize(String str, String str2) {
        int computeBytesSize = CodedOutputStream.computeBytesSize(1, ByteString.copyFromUtf8(str));
        if (str2 == null) {
            str2 = "";
        }
        return computeBytesSize + CodedOutputStream.computeBytesSize(2, ByteString.copyFromUtf8(str2));
    }

    private int getEventAppExecutionExceptionSize(Throwable th, int i) {
        int i2 = 0;
        int computeBytesSize = CodedOutputStream.computeBytesSize(1, ByteString.copyFromUtf8(th.getClass().getName())) + 0;
        String localizedMessage = th.getLocalizedMessage();
        if (localizedMessage != null) {
            computeBytesSize += CodedOutputStream.computeBytesSize(3, ByteString.copyFromUtf8(localizedMessage));
        }
        StackTraceElement[] stackTrace = th.getStackTrace();
        int length = stackTrace.length;
        int i3 = 0;
        while (i3 < length) {
            int frameSize = getFrameSize(stackTrace[i3], true);
            i3++;
            computeBytesSize = (frameSize + (CodedOutputStream.computeTagSize(4) + CodedOutputStream.computeRawVarint32Size(frameSize))) + computeBytesSize;
        }
        Throwable cause = th.getCause();
        if (cause == null) {
            return computeBytesSize;
        }
        if (i < this.maxChainedExceptionsDepth) {
            i2 = getEventAppExecutionExceptionSize(cause, i + 1);
            return computeBytesSize + (i2 + (CodedOutputStream.computeTagSize(6) + CodedOutputStream.computeRawVarint32Size(i2)));
        }
        while (cause != null) {
            cause = cause.getCause();
            i2++;
        }
        return computeBytesSize + CodedOutputStream.computeUInt32Size(7, i2);
    }

    private int getEventAppExecutionSignalSize() {
        return ((CodedOutputStream.computeBytesSize(1, SIGNAL_DEFAULT_BYTE_STRING) + 0) + CodedOutputStream.computeBytesSize(2, SIGNAL_DEFAULT_BYTE_STRING)) + CodedOutputStream.computeUInt64Size(3, 0);
    }

    private int getEventAppExecutionSize(Thread thread, Throwable th) {
        int i;
        int threadSize = getThreadSize(thread, this.exceptionStack, 4, true);
        threadSize = (threadSize + (CodedOutputStream.computeTagSize(1) + CodedOutputStream.computeRawVarint32Size(threadSize))) + 0;
        int length = this.threads.length;
        int i2 = threadSize;
        for (i = 0; i < length; i++) {
            threadSize = getThreadSize(this.threads[i], (StackTraceElement[]) this.stacks.get(i), 0, false);
            i2 += threadSize + (CodedOutputStream.computeTagSize(1) + CodedOutputStream.computeRawVarint32Size(threadSize));
        }
        threadSize = getEventAppExecutionExceptionSize(th, 1);
        i = CodedOutputStream.computeTagSize(2);
        int computeRawVarint32Size = CodedOutputStream.computeRawVarint32Size(threadSize);
        length = getEventAppExecutionSignalSize();
        int computeTagSize = CodedOutputStream.computeTagSize(3);
        int computeRawVarint32Size2 = CodedOutputStream.computeRawVarint32Size(length);
        int binaryImageSize = getBinaryImageSize();
        return (((threadSize + (i + computeRawVarint32Size)) + i2) + ((computeTagSize + computeRawVarint32Size2) + length)) + ((CodedOutputStream.computeTagSize(3) + CodedOutputStream.computeRawVarint32Size(binaryImageSize)) + binaryImageSize);
    }

    private int getEventAppSize(Thread thread, Throwable th, int i, Map<String, String> map) {
        int i2;
        int eventAppExecutionSize = getEventAppExecutionSize(thread, th);
        eventAppExecutionSize = (eventAppExecutionSize + (CodedOutputStream.computeTagSize(1) + CodedOutputStream.computeRawVarint32Size(eventAppExecutionSize))) + 0;
        if (map != null) {
            i2 = eventAppExecutionSize;
            for (Entry entry : map.entrySet()) {
                eventAppExecutionSize = getEventAppCustomAttributeSize((String) entry.getKey(), (String) entry.getValue());
                i2 = (eventAppExecutionSize + (CodedOutputStream.computeTagSize(2) + CodedOutputStream.computeRawVarint32Size(eventAppExecutionSize))) + i2;
            }
        } else {
            i2 = eventAppExecutionSize;
        }
        if (this.runningAppProcessInfo != null) {
            i2 += CodedOutputStream.computeBoolSize(3, this.runningAppProcessInfo.importance != 100);
        }
        return CodedOutputStream.computeUInt32Size(4, i) + i2;
    }

    private int getEventDeviceSize(float f, int i, boolean z, int i2, long j, long j2) {
        return (((((CodedOutputStream.computeFloatSize(1, f) + 0) + CodedOutputStream.computeSInt32Size(2, i)) + CodedOutputStream.computeBoolSize(3, z)) + CodedOutputStream.computeUInt32Size(4, i2)) + CodedOutputStream.computeUInt64Size(5, j)) + CodedOutputStream.computeUInt64Size(6, j2);
    }

    private int getEventLogSize(ByteString byteString) {
        return CodedOutputStream.computeBytesSize(1, byteString);
    }

    private int getFrameSize(StackTraceElement stackTraceElement, boolean z) {
        int computeUInt64Size = (stackTraceElement.isNativeMethod() ? CodedOutputStream.computeUInt64Size(1, (long) Math.max(stackTraceElement.getLineNumber(), 0)) + 0 : CodedOutputStream.computeUInt64Size(1, 0) + 0) + CodedOutputStream.computeBytesSize(2, ByteString.copyFromUtf8(stackTraceElement.getClassName() + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + stackTraceElement.getMethodName()));
        if (stackTraceElement.getFileName() != null) {
            computeUInt64Size += CodedOutputStream.computeBytesSize(3, ByteString.copyFromUtf8(stackTraceElement.getFileName()));
        }
        int computeUInt64Size2 = (stackTraceElement.isNativeMethod() || stackTraceElement.getLineNumber() <= 0) ? computeUInt64Size : computeUInt64Size + CodedOutputStream.computeUInt64Size(4, (long) stackTraceElement.getLineNumber());
        return CodedOutputStream.computeUInt32Size(5, z ? 2 : 0) + computeUInt64Size2;
    }

    private int getSessionAppOrgSize() {
        return CodedOutputStream.computeBytesSize(1, ByteString.copyFromUtf8(new ApiKey().getValue(this.context))) + 0;
    }

    private int getSessionAppSize(ByteString byteString, ByteString byteString2, ByteString byteString3, ByteString byteString4, int i) {
        int computeBytesSize = CodedOutputStream.computeBytesSize(1, byteString);
        int computeBytesSize2 = CodedOutputStream.computeBytesSize(2, byteString2);
        int computeBytesSize3 = CodedOutputStream.computeBytesSize(3, byteString3);
        int sessionAppOrgSize = getSessionAppOrgSize();
        return (((((computeBytesSize + 0) + computeBytesSize2) + computeBytesSize3) + ((CodedOutputStream.computeTagSize(5) + CodedOutputStream.computeRawVarint32Size(sessionAppOrgSize)) + sessionAppOrgSize)) + CodedOutputStream.computeBytesSize(6, byteString4)) + CodedOutputStream.computeEnumSize(10, i);
    }

    private int getSessionDeviceSize(int i, ByteString byteString, ByteString byteString2, int i2, long j, long j2, boolean z, Map<DeviceIdentifierType, String> map, int i3, ByteString byteString3, ByteString byteString4) {
        int i4;
        int computeBytesSize = (((((byteString2 == null ? 0 : CodedOutputStream.computeBytesSize(4, byteString2)) + ((CodedOutputStream.computeBytesSize(1, byteString) + 0) + CodedOutputStream.computeEnumSize(3, i))) + CodedOutputStream.computeUInt32Size(5, i2)) + CodedOutputStream.computeUInt64Size(6, j)) + CodedOutputStream.computeUInt64Size(7, j2)) + CodedOutputStream.computeBoolSize(10, z);
        if (map != null) {
            i4 = computeBytesSize;
            for (Entry entry : map.entrySet()) {
                computeBytesSize = getDeviceIdentifierSize((DeviceIdentifierType) entry.getKey(), (String) entry.getValue());
                i4 = (computeBytesSize + (CodedOutputStream.computeTagSize(11) + CodedOutputStream.computeRawVarint32Size(computeBytesSize))) + i4;
            }
        } else {
            i4 = computeBytesSize;
        }
        int computeUInt32Size = CodedOutputStream.computeUInt32Size(12, i3);
        return (byteString4 == null ? 0 : CodedOutputStream.computeBytesSize(14, byteString4)) + ((byteString3 == null ? 0 : CodedOutputStream.computeBytesSize(13, byteString3)) + (i4 + computeUInt32Size));
    }

    private int getSessionEventSize(Thread thread, Throwable th, String str, long j, Map<String, String> map, float f, int i, boolean z, int i2, long j2, long j3, ByteString byteString) {
        int computeUInt64Size = CodedOutputStream.computeUInt64Size(1, j);
        int computeBytesSize = CodedOutputStream.computeBytesSize(2, ByteString.copyFromUtf8(str));
        int eventAppSize = getEventAppSize(thread, th, i2, map);
        int computeTagSize = CodedOutputStream.computeTagSize(3);
        int computeRawVarint32Size = CodedOutputStream.computeRawVarint32Size(eventAppSize);
        int eventDeviceSize = getEventDeviceSize(f, i, z, i2, j2, j3);
        computeUInt64Size = (((computeUInt64Size + 0) + computeBytesSize) + ((computeTagSize + computeRawVarint32Size) + eventAppSize)) + (eventDeviceSize + (CodedOutputStream.computeTagSize(5) + CodedOutputStream.computeRawVarint32Size(eventDeviceSize)));
        if (byteString == null) {
            return computeUInt64Size;
        }
        eventDeviceSize = getEventLogSize(byteString);
        return computeUInt64Size + (eventDeviceSize + (CodedOutputStream.computeTagSize(6) + CodedOutputStream.computeRawVarint32Size(eventDeviceSize)));
    }

    private int getSessionOSSize(ByteString byteString, ByteString byteString2, boolean z) {
        return (((CodedOutputStream.computeEnumSize(1, 3) + 0) + CodedOutputStream.computeBytesSize(2, byteString)) + CodedOutputStream.computeBytesSize(3, byteString2)) + CodedOutputStream.computeBoolSize(4, z);
    }

    private int getThreadSize(Thread thread, StackTraceElement[] stackTraceElementArr, int i, boolean z) {
        int computeUInt32Size = CodedOutputStream.computeUInt32Size(2, i) + CodedOutputStream.computeBytesSize(1, ByteString.copyFromUtf8(thread.getName()));
        for (StackTraceElement frameSize : stackTraceElementArr) {
            int frameSize2 = getFrameSize(frameSize, z);
            computeUInt32Size += frameSize2 + (CodedOutputStream.computeTagSize(3) + CodedOutputStream.computeRawVarint32Size(frameSize2));
        }
        return computeUInt32Size;
    }

    private ByteString stringToByteString(String str) {
        return str == null ? null : ByteString.copyFromUtf8(str);
    }

    private void writeFrame(CodedOutputStream codedOutputStream, int i, StackTraceElement stackTraceElement, boolean z) throws Exception {
        int i2 = 4;
        codedOutputStream.writeTag(i, 2);
        codedOutputStream.writeRawVarint32(getFrameSize(stackTraceElement, z));
        if (stackTraceElement.isNativeMethod()) {
            codedOutputStream.writeUInt64(1, (long) Math.max(stackTraceElement.getLineNumber(), 0));
        } else {
            codedOutputStream.writeUInt64(1, 0);
        }
        codedOutputStream.writeBytes(2, ByteString.copyFromUtf8(stackTraceElement.getClassName() + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + stackTraceElement.getMethodName()));
        if (stackTraceElement.getFileName() != null) {
            codedOutputStream.writeBytes(3, ByteString.copyFromUtf8(stackTraceElement.getFileName()));
        }
        if (!stackTraceElement.isNativeMethod() && stackTraceElement.getLineNumber() > 0) {
            codedOutputStream.writeUInt64(4, (long) stackTraceElement.getLineNumber());
        }
        if (!z) {
            i2 = 0;
        }
        codedOutputStream.writeUInt32(5, i2);
    }

    private void writeSessionEventApp(CodedOutputStream codedOutputStream, Thread thread, Throwable th, int i, Map<String, String> map) throws Exception {
        codedOutputStream.writeTag(3, 2);
        codedOutputStream.writeRawVarint32(getEventAppSize(thread, th, i, map));
        writeSessionEventAppExecution(codedOutputStream, thread, th);
        if (!(map == null || map.isEmpty())) {
            writeSessionEventAppCustomAttributes(codedOutputStream, map);
        }
        if (this.runningAppProcessInfo != null) {
            codedOutputStream.writeBool(3, this.runningAppProcessInfo.importance != 100);
        }
        codedOutputStream.writeUInt32(4, i);
    }

    private void writeSessionEventAppCustomAttributes(CodedOutputStream codedOutputStream, Map<String, String> map) throws Exception {
        for (Entry entry : map.entrySet()) {
            codedOutputStream.writeTag(2, 2);
            codedOutputStream.writeRawVarint32(getEventAppCustomAttributeSize((String) entry.getKey(), (String) entry.getValue()));
            codedOutputStream.writeBytes(1, ByteString.copyFromUtf8((String) entry.getKey()));
            String str = (String) entry.getValue();
            if (str == null) {
                str = "";
            }
            codedOutputStream.writeBytes(2, ByteString.copyFromUtf8(str));
        }
    }

    private void writeSessionEventAppExecution(CodedOutputStream codedOutputStream, Thread thread, Throwable th) throws Exception {
        codedOutputStream.writeTag(1, 2);
        codedOutputStream.writeRawVarint32(getEventAppExecutionSize(thread, th));
        writeThread(codedOutputStream, thread, this.exceptionStack, 4, true);
        int length = this.threads.length;
        for (int i = 0; i < length; i++) {
            writeThread(codedOutputStream, this.threads[i], (StackTraceElement[]) this.stacks.get(i), 0, false);
        }
        writeSessionEventAppExecutionException(codedOutputStream, th, 1, 2);
        codedOutputStream.writeTag(3, 2);
        codedOutputStream.writeRawVarint32(getEventAppExecutionSignalSize());
        codedOutputStream.writeBytes(1, SIGNAL_DEFAULT_BYTE_STRING);
        codedOutputStream.writeBytes(2, SIGNAL_DEFAULT_BYTE_STRING);
        codedOutputStream.writeUInt64(3, 0);
        codedOutputStream.writeTag(4, 2);
        codedOutputStream.writeRawVarint32(getBinaryImageSize());
        codedOutputStream.writeUInt64(1, 0);
        codedOutputStream.writeUInt64(2, 0);
        codedOutputStream.writeBytes(3, this.packageNameBytes);
        if (this.optionalBuildIdBytes != null) {
            codedOutputStream.writeBytes(4, this.optionalBuildIdBytes);
        }
    }

    private void writeSessionEventAppExecutionException(CodedOutputStream codedOutputStream, Throwable th, int i, int i2) throws Exception {
        int i3 = 0;
        codedOutputStream.writeTag(i2, 2);
        codedOutputStream.writeRawVarint32(getEventAppExecutionExceptionSize(th, 1));
        codedOutputStream.writeBytes(1, ByteString.copyFromUtf8(th.getClass().getName()));
        String localizedMessage = th.getLocalizedMessage();
        if (localizedMessage != null) {
            codedOutputStream.writeBytes(3, ByteString.copyFromUtf8(localizedMessage));
        }
        for (StackTraceElement writeFrame : th.getStackTrace()) {
            writeFrame(codedOutputStream, 4, writeFrame, true);
        }
        Throwable cause = th.getCause();
        if (cause == null) {
            return;
        }
        if (i < this.maxChainedExceptionsDepth) {
            writeSessionEventAppExecutionException(codedOutputStream, cause, i + 1, 6);
            return;
        }
        while (cause != null) {
            cause = cause.getCause();
            i3++;
        }
        codedOutputStream.writeUInt32(7, i3);
    }

    private void writeSessionEventDevice(CodedOutputStream codedOutputStream, float f, int i, boolean z, int i2, long j, long j2) throws Exception {
        codedOutputStream.writeTag(5, 2);
        codedOutputStream.writeRawVarint32(getEventDeviceSize(f, i, z, i2, j, j2));
        codedOutputStream.writeFloat(1, f);
        codedOutputStream.writeSInt32(2, i);
        codedOutputStream.writeBool(3, z);
        codedOutputStream.writeUInt32(4, i2);
        codedOutputStream.writeUInt64(5, j);
        codedOutputStream.writeUInt64(6, j2);
    }

    private void writeSessionEventLog(CodedOutputStream codedOutputStream, ByteString byteString) throws Exception {
        if (byteString != null) {
            codedOutputStream.writeTag(6, 2);
            codedOutputStream.writeRawVarint32(getEventLogSize(byteString));
            codedOutputStream.writeBytes(1, byteString);
        }
    }

    private void writeThread(CodedOutputStream codedOutputStream, Thread thread, StackTraceElement[] stackTraceElementArr, int i, boolean z) throws Exception {
        codedOutputStream.writeTag(1, 2);
        codedOutputStream.writeRawVarint32(getThreadSize(thread, stackTraceElementArr, i, z));
        codedOutputStream.writeBytes(1, ByteString.copyFromUtf8(thread.getName()));
        codedOutputStream.writeUInt32(2, i);
        for (StackTraceElement writeFrame : stackTraceElementArr) {
            writeFrame(codedOutputStream, 3, writeFrame, z);
        }
    }

    public void writeBeginSession(CodedOutputStream codedOutputStream, String str, String str2, long j) throws Exception {
        codedOutputStream.writeBytes(1, ByteString.copyFromUtf8(str2));
        codedOutputStream.writeBytes(2, ByteString.copyFromUtf8(str));
        codedOutputStream.writeUInt64(3, j);
    }

    public void writeSessionApp(CodedOutputStream codedOutputStream, String str, String str2, String str3, String str4, int i) throws Exception {
        ByteString copyFromUtf8 = ByteString.copyFromUtf8(str);
        ByteString copyFromUtf82 = ByteString.copyFromUtf8(str2);
        ByteString copyFromUtf83 = ByteString.copyFromUtf8(str3);
        ByteString copyFromUtf84 = ByteString.copyFromUtf8(str4);
        codedOutputStream.writeTag(7, 2);
        codedOutputStream.writeRawVarint32(getSessionAppSize(copyFromUtf8, copyFromUtf82, copyFromUtf83, copyFromUtf84, i));
        codedOutputStream.writeBytes(1, copyFromUtf8);
        codedOutputStream.writeBytes(2, copyFromUtf82);
        codedOutputStream.writeBytes(3, copyFromUtf83);
        codedOutputStream.writeTag(5, 2);
        codedOutputStream.writeRawVarint32(getSessionAppOrgSize());
        codedOutputStream.writeString(1, new ApiKey().getValue(this.context));
        codedOutputStream.writeBytes(6, copyFromUtf84);
        codedOutputStream.writeEnum(10, i);
    }

    public void writeSessionDevice(CodedOutputStream codedOutputStream, String str, int i, String str2, int i2, long j, long j2, boolean z, Map<DeviceIdentifierType, String> map, int i3, String str3, String str4) throws Exception {
        ByteString copyFromUtf8 = ByteString.copyFromUtf8(str);
        ByteString stringToByteString = stringToByteString(str2);
        ByteString stringToByteString2 = stringToByteString(str4);
        ByteString stringToByteString3 = stringToByteString(str3);
        codedOutputStream.writeTag(9, 2);
        codedOutputStream.writeRawVarint32(getSessionDeviceSize(i, copyFromUtf8, stringToByteString, i2, j, j2, z, map, i3, stringToByteString3, stringToByteString2));
        codedOutputStream.writeBytes(1, copyFromUtf8);
        codedOutputStream.writeEnum(3, i);
        codedOutputStream.writeBytes(4, stringToByteString);
        codedOutputStream.writeUInt32(5, i2);
        codedOutputStream.writeUInt64(6, j);
        codedOutputStream.writeUInt64(7, j2);
        codedOutputStream.writeBool(10, z);
        for (Entry entry : map.entrySet()) {
            codedOutputStream.writeTag(11, 2);
            codedOutputStream.writeRawVarint32(getDeviceIdentifierSize((DeviceIdentifierType) entry.getKey(), (String) entry.getValue()));
            codedOutputStream.writeEnum(1, ((DeviceIdentifierType) entry.getKey()).protobufIndex);
            codedOutputStream.writeBytes(2, ByteString.copyFromUtf8((String) entry.getValue()));
        }
        codedOutputStream.writeUInt32(12, i3);
        if (stringToByteString3 != null) {
            codedOutputStream.writeBytes(13, stringToByteString3);
        }
        if (stringToByteString2 != null) {
            codedOutputStream.writeBytes(14, stringToByteString2);
        }
    }

    public void writeSessionEvent(CodedOutputStream codedOutputStream, long j, Thread thread, Throwable th, String str, Thread[] threadArr, float f, int i, boolean z, int i2, long j2, long j3, RunningAppProcessInfo runningAppProcessInfo, List<StackTraceElement[]> list, StackTraceElement[] stackTraceElementArr, LogFileManager logFileManager, Map<String, String> map) throws Exception {
        this.runningAppProcessInfo = runningAppProcessInfo;
        this.stacks = list;
        this.exceptionStack = stackTraceElementArr;
        this.threads = threadArr;
        ByteString byteStringForLog = logFileManager.getByteStringForLog();
        if (byteStringForLog == null) {
            Fabric.getLogger().mo4289d("Fabric", "No log data to include with this event.");
        }
        logFileManager.clearLog();
        codedOutputStream.writeTag(10, 2);
        codedOutputStream.writeRawVarint32(getSessionEventSize(thread, th, str, j, map, f, i, z, i2, j2, j3, byteStringForLog));
        codedOutputStream.writeUInt64(1, j);
        codedOutputStream.writeBytes(2, ByteString.copyFromUtf8(str));
        writeSessionEventApp(codedOutputStream, thread, th, i2, map);
        writeSessionEventDevice(codedOutputStream, f, i, z, i2, j2, j3);
        writeSessionEventLog(codedOutputStream, byteStringForLog);
    }

    public void writeSessionOS(CodedOutputStream codedOutputStream, boolean z) throws Exception {
        ByteString copyFromUtf8 = ByteString.copyFromUtf8(VERSION.RELEASE);
        ByteString copyFromUtf82 = ByteString.copyFromUtf8(VERSION.CODENAME);
        codedOutputStream.writeTag(8, 2);
        codedOutputStream.writeRawVarint32(getSessionOSSize(copyFromUtf8, copyFromUtf82, z));
        codedOutputStream.writeEnum(1, 3);
        codedOutputStream.writeBytes(2, copyFromUtf8);
        codedOutputStream.writeBytes(3, copyFromUtf82);
        codedOutputStream.writeBool(4, z);
    }

    public void writeSessionUser(CodedOutputStream codedOutputStream, String str, String str2, String str3) throws Exception {
        if (str == null) {
            str = "";
        }
        ByteString copyFromUtf8 = ByteString.copyFromUtf8(str);
        ByteString stringToByteString = stringToByteString(str2);
        ByteString stringToByteString2 = stringToByteString(str3);
        int computeBytesSize = CodedOutputStream.computeBytesSize(1, copyFromUtf8) + 0;
        if (str2 != null) {
            computeBytesSize += CodedOutputStream.computeBytesSize(2, stringToByteString);
        }
        if (str3 != null) {
            computeBytesSize += CodedOutputStream.computeBytesSize(3, stringToByteString2);
        }
        codedOutputStream.writeTag(6, 2);
        codedOutputStream.writeRawVarint32(computeBytesSize);
        codedOutputStream.writeBytes(1, copyFromUtf8);
        if (str2 != null) {
            codedOutputStream.writeBytes(2, stringToByteString);
        }
        if (str3 != null) {
            codedOutputStream.writeBytes(3, stringToByteString2);
        }
    }
}
