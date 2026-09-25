.class Lcom/helpshift/common/AutoRetryFailedEventDM$4;
.super Lcom/helpshift/common/domain/F;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;->onUserAuthenticationUpdated()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;


# direct methods
.method constructor <init>(Lcom/helpshift/common/AutoRetryFailedEventDM;)V
    .locals 0

    .line 182
    iput-object p1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$4;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 185
    iget-object v0, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$4;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    iget-object v1, p0, Lcom/helpshift/common/AutoRetryFailedEventDM$4;->this$0:Lcom/helpshift/common/AutoRetryFailedEventDM;

    invoke-static {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->access$000(Lcom/helpshift/common/AutoRetryFailedEventDM;)Ljava/util/Set;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/AutoRetryFailedEventDM;->retryFailedApis(Ljava/util/Set;)V

    return-void
.end method
