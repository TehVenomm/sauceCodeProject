.class Lnet/gogame/gopay/vip/tape2/QueueFile$a;
.super Ljava/lang/Object;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gopay/vip/tape2/QueueFile;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = "a"
.end annotation


# static fields
.field static final a:Lnet/gogame/gopay/vip/tape2/QueueFile$a;


# instance fields
.field final b:J

.field final c:I


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .line 704
    new-instance v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;

    const-wide/16 v1, 0x0

    const/4 v3, 0x0

    invoke-direct {v0, v1, v2, v3}, Lnet/gogame/gopay/vip/tape2/QueueFile$a;-><init>(JI)V

    sput-object v0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->a:Lnet/gogame/gopay/vip/tape2/QueueFile$a;

    return-void
.end method

.method constructor <init>(JI)V
    .locals 0

    .line 721
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 722
    iput-wide p1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->b:J

    .line 723
    iput p3, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->c:I

    return-void
.end method


# virtual methods
.method public toString()Ljava/lang/String;
    .locals 3

    .line 727
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "[position="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-wide v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->b:J

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v1, ", length="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v1, p0, Lnet/gogame/gopay/vip/tape2/QueueFile$a;->c:I

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, "]"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
