.class Lnet/gogame/gopay/vip/tape2/a$a;
.super Ljava/io/ByteArrayOutputStream;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/tape2/a;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "a"
.end annotation


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 98
    invoke-direct {p0}, Ljava/io/ByteArrayOutputStream;-><init>()V

    return-void
.end method


# virtual methods
.method public a()[B
    .locals 1

    .line 106
    iget-object v0, p0, Lnet/gogame/gopay/vip/tape2/a$a;->buf:[B

    return-object v0
.end method
