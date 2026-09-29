.class Lcom/helpshift/support/conversations/BaseConversationFragment$1;
.super Ljava/lang/Object;
.source "BaseConversationFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/BaseConversationFragment;->onRequestPermissionsResult(I[Ljava/lang/String;[I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/BaseConversationFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/BaseConversationFragment;)V
    .locals 0

    .line 140
    iput-object p1, p0, Lcom/helpshift/support/conversations/BaseConversationFragment$1;->this$0:Lcom/helpshift/support/conversations/BaseConversationFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 143
    iget-object p1, p0, Lcom/helpshift/support/conversations/BaseConversationFragment$1;->this$0:Lcom/helpshift/support/conversations/BaseConversationFragment;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/BaseConversationFragment;->getContext()Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lcom/helpshift/support/util/PermissionUtil;->showSettingsPage(Landroid/content/Context;)V

    return-void
.end method
