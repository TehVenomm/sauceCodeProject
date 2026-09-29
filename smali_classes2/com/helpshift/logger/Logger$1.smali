.class Lcom/helpshift/logger/Logger$1;
.super Ljava/lang/Object;
.source "Logger.java"

# interfaces
.implements Ljava/util/concurrent/ThreadFactory;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/logger/Logger;->enableLogging(ZZ)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/logger/Logger;


# direct methods
.method constructor <init>(Lcom/helpshift/logger/Logger;)V
    .locals 0

    .line 124
    iput-object p1, p0, Lcom/helpshift/logger/Logger$1;->this$0:Lcom/helpshift/logger/Logger;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public newThread(Ljava/lang/Runnable;)Ljava/lang/Thread;
    .locals 2

    .line 127
    new-instance v0, Ljava/lang/Thread;

    const-string v1, "HS-Logger"

    invoke-direct {v0, p1, v1}, Ljava/lang/Thread;-><init>(Ljava/lang/Runnable;Ljava/lang/String;)V

    return-object v0
.end method
