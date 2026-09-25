.class public interface abstract Lcom/helpshift/common/platform/Device;
.super Ljava/lang/Object;
.source "Device.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/common/platform/Device$PermissionType;,
        Lcom/helpshift/common/platform/Device$PermissionState;
    }
.end annotation


# virtual methods
.method public abstract changeLocale(Ljava/util/Locale;)V
.end method

.method public abstract checkPermission(Lcom/helpshift/common/platform/Device$PermissionType;)Lcom/helpshift/common/platform/Device$PermissionState;
.end method

.method public abstract getAndroidId()Ljava/lang/String;
.end method

.method public abstract getApiVersion()Ljava/lang/String;
.end method

.method public abstract getAppIdentifier()Ljava/lang/String;
.end method

.method public abstract getAppName()Ljava/lang/String;
.end method

.method public abstract getAppVersion()Ljava/lang/String;
.end method

.method public abstract getBatteryLevel()Ljava/lang/String;
.end method

.method public abstract getBatteryStatus()Ljava/lang/String;
.end method

.method public abstract getCarrierName()Ljava/lang/String;
.end method

.method public abstract getDeviceId()Ljava/lang/String;
.end method

.method public abstract getDeviceModel()Ljava/lang/String;
.end method

.method public abstract getDiskSpace()Lcom/helpshift/meta/dto/DeviceDiskSpaceDTO;
.end method

.method public abstract getLanguage()Ljava/lang/String;
.end method

.method public abstract getLocale()Ljava/util/Locale;
.end method

.method public abstract getNetworkType()Ljava/lang/String;
.end method

.method public abstract getOSVersion()Ljava/lang/String;
.end method

.method public abstract getOSVersionNumber()I
.end method

.method public abstract getPlatformName()Ljava/lang/String;
.end method

.method public abstract getPushToken()Ljava/lang/String;
.end method

.method public abstract getRom()Ljava/lang/String;
.end method

.method public abstract getSDKVersion()Ljava/lang/String;
.end method

.method public abstract getSimCountryIso()Ljava/lang/String;
.end method

.method public abstract getTimeStamp()Ljava/lang/String;
.end method

.method public abstract getTimeZoneId()Ljava/lang/String;
.end method

.method public abstract getTimeZoneOffSet()J
.end method

.method public abstract is24HourFormat()Z
.end method

.method public abstract setPushToken(Ljava/lang/String;)V
.end method
