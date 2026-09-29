.class final Lnet/gogame/gopay/vip/tape2/a$b;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/util/Iterator;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/tape2/a;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x12
    name = "b"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Iterator<",
        "TT;>;"
    }
.end annotation


# instance fields
.field final a:Ljava/util/Iterator;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Iterator<",
            "[B>;"
        }
    .end annotation
.end field

.field final synthetic b:Lnet/gogame/gopay/vip/tape2/a;


# direct methods
.method constructor <init>(Lnet/gogame/gopay/vip/tape2/a;Ljava/util/Iterator;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Iterator<",
            "[B>;)V"
        }
    .end annotation

    .line 73
    iput-object p1, p0, Lnet/gogame/gopay/vip/tape2/a$b;->b:Lnet/gogame/gopay/vip/tape2/a;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 74
    iput-object p2, p0, Lnet/gogame/gopay/vip/tape2/a$b;->a:Ljava/util/Iterator;

    return-void
.end method


# virtual methods
.method public hasNext()Z
    .locals 1

    .line 78
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a$b;->a:Ljava/util/Iterator;

    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    return v0
.end method

.method public next()Ljava/lang/Object;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()TT;"
        }
    .end annotation

    .line 82
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a$b;->a:Ljava/util/Iterator;

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [B

    .line 84
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gopay/vip/tape2/a$b;->b:Lnet/gogame/gopay/vip/tape2/a;

    iget-object v1, v1, Lnet/gogame/gopay/vip/tape2/a;->a:Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;

    invoke-interface {v1, v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;->from([B)Ljava/lang/Object;

    move-result-object v0
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    .line 86
    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "todo: throw a proper error"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1
.end method

.method public remove()V
    .locals 1

    .line 91
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a$b;->a:Ljava/util/Iterator;

    invoke-interface {v0}, Ljava/util/Iterator;->remove()V

    return-void
.end method
