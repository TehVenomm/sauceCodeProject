.class public Lnet/gogame/gowrap/ui/dialog/CustomDialog;
.super Ljava/lang/Object;
.source "CustomDialog.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;,
        Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;,
        Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;
    }
.end annotation


# instance fields
.field private canceledOnTouchOutside:Z

.field private listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

.field private message:Ljava/lang/String;

.field private final popupWindow:Landroid/widget/PopupWindow;

.field private title:Ljava/lang/String;

.field private type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

.field private final view:Landroid/view/View;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 3

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "layout_inflater"

    .line 28
    invoke-virtual {p1, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/view/LayoutInflater;

    .line 30
    sget v0, Lnet/gogame/gowrap/ui/common/R$layout;->net_gogame_gowrap_dialog:I

    const/4 v1, 0x0

    const/4 v2, 0x0

    invoke-virtual {p1, v0, v1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    .line 31
    new-instance p1, Landroid/widget/PopupWindow;

    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    const/4 v1, -0x1

    invoke-direct {p1, v0, v1, v1}, Landroid/widget/PopupWindow;-><init>(Landroid/view/View;II)V

    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->popupWindow:Landroid/widget/PopupWindow;

    .line 34
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v0, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_close_button:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    .line 36
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$1;-><init>(Lnet/gogame/gowrap/ui/dialog/CustomDialog;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 43
    iget-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$2;-><init>(Lnet/gogame/gowrap/ui/dialog/CustomDialog;)V

    invoke-virtual {p1, v0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/gowrap/ui/dialog/CustomDialog;)Z
    .locals 0

    .line 15
    iget-boolean p0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->canceledOnTouchOutside:Z

    return p0
.end method

.method public static newBuilder(Landroid/content/Context;)Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;
    .locals 1

    .line 55
    new-instance v0, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Builder;-><init>(Landroid/content/Context;)V

    return-object v0
.end method


# virtual methods
.method public dismiss()V
    .locals 3

    .line 156
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->isShowing()Z

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 159
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->dismiss()V

    .line 160
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    if-eqz v0, :cond_1

    .line 162
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    invoke-interface {v0}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;->onClosed()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 164
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_1
    :goto_0
    return-void
.end method

.method public getListener()Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;
    .locals 1

    .line 91
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1

    .line 75
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    .line 67
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->title:Ljava/lang/String;

    return-object v0
.end method

.method public getType()Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;
    .locals 1

    .line 59
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-object v0
.end method

.method public isCanceledOnTouchOutside()Z
    .locals 1

    .line 83
    iget-boolean v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->canceledOnTouchOutside:Z

    return v0
.end method

.method public setCanceledOnTouchOutside(Z)V
    .locals 0

    .line 87
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->canceledOnTouchOutside:Z

    return-void
.end method

.method public setListener(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;)V
    .locals 0

    .line 95
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->listener:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Listener;

    return-void
.end method

.method public setMessage(Ljava/lang/String;)V
    .locals 0

    .line 79
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->message:Ljava/lang/String;

    return-void
.end method

.method public setTitle(Ljava/lang/String;)V
    .locals 0

    .line 71
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->title:Ljava/lang/String;

    return-void
.end method

.method public setType(Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;)V
    .locals 0

    .line 63
    iput-object p1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    return-void
.end method

.method public show()V
    .locals 9

    .line 99
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->popupWindow:Landroid/widget/PopupWindow;

    invoke-virtual {v0}, Landroid/widget/PopupWindow;->isShowing()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 102
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v1, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_close_button:I

    invoke-virtual {v0, v1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 104
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v2, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_title:I

    invoke-virtual {v1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 106
    iget-object v2, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->title:Ljava/lang/String;

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 107
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v2, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_message:I

    invoke-virtual {v1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    check-cast v1, Landroid/widget/TextView;

    .line 109
    iget-object v2, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->message:Ljava/lang/String;

    invoke-virtual {v1, v2}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    .line 110
    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v2, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_icon_container:I

    invoke-virtual {v1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v1

    .line 111
    iget-object v2, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v3, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_icon_info:I

    invoke-virtual {v2, v3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    .line 112
    iget-object v3, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v4, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_dialog_icon_alert:I

    invoke-virtual {v3, v4}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    .line 113
    iget-object v4, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    sget v5, Lnet/gogame/gowrap/ui/common/R$id;->net_gogame_gowrap_progress_indicator:I

    invoke-virtual {v4, v5}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v4

    .line 114
    iget-object v5, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    const/4 v6, 0x0

    const/16 v7, 0x8

    if-eqz v5, :cond_1

    .line 115
    sget-object v5, Lnet/gogame/gowrap/ui/dialog/CustomDialog$3;->$SwitchMap$net$gogame$gowrap$ui$dialog$CustomDialog$Type:[I

    iget-object v8, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->type:Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;

    invoke-virtual {v8}, Lnet/gogame/gowrap/ui/dialog/CustomDialog$Type;->ordinal()I

    move-result v8

    aget v5, v5, v8

    packed-switch v5, :pswitch_data_0

    .line 141
    invoke-virtual {v0, v6}, Landroid/view/View;->setVisibility(I)V

    .line 142
    invoke-virtual {v1, v7}, Landroid/view/View;->setVisibility(I)V

    .line 143
    invoke-virtual {v2, v7}, Landroid/view/View;->setVisibility(I)V

    .line 144
    invoke-virtual {v3, v7}, Landroid/view/View;->setVisibility(I)V

    .line 145
    invoke-virtual {v4, v7}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 133
    :pswitch_0
    invoke-virtual {v0, v7}, Landroid/view/View;->setVisibility(I)V

    .line 134
    invoke-virtual {v1, v6}, Landroid/view/View;->setVisibility(I)V

    .line 135
    invoke-virtual {v2, v7}, Landroid/view/View;->setVisibility(I)V

    .line 136
    invoke-virtual {v3, v7}, Landroid/view/View;->setVisibility(I)V

    .line 137
    invoke-virtual {v4, v6}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 125
    :pswitch_1
    invoke-virtual {v0, v6}, Landroid/view/View;->setVisibility(I)V

    .line 126
    invoke-virtual {v1, v6}, Landroid/view/View;->setVisibility(I)V

    .line 127
    invoke-virtual {v2, v7}, Landroid/view/View;->setVisibility(I)V

    .line 128
    invoke-virtual {v3, v6}, Landroid/view/View;->setVisibility(I)V

    .line 129
    invoke-virtual {v4, v7}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 117
    :pswitch_2
    invoke-virtual {v0, v6}, Landroid/view/View;->setVisibility(I)V

    .line 118
    invoke-virtual {v1, v6}, Landroid/view/View;->setVisibility(I)V

    .line 119
    invoke-virtual {v2, v6}, Landroid/view/View;->setVisibility(I)V

    .line 120
    invoke-virtual {v3, v7}, Landroid/view/View;->setVisibility(I)V

    .line 121
    invoke-virtual {v4, v7}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    .line 150
    :cond_1
    invoke-virtual {v1, v7}, Landroid/view/View;->setVisibility(I)V

    .line 152
    :goto_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->popupWindow:Landroid/widget/PopupWindow;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/dialog/CustomDialog;->view:Landroid/view/View;

    const/16 v2, 0x11

    invoke-virtual {v0, v1, v2, v6, v6}, Landroid/widget/PopupWindow;->showAtLocation(Landroid/view/View;III)V

    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
