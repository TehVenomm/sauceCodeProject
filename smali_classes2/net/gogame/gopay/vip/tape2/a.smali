.class final Lnet/gogame/gopay/vip/tape2/a;
.super Lnet/gogame/gopay/vip/tape2/ObjectQueue;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/tape2/a$a;,
        Lnet/gogame/gopay/vip/tape2/a$b;
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
.field final a:Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter<",
            "TT;>;"
        }
    .end annotation
.end field

.field private final b:Lnet/gogame/gopay/vip/tape2/QueueFile;

.field private final c:Lnet/gogame/gopay/vip/tape2/a$a;

.field private final d:Ljava/io/File;


# direct methods
.method constructor <init>(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/io/File;",
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter<",
            "TT;>;)V"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 19
    invoke-direct {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;-><init>()V

    .line 14
    new-instance v0, Lnet/gogame/gopay/vip/tape2/a$a;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/tape2/a$a;-><init>()V

    iput-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->c:Lnet/gogame/gopay/vip/tape2/a$a;

    .line 20
    iput-object p1, p0, Lnet/gogame/gopay/vip/tape2/a;->d:Ljava/io/File;

    .line 21
    iput-object p2, p0, Lnet/gogame/gopay/vip/tape2/a;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;

    .line 22
    new-instance p2, Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-direct {p2, p1}, Lnet/gogame/gopay/vip/tape2/QueueFile;-><init>(Ljava/io/File;)V

    iput-object p2, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    return-void
.end method


# virtual methods
.method public add(Ljava/lang/Object;)V
    .locals 3
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

    .line 34
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->c:Lnet/gogame/gopay/vip/tape2/a$a;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/a$a;->reset()V

    .line 35
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/a;->c:Lnet/gogame/gopay/vip/tape2/a$a;

    invoke-interface {v0, p1, v1}, Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;->toStream(Ljava/lang/Object;Ljava/io/OutputStream;)V

    .line 36
    iget-object p1, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->c:Lnet/gogame/gopay/vip/tape2/a$a;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/a$a;->a()[B

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/a;->c:Lnet/gogame/gopay/vip/tape2/a$a;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/tape2/a$a;->size()I

    move-result v1

    const/4 v2, 0x0

    invoke-virtual {p1, v0, v2, v1}, Lnet/gogame/gopay/vip/tape2/QueueFile;->add([BII)V

    return-void
.end method

.method public asList()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "TT;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 46
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/a;->size()I

    move-result v0

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/vip/tape2/a;->peek(I)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public close()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 54
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->close()V

    return-void
.end method

.method public file()Ljava/io/File;
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->d:Ljava/io/File;

    return-object v0
.end method

.method public iterator()Ljava/util/Iterator;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Iterator<",
            "TT;>;"
        }
    .end annotation

    .line 67
    new-instance v0, Lnet/gogame/gopay/vip/tape2/a$b;

    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v1}, Lnet/gogame/gopay/vip/tape2/QueueFile;->iterator()Ljava/util/Iterator;

    move-result-object v1

    invoke-direct {v0, p0, v1}, Lnet/gogame/gopay/vip/tape2/a$b;-><init>(Lnet/gogame/gopay/vip/tape2/a;Ljava/util/Iterator;)V

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

    .line 40
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->peek()[B

    move-result-object v0

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    .line 42
    :cond_0
    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/a;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;

    invoke-interface {v1, v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;->from([B)Ljava/lang/Object;

    move-result-object v0

    return-object v0
.end method

.method public remove(I)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 50
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0, p1}, Lnet/gogame/gopay/vip/tape2/QueueFile;->remove(I)V

    return-void
.end method

.method public size()I
    .locals 1

    .line 30
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a;->b:Lnet/gogame/gopay/vip/tape2/QueueFile;

    invoke-virtual {v0}, Lnet/gogame/gopay/vip/tape2/QueueFile;->size()I

    move-result v0

    return v0
.end method
