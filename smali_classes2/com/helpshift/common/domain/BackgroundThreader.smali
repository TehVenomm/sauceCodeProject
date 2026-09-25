.class Lcom/helpshift/common/domain/BackgroundThreader;
.super Ljava/lang/Object;
.source "BackgroundThreader.java"

# interfaces
.implements Lcom/helpshift/common/domain/Threader;


# static fields
.field private static final TAG:Ljava/lang/String; = "Helpshift_CoreBgTh"


# instance fields
.field final service:Ljava/util/concurrent/ExecutorService;


# direct methods
.method constructor <init>(Ljava/util/concurrent/ExecutorService;)V
    .locals 0

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 27
    iput-object p1, p0, Lcom/helpshift/common/domain/BackgroundThreader;->service:Ljava/util/concurrent/ExecutorService;

    return-void
.end method


# virtual methods
.method public thread(Lcom/helpshift/common/domain/F;)Lcom/helpshift/common/domain/F;
    .locals 1

    .line 32
    new-instance v0, Lcom/helpshift/common/domain/BackgroundThreader$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/common/domain/BackgroundThreader$1;-><init>(Lcom/helpshift/common/domain/BackgroundThreader;Lcom/helpshift/common/domain/F;)V

    return-object v0
.end method
