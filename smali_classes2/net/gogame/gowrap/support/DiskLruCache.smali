.class public final Lnet/gogame/gowrap/support/DiskLruCache;
.super Ljava/lang/Object;
.source "DiskLruCache.java"

# interfaces
.implements Ljava/io/Closeable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/support/DiskLruCache$Entry;,
        Lnet/gogame/gowrap/support/DiskLruCache$Editor;,
        Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;
    }
.end annotation


# static fields
.field static final ANY_SEQUENCE_NUMBER:J = -0x1L

.field private static final CLEAN:Ljava/lang/String; = "CLEAN"

.field private static final DIRTY:Ljava/lang/String; = "DIRTY"

.field private static final IO_BUFFER_SIZE:I = 0x2000

.field static final JOURNAL_FILE:Ljava/lang/String; = "journal"

.field static final JOURNAL_FILE_TMP:Ljava/lang/String; = "journal.tmp"

.field static final MAGIC:Ljava/lang/String; = "libcore.io.DiskLruCache"

.field private static final READ:Ljava/lang/String; = "READ"

.field private static final REMOVE:Ljava/lang/String; = "REMOVE"

.field private static final UTF_8:Ljava/nio/charset/Charset;

.field static final VERSION_1:Ljava/lang/String; = "1"


# instance fields
.field private final appVersion:I

.field private final cleanupCallable:Ljava/util/concurrent/Callable;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/Callable<",
            "Ljava/lang/Void;",
            ">;"
        }
    .end annotation
.end field

.field private final directory:Ljava/io/File;

.field private final executorService:Ljava/util/concurrent/ExecutorService;

.field private final journalFile:Ljava/io/File;

.field private final journalFileTmp:Ljava/io/File;

.field private journalWriter:Ljava/io/Writer;

.field private final lruEntries:Ljava/util/LinkedHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lnet/gogame/gowrap/support/DiskLruCache$Entry;",
            ">;"
        }
    .end annotation
.end field

.field private final maxSize:J

.field private nextSequenceNumber:J

.field private redundantOpCount:I

.field private size:J

.field private final valueCount:I


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "UTF-8"

    .line 95
    invoke-static {v0}, Ljava/nio/charset/Charset;->forName(Ljava/lang/String;)Ljava/nio/charset/Charset;

    move-result-object v0

    sput-object v0, Lnet/gogame/gowrap/support/DiskLruCache;->UTF_8:Ljava/nio/charset/Charset;

    return-void
.end method

.method private constructor <init>(Ljava/io/File;IIJ)V
    .locals 13

    move-object v0, p0

    move-object v1, p1

    .line 177
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 144
    new-instance v2, Ljava/util/LinkedHashMap;

    const/4 v3, 0x0

    const/high16 v4, 0x3f400000    # 0.75f

    const/4 v5, 0x1

    invoke-direct {v2, v3, v4, v5}, Ljava/util/LinkedHashMap;-><init>(IFZ)V

    iput-object v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    .line 149
    new-instance v2, Ljava/util/concurrent/ThreadPoolExecutor;

    sget-object v11, Ljava/util/concurrent/TimeUnit;->SECONDS:Ljava/util/concurrent/TimeUnit;

    new-instance v12, Ljava/util/concurrent/LinkedBlockingQueue;

    invoke-direct {v12}, Ljava/util/concurrent/LinkedBlockingQueue;-><init>()V

    const/4 v7, 0x0

    const/4 v8, 0x1

    const-wide/16 v9, 0x3c

    move-object v6, v2

    invoke-direct/range {v6 .. v12}, Ljava/util/concurrent/ThreadPoolExecutor;-><init>(IIJLjava/util/concurrent/TimeUnit;Ljava/util/concurrent/BlockingQueue;)V

    iput-object v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->executorService:Ljava/util/concurrent/ExecutorService;

    const-wide/16 v2, 0x0

    .line 151
    iput-wide v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    .line 154
    new-instance v4, Lnet/gogame/gowrap/support/DiskLruCache$1;

    invoke-direct {v4, p0}, Lnet/gogame/gowrap/support/DiskLruCache$1;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;)V

    iput-object v4, v0, Lnet/gogame/gowrap/support/DiskLruCache;->cleanupCallable:Ljava/util/concurrent/Callable;

    .line 175
    iput-wide v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->nextSequenceNumber:J

    .line 178
    iput-object v1, v0, Lnet/gogame/gowrap/support/DiskLruCache;->directory:Ljava/io/File;

    move v2, p2

    .line 179
    iput v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->appVersion:I

    .line 180
    new-instance v2, Ljava/io/File;

    const-string v3, "journal"

    invoke-direct {v2, p1, v3}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    iput-object v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    .line 181
    new-instance v2, Ljava/io/File;

    const-string v3, "journal.tmp"

    invoke-direct {v2, p1, v3}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    iput-object v2, v0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFileTmp:Ljava/io/File;

    move/from16 v1, p3

    .line 182
    iput v1, v0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    move-wide/from16 v1, p4

    .line 183
    iput-wide v1, v0, Lnet/gogame/gowrap/support/DiskLruCache;->maxSize:J

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/support/DiskLruCache;)Ljava/io/Writer;
    .locals 0

    .line 84
    iget-object p0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/gowrap/support/DiskLruCache;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 84
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->trimToSize()V

    return-void
