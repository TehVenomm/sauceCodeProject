.class final Lnet/gogame/gopay/vip/tape2/b;
.super Lnet/gogame/gopay/vip/tape2/ObjectQueue;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/tape2/b$a;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Lnet/gogame/gopay/vip/tape2/ObjectQueue<",
        "TT;>;"
    }
.end annotation


# instance fields
.field final a:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList<",
            "TT;>;"
        }
    .end annotation
.end field

.field b:I

.field private c:Z


# direct methods
.method constructor <init>()V
    .locals 1

    .line 22
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;-><init>()V

    const/4 v0, 0x0

    .line 19
    iput v0, p0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    .line 23
    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    return-void
.end method

.method static synthetic a(Lnet/gogame/gopay/vip/tape2/b;)Z
    .locals 0

    .line 11
    iget-boolean p0, p0, Lnet/gogame/gopay/vip/tape2/b;->c:Z

    return p0
.end method


# virtual methods
.method public add(Ljava/lang/Object;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 31
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/tape2/b;->c:Z

    if-nez v0, :cond_0

    .line 32
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    .line 33
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    invoke-virtual {v0, p1}, Ljava/util/LinkedList;->add(Ljava/lang/Object;)Z

    return-void

    .line 31
    :cond_0
    new-instance p1, Ljava/io/IOException;

    const-string v0, "closed"

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public close()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x1

    .line 64
    iput-boolean v0, p0, Lnet/gogame/gopay/vip/tape2/b;->c:Z

    return-void
.end method

.method public file()Ljava/io/File;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method

.method public iterator()Ljava/util/Iterator;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Iterator<",
            "TT;>;"
        }
    .end annotation

    .line 60
    new-instance v0, Lnet/gogame/gopay/vip/tape2/b$a;

    invoke-direct {v0, p0}, Lnet/gogame/gopay/vip/tape2/b$a;-><init>(Lnet/gogame/gopay/vip/tape2/b;)V

    return-object v0
.end method

.method public peek()Ljava/lang/Object;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 37
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/tape2/b;->c:Z

    if-nez v0, :cond_0

    .line 38
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->peek()Ljava/lang/Object;

    move-result-object v0

    return-object v0

    .line 37
    :cond_0
    new-instance v0, Ljava/io/IOException;

    const-string v1, "closed"

    invoke-direct {v0, v1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v0
.end method

.method public remove(I)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 46
    iget-boolean v0, p0, Lnet/gogame/gopay/vip/tape2/b;->c:Z

    if-nez v0, :cond_1

    .line 47
    iget v0, p0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    add-int/lit8 v0, v0, 0x1

    iput v0, p0, Lnet/gogame/gopay/vip/tape2/b;->b:I

    const/4 v0, 0x0

    :goto_0
    if-ge v0, p1, :cond_0

    .line 49
    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->remove()Ljava/lang/Object;

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_0
    return-void

    .line 46
    :cond_1
    new-instance p1, Ljava/io/IOException;

    const-string v0, "closed"

    invoke-direct {p1, v0}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public size()I
    .locals 1

    .line 42
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/b;->a:Ljava/util/LinkedList;

    invoke-virtual {v0}, Ljava/util/LinkedList;->size()I

    move-result v0

    return v0
.end method
