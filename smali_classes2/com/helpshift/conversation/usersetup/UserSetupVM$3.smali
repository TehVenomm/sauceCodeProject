.class Lcom/helpshift/conversation/usersetup/UserSetupVM$3;
.super Lcom/helpshift/common/domain/F;
.source "UserSetupVM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/usersetup/UserSetupVM;->onNetworkAvailable()V
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

    .line 123
    iput-object p1, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$3;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 127
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$3;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-static {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->access$100(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/widget/MutableBaseViewState;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    .line 128
    iget-object v0, p0, Lcom/helpshift/conversation/usersetup/UserSetupVM$3;->this$0:Lcom/helpshift/conversation/usersetup/UserSetupVM;

    invoke-static {v0}, Lcom/helpshift/conversation/usersetup/UserSetupVM;->access$200(Lcom/helpshift/conversation/usersetup/UserSetupVM;)Lcom/helpshift/widget/MutableBaseViewState;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Lcom/helpshift/widget/MutableBaseViewState;->setVisible(Z)V

    return-void
.end method
