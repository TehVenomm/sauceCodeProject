.class Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;
.super Landroid/os/AsyncTask;
.source "SupportFormFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "SubmitSupportRequestTask"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Lnet/gogame/gowrap/support/SupportRequest;",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field private progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

.field private successful:Z

.field final synthetic this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;


# direct methods
.method private constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V
    .locals 0

    .line 333
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    const/4 p1, 0x0

    .line 335
    iput-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    const/4 p1, 0x0

    .line 336
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->successful:Z

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$1;)V
    .locals 0

    .line 333
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)V

    return-void
.end method


# virtual methods
.method protected bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 333
    check-cast p1, [Lnet/gogame/gowrap/support/SupportRequest;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->doInBackground([Lnet/gogame/gowrap/support/SupportRequest;)Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doInBackground([Lnet/gogame/gowrap/support/SupportRequest;)Ljava/lang/Void;
    .locals 3

    if-eqz p1, :cond_0

    .line 351
    array-length v0, p1

    if-lez v0, :cond_0

    const/4 v0, 0x0

    .line 353
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v1

    aget-object p1, p1, v0

    invoke-static {v1, p1}, Lnet/gogame/gowrap/support/SupportManager;->send(Landroid/content/Context;Lnet/gogame/gowrap/support/SupportRequest;)Ljava/lang/Long;

    const/4 p1, 0x1

    .line 354
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->successful:Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 356
    invoke-static {v1, v2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 357
    iput-boolean v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->successful:Z

    :cond_0
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method protected bridge synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    .line 333
    check-cast p1, Ljava/lang/Void;

    invoke-virtual {p0, p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->onPostExecute(Ljava/lang/Void;)V

    return-void
.end method

.method protected onPostExecute(Ljava/lang/Void;)V
    .locals 1

    .line 365
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    if-eqz p1, :cond_0

    .line 366
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->dismiss()V

    .line 368
    :cond_0
    iget-boolean p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->successful:Z

    if-eqz p1, :cond_1

    .line 369
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->INFO:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 370
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_title:I

    .line 371
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_request_submitted_message:I

    .line 372
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    new-instance v0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask$1;-><init>(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;)V

    .line 374
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withListener(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    .line 381
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    move-result-object p1

    .line 382
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->show()V

    goto :goto_0

    .line 384
    :cond_1
    iget-object p1, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {p1}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object p1

    invoke-static {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget-object v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ALERT:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 385
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_title:I

    .line 386
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    sget v0, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_request_failed_message:I

    .line 387
    invoke-virtual {p1, v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object p1

    .line 389
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    move-result-object p1

    .line 390
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->show()V

    :goto_0
    return-void
.end method

.method protected onPreExecute()V
    .locals 2

    .line 340
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->this$0:Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;

    invoke-static {v0}, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;->access$300(Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment;)Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object v0

    sget-object v1, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->PROGRESS:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    .line 341
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object v0

    sget v1, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_title:I

    .line 342
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withTitle(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object v0

    sget v1, Lnet/gogame/gowrap/R$string;->net_gogame_gowrap_support_form_submitting_request_message:I

    .line 343
    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->withMessage(I)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    move-result-object v0

    .line 345
    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;->build()Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    move-result-object v0

    iput-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    .line 346
    iget-object v0, p0, Lnet/gogame/gowrap/ui/v2017_1/SupportFormFragment$SubmitSupportRequestTask;->progressDialog:Lnet/gogame/gowrap/ui/dialog/CustomDialog;

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->show()V

    return-void
.end method
