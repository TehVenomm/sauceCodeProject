.class Lcom/crashlytics/android/core/SessionDataWriter;
.super Ljava/lang/Object;
.source "SessionDataWriter.java"


# static fields
.field private static final SIGNAL_DEFAULT:Ljava/lang/String; = "0"

.field private static final SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;


# instance fields
.field private final context:Landroid/content/Context;

.field private exceptionStack:[Ljava/lang/StackTraceElement;

.field private final maxChainedExceptionsDepth:I

.field private final optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

.field private final packageNameBytes:Lcom/crashlytics/android/core/ByteString;

.field private runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

.field private stacks:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "[",
            "Ljava/lang/StackTraceElement;",
            ">;"
        }
    .end annotation
.end field

.field private threads:[Ljava/lang/Thread;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "0"

    .line 22
    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    sput-object v0, Lcom/crashlytics/android/core/SessionDataWriter;->SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;

    return-void
.end method

.method public constructor <init>(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/16 v0, 0x8

    .line 45
    iput v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->maxChainedExceptionsDepth:I

    .line 48
    iput-object p1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->context:Landroid/content/Context;

    .line 49
    invoke-static {p3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p1

    iput-object p1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->packageNameBytes:Lcom/crashlytics/android/core/ByteString;

    if-nez p2, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    const-string p1, "-"

    const-string p3, ""

    .line 50
    invoke-virtual {p2, p1, p3}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p1

    :goto_0
    iput-object p1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

    return-void
.end method

.method private getBinaryImageSize()I
    .locals 4

    const-wide/16 v0, 0x0

    const/4 v2, 0x1

    .line 454
    invoke-static {v2, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v2

    add-int/lit8 v2, v2, 0x0

    const/4 v3, 0x2

    .line 455
    invoke-static {v3, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v0

    add-int/2addr v2, v0

    .line 456
    iget-object v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->packageNameBytes:Lcom/crashlytics/android/core/ByteString;

    const/4 v1, 0x3

    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    add-int/2addr v2, v0

    .line 457
    iget-object v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

    if-eqz v0, :cond_0

    const/4 v0, 0x4

    .line 458
    iget-object v1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

    invoke-static {v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    add-int/2addr v2, v0

    :cond_0
    return v2
.end method

.method private getDeviceIdentifierSize(Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;Ljava/lang/String;)I
    .locals 1

    .line 417
    iget p1, p1, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;->protobufIndex:I

    const/4 v0, 0x1

    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeEnumSize(II)I

    move-result p1

    .line 418
    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    const/4 v0, 0x2

    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    add-int/2addr p1, p2

    return p1
.end method

.method private getEventAppCustomAttributeSize(Ljava/lang/String;Ljava/lang/String;)I
    .locals 1

    .line 549
    invoke-static {p1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p1

    const/4 v0, 0x1

    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    if-nez p2, :cond_0

    const-string p2, ""

    .line 550
    :cond_0
    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    const/4 v0, 0x2

    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    add-int/2addr p1, p2

    return p1
.end method

.method private getEventAppExecutionExceptionSize(Ljava/lang/Throwable;I)I
    .locals 9

    .line 580
    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    const/4 v1, 0x1

    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    const/4 v2, 0x0

    add-int/2addr v0, v2

    .line 584
    invoke-virtual {p1}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v3

    if-eqz v3, :cond_0

    const/4 v4, 0x3

    .line 586
    invoke-static {v3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v3

    invoke-static {v4, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v3

    add-int/2addr v0, v3

    .line 589
    :cond_0
    invoke-virtual {p1}, Ljava/lang/Throwable;->getStackTrace()[Ljava/lang/StackTraceElement;

    move-result-object v3

    array-length v4, v3

    move v5, v0

    const/4 v0, 0x0

    :goto_0
    if-ge v0, v4, :cond_1

    aget-object v6, v3, v0

    .line 590
    invoke-direct {p0, v6, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->getFrameSize(Ljava/lang/StackTraceElement;Z)I

    move-result v6

    const/4 v7, 0x4

    .line 591
    invoke-static {v7}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v7

    invoke-static {v6}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v8

    add-int/2addr v7, v8

    add-int/2addr v7, v6

    add-int/2addr v5, v7

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 597
    :cond_1
    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p1

    if-eqz p1, :cond_4

    .line 599
    iget v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->maxChainedExceptionsDepth:I

    if-ge p2, v0, :cond_2

    add-int/2addr p2, v1

    .line 600
    invoke-direct {p0, p1, p2}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionExceptionSize(Ljava/lang/Throwable;I)I

    move-result p1

    const/4 p2, 0x6

    .line 602
    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result p2

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v0

    add-int/2addr p2, v0

    add-int/2addr p2, p1

    add-int/2addr v5, p2

    goto :goto_2

    :cond_2
    :goto_1
    if-eqz p1, :cond_3

    .line 609
    invoke-virtual {p1}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p1

    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    :cond_3
    const/4 p1, 0x7

    .line 613
    invoke-static {p1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p1

    add-int/2addr v5, p1

    :cond_4
    :goto_2
    return v5
.end method

.method private getEventAppExecutionSignalSize()I
    .locals 4

    .line 623
    sget-object v0, Lcom/crashlytics/android/core/SessionDataWriter;->SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;

    const/4 v1, 0x1

    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    add-int/lit8 v0, v0, 0x0

    .line 624
    sget-object v1, Lcom/crashlytics/android/core/SessionDataWriter;->SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;

    const/4 v2, 0x2

    invoke-static {v2, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v1

    add-int/2addr v0, v1

    const/4 v1, 0x3

    const-wide/16 v2, 0x0

    .line 625
    invoke-static {v1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v1

    add-int/2addr v0, v1

    return v0
.end method

.method private getEventAppExecutionSize(Ljava/lang/Thread;Ljava/lang/Throwable;)I
    .locals 7

    .line 521
    iget-object v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->exceptionStack:[Ljava/lang/StackTraceElement;

    const/4 v1, 0x1

    const/4 v2, 0x4

    invoke-direct {p0, p1, v0, v2, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->getThreadSize(Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)I

    move-result p1

    .line 522
    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v0

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v2

    add-int/2addr v0, v2

    add-int/2addr v0, p1

    const/4 p1, 0x0

    add-int/2addr v0, p1

    .line 525
    iget-object v2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->threads:[Ljava/lang/Thread;

    array-length v2, v2

    move v3, v0

    const/4 v0, 0x0

    :goto_0
    if-ge v0, v2, :cond_0

    .line 527
    iget-object v4, p0, Lcom/crashlytics/android/core/SessionDataWriter;->threads:[Ljava/lang/Thread;

    aget-object v4, v4, v0

    .line 528
    iget-object v5, p0, Lcom/crashlytics/android/core/SessionDataWriter;->stacks:Ljava/util/List;

    invoke-interface {v5, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, [Ljava/lang/StackTraceElement;

    invoke-direct {p0, v4, v5, p1, p1}, Lcom/crashlytics/android/core/SessionDataWriter;->getThreadSize(Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)I

    move-result v4

    .line 529
    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v5

    invoke-static {v4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v6

    add-int/2addr v5, v6

    add-int/2addr v5, v4

    add-int/2addr v3, v5

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 533
    :cond_0
    invoke-direct {p0, p2, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionExceptionSize(Ljava/lang/Throwable;I)I

    move-result p1

    const/4 p2, 0x2

    .line 534
    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result p2

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v0

    add-int/2addr p2, v0

    add-int/2addr p2, p1

    add-int/2addr v3, p2

    .line 537
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionSignalSize()I

    move-result p1

    const/4 p2, 0x3

    .line 538
    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v0

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v1

    add-int/2addr v0, v1

    add-int/2addr v0, p1

    add-int/2addr v3, v0

    .line 541
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getBinaryImageSize()I

    move-result p1

    .line 542
    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result p2

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v0

    add-int/2addr p2, v0

    add-int/2addr p2, p1

    add-int/2addr v3, p2

    return v3
.end method

.method private getEventAppSize(Ljava/lang/Thread;Ljava/lang/Throwable;ILjava/util/Map;)I
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Thread;",
            "Ljava/lang/Throwable;",
            "I",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)I"
        }
    .end annotation

    .line 494
    invoke-direct {p0, p1, p2}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionSize(Ljava/lang/Thread;Ljava/lang/Throwable;)I

    move-result p1

    const/4 p2, 0x1

    .line 495
    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v0

    invoke-static {p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v1

    add-int/2addr v0, v1

    add-int/2addr v0, p1

    const/4 p1, 0x0

    add-int/2addr v0, p1

    if-eqz p4, :cond_0

    .line 499
    invoke-interface {p4}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p4

    invoke-interface {p4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p4

    :goto_0
    invoke-interface {p4}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {p4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 500
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-direct {p0, v2, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppCustomAttributeSize(Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    const/4 v2, 0x2

    .line 502
    invoke-static {v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v2

    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v3

    add-int/2addr v2, v3

    add-int/2addr v2, v1

    add-int/2addr v0, v2

    goto :goto_0

    .line 508
    :cond_0
    iget-object p4, p0, Lcom/crashlytics/android/core/SessionDataWriter;->runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

    if-eqz p4, :cond_2

    const/4 p4, 0x3

    .line 509
    iget-object v1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

    iget v1, v1, Landroid/app/ActivityManager$RunningAppProcessInfo;->importance:I

    const/16 v2, 0x64

    if-eq v1, v2, :cond_1

    const/4 p1, 0x1

    :cond_1
    invoke-static {p4, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBoolSize(IZ)I

    move-result p1

    add-int/2addr v0, p1

    :cond_2
    const/4 p1, 0x4

    .line 513
    invoke-static {p1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p1

    add-int/2addr v0, p1

    return v0
.end method

.method private getEventDeviceSize(FIZIJJ)I
    .locals 1

    const/4 v0, 0x1

    .line 560
    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeFloatSize(IF)I

    move-result p1

    add-int/lit8 p1, p1, 0x0

    const/4 v0, 0x2

    .line 561
    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeSInt32Size(II)I

    move-result p2

    add-int/2addr p1, p2

    const/4 p2, 0x3

    .line 562
    invoke-static {p2, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBoolSize(IZ)I

    move-result p2

    add-int/2addr p1, p2

    const/4 p2, 0x4

    .line 563
    invoke-static {p2, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p2

    add-int/2addr p1, p2

    const/4 p2, 0x5

    .line 564
    invoke-static {p2, p5, p6}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result p2

    add-int/2addr p1, p2

    const/4 p2, 0x6

    .line 565
    invoke-static {p2, p7, p8}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result p2

    add-int/2addr p1, p2

    return p1
.end method

.method private getEventLogSize(Lcom/crashlytics/android/core/ByteString;)I
    .locals 1

    const/4 v0, 0x1

    .line 574
    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    return p1
.end method

.method private getFrameSize(Ljava/lang/StackTraceElement;Z)I
    .locals 6

    .line 633
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->isNativeMethod()Z

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    .line 635
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result v0

    invoke-static {v0, v2}, Ljava/lang/Math;->max(II)I

    move-result v0

    int-to-long v3, v0

    invoke-static {v1, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v0

    add-int/2addr v0, v2

    goto :goto_0

    :cond_0
    const-wide/16 v3, 0x0

    .line 637
    invoke-static {v1, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v0

    add-int/2addr v0, v2

    .line 640
    :goto_0
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getClassName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "."

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getMethodName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v1

    const/4 v3, 0x2

    invoke-static {v3, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v1

    add-int/2addr v0, v1

    .line 643
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getFileName()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    const/4 v1, 0x3

    .line 644
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getFileName()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v4

    invoke-static {v1, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v1

    add-int/2addr v0, v1

    .line 648
    :cond_1
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->isNativeMethod()Z

    move-result v1

    if-nez v1, :cond_2

    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result v1

    if-lez v1, :cond_2

    const/4 v1, 0x4

    .line 650
    invoke-virtual {p1}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result p1

    int-to-long v4, p1

    invoke-static {v1, v4, v5}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result p1

    add-int/2addr v0, p1

    :cond_2
    const/4 p1, 0x5

    if-eqz p2, :cond_3

    const/4 v2, 0x2

    .line 652
    :cond_3
    invoke-static {p1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p1

    add-int/2addr v0, p1

    return v0
.end method

.method private getSessionAppOrgSize()I
    .locals 2

    .line 399
    new-instance v0, Lio/fabric/sdk/android/services/common/ApiKey;

    invoke-direct {v0}, Lio/fabric/sdk/android/services/common/ApiKey;-><init>()V

    iget-object v1, p0, Lcom/crashlytics/android/core/SessionDataWriter;->context:Landroid/content/Context;

    invoke-virtual {v0, v1}, Lio/fabric/sdk/android/services/common/ApiKey;->getValue(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    const/4 v1, 0x1

    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    add-int/lit8 v0, v0, 0x0

    return v0
.end method

.method private getSessionAppSize(Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;I)I
    .locals 1

    const/4 v0, 0x1

    .line 385
    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    add-int/lit8 p1, p1, 0x0

    const/4 v0, 0x2

    .line 386
    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    add-int/2addr p1, p2

    const/4 p2, 0x3

    .line 387
    invoke-static {p2, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    add-int/2addr p1, p2

    .line 388
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionAppOrgSize()I

    move-result p2

    const/4 p3, 0x5

    .line 389
    invoke-static {p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result p3

    invoke-static {p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v0

    add-int/2addr p3, v0

    add-int/2addr p3, p2

    add-int/2addr p1, p3

    const/4 p2, 0x6

    .line 391
    invoke-static {p2, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    add-int/2addr p1, p2

    const/16 p2, 0xa

    .line 392
    invoke-static {p2, p5}, Lcom/crashlytics/android/core/CodedOutputStream;->computeEnumSize(II)I

    move-result p2

    add-int/2addr p1, p2

    return p1
.end method

.method private getSessionDeviceSize(ILcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;IJJZLjava/util/Map;ILcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;)I
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I",
            "Lcom/crashlytics/android/core/ByteString;",
            "Lcom/crashlytics/android/core/ByteString;",
            "IJJZ",
            "Ljava/util/Map<",
            "Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;",
            "Ljava/lang/String;",
            ">;I",
            "Lcom/crashlytics/android/core/ByteString;",
            "Lcom/crashlytics/android/core/ByteString;",
            ")I"
        }
    .end annotation

    const/4 v0, 0x1

    .line 430
    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p2

    const/4 v0, 0x0

    add-int/2addr p2, v0

    const/4 v1, 0x3

    .line 431
    invoke-static {v1, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeEnumSize(II)I

    move-result p1

    add-int/2addr p2, p1

    if-nez p3, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    :cond_0
    const/4 p1, 0x4

    .line 432
    invoke-static {p1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    :goto_0
    add-int/2addr p2, p1

    const/4 p1, 0x5

    .line 433
    invoke-static {p1, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p1

    add-int/2addr p2, p1

    const/4 p1, 0x6

    .line 434
    invoke-static {p1, p5, p6}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result p1

    add-int/2addr p2, p1

    const/4 p1, 0x7

    .line 435
    invoke-static {p1, p7, p8}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result p1

    add-int/2addr p2, p1

    const/16 p1, 0xa

    .line 436
    invoke-static {p1, p9}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBoolSize(IZ)I

    move-result p1

    add-int/2addr p2, p1

    if-eqz p10, :cond_1

    .line 438
    invoke-interface {p10}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_1
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p3

    if-eqz p3, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Ljava/util/Map$Entry;

    .line 439
    invoke-interface {p3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object p4

    check-cast p4, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    invoke-interface {p3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object p3

    check-cast p3, Ljava/lang/String;

    invoke-direct {p0, p4, p3}, Lcom/crashlytics/android/core/SessionDataWriter;->getDeviceIdentifierSize(Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;Ljava/lang/String;)I

    move-result p3

    const/16 p4, 0xb

    .line 440
    invoke-static {p4}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result p4

    invoke-static {p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result p5

    add-int/2addr p4, p5

    add-int/2addr p4, p3

    add-int/2addr p2, p4

    goto :goto_1

    :cond_1
    const/16 p1, 0xc

    .line 444
    invoke-static {p1, p11}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p1

    add-int/2addr p2, p1

    if-nez p12, :cond_2

    const/4 p1, 0x0

    goto :goto_2

    :cond_2
    const/16 p1, 0xd

    .line 445
    invoke-static {p1, p12}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    :goto_2
    add-int/2addr p2, p1

    if-nez p13, :cond_3

    goto :goto_3

    :cond_3
    const/16 p1, 0xe

    .line 446
    invoke-static {p1, p13}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v0

    :goto_3
    add-int/2addr p2, v0

    return p2
.end method

.method private getSessionEventSize(Ljava/lang/Thread;Ljava/lang/Throwable;Ljava/lang/String;JLjava/util/Map;FIZIJJLcom/crashlytics/android/core/ByteString;)I
    .locals 12
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Thread;",
            "Ljava/lang/Throwable;",
            "Ljava/lang/String;",
            "J",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;FIZIJJ",
            "Lcom/crashlytics/android/core/ByteString;",
            ")I"
        }
    .end annotation

    move-object v9, p0

    move-object/from16 v10, p15

    const/4 v0, 0x1

    move-wide/from16 v1, p4

    .line 471
    invoke-static {v0, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt64Size(IJ)I

    move-result v0

    add-int/lit8 v0, v0, 0x0

    .line 472
    invoke-static {p3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v1

    const/4 v2, 0x2

    invoke-static {v2, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v1

    add-int/2addr v0, v1

    move-object v1, p1

    move-object v2, p2

    move-object/from16 v3, p6

    move/from16 v4, p10

    .line 473
    invoke-direct {p0, p1, p2, v4, v3}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppSize(Ljava/lang/Thread;Ljava/lang/Throwable;ILjava/util/Map;)I

    move-result v1

    const/4 v2, 0x3

    .line 474
    invoke-static {v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v2

    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v3

    add-int/2addr v2, v3

    add-int/2addr v2, v1

    add-int v11, v0, v2

    move-object v0, p0

    move/from16 v1, p7

    move/from16 v2, p8

    move/from16 v3, p9

    move-wide/from16 v5, p11

    move-wide/from16 v7, p13

    .line 476
    invoke-direct/range {v0 .. v8}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventDeviceSize(FIZIJJ)I

    move-result v0

    const/4 v1, 0x5

    .line 478
    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v1

    invoke-static {v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v2

    add-int/2addr v1, v2

    add-int/2addr v1, v0

    add-int/2addr v11, v1

    if-eqz v10, :cond_0

    .line 482
    invoke-direct {p0, v10}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventLogSize(Lcom/crashlytics/android/core/ByteString;)I

    move-result v0

    const/4 v1, 0x6

    .line 483
    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v1

    invoke-static {v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v2

    add-int/2addr v1, v2

    add-int/2addr v1, v0

    add-int/2addr v11, v1

    :cond_0
    return v11
.end method

.method private getSessionOSSize(Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Z)I
    .locals 3

    const/4 v0, 0x3

    const/4 v1, 0x1

    .line 408
    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeEnumSize(II)I

    move-result v1

    add-int/lit8 v1, v1, 0x0

    const/4 v2, 0x2

    .line 409
    invoke-static {v2, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    add-int/2addr v1, p1

    .line 410
    invoke-static {v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    add-int/2addr v1, p1

    const/4 p1, 0x4

    .line 411
    invoke-static {p1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBoolSize(IZ)I

    move-result p1

    add-int/2addr v1, p1

    return v1
.end method

.method private getThreadSize(Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)I
    .locals 4

    .line 659
    invoke-virtual {p1}, Ljava/lang/Thread;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p1

    const/4 v0, 0x1

    invoke-static {v0, p1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result p1

    const/4 v0, 0x2

    .line 660
    invoke-static {v0, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->computeUInt32Size(II)I

    move-result p3

    add-int/2addr p1, p3

    .line 662
    array-length p3, p2

    const/4 v0, 0x0

    :goto_0
    if-ge v0, p3, :cond_0

    aget-object v1, p2, v0

    .line 663
    invoke-direct {p0, v1, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->getFrameSize(Ljava/lang/StackTraceElement;Z)I

    move-result v1

    const/4 v2, 0x3

    .line 664
    invoke-static {v2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeTagSize(I)I

    move-result v2

    invoke-static {v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeRawVarint32Size(I)I

    move-result v3

    add-int/2addr v2, v3

    add-int/2addr v2, v1

    add-int/2addr p1, v2

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    return p1
.end method

.method private stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;
    .locals 0

    if-nez p1, :cond_0

    const/4 p1, 0x0

    goto :goto_0

    .line 672
    :cond_0
    invoke-static {p1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method private writeFrame(Lcom/crashlytics/android/core/CodedOutputStream;ILjava/lang/StackTraceElement;Z)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    const/4 v0, 0x2

    .line 327
    invoke-virtual {p1, p2, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 328
    invoke-direct {p0, p3, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->getFrameSize(Ljava/lang/StackTraceElement;Z)I

    move-result p2

    invoke-virtual {p1, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 330
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->isNativeMethod()Z

    move-result p2

    const/4 v1, 0x0

    const/4 v2, 0x1

    if-eqz p2, :cond_0

    .line 333
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result p2

    invoke-static {p2, v1}, Ljava/lang/Math;->max(II)I

    move-result p2

    int-to-long v3, p2

    invoke-virtual {p1, v2, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    goto :goto_0

    :cond_0
    const-wide/16 v3, 0x0

    .line 335
    invoke-virtual {p1, v2, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    .line 338
    :goto_0
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getClassName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "."

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getMethodName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 341
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getFileName()Ljava/lang/String;

    move-result-object p2

    if-eqz p2, :cond_1

    const/4 p2, 0x3

    .line 342
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getFileName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    invoke-virtual {p1, p2, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 347
    :cond_1
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->isNativeMethod()Z

    move-result p2

    const/4 v0, 0x4

    if-nez p2, :cond_2

    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result p2

    if-lez p2, :cond_2

    .line 348
    invoke-virtual {p3}, Ljava/lang/StackTraceElement;->getLineNumber()I

    move-result p2

    int-to-long p2, p2

    invoke-virtual {p1, v0, p2, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    :cond_2
    const/4 p2, 0x5

    if-eqz p4, :cond_3

    goto :goto_1

    :cond_3
    const/4 v0, 0x0

    .line 354
    :goto_1
    invoke-virtual {p1, p2, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    return-void
.end method

.method private writeSessionEventApp(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;Ljava/lang/Throwable;ILjava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/crashlytics/android/core/CodedOutputStream;",
            "Ljava/lang/Thread;",
            "Ljava/lang/Throwable;",
            "I",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    const/4 v0, 0x3

    const/4 v1, 0x2

    .line 210
    invoke-virtual {p1, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 211
    invoke-direct {p0, p2, p3, p4, p5}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppSize(Ljava/lang/Thread;Ljava/lang/Throwable;ILjava/util/Map;)I

    move-result v1

    invoke-virtual {p1, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 213
    invoke-direct {p0, p1, p2, p3}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventAppExecution(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;Ljava/lang/Throwable;)V

    if-eqz p5, :cond_0

    .line 215
    invoke-interface {p5}, Ljava/util/Map;->isEmpty()Z

    move-result p2

    if-nez p2, :cond_0

    .line 216
    invoke-direct {p0, p1, p5}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventAppCustomAttributes(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/util/Map;)V

    .line 220
    :cond_0
    iget-object p2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

    if-eqz p2, :cond_2

    .line 222
    iget-object p2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

    iget p2, p2, Landroid/app/ActivityManager$RunningAppProcessInfo;->importance:I

    const/16 p3, 0x64

    if-eq p2, p3, :cond_1

    const/4 p2, 0x1

    goto :goto_0

    :cond_1
    const/4 p2, 0x0

    :goto_0
    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBool(IZ)V

    :cond_2
    const/4 p2, 0x4

    .line 226
    invoke-virtual {p1, p2, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    return-void
.end method

.method private writeSessionEventAppCustomAttributes(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/util/Map;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/crashlytics/android/core/CodedOutputStream;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 266
    invoke-interface {p2}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object p2

    invoke-interface {p2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :goto_0
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    const/4 v1, 0x2

    .line 267
    invoke-virtual {p1, v1, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 268
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-direct {p0, v2, v3}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppCustomAttributeSize(Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    invoke-virtual {p1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v2, 0x1

    .line 271
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-static {v3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v3

    invoke-virtual {p1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 272
    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, ""

    .line 273
    :cond_0
    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    invoke-virtual {p1, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private writeSessionEventAppExecution(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;Ljava/lang/Throwable;)V
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    const/4 v0, 0x1

    const/4 v1, 0x2

    .line 231
    invoke-virtual {p1, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 232
    invoke-direct {p0, p2, p3}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionSize(Ljava/lang/Thread;Ljava/lang/Throwable;)I

    move-result v2

    invoke-virtual {p1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 234
    iget-object v6, p0, Lcom/crashlytics/android/core/SessionDataWriter;->exceptionStack:[Ljava/lang/StackTraceElement;

    const/4 v7, 0x4

    const/4 v8, 0x1

    move-object v3, p0

    move-object v4, p1

    move-object v5, p2

    invoke-direct/range {v3 .. v8}, Lcom/crashlytics/android/core/SessionDataWriter;->writeThread(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)V

    .line 238
    iget-object p2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->threads:[Ljava/lang/Thread;

    array-length p2, p2

    const/4 v2, 0x0

    :goto_0
    if-ge v2, p2, :cond_0

    .line 240
    iget-object v3, p0, Lcom/crashlytics/android/core/SessionDataWriter;->threads:[Ljava/lang/Thread;

    aget-object v6, v3, v2

    .line 241
    iget-object v3, p0, Lcom/crashlytics/android/core/SessionDataWriter;->stacks:Ljava/util/List;

    invoke-interface {v3, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    move-object v7, v3

    check-cast v7, [Ljava/lang/StackTraceElement;

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v4, p0

    move-object v5, p1

    invoke-direct/range {v4 .. v9}, Lcom/crashlytics/android/core/SessionDataWriter;->writeThread(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)V

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 244
    :cond_0
    invoke-direct {p0, p1, p3, v0, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventAppExecutionException(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Throwable;II)V

    const/4 p2, 0x3

    .line 246
    invoke-virtual {p1, p2, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 247
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionSignalSize()I

    move-result p3

    invoke-virtual {p1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 248
    sget-object p3, Lcom/crashlytics/android/core/SessionDataWriter;->SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;

    invoke-virtual {p1, v0, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 249
    sget-object p3, Lcom/crashlytics/android/core/SessionDataWriter;->SIGNAL_DEFAULT_BYTE_STRING:Lcom/crashlytics/android/core/ByteString;

    invoke-virtual {p1, v1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const-wide/16 v2, 0x0

    .line 250
    invoke-virtual {p1, p2, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    const/4 p3, 0x4

    .line 252
    invoke-virtual {p1, p3, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 253
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getBinaryImageSize()I

    move-result v4

    invoke-virtual {p1, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 254
    invoke-virtual {p1, v0, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    .line 255
    invoke-virtual {p1, v1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    .line 256
    iget-object v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->packageNameBytes:Lcom/crashlytics/android/core/ByteString;

    invoke-virtual {p1, p2, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 257
    iget-object p2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

    if-eqz p2, :cond_1

    .line 258
    iget-object p2, p0, Lcom/crashlytics/android/core/SessionDataWriter;->optionalBuildIdBytes:Lcom/crashlytics/android/core/ByteString;

    invoke-virtual {p1, p3, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_1
    return-void
.end method

.method private writeSessionEventAppExecutionException(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Throwable;II)V
    .locals 6
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    const/4 v0, 0x2

    .line 280
    invoke-virtual {p1, p4, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    const/4 p4, 0x1

    .line 281
    invoke-direct {p0, p2, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventAppExecutionExceptionSize(Ljava/lang/Throwable;I)I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 283
    invoke-virtual {p2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    invoke-virtual {p1, p4, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 284
    invoke-virtual {p2}, Ljava/lang/Throwable;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_0

    const/4 v1, 0x3

    .line 286
    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    invoke-virtual {p1, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 289
    :cond_0
    invoke-virtual {p2}, Ljava/lang/Throwable;->getStackTrace()[Ljava/lang/StackTraceElement;

    move-result-object v0

    array-length v1, v0

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_1

    aget-object v4, v0, v3

    const/4 v5, 0x4

    .line 290
    invoke-direct {p0, p1, v5, v4, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->writeFrame(Lcom/crashlytics/android/core/CodedOutputStream;ILjava/lang/StackTraceElement;Z)V

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 293
    :cond_1
    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p2

    if-eqz p2, :cond_4

    .line 295
    iget v0, p0, Lcom/crashlytics/android/core/SessionDataWriter;->maxChainedExceptionsDepth:I

    if-ge p3, v0, :cond_2

    add-int/2addr p3, p4

    const/4 p4, 0x6

    .line 296
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventAppExecutionException(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Throwable;II)V

    goto :goto_2

    :cond_2
    :goto_1
    if-eqz p2, :cond_3

    .line 302
    invoke-virtual {p2}, Ljava/lang/Throwable;->getCause()Ljava/lang/Throwable;

    move-result-object p2

    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    :cond_3
    const/4 p2, 0x7

    .line 305
    invoke-virtual {p1, p2, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    :cond_4
    :goto_2
    return-void
.end method

.method private writeSessionEventDevice(Lcom/crashlytics/android/core/CodedOutputStream;FIZIJJ)V
    .locals 12
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    move-object v0, p1

    const/4 v1, 0x2

    const/4 v2, 0x5

    .line 361
    invoke-virtual {p1, v2, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    move-object v3, p0

    move v4, p2

    move v5, p3

    move/from16 v6, p4

    move/from16 v7, p5

    move-wide/from16 v8, p6

    move-wide/from16 v10, p8

    .line 362
    invoke-direct/range {v3 .. v11}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventDeviceSize(FIZIJJ)I

    move-result v3

    invoke-virtual {p1, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v3, 0x1

    .line 364
    invoke-virtual {p1, v3, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeFloat(IF)V

    move v3, p3

    .line 365
    invoke-virtual {p1, v1, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeSInt32(II)V

    const/4 v1, 0x3

    move/from16 v3, p4

    .line 366
    invoke-virtual {p1, v1, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBool(IZ)V

    const/4 v1, 0x4

    move/from16 v3, p5

    .line 367
    invoke-virtual {p1, v1, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    move-wide/from16 v3, p6

    .line 368
    invoke-virtual {p1, v2, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    const/4 v1, 0x6

    move-wide/from16 v2, p8

    .line 369
    invoke-virtual {p1, v1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    return-void
.end method

.method private writeSessionEventLog(Lcom/crashlytics/android/core/CodedOutputStream;Lcom/crashlytics/android/core/ByteString;)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    if-eqz p2, :cond_0

    const/4 v0, 0x6

    const/4 v1, 0x2

    .line 374
    invoke-virtual {p1, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 375
    invoke-direct {p0, p2}, Lcom/crashlytics/android/core/SessionDataWriter;->getEventLogSize(Lcom/crashlytics/android/core/ByteString;)I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v0, 0x1

    .line 376
    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_0
    return-void
.end method

.method private writeThread(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    const/4 v0, 0x2

    const/4 v1, 0x1

    .line 314
    invoke-virtual {p1, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 315
    invoke-direct {p0, p2, p3, p4, p5}, Lcom/crashlytics/android/core/SessionDataWriter;->getThreadSize(Ljava/lang/Thread;[Ljava/lang/StackTraceElement;IZ)I

    move-result v2

    .line 316
    invoke-virtual {p1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 317
    invoke-virtual {p2}, Ljava/lang/Thread;->getName()Ljava/lang/String;

    move-result-object p2

    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    invoke-virtual {p1, v1, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 318
    invoke-virtual {p1, v0, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    .line 320
    array-length p2, p3

    const/4 p4, 0x0

    :goto_0
    if-ge p4, p2, :cond_0

    aget-object v0, p3, p4

    const/4 v1, 0x3

    .line 321
    invoke-direct {p0, p1, v1, v0, p5}, Lcom/crashlytics/android/core/SessionDataWriter;->writeFrame(Lcom/crashlytics/android/core/CodedOutputStream;ILjava/lang/StackTraceElement;Z)V

    add-int/lit8 p4, p4, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method


# virtual methods
.method public writeBeginSession(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/String;Ljava/lang/String;J)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 56
    invoke-static {p3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p3

    const/4 v0, 0x1

    invoke-virtual {p1, v0, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 57
    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    const/4 p3, 0x2

    invoke-virtual {p1, p3, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 p2, 0x3

    .line 58
    invoke-virtual {p1, p2, p4, p5}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    return-void
.end method

.method public writeSessionApp(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 64
    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    .line 65
    invoke-static {p3}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p3

    .line 66
    invoke-static {p4}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p4

    .line 67
    invoke-static {p5}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p5

    const/4 v6, 0x2

    const/4 v0, 0x7

    .line 70
    invoke-virtual {p1, v0, v6}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    move-object v0, p0

    move-object v1, p2

    move-object v2, p3

    move-object v3, p4

    move-object v4, p5

    move v5, p6

    .line 71
    invoke-direct/range {v0 .. v5}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionAppSize(Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;I)I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v0, 0x1

    .line 73
    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 74
    invoke-virtual {p1, v6, p3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 p2, 0x3

    .line 75
    invoke-virtual {p1, p2, p4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 p2, 0x5

    .line 78
    invoke-virtual {p1, p2, v6}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 79
    invoke-direct {p0}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionAppOrgSize()I

    move-result p2

    invoke-virtual {p1, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 80
    new-instance p2, Lio/fabric/sdk/android/services/common/ApiKey;

    invoke-direct {p2}, Lio/fabric/sdk/android/services/common/ApiKey;-><init>()V

    iget-object p3, p0, Lcom/crashlytics/android/core/SessionDataWriter;->context:Landroid/content/Context;

    invoke-virtual {p2, p3}, Lio/fabric/sdk/android/services/common/ApiKey;->getValue(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeString(ILjava/lang/String;)V

    const/4 p2, 0x6

    .line 83
    invoke-virtual {p1, p2, p5}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/16 p2, 0xa

    .line 84
    invoke-virtual {p1, p2, p6}, Lcom/crashlytics/android/core/CodedOutputStream;->writeEnum(II)V

    return-void
.end method

.method public writeSessionDevice(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/String;ILjava/lang/String;IJJZLjava/util/Map;ILjava/lang/String;Ljava/lang/String;)V
    .locals 19
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/crashlytics/android/core/CodedOutputStream;",
            "Ljava/lang/String;",
            "I",
            "Ljava/lang/String;",
            "IJJZ",
            "Ljava/util/Map<",
            "Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;",
            "Ljava/lang/String;",
            ">;I",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    move-object/from16 v14, p0

    move-object/from16 v15, p1

    .line 105
    invoke-static/range {p2 .. p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v13

    move-object/from16 v0, p4

    .line 106
    invoke-direct {v14, v0}, Lcom/crashlytics/android/core/SessionDataWriter;->stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v12

    move-object/from16 v0, p14

    .line 107
    invoke-direct {v14, v0}, Lcom/crashlytics/android/core/SessionDataWriter;->stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v11

    move-object/from16 v0, p13

    .line 108
    invoke-direct {v14, v0}, Lcom/crashlytics/android/core/SessionDataWriter;->stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v10

    const/4 v9, 0x2

    const/16 v0, 0x9

    .line 110
    invoke-virtual {v15, v0, v9}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    move-object/from16 v0, p0

    move/from16 v1, p3

    move-object v2, v13

    move-object v3, v12

    move/from16 v4, p5

    move-wide/from16 v5, p6

    move-wide/from16 v7, p8

    const/4 v14, 0x2

    move/from16 v9, p10

    move-object/from16 v16, v10

    move-object/from16 v10, p11

    move-object/from16 v17, v11

    move/from16 v11, p12

    move-object v14, v12

    move-object/from16 v12, v16

    move-object/from16 v18, v14

    move-object v14, v13

    move-object/from16 v13, v17

    .line 112
    invoke-direct/range {v0 .. v13}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionDeviceSize(ILcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;IJJZLjava/util/Map;ILcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;)I

    move-result v0

    invoke-virtual {v15, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v0, 0x1

    .line 116
    invoke-virtual {v15, v0, v14}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 v1, 0x3

    move/from16 v2, p3

    .line 118
    invoke-virtual {v15, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeEnum(II)V

    const/4 v1, 0x4

    move-object/from16 v2, v18

    .line 119
    invoke-virtual {v15, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 v1, 0x5

    move/from16 v2, p5

    .line 120
    invoke-virtual {v15, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    const/4 v1, 0x6

    move-wide/from16 v2, p6

    .line 121
    invoke-virtual {v15, v1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    const/4 v1, 0x7

    move-wide/from16 v2, p8

    .line 122
    invoke-virtual {v15, v1, v2, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    const/16 v1, 0xa

    move/from16 v2, p10

    .line 123
    invoke-virtual {v15, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBool(IZ)V

    .line 125
    invoke-interface/range {p11 .. p11}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    const/16 v3, 0xb

    const/4 v4, 0x2

    .line 126
    invoke-virtual {v15, v3, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 127
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    move-object/from16 v4, p0

    const/4 v6, 0x2

    invoke-direct {v4, v3, v5}, Lcom/crashlytics/android/core/SessionDataWriter;->getDeviceIdentifierSize(Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;Ljava/lang/String;)I

    move-result v3

    invoke-virtual {v15, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 129
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;

    iget v3, v3, Lio/fabric/sdk/android/services/common/IdManager$DeviceIdentifierType;->protobufIndex:I

    invoke-virtual {v15, v0, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeEnum(II)V

    .line 130
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-static {v2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v2

    invoke-virtual {v15, v6, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    goto :goto_0

    :cond_0
    move-object/from16 v4, p0

    const/16 v0, 0xc

    move/from16 v1, p12

    .line 132
    invoke-virtual {v15, v0, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt32(II)V

    move-object/from16 v0, v16

    if-eqz v0, :cond_1

    const/16 v1, 0xd

    .line 135
    invoke-virtual {v15, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_1
    move-object/from16 v0, v17

    if-eqz v0, :cond_2

    const/16 v1, 0xe

    .line 138
    invoke-virtual {v15, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_2
    return-void
.end method

.method public writeSessionEvent(Lcom/crashlytics/android/core/CodedOutputStream;JLjava/lang/Thread;Ljava/lang/Throwable;Ljava/lang/String;[Ljava/lang/Thread;FIZIJJLandroid/app/ActivityManager$RunningAppProcessInfo;Ljava/util/List;[Ljava/lang/StackTraceElement;Lcom/crashlytics/android/core/LogFileManager;Ljava/util/Map;)V
    .locals 17
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/crashlytics/android/core/CodedOutputStream;",
            "J",
            "Ljava/lang/Thread;",
            "Ljava/lang/Throwable;",
            "Ljava/lang/String;",
            "[",
            "Ljava/lang/Thread;",
            "FIZIJJ",
            "Landroid/app/ActivityManager$RunningAppProcessInfo;",
            "Ljava/util/List<",
            "[",
            "Ljava/lang/StackTraceElement;",
            ">;[",
            "Ljava/lang/StackTraceElement;",
            "Lcom/crashlytics/android/core/LogFileManager;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    move-object/from16 v15, p0

    move-object/from16 v13, p1

    move-object/from16 v0, p16

    .line 180
    iput-object v0, v15, Lcom/crashlytics/android/core/SessionDataWriter;->runningAppProcessInfo:Landroid/app/ActivityManager$RunningAppProcessInfo;

    move-object/from16 v0, p17

    .line 181
    iput-object v0, v15, Lcom/crashlytics/android/core/SessionDataWriter;->stacks:Ljava/util/List;

    move-object/from16 v0, p18

    .line 182
    iput-object v0, v15, Lcom/crashlytics/android/core/SessionDataWriter;->exceptionStack:[Ljava/lang/StackTraceElement;

    move-object/from16 v0, p7

    .line 183
    iput-object v0, v15, Lcom/crashlytics/android/core/SessionDataWriter;->threads:[Ljava/lang/Thread;

    .line 185
    invoke-virtual/range {p19 .. p19}, Lcom/crashlytics/android/core/LogFileManager;->getByteStringForLog()Lcom/crashlytics/android/core/ByteString;

    move-result-object v14

    if-nez v14, :cond_0

    .line 188
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "No log data to include with this event."

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 192
    :cond_0
    invoke-virtual/range {p19 .. p19}, Lcom/crashlytics/android/core/LogFileManager;->clearLog()V

    const/16 v0, 0xa

    const/4 v11, 0x2

    .line 194
    invoke-virtual {v13, v0, v11}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    move-object/from16 v0, p0

    move-object/from16 v1, p4

    move-object/from16 v2, p5

    move-object/from16 v3, p6

    move-wide/from16 v4, p2

    move-object/from16 v6, p20

    move/from16 v7, p8

    move/from16 v8, p9

    move/from16 v9, p10

    move/from16 v10, p11

    move-wide/from16 v11, p12

    move-object/from16 v16, v14

    move-wide/from16 v13, p14

    move-object/from16 v15, v16

    .line 195
    invoke-direct/range {v0 .. v15}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionEventSize(Ljava/lang/Thread;Ljava/lang/Throwable;Ljava/lang/String;JLjava/util/Map;FIZIJJLcom/crashlytics/android/core/ByteString;)I

    move-result v0

    move-object/from16 v10, p1

    invoke-virtual {v10, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v0, 0x1

    move-wide/from16 v1, p2

    .line 198
    invoke-virtual {v10, v0, v1, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeUInt64(IJ)V

    .line 199
    invoke-static/range {p6 .. p6}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    const/4 v1, 0x2

    invoke-virtual {v10, v1, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v2, p4

    move-object/from16 v3, p5

    move/from16 v4, p11

    move-object/from16 v5, p20

    .line 201
    invoke-direct/range {v0 .. v5}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventApp(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/Thread;Ljava/lang/Throwable;ILjava/util/Map;)V

    move/from16 v2, p8

    move/from16 v3, p9

    move/from16 v4, p10

    move/from16 v5, p11

    move-wide/from16 v6, p12

    move-wide/from16 v8, p14

    .line 202
    invoke-direct/range {v0 .. v9}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventDevice(Lcom/crashlytics/android/core/CodedOutputStream;FIZIJJ)V

    move-object/from16 v1, v16

    .line 204
    invoke-direct {v0, v10, v1}, Lcom/crashlytics/android/core/SessionDataWriter;->writeSessionEventLog(Lcom/crashlytics/android/core/CodedOutputStream;Lcom/crashlytics/android/core/ByteString;)V

    return-void
.end method

.method public writeSessionOS(Lcom/crashlytics/android/core/CodedOutputStream;Z)V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 88
    sget-object v0, Landroid/os/Build$VERSION;->RELEASE:Ljava/lang/String;

    invoke-static {v0}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    .line 89
    sget-object v1, Landroid/os/Build$VERSION;->CODENAME:Ljava/lang/String;

    invoke-static {v1}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v1

    const/4 v2, 0x2

    const/16 v3, 0x8

    .line 91
    invoke-virtual {p1, v3, v2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 92
    invoke-direct {p0, v0, v1, p2}, Lcom/crashlytics/android/core/SessionDataWriter;->getSessionOSSize(Lcom/crashlytics/android/core/ByteString;Lcom/crashlytics/android/core/ByteString;Z)I

    move-result v3

    invoke-virtual {p1, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    const/4 v3, 0x3

    const/4 v4, 0x1

    .line 93
    invoke-virtual {p1, v4, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeEnum(II)V

    .line 94
    invoke-virtual {p1, v2, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    .line 95
    invoke-virtual {p1, v3, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    const/4 v0, 0x4

    .line 96
    invoke-virtual {p1, v0, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBool(IZ)V

    return-void
.end method

.method public writeSessionUser(Lcom/crashlytics/android/core/CodedOutputStream;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    if-nez p2, :cond_0

    const-string p2, ""

    .line 145
    :cond_0
    invoke-static {p2}, Lcom/crashlytics/android/core/ByteString;->copyFromUtf8(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object p2

    .line 146
    invoke-direct {p0, p3}, Lcom/crashlytics/android/core/SessionDataWriter;->stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v0

    .line 147
    invoke-direct {p0, p4}, Lcom/crashlytics/android/core/SessionDataWriter;->stringToByteString(Ljava/lang/String;)Lcom/crashlytics/android/core/ByteString;

    move-result-object v1

    const/4 v2, 0x1

    .line 150
    invoke-static {v2, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v3

    add-int/lit8 v3, v3, 0x0

    const/4 v4, 0x2

    if-eqz p3, :cond_1

    .line 152
    invoke-static {v4, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v5

    add-int/2addr v3, v5

    :cond_1
    const/4 v5, 0x3

    if-eqz p4, :cond_2

    .line 155
    invoke-static {v5, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->computeBytesSize(ILcom/crashlytics/android/core/ByteString;)I

    move-result v6

    add-int/2addr v3, v6

    :cond_2
    const/4 v6, 0x6

    .line 158
    invoke-virtual {p1, v6, v4}, Lcom/crashlytics/android/core/CodedOutputStream;->writeTag(II)V

    .line 159
    invoke-virtual {p1, v3}, Lcom/crashlytics/android/core/CodedOutputStream;->writeRawVarint32(I)V

    .line 160
    invoke-virtual {p1, v2, p2}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    if-eqz p3, :cond_3

    .line 162
    invoke-virtual {p1, v4, v0}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_3
    if-eqz p4, :cond_4

    .line 165
    invoke-virtual {p1, v5, v1}, Lcom/crashlytics/android/core/CodedOutputStream;->writeBytes(ILcom/crashlytics/android/core/ByteString;)V

    :cond_4
    return-void
.end method