.end method

.method static synthetic access$1500(Lnet/gogame/gowrap/support/DiskLruCache;Ljava/lang/String;J)Lnet/gogame/gowrap/support/DiskLruCache$Editor;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 84
    invoke-direct {p0, p1, p2, p3}, Lnet/gogame/gowrap/support/DiskLruCache;->edit(Ljava/lang/String;J)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$1600(Ljava/io/InputStream;)Ljava/lang/String;
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 84
    invoke-static {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->inputStreamToString(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$1800()Ljava/nio/charset/Charset;
    .locals 1

    .line 84
    sget-object v0, Lnet/gogame/gowrap/support/DiskLruCache;->UTF_8:Ljava/nio/charset/Charset;

    return-object v0
.end method

.method static synthetic access$1900(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 84
    invoke-direct {p0, p1, p2}, Lnet/gogame/gowrap/support/DiskLruCache;->completeEdit(Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V

    return-void
.end method

.method static synthetic access$200(Lnet/gogame/gowrap/support/DiskLruCache;)Z
    .locals 0

    .line 84
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->journalRebuildRequired()Z

    move-result p0

    return p0
.end method

.method static synthetic access$2100(Lnet/gogame/gowrap/support/DiskLruCache;)I
    .locals 0

    .line 84
    iget p0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    return p0
.end method

.method static synthetic access$2200(Lnet/gogame/gowrap/support/DiskLruCache;)Ljava/io/File;
    .locals 0

    .line 84
    iget-object p0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->directory:Ljava/io/File;

    return-object p0
.end method

.method static synthetic access$300(Lnet/gogame/gowrap/support/DiskLruCache;)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 84
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->rebuildJournal()V

    return-void
.end method

.method static synthetic access$402(Lnet/gogame/gowrap/support/DiskLruCache;I)I
    .locals 0

    .line 84
    iput p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    return p1
.end method

.method private checkNotClosed()V
    .locals 2

    .line 656
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    if-eqz v0, :cond_0

    return-void

    .line 657
    :cond_0
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "cache is closed"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public static closeQuietly(Ljava/io/Closeable;)V
    .locals 0

    if-eqz p0, :cond_0

    .line 255
    :try_start_0
    invoke-interface {p0}, Ljava/io/Closeable;->close()V
    :try_end_0
    .catch Ljava/lang/RuntimeException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    goto :goto_0

    :catch_0
    move-exception p0

    .line 257
    throw p0

    :catch_1
    :cond_0
    :goto_0
    return-void
.end method

.method private declared-synchronized completeEdit(Lnet/gogame/gowrap/support/DiskLruCache$Editor;Z)V
    .locals 9
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 555
    :try_start_0
    invoke-static {p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->access$1400(Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    move-result-object v0

    .line 556
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v1

    if-ne v1, p1, :cond_9

    const/4 v1, 0x0

    if-eqz p2, :cond_1

    .line 561
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$600(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Z

    move-result v2

    if-nez v2, :cond_1

    const/4 v2, 0x0

    .line 562
    :goto_0
    iget v3, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v2, v3, :cond_1

    .line 563
    invoke-virtual {v0, v2}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getDirtyFile(I)Ljava/io/File;

    move-result-object v3

    invoke-virtual {v3}, Ljava/io/File;->exists()Z

    move-result v3

    if-eqz v3, :cond_0

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 564
    :cond_0
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->abort()V

    .line 565
    new-instance p1, Ljava/lang/IllegalStateException;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "edit didn\'t create file "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 570
    :cond_1
    :goto_1
    iget p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v1, p1, :cond_4

    .line 571
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getDirtyFile(I)Ljava/io/File;

    move-result-object p1

    if-eqz p2, :cond_2

    .line 573
    invoke-virtual {p1}, Ljava/io/File;->exists()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 574
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getCleanFile(I)Ljava/io/File;

    move-result-object v2

    .line 575
    invoke-virtual {p1, v2}, Ljava/io/File;->renameTo(Ljava/io/File;)Z

    .line 576
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1000(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)[J

    move-result-object p1

    aget-wide v3, p1, v1

    .line 577
    invoke-virtual {v2}, Ljava/io/File;->length()J

    move-result-wide v5

    .line 578
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1000(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)[J

    move-result-object p1

    aput-wide v5, p1, v1

    .line 579
    iget-wide v7, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    const/4 p1, 0x0

    sub-long/2addr v7, v3

    add-long/2addr v7, v5

    iput-wide v7, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    goto :goto_2

    .line 582
    :cond_2
    invoke-static {p1}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteIfExists(Ljava/io/File;)V

    :cond_3
    :goto_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_1

    .line 586
    :cond_4
    iget p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    const/4 v1, 0x1

    add-int/2addr p1, v1

    iput p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    const/4 p1, 0x0

    .line 587
    invoke-static {v0, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$702(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 588
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$600(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Z

    move-result p1

    or-int/2addr p1, p2

    const/16 v2, 0xa

    if-eqz p1, :cond_5

    .line 589
    invoke-static {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$602(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Z)Z

    .line 590
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "CLEAN "

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getLengths()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    if-eqz p2, :cond_6

    .line 592
    iget-wide p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->nextSequenceNumber:J

    const-wide/16 v1, 0x1

    add-long/2addr v1, p1

    iput-wide v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->nextSequenceNumber:J

    invoke-static {v0, p1, p2}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1202(Lnet/gogame/gowrap/support/DiskLruCache$Entry;J)J

    goto :goto_3

    .line 595
    :cond_5
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 596
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "REMOVE "

    invoke-virtual {p2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v2}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    .line 599
    :cond_6
    :goto_3
    iget-wide p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    iget-wide v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->maxSize:J

    cmp-long v2, p1, v0

    if-gtz v2, :cond_7

    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->journalRebuildRequired()Z

    move-result p1

    if-eqz p1, :cond_8

    .line 600
    :cond_7
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->executorService:Ljava/util/concurrent/ExecutorService;

    iget-object p2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->cleanupCallable:Ljava/util/concurrent/Callable;

    invoke-interface {p1, p2}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 602
    :cond_8
    monitor-exit p0

    return-void

    .line 557
    :cond_9
    :try_start_1
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :catchall_0
    move-exception p1

    .line 554
    monitor-exit p0

    throw p1
.end method

.method private static copyOfRange([Ljava/lang/Object;II)[Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">([TT;II)[TT;"
        }
    .end annotation

    .line 189
    array-length v0, p0

    if-gt p1, p2, :cond_1

    if-ltz p1, :cond_0

    if-gt p1, v0, :cond_0

    sub-int/2addr p2, p1

    sub-int/2addr v0, p1

    .line 197
    invoke-static {p2, v0}, Ljava/lang/Math;->min(II)I

    move-result v0

    .line 199
    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getComponentType()Ljava/lang/Class;

    move-result-object v1

    invoke-static {v1, p2}, Ljava/lang/reflect/Array;->newInstance(Ljava/lang/Class;I)Ljava/lang/Object;

    move-result-object p2

    check-cast p2, [Ljava/lang/Object;

    const/4 v1, 0x0

    .line 200
    invoke-static {p0, p1, p2, v1, v0}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    return-object p2

    .line 194
    :cond_0
    new-instance p0, Ljava/lang/ArrayIndexOutOfBoundsException;

    invoke-direct {p0}, Ljava/lang/ArrayIndexOutOfBoundsException;-><init>()V

    throw p0

    .line 191
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    invoke-direct {p0}, Ljava/lang/IllegalArgumentException;-><init>()V

    throw p0
.end method

.method public static deleteContents(Ljava/io/File;)V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 268
    invoke-virtual {p0}, Ljava/io/File;->listFiles()[Ljava/io/File;

    move-result-object v0

    if-eqz v0, :cond_3

    .line 272
    array-length p0, v0

    const/4 v1, 0x0

    :goto_0
    if-ge v1, p0, :cond_2

    aget-object v2, v0, v1

    .line 273
    invoke-virtual {v2}, Ljava/io/File;->isDirectory()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 274
    invoke-static {v2}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteContents(Ljava/io/File;)V

    .line 276
    :cond_0
    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    move-result v3

    if-eqz v3, :cond_1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 277
    :cond_1
    new-instance p0, Ljava/io/IOException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "failed to delete file: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p0

    :cond_2
    return-void

    .line 270
    :cond_3
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "not a directory: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-direct {v0, p0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private static deleteIfExists(Ljava/io/File;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 332
    invoke-virtual {p0}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-virtual {p0}, Ljava/io/File;->delete()Z

    move-result p0

    if-eqz p0, :cond_0

    goto :goto_0

    .line 333
    :cond_0
    new-instance p0, Ljava/io/IOException;

    invoke-direct {p0}, Ljava/io/IOException;-><init>()V

    throw p0

    :cond_1
    :goto_0
    return-void
.end method

.method private declared-synchronized edit(Ljava/lang/String;J)Lnet/gogame/gowrap/support/DiskLruCache$Editor;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 507
    :try_start_0
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->checkNotClosed()V

    .line 508
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/DiskLruCache;->validateKey(Ljava/lang/String;)V

    .line 509
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0, p1}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    const-wide/16 v1, -0x1

    cmp-long v3, p2, v1

    const/4 v1, 0x0

    if-eqz v3, :cond_1

    if-eqz v0, :cond_0

    .line 511
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1200(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)J

    move-result-wide v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    cmp-long v4, v2, p2

    if-eqz v4, :cond_1

    .line 512
    :cond_0
    monitor-exit p0

    return-object v1

    :cond_1
    if-nez v0, :cond_2

    .line 515
    :try_start_1
    new-instance v0, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-direct {v0, p0, p1, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Ljava/lang/String;Lnet/gogame/gowrap/support/DiskLruCache$1;)V

    .line 516
    iget-object p2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {p2, p1, v0}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 517
    :cond_2
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object p2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz p2, :cond_3

    .line 518
    monitor-exit p0

    return-object v1

    .line 521
    :cond_3
    :goto_0
    :try_start_2
    new-instance p2, Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    invoke-direct {p2, p0, v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$1;)V

    .line 522
    invoke-static {v0, p2}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$702(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 525
    iget-object p3, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "DIRTY "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 p1, 0xa

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p3, p1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    .line 526
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    invoke-virtual {p1}, Ljava/io/Writer;->flush()V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 527
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 506
    monitor-exit p0

    throw p1
.end method

.method private static inputStreamToString(Ljava/io/InputStream;)Ljava/lang/String;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 338
    new-instance v0, Ljava/io/InputStreamReader;

    sget-object v1, Lnet/gogame/gowrap/support/DiskLruCache;->UTF_8:Ljava/nio/charset/Charset;

    invoke-direct {v0, p0, v1}, Ljava/io/InputStreamReader;-><init>(Ljava/io/InputStream;Ljava/nio/charset/Charset;)V

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readFully(Ljava/io/Reader;)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private journalRebuildRequired()Z
    .locals 2

    .line 610
    iget v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    const/16 v1, 0x7d0

    if-lt v0, v1, :cond_0

    iget v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    .line 611
    invoke-virtual {v1}, Ljava/util/LinkedHashMap;->size()I

    move-result v1

    if-lt v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public static open(Ljava/io/File;IIJ)Lnet/gogame/gowrap/support/DiskLruCache;
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-wide/16 v0, 0x0

    cmp-long v2, p3, v0

    if-lez v2, :cond_2

    if-lez p2, :cond_1

    .line 302
    new-instance v0, Lnet/gogame/gowrap/support/DiskLruCache;

    move-object v3, v0

    move-object v4, p0

    move v5, p1

    move v6, p2

    move-wide v7, p3

    invoke-direct/range {v3 .. v8}, Lnet/gogame/gowrap/support/DiskLruCache;-><init>(Ljava/io/File;IIJ)V

    .line 303
    iget-object v1, v0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    invoke-virtual {v1}, Ljava/io/File;->exists()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 305
    :try_start_0
    invoke-direct {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readJournal()V

    .line 306
    invoke-direct {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->processJournal()V

    .line 307
    new-instance v1, Ljava/io/BufferedWriter;

    new-instance v2, Ljava/io/FileWriter;

    iget-object v3, v0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    const/4 v4, 0x1

    invoke-direct {v2, v3, v4}, Ljava/io/FileWriter;-><init>(Ljava/io/File;Z)V

    const/16 v3, 0x2000

    invoke-direct {v1, v2, v3}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;I)V

    iput-object v1, v0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    .line 313
    :catch_0
    invoke-virtual {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->delete()V

    .line 318
    :cond_0
    invoke-virtual {p0}, Ljava/io/File;->mkdirs()Z

    .line 319
    new-instance v0, Lnet/gogame/gowrap/support/DiskLruCache;

    move-object v4, v0

    move-object v5, p0

    move v6, p1

    move v7, p2

    move-wide v8, p3

    invoke-direct/range {v4 .. v9}, Lnet/gogame/gowrap/support/DiskLruCache;-><init>(Ljava/io/File;IIJ)V

    .line 320
    invoke-direct {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->rebuildJournal()V

    return-object v0

    .line 298
    :cond_1
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string p1, "valueCount <= 0"

    invoke-direct {p0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 295
    :cond_2
    new-instance p0, Ljava/lang/IllegalArgumentException;

    const-string p1, "maxSize <= 0"

    invoke-direct {p0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0
.end method

.method private processJournal()V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 406
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFileTmp:Ljava/io/File;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteIfExists(Ljava/io/File;)V

    .line 407
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_3

    .line 408
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    .line 409
    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v2

    const/4 v3, 0x0

    if-nez v2, :cond_1

    .line 410
    :goto_1
    iget v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v3, v2, :cond_0

    .line 411
    iget-wide v4, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1000(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)[J

    move-result-object v2

    aget-wide v6, v2, v3

    add-long/2addr v4, v6

    iput-wide v4, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    add-int/lit8 v3, v3, 0x1

    goto :goto_1

    :cond_1
    const/4 v2, 0x0

    .line 414
    invoke-static {v1, v2}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$702(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 415
    :goto_2
    iget v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v3, v2, :cond_2

    .line 416
    invoke-virtual {v1, v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getCleanFile(I)Ljava/io/File;

    move-result-object v2

    invoke-static {v2}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteIfExists(Ljava/io/File;)V

    .line 417
    invoke-virtual {v1, v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getDirtyFile(I)Ljava/io/File;

    move-result-object v2

    invoke-static {v2}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteIfExists(Ljava/io/File;)V

    add-int/lit8 v3, v3, 0x1

    goto :goto_2

    .line 419
    :cond_2
    invoke-interface {v0}, Ljava/util/Iterator;->remove()V

    goto :goto_0

    :cond_3
    return-void
.end method

.method public static readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 231
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v1, 0x50

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 233
    :goto_0
    invoke-virtual {p0}, Ljava/io/InputStream;->read()I

    move-result v1

    const/4 v2, -0x1

    if-eq v1, v2, :cond_2

    const/16 v2, 0xa

    if-ne v1, v2, :cond_1

    .line 242
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->length()I

    move-result p0

    if-lez p0, :cond_0

    add-int/lit8 p0, p0, -0x1

    .line 243
    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->charAt(I)C

    move-result v1

    const/16 v2, 0xd

    if-ne v1, v2, :cond_0

    .line 244
    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->setLength(I)V

    .line 246
    :cond_0
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_1
    int-to-char v1, v1

    .line 240
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 235
    :cond_2
    new-instance p0, Ljava/io/EOFException;

    invoke-direct {p0}, Ljava/io/EOFException;-><init>()V

    throw p0
.end method

.method public static readFully(Ljava/io/Reader;)Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 209
    :try_start_0
    new-instance v0, Ljava/io/StringWriter;

    invoke-direct {v0}, Ljava/io/StringWriter;-><init>()V

    const/16 v1, 0x400

    .line 210
    new-array v1, v1, [C

    .line 212
    :goto_0
    invoke-virtual {p0, v1}, Ljava/io/Reader;->read([C)I

    move-result v2

    const/4 v3, -0x1

    if-eq v2, v3, :cond_0

    const/4 v3, 0x0

    .line 213
    invoke-virtual {v0, v1, v3, v2}, Ljava/io/StringWriter;->write([CII)V

    goto :goto_0

    .line 215
    :cond_0
    invoke-virtual {v0}, Ljava/io/StringWriter;->toString()Ljava/lang/String;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 217
    invoke-virtual {p0}, Ljava/io/Reader;->close()V

    return-object v0

    :catchall_0
    move-exception v0

    invoke-virtual {p0}, Ljava/io/Reader;->close()V

    .line 218
    throw v0
.end method

.method private readJournal()V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 342
    new-instance v0, Ljava/io/BufferedInputStream;

    new-instance v1, Ljava/io/FileInputStream;

    iget-object v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    invoke-direct {v1, v2}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    const/16 v2, 0x2000

    invoke-direct {v0, v1, v2}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;I)V

    .line 344
    :try_start_0
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v1

    .line 345
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v2

    .line 346
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v3

    .line 347
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v4

    .line 348
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "libcore.io.DiskLruCache"

    .line 349
    invoke-virtual {v6, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    const-string v6, "1"

    .line 350
    invoke-virtual {v6, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    iget v6, p0, Lnet/gogame/gowrap/support/DiskLruCache;->appVersion:I

    .line 351
    invoke-static {v6}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v6, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    iget v3, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    .line 352
    invoke-static {v3}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v3, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    const-string v3, ""

    .line 353
    invoke-virtual {v3, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v3, :cond_0

    .line 360
    :goto_0
    :try_start_1
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->readAsciiLine(Ljava/io/InputStream;)Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->readJournalLine(Ljava/lang/String;)V
    :try_end_1
    .catch Ljava/io/EOFException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    .line 366
    :catch_0
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->closeQuietly(Ljava/io/Closeable;)V

    return-void

    .line 354
    :cond_0
    :try_start_2
    new-instance v3, Ljava/io/IOException;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "unexpected journal header: ["

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", "

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "]"

    invoke-virtual {v6, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-direct {v3, v1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    :catchall_0
    move-exception v1

    .line 366
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->closeQuietly(Ljava/io/Closeable;)V

    .line 367
    throw v1
.end method

.method private readJournalLine(Ljava/lang/String;)V
    .locals 8
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-string v0, " "

    .line 371
    invoke-virtual {p1, v0}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v0

    .line 372
    array-length v1, v0

    const/4 v2, 0x2

    if-lt v1, v2, :cond_5

    const/4 v1, 0x1

    .line 376
    aget-object v3, v0, v1

    const/4 v4, 0x0

    .line 377
    aget-object v5, v0, v4

    const-string v6, "REMOVE"

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    array-length v5, v0

    if-ne v5, v2, :cond_0

    .line 378
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {p1, v3}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    .line 382
    :cond_0
    iget-object v5, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v5, v3}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    const/4 v6, 0x0

    if-nez v5, :cond_1

    .line 384
    new-instance v5, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    invoke-direct {v5, p0, v3, v6}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Ljava/lang/String;Lnet/gogame/gowrap/support/DiskLruCache$1;)V

    .line 385
    iget-object v7, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v7, v3, v5}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 388
    :cond_1
    aget-object v3, v0, v4

    const-string v7, "CLEAN"

    invoke-virtual {v3, v7}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    array-length v3, v0

    iget v7, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    add-int/2addr v7, v2

    if-ne v3, v7, :cond_2

    .line 389
    invoke-static {v5, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$602(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Z)Z

    .line 390
    invoke-static {v5, v6}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$702(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    .line 391
    array-length p1, v0

    invoke-static {v0, v2, p1}, Lnet/gogame/gowrap/support/DiskLruCache;->copyOfRange([Ljava/lang/Object;II)[Ljava/lang/Object;

    move-result-object p1

    check-cast p1, [Ljava/lang/String;

    invoke-static {v5, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$800(Lnet/gogame/gowrap/support/DiskLruCache$Entry;[Ljava/lang/String;)V

    goto :goto_0

    .line 392
    :cond_2
    aget-object v1, v0, v4

    const-string v3, "DIRTY"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_3

    array-length v1, v0

    if-ne v1, v2, :cond_3

    .line 393
    new-instance p1, Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    invoke-direct {p1, p0, v5, v6}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$1;)V

    invoke-static {v5, p1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$702(Lnet/gogame/gowrap/support/DiskLruCache$Entry;Lnet/gogame/gowrap/support/DiskLruCache$Editor;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    goto :goto_0

    .line 394
    :cond_3
    aget-object v1, v0, v4

    const-string v3, "READ"

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_4

    array-length v0, v0

    if-ne v0, v2, :cond_4

    :goto_0
    return-void

    .line 397
    :cond_4
    new-instance v0, Ljava/io/IOException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "unexpected journal line: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 373
    :cond_5
    new-instance v0, Ljava/io/IOException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "unexpected journal line: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method private declared-synchronized rebuildJournal()V
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 429
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    if-eqz v0, :cond_0

    .line 430
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    invoke-virtual {v0}, Ljava/io/Writer;->close()V

    .line 433
    :cond_0
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/FileWriter;

    iget-object v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFileTmp:Ljava/io/File;

    invoke-direct {v1, v2}, Ljava/io/FileWriter;-><init>(Ljava/io/File;)V

    const/16 v2, 0x2000

    invoke-direct {v0, v1, v2}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;I)V

    const-string v1, "libcore.io.DiskLruCache"

    .line 434
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "\n"

    .line 435
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "1"

    .line 436
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "\n"

    .line 437
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    .line 438
    iget v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->appVersion:I

    invoke-static {v1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "\n"

    .line 439
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    .line 440
    iget v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    invoke-static {v1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "\n"

    .line 441
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    const-string v1, "\n"

    .line 442
    invoke-virtual {v0, v1}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    .line 444
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v1}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    .line 445
    invoke-static {v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v4

    const/16 v5, 0xa

    if-eqz v4, :cond_1

    .line 446
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "DIRTY "

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    goto :goto_0

    .line 448
    :cond_1
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "CLEAN "

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1100(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getLengths()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v4, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v3}, Ljava/io/Writer;->write(Ljava/lang/String;)V

    goto :goto_0

    .line 452
    :cond_2
    invoke-virtual {v0}, Ljava/io/Writer;->close()V

    .line 453
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFileTmp:Ljava/io/File;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    invoke-virtual {v0, v1}, Ljava/io/File;->renameTo(Ljava/io/File;)Z

    .line 454
    new-instance v0, Ljava/io/BufferedWriter;

    new-instance v1, Ljava/io/FileWriter;

    iget-object v3, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalFile:Ljava/io/File;

    const/4 v4, 0x1

    invoke-direct {v1, v3, v4}, Ljava/io/FileWriter;-><init>(Ljava/io/File;Z)V

    invoke-direct {v0, v1, v2}, Ljava/io/BufferedWriter;-><init>(Ljava/io/Writer;I)V

    iput-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 455
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 428
    monitor-exit p0

    throw v0
.end method

.method private trimToSize()V
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 688
    :goto_0
    iget-wide v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    iget-wide v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->maxSize:J

    cmp-long v4, v0, v2

    if-lez v4, :cond_0

    .line 690
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0}, Ljava/util/LinkedHashMap;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 691
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    invoke-virtual {p0, v0}, Lnet/gogame/gowrap/support/DiskLruCache;->remove(Ljava/lang/String;)Z

    goto :goto_0

    :cond_0
    return-void
.end method

.method private validateKey(Ljava/lang/String;)V
    .locals 3

    const-string v0, " "

    .line 706
    invoke-virtual {p1, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "\n"

    invoke-virtual {p1, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "\r"

    invoke-virtual {p1, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 707
    :cond_0
    new-instance v0, Ljava/lang/IllegalArgumentException;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "keys must not contain spaces or newlines: \""

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "\""

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v0
.end method


# virtual methods
.method public declared-synchronized close()V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 674
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    .line 675
    monitor-exit p0

    return-void

    .line 677
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v1}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    invoke-virtual {v0}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    .line 678
    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 679
    invoke-static {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v1

    invoke-virtual {v1}, Lnet/gogame/gowrap/support/DiskLruCache$Editor;->abort()V

    goto :goto_0

    .line 682
    :cond_2
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->trimToSize()V

    .line 683
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    invoke-virtual {v0}, Ljava/io/Writer;->close()V

    const/4 v0, 0x0

    .line 684
    iput-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 685
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 673
    monitor-exit p0

    throw v0
.end method

.method public delete()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 701
    invoke-virtual {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->close()V

    .line 702
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->directory:Ljava/io/File;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache;->deleteContents(Ljava/io/File;)V

    return-void
.end method

.method public edit(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const-wide/16 v0, -0x1

    .line 503
    invoke-direct {p0, p1, v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache;->edit(Ljava/lang/String;J)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized flush()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 665
    :try_start_0
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->checkNotClosed()V

    .line 666
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->trimToSize()V

    .line 667
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    invoke-virtual {v0}, Ljava/io/Writer;->flush()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 668
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 664
    monitor-exit p0

    throw v0
.end method

.method public declared-synchronized get(Ljava/lang/String;)Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;
    .locals 10
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 463
    :try_start_0
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->checkNotClosed()V

    .line 464
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/DiskLruCache;->validateKey(Ljava/lang/String;)V

    .line 465
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0, p1}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/support/DiskLruCache$Entry;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    .line 467
    monitor-exit p0

    return-object v1

    .line 470
    :cond_0
    :try_start_1
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$600(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Z

    move-result v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-nez v2, :cond_1

    .line 471
    monitor-exit p0

    return-object v1

    .line 479
    :cond_1
    :try_start_2
    iget v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    new-array v8, v2, [Ljava/io/InputStream;
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    const/4 v2, 0x0

    .line 481
    :goto_0
    :try_start_3
    iget v3, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v2, v3, :cond_2

    .line 482
    new-instance v3, Ljava/io/FileInputStream;

    invoke-virtual {v0, v2}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getCleanFile(I)Ljava/io/File;

    move-result-object v4

    invoke-direct {v3, v4}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    aput-object v3, v8, v2
    :try_end_3
    .catch Ljava/io/FileNotFoundException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 489
    :cond_2
    :try_start_4
    iget v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    add-int/lit8 v1, v1, 0x1

    iput v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    .line 490
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "READ "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v3, 0xa

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/io/Writer;->append(Ljava/lang/CharSequence;)Ljava/io/Writer;

    .line 491
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->journalRebuildRequired()Z

    move-result v1

    if-eqz v1, :cond_3

    .line 492
    iget-object v1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->executorService:Ljava/util/concurrent/ExecutorService;

    iget-object v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->cleanupCallable:Ljava/util/concurrent/Callable;

    invoke-interface {v1, v2}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;

    .line 495
    :cond_3
    new-instance v1, Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1200(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)J

    move-result-wide v6

    const/4 v9, 0x0

    move-object v3, v1

    move-object v4, p0

    move-object v5, p1

    invoke-direct/range {v3 .. v9}, Lnet/gogame/gowrap/support/DiskLruCache$Snapshot;-><init>(Lnet/gogame/gowrap/support/DiskLruCache;Ljava/lang/String;J[Ljava/io/InputStream;Lnet/gogame/gowrap/support/DiskLruCache$1;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    monitor-exit p0

    return-object v1

    .line 486
    :catch_0
    monitor-exit p0

    return-object v1

    :catchall_0
    move-exception p1

    .line 462
    monitor-exit p0

    throw p1
.end method

.method public getDirectory()Ljava/io/File;
    .locals 1

    .line 534
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->directory:Ljava/io/File;

    return-object v0
.end method

.method public isClosed()Z
    .locals 1

    .line 652
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public maxSize()J
    .locals 2

    .line 542
    iget-wide v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->maxSize:J

    return-wide v0
.end method

.method public declared-synchronized remove(Ljava/lang/String;)Z
    .locals 7
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    monitor-enter p0

    .line 621
    :try_start_0
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->checkNotClosed()V

    .line 622
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/support/DiskLruCache;->validateKey(Ljava/lang/String;)V

    .line 623
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0, p1}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lnet/gogame/gowrap/support/DiskLruCache$Entry;

    const/4 v1, 0x0

    if-eqz v0, :cond_4

    .line 624
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$700(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)Lnet/gogame/gowrap/support/DiskLruCache$Editor;

    move-result-object v2

    if-eqz v2, :cond_0

    goto :goto_1

    .line 628
    :cond_0
    :goto_0
    iget v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->valueCount:I

    if-ge v1, v2, :cond_2

    .line 629
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->getCleanFile(I)Ljava/io/File;

    move-result-object v2

    .line 630
    invoke-virtual {v2}, Ljava/io/File;->delete()Z

    move-result v3

    if-eqz v3, :cond_1

    .line 633
    iget-wide v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1000(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)[J

    move-result-object v4

    aget-wide v5, v4, v1

    const/4 v4, 0x0

    sub-long/2addr v2, v5

    iput-wide v2, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J

    .line 634
    invoke-static {v0}, Lnet/gogame/gowrap/support/DiskLruCache$Entry;->access$1000(Lnet/gogame/gowrap/support/DiskLruCache$Entry;)[J

    move-result-object v2

    const-wide/16 v3, 0x0

    aput-wide v3, v2, v1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 631
    :cond_1
    new-instance p1, Ljava/io/IOException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "failed to delete "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1

    .line 637
    :cond_2
    iget v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    const/4 v1, 0x1

    add-int/2addr v0, v1

    iput v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->redundantOpCount:I

    .line 638
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->journalWriter:Ljava/io/Writer;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "REMOVE "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const/16 v3, 0xa

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/io/Writer;->append(Ljava/lang/CharSequence;)Ljava/io/Writer;

    .line 639
    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->lruEntries:Ljava/util/LinkedHashMap;

    invoke-virtual {v0, p1}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 641
    invoke-direct {p0}, Lnet/gogame/gowrap/support/DiskLruCache;->journalRebuildRequired()Z

    move-result p1

    if-eqz p1, :cond_3

    .line 642
    iget-object p1, p0, Lnet/gogame/gowrap/support/DiskLruCache;->executorService:Ljava/util/concurrent/ExecutorService;

    iget-object v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->cleanupCallable:Ljava/util/concurrent/Callable;

    invoke-interface {p1, v0}, Ljava/util/concurrent/ExecutorService;->submit(Ljava/util/concurrent/Callable;)Ljava/util/concurrent/Future;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 645
    :cond_3
    monitor-exit p0

    return v1

    .line 625
    :cond_4
    :goto_1
    monitor-exit p0

    return v1

    :catchall_0
    move-exception p1

    .line 620
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized size()J
    .locals 2

    monitor-enter p0

    .line 551
    :try_start_0
    iget-wide v0, p0, Lnet/gogame/gowrap/support/DiskLruCache;->size:J
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-wide v0

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method
