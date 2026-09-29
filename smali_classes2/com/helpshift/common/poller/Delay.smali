.class public Lcom/helpshift/common/poller/Delay;
.super Ljava/lang/Object;
.source "Delay.java"


# instance fields
.field public final delay:J

.field public final timeUnit:Ljava/util/concurrent/TimeUnit;


# direct methods
.method private constructor <init>(JLjava/util/concurrent/TimeUnit;)V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 11
    iput-wide p1, p0, Lcom/helpshift/common/poller/Delay;->delay:J

    .line 12
    iput-object p3, p0, Lcom/helpshift/common/poller/Delay;->timeUnit:Ljava/util/concurrent/TimeUnit;

    return-void
.end method

.method public static of(JLjava/util/concurrent/TimeUnit;)Lcom/helpshift/common/poller/Delay;
    .locals 1

    .line 16
    new-instance v0, Lcom/helpshift/common/poller/Delay;

    invoke-direct {v0, p0, p1, p2}, Lcom/helpshift/common/poller/Delay;-><init>(JLjava/util/concurrent/TimeUnit;)V

    return-object v0
.end method


# virtual methods
.method public toString()Ljava/lang/String;
    .locals 3

    .line 21
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    iget-wide v1, p0, Lcom/helpshift/common/poller/Delay;->delay:J

    invoke-virtual {v0, v1, v2}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    const-string v1, " "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/helpshift/common/poller/Delay;->timeUnit:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
