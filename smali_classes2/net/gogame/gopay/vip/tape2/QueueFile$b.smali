.class final Lnet/gogame/gopay/vip/tape2/QueueFile$b;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/util/Iterator;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/tape2/QueueFile;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x12
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Iterator<",
        "[B>;"
    }
.end annotation


# instance fields
.field a:I

.field b:I

.field final synthetic c:Lnet/gogame/gopay/vip/tape2/QueueFile;

.field private d:J


# direct methods
.method constructor <init>(Lnet/gogame/gopay/vip/tape2/QueueFile;)V
    .locals 2

    .line 548
    iput-object p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 537
    iput p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    .line 540
    iget-object p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-object p1, p1, Lnet/gogame/gopay/vip/tape2/QueueFile;->f:Lnet/gogame/gopay/vip/tape2/QueueFile$a;

    iget-wide v0, p1, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->b:J

    iput-wide v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->d:J

    .line 546
    iget-object p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget p1, p1, Lnet/gogame/gopay/vip/tape2/QueueFile;->g:I

    iput p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b:I

    return-void
.end method

.method private b()V
    .locals 2

    .line 552
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget v0, v0, Lnet/gogame/gopay/vip/tape2/QueueFile;->g:I

    iget v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b:I

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    new-instance v0, Ljava/util/ConcurrentModificationException;

    invoke-direct {v0}, Ljava/util/ConcurrentModificationException;-><init>()V

    throw v0
.end method


# virtual methods
.method public a()[B
    .locals 10

    .line 562
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-boolean v0, v0, Lnet/gogame/gopay/vip/tape2/QueueFile;->h:Z

    if-nez v0, :cond_2

    .line 563
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b()V

    .line 564
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_1

    .line 565
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget v1, v1, Lnet/gogame/gopay/vip/tape2/QueueFile;->e:I

    if-ge v0, v1, :cond_0

    .line 569
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-wide v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->d:J

    invoke-virtual {v0, v1, v2}, Lnet/gogame/gopay/vip/tape2/QueueFile;->a(J)Lnet/gogame/gopay/vip/tape2/QueueFile$a;

    move-result-object v0

    .line 570
    iget v1, v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->c:I

    new-array v1, v1, [B

    .line 571
    iget-object v2, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-wide v3, v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->b:J

    const-wide/16 v8, 0x4

    add-long/2addr v3, v8

    invoke-virtual {v2, v3, v4}, Lnet/gogame/gopay/vip/tape2/QueueFile;->b(J)J

    move-result-wide v2

    iput-wide v2, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->d:J

    .line 572
    iget-object v2, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-wide v3, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->d:J

    const/4 v6, 0x0

    iget v7, v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->c:I

    move-object v5, v1

    invoke-virtual/range {v2 .. v7}, Lnet/gogame/gopay/vip/tape2/QueueFile;->a(J[BII)V

    .line 575
    iget-object v2, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-wide v3, v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->b:J

    const/4 v5, 0x0

    add-long/2addr v3, v8

    iget v0, v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->c:I

    int-to-long v5, v0

    add-long/2addr v3, v5

    .line 576
    invoke-virtual {v2, v3, v4}, Lnet/gogame/gopay/vip/tape2/QueueFile;->b(J)J

    move-result-wide v2

    iput-wide v2, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->d:J

    .line 577
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v1

    :catch_0
    move-exception v0

    .line 582
    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "todo: throw a proper error"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    .line 565
    :cond_0
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0

    .line 564
    :cond_1
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0

    .line 562
    :cond_2
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "closed"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public hasNext()Z
    .locals 2

    .line 556
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-boolean v0, v0, Lnet/gogame/gopay/vip/tape2/QueueFile;->h:Z

    if-nez v0, :cond_1

    .line 557
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b()V

    .line 558
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget v1, v1, Lnet/gogame/gopay/vip/tape2/QueueFile;->e:I

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0

    .line 556
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "closed"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public synthetic next()Ljava/lang/Object;
    .locals 1

    .line 535
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a()[B

    move-result-object v0

    return-object v0
.end method

.method public remove()V
    .locals 3

    .line 587
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b()V

    .line 589
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_1

    .line 590
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    .line 595
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->remove()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 600
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->c:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget v0, v0, Lnet/gogame/gopay/vip/tape2/QueueFile;->g:I

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->b:I

    .line 601
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    sub-int/2addr v0, v1

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$b;->a:I

    return-void

    :catch_0
    move-exception v0

    .line 597
    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "todo: throw a proper error"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    .line 591
    :cond_0
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    const-string v1, "Removal is only permitted from the head."

    invoke-direct {v0, v1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 589
    :cond_1
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0
.end method
