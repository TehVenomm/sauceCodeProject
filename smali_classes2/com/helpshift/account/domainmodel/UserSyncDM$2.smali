.class Lcom/helpshift/account/domainmodel/UserSyncDM$2;
.super Lcom/helpshift/common/domain/F;
.source "UserSyncDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/account/domainmodel/UserSyncDM;->syncUser()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;


# direct methods
.method constructor <init>(Lcom/helpshift/account/domainmodel/UserSyncDM;)V
    .locals 0

    .line 90
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 94
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-static {v0}, Lcom/helpshift/account/domainmodel/UserSyncDM;->access$100(Lcom/helpshift/account/domainmodel/UserSyncDM;)V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 98
    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$2;->this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-static {v1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->access$200(Lcom/helpshift/account/domainmodel/UserSyncDM;)Lcom/helpshift/common/domain/Domain;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V

    .line 99
    throw v0
.end method
