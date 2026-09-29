.class public interface abstract Lcom/helpshift/specifications/SyncSpecification;
.super Ljava/lang/Object;
.source "SyncSpecification.java"


# static fields
.field public static final APP_SESSIONS_DATA_CHANGE_THRESHOLD:I = 0x1

.field public static final DAILY_SYNC_FREQUENCY:I = 0x4

.field public static final DECAY_SEED_VALUE:I = 0x5

.field public static final USER_PROPERTIES_DATA_CHANGE_THRESHOLD:I = 0x1


# virtual methods
.method public abstract getDataType()Ljava/lang/String;
.end method

.method public abstract isSatisfied(IJ)Z
.end method
