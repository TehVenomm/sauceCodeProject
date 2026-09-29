.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;
.super Ljava/lang/Object;
.source "SupportFormFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

.field final synthetic val$sendButton:Landroid/view/View;

.field final synthetic val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;Landroid/view/View;)V
    .locals 0

    .line 231
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    iput-object p3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->val$sendButton:Landroid/view/View;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 5

    .line 235
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->val$supportRequestCollector:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;

    invoke-interface {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SupportRequestCollector;->collect()Lnet/gogame/gowrap/support/SupportRequest;

    move-result-object p1

    .line 236
    invoke-static {p1}, Lnet/gogame/gowrap/support/SupportManager;->isValid(Lnet/gogame/gowrap/support/SupportRequest;)Z

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-nez v0, :cond_0

    .line 237
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->val$sendButton:Landroid/view/View;

    invoke-virtual {p1, v2}, Landroid/view/View;->setSelected(Z)V

    .line 238
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 239
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_title:I

    .line 240
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_invalid_message:I

    .line 241
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    .line 242
    invoke-virtual {p1, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withCanceledOnTouchOutside(Z)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    .line 243
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    move-result-object p1

    .line 244
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->show()V

    return-void

    .line 248
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->getActivity()Landroid/app/Activity;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/utils/DisplayUtils;->hideSoftKeyboard(Landroid/app/Activity;)V

    .line 249
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v0

    const-string v3, "saved_name"

    .line 250
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getName()Ljava/lang/String;

    move-result-object v4

    .line 249
    invoke-static {v0, v3, v4}, Lnet/gogame/gowrap/support/PreferenceUtils;->setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 251
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v0

    const-string v3, "saved_email"

    .line 252
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getEmail()Ljava/lang/String;

    move-result-object v4

    .line 251
    invoke-static {v0, v3, v4}, Lnet/gogame/gowrap/support/PreferenceUtils;->setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 253
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v0

    const-string v3, "saved_mobile_number"

    .line 254
    invoke-virtual {p1}, Lnet/gogame/gowrap/support/SupportRequest;->getMobileNumber()Ljava/lang/String;

    move-result-object v4

    .line 253
    invoke-static {v0, v3, v4}, Lnet/gogame/gowrap/support/PreferenceUtils;->setPreference(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)V

    .line 256
    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;

    iget-object v3, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$8;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    const/4 v4, 0x0

    invoke-direct {v0, v3, v4}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;)V

    new-array v1, v1, [Lnet/gogame/gowrap/support/SupportRequest;

    aput-object p1, v1, v2

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    return-void
.end method
