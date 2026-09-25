.class public abstract Lnet/gogame/gopay/vip/tape2/ObjectQueue;
.super Ljava/lang/Object;
.source "SourceFile"

# interfaces
.implements Ljava/io/Closeable;
.implements Ljava/lang/Iterable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Ljava/lang/Object;",
        ">",
        "Ljava/lang/Object;",
        "Ljava/io/Closeable;",
        "Ljava/lang/Iterable<",
        "TT;>;"
    }
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static create(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;)Lnet/gogame/gopay/vip/tape2/ObjectQueue;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">(",
            "Ljava/io/File;",
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter<",
            "TT;>;)",
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue<",
            "TT;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 17
    new-instance v0, Lnet/gogame/gopay/vip/tape2/a;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gopay/vip/tape2/a;-><init>(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;)V

    return-object v0
.end method

.method public static createInMemory()Lnet/gogame/gopay/vip/tape2/ObjectQueue;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "<T:",
            "Ljava/lang/Object;",
            ">()",
            "Lnet/gogame/gopay/vip/tape2/ObjectQueue<",
            "TT;>;"
        }
    .end annotation

    .line 25
    new-instance v0, Lnet/gogame/gopay/vip/tape2/b;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/tape2/b;-><init>()V

    return-object v0
.end method


# virtual methods
.method public abstract add(Ljava/lang/Object;)V
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

    .line 65
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->size()I

    move-result v0

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->peek(I)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method public clear()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 78
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->size()I

    move-result v0

    invoke-virtual {p0, v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->remove(I)V

    return-void
.end method

.method public abstract file()Ljava/io/File;
.end method

.method public isEmpty()Z
    .locals 1

    .line 36
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->size()I

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public abstract peek()Ljava/lang/Object;
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
.end method

.method public peek(I)Ljava/util/List;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(I)",
            "Ljava/util/List<",
            "TT;>;"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 54
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->size()I

    move-result v0

    invoke-static {p1, v0}, Ljava/lang/Math;->min(II)I

    move-result p1

    .line 55
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0, p1}, Ljava/util/ArrayList;-><init>(I)V

    .line 56
    invoke-virtual {p0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->iterator()Ljava/util/Iterator;

    move-result-object v1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, p1, :cond_0

    .line 58
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 60
    :cond_0
    invoke-static {v0}, Ljava/util/Collections;->unmodifiableList(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method public remove()V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    const/4 v0, 0x1

    .line 70
    invoke-virtual {p0, v0}, Lnet/gogame/gopay/vip/tape2/ObjectQueue;->remove(I)V

    return-void
.end method

.method public abstract remove(I)V
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation
.end method

.method public abstract size()I
.end method
