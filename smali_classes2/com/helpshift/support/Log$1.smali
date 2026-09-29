.class final Lcom/helpshift/support/Log$1;
.super Ljava/lang/Object;
.source "Log.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/Log;->appendLog(Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$debugLogDTO:Lcom/helpshift/meta/dto/DebugLogDTO;


# direct methods
.method constructor <init>(Lcom/helpshift/meta/dto/DebugLogDTO;)V
    .locals 0

    .line 52
    iput-object p1, p0, Lcom/helpshift/support/Log$1;->val$debugLogDTO:Lcom/helpshift/meta/dto/DebugLogDTO;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 56
    :try_start_0
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getMetaDataDM()Lcom/helpshift/meta/MetaDataDM;

    move-result-object v0

    iget-object v1, p0, Lcom/helpshift/support/Log$1;->val$debugLogDTO:Lcom/helpshift/meta/dto/DebugLogDTO;

    invoke-virtual {v0, v1}, Lcom/helpshift/meta/MetaDataDM;->addDebugLog(Lcom/helpshift/meta/dto/DebugLogDTO;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method
