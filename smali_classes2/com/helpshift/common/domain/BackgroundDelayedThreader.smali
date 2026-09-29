.class Lcom/helpshift/common/domain/BackgroundDelayedThreader;
.super Ljava/lang/Object;
.source "BackgroundDelayedThreader.java"

# interfaces
.implements Lcom/helpshift/common/domain/DelayedThreader;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_CoreDelayTh"


# instance fields
.field final service:Ljava/util/concurrent/ScheduledExecutorService;


# direct methods
.method constructor <init>(Ljava/util/concurrent/ScheduledExecutorService;)V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 19
    iput-object p1, p0, Lcom/helpshift/common/domain/BackgroundDelayedThreader;->service:Ljava/util/concurrent/ScheduledExecutorService;

    return-void
.end method


# virtual methods
.method public thread(Lcom/helpshift/common/domain/F;J)Lcom/helpshift/common/domain/F;
    .locals 1

    .line 24
    new-instance v0, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;

    invoke-direct {v0, p0, p1, p2, p3}, Lcom/helpshift/common/domain/BackgroundDelayedThreader$1;-><init>(Lcom/helpshift/common/domain/BackgroundDelayedThreader;Lcom/helpshift/common/domain/F;J)V

    return-object v0
.end method
