.class Lcom/helpshift/conversation/usersetup/UserSetupVM$2;
.super Lcom/helpshift/common/domain/F;
.source "UserSetupVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/usersetup/UserSetupVM;->onAuthenticationFailure()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/usersetup/UserSetupVM;)V
    .locals 0

    .line 111
    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$2;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 1

    .line 114
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$2;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-static {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->access$000(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 115
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$2;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-static {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->access$000(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/conversation/activeconversation/usersetup/UserSetupRenderer;->onAuthenticationFailure()V

    :cond_0
    return-void
.end method
