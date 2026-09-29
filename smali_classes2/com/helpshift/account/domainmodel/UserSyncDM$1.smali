.class Lcom/helpshift/account/domainmodel/UserSyncDM$1;
.super Lcom/helpshift/common/domain/F;
.source "UserSyncDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/account/domainmodel/UserSyncDM;->updateUserSyncState(Lcom/helpshift/account/domainmodel/UserSyncStatus;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

.field final synthetic val$finalListener:Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;

.field final synthetic val$newState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

.field final synthetic val$oldState:Lcom/helpshift/account/domainmodel/UserSyncStatus;


# direct methods
.method constructor <init>(Lcom/helpshift/account/domainmodel/UserSyncDM;Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;Lcom/helpshift/account/domainmodel/UserSyncStatus;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V
    .locals 0

    .line 70
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$finalListener:Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;

    iput-object p3, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$oldState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    iput-object p4, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$newState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 73
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$finalListener:Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserSyncDM;

    invoke-static {v1}, Lcom/helpshift/account/domainmodel/UserSyncDM;->access$000(Lcom/helpshift/account/domainmodel/UserSyncDM;)Lcom/helpshift/account/domainmodel/UserDM;

    move-result-object v1

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$oldState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    iget-object v3, p0, Lcom/helpshift/account/domainmodel/UserSyncDM$1;->val$newState:Lcom/helpshift/account/domainmodel/UserSyncStatus;

    invoke-interface {v0, v1, v2, v3}, Lcom/helpshift/account/domainmodel/UserSyncDM$UserSyncListener;->userSyncStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSyncStatus;Lcom/helpshift/account/domainmodel/UserSyncStatus;)V

    return-void
.end method
