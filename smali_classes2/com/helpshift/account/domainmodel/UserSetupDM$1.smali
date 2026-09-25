.class Lcom/helpshift/account/domainmodel/UserSetupDM$1;
.super Lcom/helpshift/common/domain/F;
.source "UserSetupDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/account/domainmodel/UserSetupDM;->updateUserSetupStateChange(Lcom/helpshift/account/domainmodel/UserSetupState;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

.field final synthetic val$newState:Lcom/helpshift/account/domainmodel/UserSetupState;

.field final synthetic val$setupListener:Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;


# direct methods
.method constructor <init>(Lcom/helpshift/account/domainmodel/UserSetupDM;Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;Lcom/helpshift/account/domainmodel/UserSetupState;)V
    .locals 0

    .line 197
    iput-object p1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iput-object p2, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->val$setupListener:Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;

    iput-object p3, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->val$newState:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 200
    iget-object v0, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->val$setupListener:Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;

    iget-object v1, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->this$0:Lcom/helpshift/account/domainmodel/UserSetupDM;

    iget-object v1, v1, Lcom/helpshift/account/domainmodel/UserSetupDM;->userDM:Lcom/helpshift/account/domainmodel/UserDM;

    iget-object v2, p0, Lcom/helpshift/account/domainmodel/UserSetupDM$1;->val$newState:Lcom/helpshift/account/domainmodel/UserSetupState;

    invoke-interface {v0, v1, v2}, Lcom/helpshift/account/domainmodel/UserSetupDM$UserSetupListener;->userSetupStateChanged(Lcom/helpshift/account/domainmodel/UserDM;Lcom/helpshift/account/domainmodel/UserSetupState;)V

    return-void
.end method
