.class Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;
.super Ljava/lang/Object;
.source "HelpFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)V
    .locals 0

    .line 49
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 53
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->access$000(Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;)Lnet/gogame/gowrap/ui/UIContext;

    move-result-object p1

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/UIContext;->isVipChatEnabled()Z

    move-result p1

    if-nez p1, :cond_1

    sget-object p1, Lnet/gogame/gowrap/integrations/core/Wrapper;->INSTANCE:Lnet/gogame/gowrap/integrations/core/Wrapper;

    invoke-virtual {p1}, Lnet/gogame/gowrap/integrations/core/Wrapper;->isChatBotEnabled()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    .line 56
    :cond_0
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment$3;->this$0:Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/v2017_2/HelpFragment;->getActivity()Landroid/app/Activity;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 57
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_title:I

    .line 58
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_chat_vip_only_message:I

    .line 59
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    .line 61
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    move-result-object p1

    .line 62
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->show()V

    goto :goto_1

    .line 54
    :cond_1
    :goto_0
    sget-object p1, Lnet/gogame/gowrap/GoWrapImpl;->INSTANCE:Lnet/gogame/gowrap/GoWrapImpl;

    invoke-virtual {p1}, Lnet/gogame/gowrap/GoWrapImpl;->startChat()V

    :goto_1
    return-void
.end method
