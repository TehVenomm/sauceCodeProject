.class final Lnet/gogame/gopay/vip/tape2/b$a;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/util/Iterator;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/tape2/b;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x12
    name = "a"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Iterator<",
        "TT;>;"
    }
.end annotation


# instance fields
.field a:I

.field b:I

.field final synthetic c:Lnet/gogame/gopay/vip/tape2/b;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/vip/tape2/b;)V
    .locals 0

    .line 77
    iput-object p1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 p1, 0x0

    .line 69
    iput p1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    .line 75
    iget-object p1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    iget p1, p1, Lnet/gogame/gopay/vip/tape2/b;->b:I

    iput p1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->b:I

    return-void
.end method

.method private a()V
    .locals 2

    .line 116
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    iget v0, v0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    iget v1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->b:I

    if-ne v0, v1, :cond_0

    return-void

    :cond_0
    new-instance v0, Ljava/util/ConcurrentModificationException;

    invoke-direct {v0}, Ljava/util/ConcurrentModificationException;-><init>()V

    throw v0
.end method


# virtual methods
.method public hasNext()Z
    .locals 2

    .line 82
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/b$a;->a()V

    .line 84
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/tape2/b;->size()I

    move-result v1

    if-eq v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public next()Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    .line 88
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-static {v0}, Lnet/gogame/gopay/vip/tape2/b;->a(Lnet/gogame/gopay/vip/tape2/b;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 89
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/b$a;->a()V

    .line 91
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/tape2/b;->size()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 93
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    iget-object v0, v0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    iget v1, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    add-int/lit8 v2, v1, 0x1

    iput v2, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    invoke-virtual {v0, v1}, Ljava/util/LinkedList;->get(I)Ljava/lang/Object;

    move-result-object v0

    return-object v0

    .line 91
    :cond_0
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0

    .line 88
    :cond_1
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "closed"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public remove()V
    .locals 3

    .line 97
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-static {v0}, Lnet/gogame/gopay/vip/tape2/b;->a(Lnet/gogame/gopay/vip/tape2/b;)Z

    move-result v0

    if-nez v0, :cond_2

    .line 98
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/b$a;->a()V

    .line 100
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/b;->size()I

    move-result v0

    if-eqz v0, :cond_1

    .line 101
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    const/4 v1, 0x1

    if-ne v0, v1, :cond_0

    .line 106
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/b;->remove()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 111
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->c:Lnet/gogame/gopay/vip/tape2/b;

    iget v0, v0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->b:I

    .line 112
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    sub-int/2addr v0, v1

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/b$a;->a:I

    return-void

    :catch_0
    move-exception v0

    .line 108
    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "todo: throw a proper error"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    .line 102
    :cond_0
    new-instance v0, Ljava/lang/UnsupportedOperationException;

    const-string v1, "Removal is only permitted from the head."

    invoke-direct {v0, v1}, Ljava/lang/UnsupportedOperationException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 100
    :cond_1
    new-instance v0, Ljava/util/NoSuchElementException;

    invoke-direct {v0}, Ljava/util/NoSuchElementException;-><init>()V

    throw v0

    .line 97
    :cond_2
    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "closed"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0
.end method
